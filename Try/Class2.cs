using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Try
{
    public class BorderedGradientPanel : Panel
    {
        private Color gradientStartColor = Color.LightSkyBlue;
        private Color gradientEndColor = Color.MidnightBlue;
        private Color borderColor = Color.Black;
        private int borderWidth = 1;
        private int cornerRadius = 15;

        [Category("Appearance")]
        [Description("The starting top color of the gradient.")]
        public Color GradientStartColor
        {
            get => gradientStartColor;
            set
            {
                gradientStartColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The ending bottom color of the gradient.")]
        public Color GradientEndColor
        {
            get => gradientEndColor;
            set
            {
                gradientEndColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The color of the panel border.")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The thickness of the panel border in pixels.")]
        public int BorderWidth
        {
            get => borderWidth;
            set
            {
                borderWidth = Math.Max(0, value);
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The radius of the rounded corners.")]
        public int CornerRadius
        {
            get => cornerRadius;
            set
            {
                cornerRadius = Math.Max(0, value);
                Invalidate();
            }
        }

        public BorderedGradientPanel()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Suppress background erase to prevent flickering
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (ClientRectangle.Width <= 0 || ClientRectangle.Height <= 0)
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            // 1. Draw Rounded Border (Outer Path)
            if (borderWidth > 0)
            {
                using (GraphicsPath outerPath = GetRoundedRectanglePath(rect, cornerRadius))
                using (SolidBrush borderBrush = new SolidBrush(borderColor))
                {
                    g.FillPath(borderBrush, outerPath);
                }
            }

            // 2. Draw Inner Gradient Background
            Rectangle innerRect = rect;
            innerRect.Inflate(-borderWidth, -borderWidth);

            if (innerRect.Width > 0 && innerRect.Height > 0)
            {
                int innerRadius = Math.Max(0, cornerRadius - borderWidth);
                using (GraphicsPath innerPath = GetRoundedRectanglePath(innerRect, innerRadius))
                using (LinearGradientBrush brush = new LinearGradientBrush(
                           innerRect,
                           gradientStartColor,
                           gradientEndColor,
                           LinearGradientMode.Vertical))
                {
                    g.FillPath(brush, innerPath);
                }
            }

            base.OnPaint(e);
        }

        private GraphicsPath GetRoundedRectanglePath(Rectangle r, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            if (d > r.Width) d = r.Width;
            if (d > r.Height) d = r.Height;

            if (d <= 0)
            {
                path.AddRectangle(r);
                return path;
            }

            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}