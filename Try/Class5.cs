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
 

    public class FadeGradientPanel : Panel
    {
        private Color _boxColor = Color.DodgerBlue;
        private Color _backgroundColor = Color.White; // Set this to match your Form's BackColor

        public Color BoxColor
        {
            get => _boxColor;
            set { _boxColor = value; Invalidate(); }
        }

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set { _backgroundColor = value; Invalidate(); }
        }

        public FadeGradientPanel()
        {
            // Prevent flickering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                _boxColor,
                _backgroundColor,
                LinearGradientMode.Horizontal)) // Use LinearGradientMode.Vertical for top-to-bottom
            {
                // ColorBlend lets us keep the first half solid, and fade the second half
                ColorBlend blend = new ColorBlend();
                blend.Colors = new Color[] { _boxColor, _boxColor, _backgroundColor };
                blend.Positions = new float[] { 0.0f, 0.5f, 1.0f }; // 0% to 50% is solid; 50% to 100% fades out
                brush.InterpolationColors = blend;

                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
    }
}
