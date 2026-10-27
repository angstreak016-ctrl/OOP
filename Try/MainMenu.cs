using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormAnimationDotNet;


namespace Try
{
    public partial class MainMenu : Form
    {
        Database db = new Database();

        private void MovePanel()
        {
            // Ensure you have reference extensions if using Float2D, or pass coordinate bounds
            // This moves the control smoothly to a new X/Y position over 400 milliseconds
            int moveDistance = 100;

            int startX = panel1.Left;
            int startY = panel1.Top;
            int targetX = panel1.Left;
            int targetY = panel1.Top - moveDistance; // Subtracting moves it UP on the screen

            // Create a 2D path: (StartX, StartY, EndX, EndY, DurationInMilliseconds)
            var animator = new Animator2D(
                new Path2D(startX, startY, targetX, targetY, 300) // 300ms duration
            );

            // Play the animation targeting the panel's Location property
            animator.Play(panel1, Animator2D.KnownProperties.Location);
        }
        public class OutlinedButton : Button
        {
            private int _borderRadius = 16;
            public int BorderRadius
            {
                get => _borderRadius;
                set { _borderRadius = value; Invalidate(); }
            }

            public Color OutlineColor { get; set; } = Color.Black;
            public float OutlineWidth { get; set; } = 2.0f;
            public bool IsPillShape { get; set; } = false;

            public OutlinedButton()
            {
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            }

            protected override void OnResize(System.EventArgs e)
            {
                base.OnResize(e);

                // This clips the actual control boundaries to match the rounded shape, 
                // removing those ugly black/unwanted background corners!
                int effectiveRadius = IsPillShape ? Height / 2 : BorderRadius;
                using (GraphicsPath path = GetRoundedRectPath(new RectangleF(0, 0, Width, Height), effectiveRadius))
                {
                    this.Region = new Region(path);
                }
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                Graphics g = pevent.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // Use a slight inset so the border doesn't get clipped by the window region boundary
                RectangleF clientRect = new RectangleF(0, 0, Width - 1, Height - 1);
                int effectiveRadius = IsPillShape ? Height / 2 : BorderRadius;

                // 1. Draw the rounded button background
                Color currentBackColor = Enabled ? BackColor : Color.LightGray;
                using (GraphicsPath bgPath = GetRoundedRectPath(clientRect, effectiveRadius))
                using (Brush backBrush = new SolidBrush(currentBackColor))
                {
                    g.FillPath(backBrush, bgPath);
                }

                // 2. Draw the rounded button border
                using (GraphicsPath borderPath = GetRoundedRectPath(clientRect, effectiveRadius))
                using (Pen borderPen = new Pen(Enabled ? Color.Black : Color.Black, 5.0f))
                {
                    g.DrawPath(borderPen, borderPath);
                }

                // 3. Draw the outlined text in the center
                if (!string.IsNullOrEmpty(Text))
                {
                    using (GraphicsPath textPath = new GraphicsPath())
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;

                        RectangleF rect = new RectangleF(0, 0, Width, Height);

                        textPath.AddString(
                            Text,
                            Font.FontFamily,
                            (int)Font.Style,
                            Font.Size,
                            rect,
                            sf
                        );

                        // Draw the text outline (stroke)
                        using (Pen outlinePen = new Pen(OutlineColor, OutlineWidth))
                        {
                            outlinePen.LineJoin = LineJoin.Round;
                            g.DrawPath(outlinePen, textPath);
                        }

                        // Fill the inside of the text
                        Color currentForeColor = Enabled ? ForeColor : Color.DarkGray;
                        using (Brush textBrush = new SolidBrush(currentForeColor))
                        {
                            g.FillPath(textBrush, textPath);
                        }
                    }
                }
            }

            private GraphicsPath GetRoundedRectPath(RectangleF rect, float radius)
            {
                GraphicsPath path = new GraphicsPath();
                float diameter = radius * 2;

                if (diameter > rect.Width) diameter = rect.Width;
                if (diameter > rect.Height) diameter = rect.Height;

                path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
                path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
                path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
                path.CloseFigure();

                return path;
            }
        }
        public Color FormGradientTop { get; set; } = Color.Peru;
        public Color FormGradientBottom { get; set; } = Color.Gold;
        public float FormGradientAngle { get; set; } = 90f;
        private Image bgImage;
        public MainMenu()
        {
            bgImage = Properties.Resources.image_Photoroom;
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true); panel1.BackColor = Color.Transparent;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF formRect = new RectangleF(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);

