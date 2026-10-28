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
using WinFormAnimationDotNet;

namespace Try
{

    public partial class SignInForm : Form
    {
        Database db = new Database();

        public SignInForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            FormBorderStyle = FormBorderStyle.None;
        }
            public class BorderedRoundButton : Button
        {
            public int BorderRadius { get; set; } = 20;
            public int BorderSize { get; set; } = 2; // Thickness of the black border
            public Color BorderColor { get; set; } = Color.Black;

            public BorderedRoundButton()
            {
                this.FlatStyle = FlatStyle.Standard;
                this.BackColor = Color.White; // Or your preferred background color
                this.ForeColor = Color.Black;
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                base.OnPaint(pevent);
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle rectSurface = this.ClientRectangle;
                Rectangle rectBorder = Rectangle.Inflate(rectSurface, -1, -1);

                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, BorderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, BorderRadius - 1))
                using (Pen penBorder = new Pen(BorderColor, BorderSize))
                {
                    // Apply the rounded shape to the button
                    this.Region = new Region(pathSurface);

                    // Draw the smooth black border
                    penBorder.Alignment = PenAlignment.Center;
                    pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }

            private GraphicsPath GetFigurePath(Rectangle rect, float radius)
            {
                GraphicsPath path = new GraphicsPath();
                float curveSize = radius * 2F;

                path.StartFigure();
                path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
                path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 240, 60);
                path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
                path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
                path.CloseFigure();

                return path;
            }
        }
        

        private void SignInForm_Load(object sender, EventArgs e)
        {
            PlayStartupSound();
        }
        private void PlayStartupSound()
        {
            // Replace with the path to your .wav file
            string soundPath = @"C:\Users\Ard\source\repos\Try\Try\Resources\videoplayback (1).wav";

            if (File.Exists(soundPath))
            {
                try
                {
                    using (SoundPlayer player = new SoundPlayer(soundPath))
                    {
                        // player.Play();     // Plays the sound asynchronously in the background
                        player.Play();    // Plays the sound and waits for it to finish before loading the form fully
                    }
                }
                catch
                {
                    // Suppress exceptions if the file is locked or invalid
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Visible = false;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            bool isTrue = true;
            if (isTrue == true)
            {
                MessageBox.Show("Log in Successfully!");
            }
            else
            {
                MessageBox.Show("Failed!");
            }

        }

        private void outlinedButton1_Click(object sender, EventArgs e)
        {
            Visible = false;
            Close();
        }

        private void outlinedButton2_Click(object sender, EventArgs e)
        {
            bool isTrue = true;
            if (isTrue == true)
            {
                MessageBox.Show("Log in Successfully!");
            }
            else
            {
                MessageBox.Show("Failed!");
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
