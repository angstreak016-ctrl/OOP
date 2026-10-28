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
    public class OutlinedLabel : Label
    {
        private Color outlineColor = Color.Black;
        private float outlineWidth = 2.0f;

        /// <summary>
        /// Gets or sets the color of the text outline.
        /// </summary>
        public Color OutlineColor
        {
            get => outlineColor;
            set { outlineColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the thickness of the text outline.
        /// </summary>
        public float OutlineWidth
        {
            get => outlineWidth;
            set { outlineWidth = value; Invalidate(); }
        }

        public OutlinedLabel()
        {
            // Enable double buffering to prevent flickering during redraws
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Enable high-quality rendering for smooth curves and anti-aliasing
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            using (GraphicsPath path = new GraphicsPath())
            {
                // Map WinForms TextAlign to GDI+ StringFormat
                StringFormat sf = new StringFormat();
                switch (TextAlign)
                {
                    case ContentAlignment.MiddleCenter:
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.TopCenter:
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopRight:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.BottomCenter:
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.BottomLeft:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.BottomRight:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.MiddleLeft:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.MiddleRight:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Center;
                        break;
                    default:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Near;
                        break;
                }

                RectangleF rect = new RectangleF(0, 0, Width, Height);

                // Convert font size from points to pixels based on current screen DPI
                float emSizePx = Font.Size * (e.Graphics.DpiY / 72.0f);

                // Create the path using the text and font properties
                path.AddString(
                    Text,
                    Font.FontFamily,
                    (int)Font.Style,
                    emSizePx,
                    rect,
                    sf
                );

                // 1. Draw the Outline (Stroke)
                using (Pen pen = new Pen(outlineColor, outlineWidth))
                {
                    pen.LineJoin = LineJoin.Round; // Gives corners a smooth, rounded look
                    e.Graphics.DrawPath(pen, path);
                }

                // 2. Fill the Text Interior
                using (Brush brush = new SolidBrush(ForeColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }
    }
}
