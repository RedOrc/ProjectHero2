using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectHero2.Core.Dialogs
{
    public partial class frmAbout : Form
    {
        private Timer tmrAnimation;
        private const string Fonzie = "Coded By:\n   -- FoNzIe ++";
        private readonly Font font = new Font("Comic Sans", 14.0f, FontStyle.Bold);
        private SizeF stringSize;
        private SizeF versionSize;
        private string version = $"Version: {PHVersion.Version}";
        private int xAdvance = 1;
        private int yAdvance = 1;
        private int x;
        private int y;
        
        internal class Star
        {
            public float x;
            public float y;
            public float speed;
        }
        private readonly ConcurrentQueue<Star> starQueue = new ConcurrentQueue<Star>();
        private int maxStars = 0;

        public frmAbout()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            BackColor = Color.Black;

            Graphics g = CreateGraphics();
            stringSize = g.MeasureString(Fonzie, font);
            versionSize = g.MeasureString(version, SystemFonts.CaptionFont);

            x = ClientRectangle.Width / 2 - (int)(stringSize.Width / 2);
            y = ClientRectangle.Height / 2 - (int)(stringSize.Height / 2);

            tmrAnimation = new Timer();
            tmrAnimation.Interval = 5;
            tmrAnimation.Tick += TmrAnimation_Tick;
            tmrAnimation.Start();
        }

        private void TmrAnimation_Tick(object sender, EventArgs e)
        {
            ReCalculatePositioning();
            GenerateAndMoveStars();
            this.Refresh();
        }

        private void FrmAbout_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrAnimation.Stop();
            tmrAnimation.Dispose();

            font.Dispose();
        }

        private void FrmAbout_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawString(Fonzie, font, Brushes.Wheat, x, y);
            g.DrawString(version, SystemFonts.CaptionFont, Brushes.Wheat,
                (ClientRectangle.Width - versionSize.Width - 5),
                (ClientRectangle.Height - versionSize.Height - 5)
            );

            foreach (Star star in starQueue)
            {
                g.DrawString("*", SystemFonts.SmallCaptionFont, Brushes.White, star.x, star.y);
            }
        }

        private void GenerateAndMoveStars()
        {
            maxStars = (int)(ClientRectangle.Width * .10);
            int starsToGen = maxStars - starQueue.Count;
            if (starsToGen > 10)
            {
                Random rnd = new Random((int)DateTime.Now.Ticks);
                for (int i = 0; i < starsToGen; i++)
                {
                    Star star = new Star();
                    star.x = ClientRectangle.Width + rnd.Next(10, (int)(ClientRectangle.Width * .50f));
                    star.y = rnd.Next(5, ClientRectangle.Height - 5);
                    star.speed = rnd.Next(1, 5);

                    starQueue.Enqueue(star);
                }
            }

            Star fallenStar;
            foreach (Star star in starQueue)
            {
                star.x -= star.speed;
                if (star.x < -(ClientRectangle.Width << 1))
                {
                    starQueue.TryDequeue(out fallenStar);
                }
            }
        }

        private void ReCalculatePositioning()
        {
            if (x <= 0)
                xAdvance = 1;
            else if ((x + (int)stringSize.Width) >= ClientRectangle.Width)
                xAdvance = -1;

            if (y <= 0)
                yAdvance = 1;
            else if ((y + (int)stringSize.Height) >= ClientRectangle.Height)
                yAdvance = -1;

            x += xAdvance;
            y += yAdvance;
        }
    }
}