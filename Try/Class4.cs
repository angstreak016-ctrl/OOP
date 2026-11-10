using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Try
{
    public class GradientLabel : Control
    {
        private Color gradientStartColor = Color.LightSkyBlue;
        private Color gradientEndColor = Color.MidnightBlue;
        private LinearGradientMode gradientMode = LinearGradientMode.Vertical;
        private ContentAlignment textAlign = ContentAlignment.MiddleCenter;

        [Category("Appearance")]
        [Description("The starting color of the text gradient.")]
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
        [Description("The ending color of the text gradient.")]
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
        [Description("The direction of the text gradient.")]
        public LinearGradientMode GradientMode
        {
            get => gradientMode;
            set
            {
                gradientMode = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The alignment of the text within the control.")]
        public ContentAlignment TextAlign
        {
            get => textAlign;
            set
            {
                textAlign = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        public GradientLabel()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            
            BackColor = Color.Transparent;
            Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            Text = "Gradient Label";
            Size = new Size(160, 40);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (string.IsNullOrEmpty(Text))
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // 1. Draw Background if not transparent
            if (BackColor != Color.Transparent)
            {
                using (SolidBrush bgBrush = new SolidBrush(BackColor))
                {
                    g.FillRectangle(bgBrush, ClientRectangle);
                }
            }

            // 2. Configure String Alignment
            using (StringFormat sf = new StringFormat())
            {
                switch (textAlign)
                {
                    case ContentAlignment.TopLeft:
                        sf.Alignment = StringAlignment.Near; sf.LineAlignment = StringAlignment.Near; break;
                    case ContentAlignment.TopCenter:
                        sf.Alignment = StringAlignment.Center; sf.LineAlignment = StringAlignment.Near; break;
                    case ContentAlignment.TopRight:
                        sf.Alignment = StringAlignment.Far; sf.LineAlignment = StringAlignment.Near; break;
                    case ContentAlignment.MiddleLeft:
                        sf.Alignment = StringAlignment.Near; sf.LineAlignment = StringAlignment.Center; break;
                    case ContentAlignment.MiddleCenter:
                        sf.Alignment = StringAlignment.Center; sf.LineAlignment = StringAlignment.Center; break;
                    case ContentAlignment.MiddleRight:
                        sf.Alignment = StringAlignment.Far; sf.LineAlignment = StringAlignment.Center; break;
                    case ContentAlignment.BottomLeft:
                        sf.Alignment = StringAlignment.Near; sf.LineAlignment = StringAlignment.Far; break;
                    case ContentAlignment.BottomCenter:
                        sf.Alignment = StringAlignment.Center; sf.LineAlignment = StringAlignment.Far; break;
                    case ContentAlignment.BottomRight:
                        sf.Alignment = StringAlignment.Far; sf.LineAlignment = StringAlignment.Far; break;
                }

                // 3. Render Gradient Text via GraphicsPath
                try
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        float emSize = g.DpiY * Font.Size / 72f;
                        path.AddString(Text, Font.FontFamily, (int)Font.Style, emSize, ClientRectangle, sf);

                        using (LinearGradientBrush brush = new LinearGradientBrush(
                            ClientRectangle,
                            gradientStartColor,
                            gradientEndColor,
                            gradientMode))
                        {
                            g.FillPath(brush, path);
                        }
                    }
                }
                catch
                {
                    // Fallback if path generation fails
                    using (SolidBrush fallbackBrush = new SolidBrush(ForeColor))
                    {
                        g.DrawString(Text, Font, fallbackBrush, ClientRectangle, sf);
                    }
                }
            }
        }
    }
}