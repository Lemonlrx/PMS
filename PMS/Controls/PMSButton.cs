using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PMS.Controls
{
    [ToolboxItem(true)]
    public partial class PMSButton : Button
    {
        private int borderRadius = 10;
        private int borderSize = 0;

        private Color borderColor = Color.Transparent;
        private Color hoverBorderColor = Color.Transparent;
        private Color pressedBorderColor = Color.Transparent;

        private Color hoverBackColor = Color.FromArgb(90, 120, 255);
        private Color pressedBackColor = Color.FromArgb(55, 80, 210);

        private Color disabledBackColor = Color.FromArgb(45, 46, 52);
        private Color disabledBorderColor = Color.FromArgb(60, 61, 68);
        private Color disabledTextColor = Color.FromArgb(110, 110, 120);

        private Color textColor = Color.White;

        private bool useGradient = false;
        private Color gradientColor1 = Color.FromArgb(75, 110, 255);
        private Color gradientColor2 = Color.FromArgb(100, 130, 255);

        private bool isHovered = false;
        private bool isPressed = false;

        // =========================================================
        // Border Radius
        // =========================================================

        [Category("PMS Appearance")]
        [Description("ความมนของมุมปุ่ม")]
        [DefaultValue(10)]
        public int BorderRadius
        {
            get => borderRadius;
            set
            {
                borderRadius = Math.Max(0, value);
                UpdateRegion();
                Invalidate();
            }
        }

        // =========================================================
        // Border Size
        // =========================================================

        [Category("PMS Appearance")]
        [Description("ความหนาของเส้นขอบ")]
        [DefaultValue(0)]
        public int BorderSize
        {
            get => borderSize;
            set
            {
                borderSize = Math.Max(0, value);
                Invalidate();
            }
        }

        // =========================================================
        // Border Color
        // =========================================================

        [Category("PMS Appearance")]
        [Description("สีเส้นขอบปกติ")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("PMS Appearance")]
        [Description("สีเส้นขอบเมื่อ Mouse Hover")]
        public Color HoverBorderColor
        {
            get => hoverBorderColor;
            set
            {
                hoverBorderColor = value;
                Invalidate();
            }
        }

        [Category("PMS Appearance")]
        [Description("สีเส้นขอบขณะกด")]
        public Color PressedBorderColor
        {
            get => pressedBorderColor;
            set
            {
                pressedBorderColor = value;
                Invalidate();
            }
        }

        // =========================================================
        // Background Colors
        // =========================================================

        [Category("PMS Appearance")]
        [Description("สีพื้นหลังเมื่อ Mouse Hover")]
        public Color HoverBackColor
        {
            get => hoverBackColor;
            set
            {
                hoverBackColor = value;
                Invalidate();
            }
        }

        [Category("PMS Appearance")]
        [Description("สีพื้นหลังขณะกด")]
        public Color PressedBackColor
        {
            get => pressedBackColor;
            set
            {
                pressedBackColor = value;
                Invalidate();
            }
        }

        // =========================================================
        // Disabled Colors
        // =========================================================

        [Category("PMS Appearance")]
        [Description("สีพื้นหลังเมื่อ Disabled")]
        public Color DisabledBackColor
        {
            get => disabledBackColor;
            set
            {
                disabledBackColor = value;
                Invalidate();
            }
        }

        [Category("PMS Appearance")]
        [Description("สีเส้นขอบเมื่อ Disabled")]
        public Color DisabledBorderColor
        {
            get => disabledBorderColor;
            set
            {
                disabledBorderColor = value;
                Invalidate();
            }
        }

        [Category("PMS Appearance")]
        [Description("สีข้อความเมื่อ Disabled")]
        public Color DisabledTextColor
        {
            get => disabledTextColor;
            set
            {
                disabledTextColor = value;
                Invalidate();
            }
        }

        // =========================================================
        // Text Color
        // =========================================================

        [Category("PMS Appearance")]
        [Description("สีข้อความ")]
        public Color TextColor
        {
            get => textColor;
            set
            {
                textColor = value;
                Invalidate();
            }
        }

        // =========================================================
        // Gradient
        // =========================================================

        [Category("PMS Gradient")]
        [Description("เปิดใช้งาน Gradient")]
        [DefaultValue(false)]
        public bool UseGradient
        {
            get => useGradient;
            set
            {
                useGradient = value;
                Invalidate();
            }
        }

        [Category("PMS Gradient")]
        [Description("สี Gradient จุดเริ่มต้น")]
        public Color GradientColor1
        {
            get => gradientColor1;
            set
            {
                gradientColor1 = value;
                Invalidate();
            }
        }

        [Category("PMS Gradient")]
        [Description("สี Gradient จุดสิ้นสุด")]
        public Color GradientColor2
        {
            get => gradientColor2;
            set
            {
                gradientColor2 = value;
                Invalidate();
            }
        }

        // =========================================================
        // Constructor
        // =========================================================

        public PMSButton()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true
            );

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            UseVisualStyleBackColor = false;

            Cursor = Cursors.Hand;

            DoubleBuffered = true;

            UpdateRegion();
        }

        // =========================================================
        // Paint
        // =========================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle rect = new Rectangle(
                0,
                0,
                Width - 1,
                Height - 1);

            int radius = Math.Min(
                BorderRadius,
                Math.Min(
                    Width / 2,
                    Height / 2));

            using (GraphicsPath path =
                   CreateRoundRectangle(rect, radius))
            {
                // =================================================
                // Background
                // =================================================

                Color backgroundColor;

                if (!Enabled)
                {
                    backgroundColor = DisabledBackColor;
                }
                else if (isPressed)
                {
                    backgroundColor = PressedBackColor;
                }
                else if (isHovered)
                {
                    backgroundColor = HoverBackColor;
                }
                else
                {
                    backgroundColor = BackColor;
                }

                if (UseGradient &&
                    Enabled &&
                    !isPressed)
                {
                    Color color1 =
                        isHovered
                            ? HoverBackColor
                            : GradientColor1;

                    Color color2 =
                        isHovered
                            ? HoverBackColor
                            : GradientColor2;

                    using (LinearGradientBrush brush =
                           new LinearGradientBrush(
                               rect,
                               color1,
                               color2,
                               LinearGradientMode.Horizontal))
                    {
                        g.FillPath(brush, path);
                    }
                }
                else
                {
                    using (SolidBrush brush =
                           new SolidBrush(backgroundColor))
                    {
                        g.FillPath(brush, path);
                    }
                }

                // =================================================
                // Border
                // =================================================

                if (BorderSize > 0)
                {
                    Color currentBorderColor;

                    if (!Enabled)
                    {
                        currentBorderColor =
                            DisabledBorderColor;
                    }
                    else if (isPressed)
                    {
                        currentBorderColor =
                            PressedBorderColor;
                    }
                    else if (isHovered)
                    {
                        currentBorderColor =
                            HoverBorderColor;
                    }
                    else
                    {
                        currentBorderColor =
                            BorderColor;
                    }

                    if (currentBorderColor != Color.Transparent)
                    {
                        using (Pen pen =
                               new Pen(
                                   currentBorderColor,
                                   BorderSize))
                        {
                            pen.Alignment =
                                PenAlignment.Inset;

                            g.DrawPath(
                                pen,
                                path);
                        }
                    }
                }
            }

            // =====================================================
            // Text
            // =====================================================

            Color currentTextColor =
                Enabled
                    ? TextColor
                    : DisabledTextColor;

            TextRenderer.DrawText(
                g,
                Text,
                Font,
                new Rectangle(
                    0,
                    0,
                    Width,
                    Height),
                currentTextColor,
                GetTextFormatFlags());
        }

        // =========================================================
        // Text Alignment
        // =========================================================

        private TextFormatFlags GetTextFormatFlags()
        {
            TextFormatFlags flags =
                TextFormatFlags.NoPadding |
                TextFormatFlags.EndEllipsis;

            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                    flags |=
                        TextFormatFlags.Top |
                        TextFormatFlags.Left;
                    break;

                case ContentAlignment.TopCenter:
                    flags |=
                        TextFormatFlags.Top |
                        TextFormatFlags.HorizontalCenter;
                    break;

                case ContentAlignment.TopRight:
                    flags |=
                        TextFormatFlags.Top |
                        TextFormatFlags.Right;
                    break;

                case ContentAlignment.MiddleLeft:
                    flags |=
                        TextFormatFlags.VerticalCenter |
                        TextFormatFlags.Left;
                    break;

                case ContentAlignment.MiddleCenter:
                    flags |=
                        TextFormatFlags.VerticalCenter |
                        TextFormatFlags.HorizontalCenter;
                    break;

                case ContentAlignment.MiddleRight:
                    flags |=
                        TextFormatFlags.VerticalCenter |
                        TextFormatFlags.Right;
                    break;

                case ContentAlignment.BottomLeft:
                    flags |=
                        TextFormatFlags.Bottom |
                        TextFormatFlags.Left;
                    break;

                case ContentAlignment.BottomCenter:
                    flags |=
                        TextFormatFlags.Bottom |
                        TextFormatFlags.HorizontalCenter;
                    break;

                case ContentAlignment.BottomRight:
                    flags |=
                        TextFormatFlags.Bottom |
                        TextFormatFlags.Right;
                    break;
            }

            return flags;
        }

        // =========================================================
        // Mouse Events
        // =========================================================

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            isHovered = true;

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            isHovered = false;
            isPressed = false;

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                isPressed = true;

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            isPressed = false;

            Invalidate();
        }

        // =========================================================
        // Enabled
        // =========================================================

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            Invalidate();
        }

        // =========================================================
        // Resize
        // =========================================================

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            UpdateRegion();

            Invalidate();
        }

        // =========================================================
        // Rounded Region
        // =========================================================

        private void UpdateRegion()
        {
            if (Width <= 0 || Height <= 0)
                return;

            int radius =
                Math.Min(
                    BorderRadius,
                    Math.Min(
                        Width / 2,
                        Height / 2));

            using (GraphicsPath path =
                   CreateRoundRectangle(
                       new Rectangle(
                           0,
                           0,
                           Width,
                           Height),
                       radius))
            {
                Region = new Region(path);
            }
        }

        // =========================================================
        // Create Rounded Rectangle
        // =========================================================

        private GraphicsPath CreateRoundRectangle(
            Rectangle rect,
            int radius)
        {
            GraphicsPath path =
                new GraphicsPath();

            if (radius <= 0)
            {
                path.AddRectangle(rect);

                return path;
            }

            int diameter = radius * 2;

            path.AddArc(
                rect.X,
                rect.Y,
                diameter,
                diameter,
                180,
                90);

            path.AddArc(
                rect.Right - diameter,
                rect.Y,
                diameter,
                diameter,
                270,
                90);

            path.AddArc(
                rect.Right - diameter,
                rect.Bottom - diameter,
                diameter,
                diameter,
                0,
                90);

            path.AddArc(
                rect.X,
                rect.Bottom - diameter,
                diameter,
                diameter,
                90,
                90);

            path.CloseFigure();

            return path;
        }
    }
}