            // 1. LAYER 1: Draw the Gradient first (Bottom Layer)
            using (LinearGradientBrush gradBrush = new LinearGradientBrush(formRect, FormGradientTop, FormGradientBottom, FormGradientAngle))
            {
                g.FillRectangle(gradBrush, formRect);
            }

            // 2. LAYER 2: Draw your Background Image on top of the gradient
            if (bgImage != null)
            {
                // This stretches the image to fit the form. 
                // Change coordinates if you want it centered or tiled instead.
                g.DrawImage(bgImage, this.ClientRectangle);
            }
        }

        public class SmoothPanel : Panel
        {
            public SmoothPanel()
            {
                // Enable double buffering and smooth painting optimizations
                this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                              ControlStyles.UserPaint |
                              ControlStyles.OptimizedDoubleBuffer |
                              ControlStyles.ResizeRedraw, true);
                this.UpdateStyles();
            }
        }

        //public static async Task SlidePanelUpSmoothAsync(Control control, int distance, int durationMs = 200)
        //{
        //    Color originalBackColor = control.BackColor;
        //    control.BackColor = Color.Peru;
        //    int startY = control.Top;
        //    int targetY = startY - distance;
        //    int frames = Math.Max(durationMs / 16, 1);

        //    for (int i = 1; i <= frames; i++)
        //    {
        //        float progress = (float)i / frames;
        //        // Ease-out curve for smooth deceleration
        //        progress = (float)Math.Sin(progress * Math.PI / 2);

        //        control.Top = (int)(startY + (targetY - startY) * progress);
        //        await Task.Delay(16);
        //    }
        //    control.Top = targetY;
        //    control.ResumeLayout(true);

        //    // 3. ANIMATION DONE: Instantly restore the transparent backcolor
        //    control.BackColor = originalBackColor;
        //    control.Invalidate(true); // Force a clean final redraw
        //}




        //public static async Task SlidePanelDownSmoothAsync(Control control, int distance, int durationMs = 200)
        //{
        //    // 1. Save your original background color
        //    Color originalBackColor = control.BackColor;

        //    // 2. Switch to a solid theme color during motion to eliminate all jitter/tearing
        //    control.BackColor = Color.Peru;

        //    int startY = control.Top;
        //    int targetY = startY + distance; // Plus distance moves it down
        //    int frames = Math.Max(durationMs / 16, 1);

        //    control.SuspendLayout();

        //    for (int i = 1; i <= frames; i++)
        //    {
        //        float progress = (float)i / frames;
        //        // Ease-out curve for smooth deceleration
        //        progress = (float)Math.Sin(progress * Math.PI / 2);

        //        // Move downward smoothly
        //        control.Top = (int)(startY + (targetY - startY) * progress);
        //        await Task.Delay(16);
        //    }

        //    control.Top = targetY;
        //    control.ResumeLayout(true);

        //    // 3. Animation finished: restore original background and hide the panel
        //    control.BackColor = originalBackColor;
        //    control.Invalidate(true); // Clean final redraw

        //    // Hide it completely when it finishes sliding down out of view
        //    control.Visible = false;
        //}




        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        //private void ShowmemyForm(Form f, Panel host)
        //{
        //    f.FormClosed += (s, e) => SlidePanelDownSmoothAsync(host, 450); // Slide down when the form is closed
        //    f.TopLevel = false;
        //    host.Visible = true;
        //    host.Controls.Add(f);
        //    f.Show();            
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    SlidePanelUpSmoothAsync(panel1, 450);
        //    ShowmemyForm(new SignInForm(), panel1);
        //}

        private async void button1_Click(object sender, EventArgs e)
        {
            SignInForm signupForm = new SignInForm();


            await PanelAnimator.ShowFormInsidePanelAsync(signupForm, panel1, distance: 450);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void outlinedButton1_Click(object sender, EventArgs e)
        {

        }

        private async void outlinedButton1_Click_1(object sender, EventArgs e)
        {
            SignInForm signupForm = new SignInForm();
            await PanelAnimator.ShowFormInsidePanelAsync(signupForm, panel1, distance: 445);
        }

        private void outlinedButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
