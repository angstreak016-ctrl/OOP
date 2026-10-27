using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Try
{
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

        // Hover Outline Animation Colors (Yellow to Pink loop)
        public Color HoverOutlineStartColor { get; set; } = Color.Yellow;
        public Color HoverOutlineEndColor { get; set; } = Color.Magenta;

        // Gradient Background Properties
        public Color BackColorStart { get; set; } = Color.Peru;
        public Color BackColorEnd { get; set; } = Color.Gold;
        public LinearGradientMode GradientMode { get; set; } = LinearGradientMode.Vertical;

        // Hover state & Color Loop Animation fields
        private bool _isHovered = false;
        private Timer _colorAnimationTimer;
        private float _colorProgress = 0f;
        private bool _animatingForward = true;

        // Click Scale Animation fields & Base size tracking
        private Timer _clickAnimationTimer;
        private float _currentScale = 1.0f;
        private float _targetScale = 1.10f; // 20% larger growth
        private bool _isExpanding = true;

        private int _originalWidth = 0;
        private int _originalHeight = 0;
        private int _originalLeft = 0;
        private int _originalTop = 0;
        private bool _isOriginalBoundsStored = false;

        public OutlinedButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            // 1. Initialize outline color loop timer (~30 FPS)
            _colorAnimationTimer = new Timer();
            _colorAnimationTimer.Interval = 1;
            _colorAnimationTimer.Tick += (s, e) =>
            {
                if (_animatingForward)
                {
                    _colorProgress += 1f;
                    if (_colorProgress >= 1f)
                    {
                        _colorProgress = 1f;
                        _animatingForward = false;
                    }
                }
                else
                {
                    _colorProgress -= 0.2f;
                    if (_colorProgress <= 0f)
                    {
                        _colorProgress = 0f;
                        _animatingForward = true;
                    }
                }
                Invalidate();
            };

            // 2. Initialize click scale animation timer
            _clickAnimationTimer = new Timer();
            _clickAnimationTimer.Interval = 15;
            _clickAnimationTimer.Tick += (s, e) =>
            {
                if (_isExpanding)
                {
                    _currentScale += 0.03f;
                    if (_currentScale >= _targetScale)
                    {
                        _currentScale = _targetScale;
                        _isExpanding = false;
                    }
                }
                else
                {
                    _currentScale -= 0.03f;
                    if (_currentScale <= 1.0f)
                    {
                        _currentScale = 1.0f;
                        _isExpanding = true;
                        _clickAnimationTimer.Stop();
                    }
                }
                ApplyAnimatedBounds();
                Invalidate();
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _colorAnimationTimer?.Dispose();
                _clickAnimationTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            _colorAnimationTimer.Start();
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            _colorAnimationTimer.Stop();
            _colorProgress = 0f;
            _animatingForward = true;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            TriggerClickAnimation();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            TriggerClickAnimation();
        }

        private void TriggerClickAnimation()
        {
            StoreOriginalBounds();
            if (!_clickAnimationTimer.Enabled)
            {
                _currentScale = 1.0f;
                _isExpanding = true;
                _clickAnimationTimer.Start();
            }
        }

        private void StoreOriginalBounds()
        {
            if (!_isOriginalBoundsStored && !DesignMode)
            {
                _originalWidth = Width;
                _originalHeight = Height;
                _originalLeft = Left;
                _originalTop = Top;
                _isOriginalBoundsStored = true;
            }
        }

        private void ApplyAnimatedBounds()
        {
            if (_isOriginalBoundsStored && !DesignMode)
            {
                int newWidth = (int)(_originalWidth * _currentScale);
                int newHeight = (int)(_originalHeight * _currentScale);
                int newLeft = _originalLeft - (newWidth - _originalWidth) / 2;
                int newTop = _originalTop - (newHeight - _originalHeight) / 2;

                // Temporarily update bounds without triggering full layout resets
                base.SetBounds(newLeft, newTop, newWidth, newHeight, BoundsSpecified.All);
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            if (_currentScale == 1.0f)
            {
                _isOriginalBoundsStored = false; // Reset if moved externally
            }
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            if (_currentScale == 1.0f)
            {
                _isOriginalBoundsStored = false;
            }

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

            RectangleF clientRect = new RectangleF(0, 0, Width - 1, Height - 1);
            int effectiveRadius = IsPillShape ? Height / 2 : BorderRadius;

            // 1. Draw background gradient
            Color startColor = Enabled ? BackColorStart : Color.LightGray;
            Color endColor = Enabled ? BackColorEnd : Color.DarkGray;

            using (GraphicsPath bgPath = GetRoundedRectPath(clientRect, effectiveRadius))
            using (LinearGradientBrush backBrush = new LinearGradientBrush(clientRect, startColor, endColor, GradientMode))
            {
                g.FillPath(backBrush, bgPath);
            }

            // 2. Determine border color (loops between yellow and pink on hover, normal otherwise)
            Color currentOutlineColor = OutlineColor;
            if (_isHovered)
            {
                currentOutlineColor = InterpolateColor(HoverOutlineStartColor, HoverOutlineEndColor, _colorProgress);
            }

            using (GraphicsPath borderPath = GetRoundedRectPath(clientRect, effectiveRadius))
            using (Pen borderPen = new Pen(currentOutlineColor, OutlineWidth))
            {
                borderPen.LineJoin = LineJoin.Round;
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

                    RectangleF rect = new RectangleF(0, 3, Width, Height);

                    textPath.AddString(
                        Text,
                        Font.FontFamily,
                        (int)Font.Style,
                        Font.Size,
                        rect,
                        sf
                    );

                    // Draw text outline
                    using (Pen outlinePen = new Pen(OutlineColor, OutlineWidth))
                    {
                        outlinePen.LineJoin = LineJoin.Round;
                        g.DrawPath(outlinePen, textPath);
                    }

                    // Fill text inside
                    Color currentForeColor = Enabled ? ForeColor : Color.DarkGray;
                    using (Brush textBrush = new SolidBrush(currentForeColor))
                    {
                        g.FillPath(textBrush, textPath);
                    }
                }
            }
        }

        private Color InterpolateColor(Color color1, Color color2, float progress)
        {
            int r = (int)(color1.R + (color2.R - color1.R) * progress);
            int g = (int)(color1.G + (color2.G - color1.G) * progress);
            int b = (int)(color1.B + (color2.B - color1.B) * progress);
            return Color.FromArgb(r, g, b);
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
}

       