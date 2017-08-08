using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core
{
    public sealed class QuickSyncRunner
    {
        private volatile List<SourceProjectBinding> m_Bindings = new List<SourceProjectBinding>();
        private volatile ArrayList m_Workers = new ArrayList();

        public delegate void OnSyncUpdate(string status);
        public event OnSyncUpdate SyncUpdate;

        protected void RaiseOnSyncUpdate(string status)
        {
            if (SyncUpdate != null)
                SyncUpdate(status);
        }

        public delegate void OnSyncComplete(bool isCancelled);
        public event OnSyncComplete SyncComplete;

        protected void RaiseOnSyncComplete(bool isCancelled)
        {
            if (SyncComplete != null)
                SyncComplete(isCancelled);
        }

        private object locker = new object();
        private bool _shouldCancel = false;

        public void Add(SourceProjectBinding binding)
        {
            lock (locker)
            {
                m_Bindings.Add(binding);
            }

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = false;
            worker.RunWorkerAsync(binding);

            UpdateStatus();
        }

        public void CancelAllWork()
        {
            try
            {
                if (m_Workers.Count > 0)
                {
                    foreach (BackgroundWorker worker in m_Workers)
                    {
                        if (worker.IsBusy)
                            worker.CancelAsync();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                m_Bindings.Clear();
                m_Workers.Clear();

                RaiseOnSyncComplete(true);
            }
        }

        private void UpdateStatus()
        {
            lock (locker)
            {
                RaiseOnSyncUpdate(string.Format("{0} Projects being synchronized.", m_Bindings.Count()));
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            try
            {
                lock (locker)
                {
                    m_Bindings.Remove(e.Result as SourceProjectBinding);
                    m_Workers.Remove(worker);
                }

                if (m_Bindings.Count > 0)
                {
                    UpdateStatus();
                }
                else
                {
                    RaiseOnSyncComplete(_shouldCancel);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                worker.Dispose();
                worker = null;
            }
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            SourceProjectBinding binding = e.Argument as SourceProjectBinding;
            e.Result = binding;

            try
            {
                if (binding.SyncAssociationCollection.Count > 0 && binding.DestinationFolderCollection.Count > 0)
                {
                    foreach (DestinationFolderBinding folderBinding in binding.DestinationFolderCollection)
                    {
                        try
                        {
                            if (!Directory.Exists(folderBinding.FolderPath))
                                Directory.CreateDirectory(folderBinding.FolderPath);
                            else
                            {
                                try
                                {
                                    foreach (string childDirectory in Directory.GetDirectories(folderBinding.FolderPath))
                                        Directory.Delete(childDirectory, true);
                                }
                                catch { }

                                try
                                {
                                    foreach (string childFile in Directory.GetFiles(folderBinding.FolderPath))
                                    {
                                        if (!childFile.EndsWith(".config"))
                                            File.Delete(childFile);
                                    }
                                }
                                catch { }
                            }

                            string destDirPath = folderBinding.FolderPath;

                            // ================================================================================
                            // Copy each file and folder to the new directory locations specified.
                            // ================================================================================
                            foreach (SysFileItem item in binding.SyncAssociationCollection)
                            {
                                if (item.FileType == FileType.File)
                                {
                                    string fName = Path.GetFileName(item.FullPath);
                                    string fPath = Path.Combine(binding.ProjectFilePath, fName);

                                    if (!File.Exists(fPath))
                                        continue;

                                    FileInfo srcFileInfo = new FileInfo(fPath);
                                    string fDestFilePath = Path.Combine(destDirPath, fName);

                                    if (File.Exists(fDestFilePath))
                                    {
                                        FileInfo destFileInfo = new FileInfo(fDestFilePath);

                                        if (srcFileInfo.LastWriteTime > destFileInfo.LastWriteTime)
                                            srcFileInfo.CopyTo(fDestFilePath, true);
                                    }
                                    else
                                    {
                                        srcFileInfo.CopyTo(fDestFilePath, true);
                                    }
                                }
                                else
                                {
                                    string oldDirName = Path.Combine(binding.ProjectFilePath, item.Name);
                                    if (!Directory.Exists(oldDirName))
                                        continue;

                                    string newDirName = Path.Combine(destDirPath, item.Name);

                                    // ================================================================================
                                    // We should delete the existing directory since it's possible files were
                                    // added or removed and we want to be in a 100% synchronized state.
                                    // ================================================================================
                                    try
                                    {
                                        if (Directory.Exists(newDirName))
                                            Directory.Delete(newDirName, true);
                                    }
                                    catch { }

                                    // ================================================================================
                                    // We are much better served by using "xcopy" to do the transfer as it does
                                    // a great job at traversing the NTFS tree and has been proven to be efficient.
                                    // ================================================================================

                                    System.Diagnostics.Process copyProcess = new System.Diagnostics.Process();
                                    copyProcess.StartInfo.UseShellExecute = true;
                                    copyProcess.StartInfo.CreateNoWindow = true;
                                    copyProcess.StartInfo.FileName = @"C:\Windows\system32\xcopy.exe";
                                    copyProcess.StartInfo.Arguments = string.Format("/E /I /Y /C /S /V \"{0}\" \"{1}\" ", oldDirName, newDirName); //Removed '/D'
                                    copyProcess.Start();
                                    copyProcess.WaitForExit(60);
                                }
                            }
                        }
                        catch
                        {
                            // Don't stop the part because of some permission issues.
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
