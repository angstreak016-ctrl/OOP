using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Try
{
    public class RoundedPanel : Panel
    {
        private int borderRadius = 20;
        private Color borderColor = Color.LightGray;
        private int borderSize = 1;

        /// <summary>
        /// Gets or sets the corner radius of the panel.
        /// </summary>
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color of the panel's border.
        /// </summary>
        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the thickness of the panel's border.
        /// </summary>
        public int BorderSize
        {
            get => borderSize;
            set { borderSize = value; Invalidate(); }
        }

        public RoundedPanel()
        {
            // Enable double buffering to eliminate flickering
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
        }

        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rectSurface = ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = borderSize > 0 ? borderSize : 1;

            if (borderRadius > 2)
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    // Clips the panel layout to the rounded shape so child controls don't overflow the corners
                    this.Region = new Region(pathSurface);

                    // Draw the custom border
                    if (borderSize > 0)
                    {
                        e.Graphics.DrawPath(penBorder, pathBorder);
                    }
                }
            }
            else
            {
                // Fallback to square corners if radius is too small
                this.Region = new Region(rectSurface);
                if (borderSize > 0)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        e.Graphics.DrawRectangle(penBorder, 0, 0, Width - 1, Height - 1);
                    }
                }
            }
        }
    }
}
