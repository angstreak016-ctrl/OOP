using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Try
{
    public class GradientPanel : Panel
    {
        private Color startColor = Color.LightSkyBlue;
        private Color endColor = Color.MidnightBlue;

        [Category("Gradient Properties")]
        [Description("The starting top color of the gradient.")]
        public Color StartColor
        {
            get => startColor;
            set
            {
                startColor = value;
                Invalidate();
            }
        }

        [Category("Gradient Properties")]
        [Description("The ending bottom color of the gradient.")]
        public Color EndColor
        {
            get => endColor;
            set
            {
                endColor = value;
                Invalidate();
            }
        }

        public GradientPanel()
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

            using (LinearGradientBrush brush = new LinearGradientBrush(
                       ClientRectangle,
                       startColor,
                       endColor,
                       LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }

            base.OnPaint(e);
        }
    }
}
