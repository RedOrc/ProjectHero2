using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.VSEventArgs
{
    internal class CommandExecuteEventArg
    {
        public EnvDTE.Command Command { get; private set; }
        public string Guid { get; private set; }
        public int ID { get; private set; }
        public object CustomIn { get; private set; }
        public object CustomOut { get; private set; }
        public bool ShouldCancel { get; set; }

        public CommandExecuteEventArg(string guid, int id, object customIn, object customOut)
        {
            this.Guid = guid;
            this.ID = id;
            this.CustomIn = customIn;
            this.CustomOut = customOut;
        }

        public CommandExecuteEventArg(string guid, int id, object customIn, object customOut, EnvDTE.Command command)
        {
            this.Guid = guid;
            this.ID = id;
            this.CustomIn = customIn;
            this.CustomOut = customOut;
            this.Command = command;
        }

        public CommandExecuteEventArg(string guid, int id, object customIn, object customOut, ref bool cancelDefault)
        {
            this.Guid = guid;
            this.ID = id;
            this.CustomIn = customIn;
            this.CustomOut = customOut;
            this.ShouldCancel = cancelDefault;
        }

        public CommandExecuteEventArg(string guid, int id, object customIn, object customOut, ref bool cancelDefault, EnvDTE.Command command)
        {
            this.Guid = guid;
            this.ID = id;
            this.CustomIn = customIn;
            this.CustomOut = customOut;
            this.ShouldCancel = cancelDefault;
            this.Command = command;
        }
    }
}
