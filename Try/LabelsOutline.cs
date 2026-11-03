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

        // Gradient fields
        private bool useGradient = false;
        private Color textColorStart = Color.Yellow;
        private Color textColorEnd = Color.Red;
        private LinearGradientMode gradientMode = LinearGradientMode.Vertical;

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

        /// <summary>
        /// Gets or sets a value indicating whether the text interior uses a gradient fill.
        /// </summary>
        public bool UseGradient
        {
            get => useGradient;
            set { useGradient = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the starting color of the text gradient.
        /// </summary>
        public Color TextColorStart
        {
            get => textColorStart;
            set { textColorStart = value; ForeColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the ending color of the text gradient.
        /// </summary>
        public Color TextColorEnd
        {
            get => textColorEnd;
            set { textColorEnd = value; Invalidate(); }
        }

        public Color BackColorStart
        {
            get => TextColorStart;
            set => TextColorStart = value;
        }

        public Color BackColorEnd
        {
            get => TextColorEnd;
            set => TextColorEnd = value;
        }

        /// <summary>
        /// Gets or sets the direction/mode of the gradient.
        /// </summary>
        public LinearGradientMode GradientMode
        {
            get => gradientMode;
            set { gradientMode = value; Invalidate(); }
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

                // 2. Fill the Text Interior (Solid or Gradient)
                if (useGradient)
                {
                    // Use the control's full rectangle bounds so the gradient spans properly
                    RectangleF gradRect = new RectangleF(0, 0, Math.Max(Width, 1), Math.Max(Height, 1));

                    using (LinearGradientBrush gradBrush = new LinearGradientBrush(gradRect, textColorStart, textColorEnd, gradientMode))
                    {
                        e.Graphics.FillPath(gradBrush, path);
                    }
                }
                else
                {
                    using (Brush solidBrush = new SolidBrush(ForeColor))
                    {
                        e.Graphics.FillPath(solidBrush, path);
                    }
                }
            }
        }
    }
}