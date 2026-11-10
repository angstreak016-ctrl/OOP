using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentTransitions;
using NAudio;
using NAudio.Wave;
using WinFormAnimationDotNet;





namespace Try
{
    public partial class MainMenu : Form
    {
        private WaveOutEvent musicOut;
        private AudioFileReader musicReader;
        Database db = new Database();
        public Color FormGradientTop { get; set; } = Color.Peru;
        public Color FormGradientBottom { get; set; } = Color.Gold;
        public float FormGradientAngle { get; set; } = 90f;
        private Image bgImage;
        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, IntPtr hwndCallback);
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF formRect = new RectangleF(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            using (LinearGradientBrush gradBrush = new LinearGradientBrush(formRect, FormGradientTop, FormGradientBottom, FormGradientAngle))
            {
                g.FillRectangle(gradBrush, formRect);
            }
            if (bgImage != null)
            {
                g.DrawImage(bgImage, this.ClientRectangle);
            }
        }
        public class SmoothPanel : Panel
        {
            public SmoothPanel()
            {
               
                this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                              ControlStyles.UserPaint |
                              ControlStyles.OptimizedDoubleBuffer |
                              ControlStyles.ResizeRedraw, true);
                this.UpdateStyles();
            }
        }
        public MainMenu()
        {
            
            bgImage = Properties.Resources.image_Photoroom;
            InitializeComponent();
            this.Load += new EventHandler(MainMenu_Load);
            this.SetStyle(ControlStyles.ResizeRedraw |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true); 
            panel1.BackColor = Color.Transparent;
        }
        public static class BgMusic
        {
            private static WaveOutEvent musicOut;
            private static AudioFileReader musicReader;
            public static void Play(string filePath)
            {
                if (musicOut != null && musicOut.PlaybackState == PlaybackState.Playing)
                {
                    return;
                }
                musicReader = new AudioFileReader(filePath);
                musicOut = new WaveOutEvent();
                musicOut.Init(musicReader);
                musicOut.Play();

                musicOut.PlaybackStopped += (s, args) =>
                {
                    musicReader.Position = 0; // Reset position to the beginning
                    musicOut.Play(); // Play again
                };
            }
            public static void Stop()
            {
                var o = musicOut;
                musicOut = null;
                o?.Stop();
                o?.Dispose();
                musicReader?.Dispose();
                musicReader = null;
            }
        }
        private void MainMenu_Load(object sender, EventArgs e)
        {
            BgMusic.Play(@"C:\Users\Ard\source\repos\Try\Try\Resources\videoplayback (1).wav");
            panel2.Location = new Point(0, 450);
        }
        private void outlinedButton1_Click(object sender, EventArgs e)
        {

        }

        private async void outlinedButton1_Click_1(object sender, EventArgs e)
        {
            SignInForm childForm = new SignInForm();
            PanelFormAnimator animator = new PanelFormAnimator();
            animator.ShowFormInPanel(childForm, panel2, slideDistance: 440);
        }

        private void outlinedButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void outlinedButton3_Click(object sender, EventArgs e)
        {
            ClientPanel Form1 = new ClientPanel();
            Form1.Show();
        }
    }
}
