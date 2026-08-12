using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PMS.Controls
{
    public partial class PMSCheckBox : CheckBox
    {
        // =========================================================
        // COLORS
        // =========================================================

        private Color boxColor =
            Color.FromArgb(38, 39, 45);

        private Color checkedColor =
            Color.FromArgb(75, 110, 255);

        private Color hoverColor =
            Color.FromArgb(90, 123, 255);

        private Color borderColor =
            Color.FromArgb(75, 78, 88);

        private Color checkedBorderColor =
            Color.FromArgb(75, 110, 255);

        private Color textColor =
            Color.FromArgb(235, 236, 240);

        private Color disabledColor =
            Color.FromArgb(90, 92, 100);

        // =========================================================
        // SIZE
        // =========================================================

        private int boxSize = 20;

        private int borderSize = 1;

        private int borderRadius = 5;

        private int spacing = 9;

        // =========================================================
        // STATE
        // =========================================================

        private bool isHovered = false;

        // =========================================================
        // BOX COLOR
        // =========================================================

        [Category("PMS CheckBox")]
        [Description("สีพื้นหลังของ Checkbox")]
        public Color BoxColor
        {
            get => boxColor;

            set
            {
                boxColor = value;
                Invalidate();
            }
        }

        // =========================================================
        // CHECKED COLOR
        // =========================================================

        [Category("PMS CheckBox")]
        [Description("สีเมื่อ Checkbox ถูกเลือก")]
        public Color CheckedColor
        {
            get => checkedColor;

            set
            {
                checkedColor = value;
                Invalidate();
            }
        }

        // =========================================================
        // HOVER COLOR
        // =========================================================

        [Category("PMS CheckBox")]
        [Description("สีเมื่อ Mouse อยู่เหนือ Checkbox")]
        public Color HoverColor
        {
            get => hoverColor;

            set
            {
                hoverColor = value;
                Invalidate();
            }
        }

        // =========================================================
        // BORDER
        // =========================================================

        [Category("PMS Border")]
        [Description("สีขอบ Checkbox")]
        public Color BorderColor
        {
            get => borderColor;

            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("PMS Border")]
        [Description("สีขอบเมื่อ Checkbox ถูกเลือก")]
        public Color CheckedBorderColor
        {
            get => checkedBorderColor;

            set
            {
                checkedBorderColor = value;
                Invalidate();
            }
        }

        [Category("PMS Border")]
        [Description("ความหนาของขอบ")]
        public int BorderSize
        {
            get => borderSize;

            set
            {
                borderSize = Math.Max(0, value);
                Invalidate();
            }
        }

        [Category("PMS Border")]
        [Description("ความโค้งของ Checkbox")]
        public int BorderRadius
        {
            get => borderRadius;

            set
            {
                borderRadius = Math.Max(0, value);
                Invalidate();
            }
        }

        // =========================================================
        // TEXT
        // =========================================================

        [Category("PMS Theme")]
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

        [Category("PMS Theme")]
        [Description("สีเมื่อ Checkbox ถูก Disabled")]
        public Color DisabledColor
        {
            get => disabledColor;

            set
            {
                disabledColor = value;
                Invalidate();
            }
        }

        // =========================================================
        // SIZE
        // =========================================================

        [Category("PMS Layout")]
        [Description("ขนาดของ Checkbox")]
        public int BoxSize
        {
            get => boxSize;

            set
            {
                boxSize =
                    Math.Max(
                        12,
                        Math.Min(
                            40,
                            value));

                Height =
                    Math.Max(
                        Height,
                        boxSize + 4);

                Invalidate();
            }
        }

        [Category("PMS Layout")]
        [Description("ระยะห่างระหว่าง Checkbox กับข้อความ")]
        public int TextSpacing
        {
            get => spacing;

            set
            {
                spacing = Math.Max(0, value);
                Invalidate();
            }
        }

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public PMSCheckBox()
        {
            // -----------------------------------------------------
            // Use custom drawing
            // -----------------------------------------------------

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            AutoSize = false;

            TextAlign =
                ContentAlignment.MiddleLeft;

            ForeColor =
                textColor;

            Font =
                new Font(
                    "Segoe UI",
                    9.5F);

            Height = 28;

            Cursor =
                Cursors.Hand;

            // -----------------------------------------------------
            // Mouse
            // -----------------------------------------------------

            MouseEnter +=
                PMSCheckBox_MouseEnter;

            MouseLeave +=
                PMSCheckBox_MouseLeave;

            // -----------------------------------------------------
            // State
            // -----------------------------------------------------

            CheckedChanged +=
                (s, e) =>
                {
                    Invalidate();
                };

            CheckStateChanged +=
                (s, e) =>
                {
                    Invalidate();
                };

            EnabledChanged +=
                (s, e) =>
                {
                    Invalidate();
                };
        }

        // =========================================================
        // MOUSE
        // =========================================================

        private void PMSCheckBox_MouseEnter(
            object sender,
            EventArgs e)
        {
            isHovered = true;

            Invalidate();
        }

        private void PMSCheckBox_MouseLeave(
            object sender,
            EventArgs e)
        {
            isHovered = false;

            Invalidate();
        }

        // =========================================================
        // PAINT
        // =========================================================

        protected override void OnPaint(
            PaintEventArgs e)
        {
            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            e.Graphics.TextRenderingHint =
                System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // -----------------------------------------------------
            // Calculate box
            // -----------------------------------------------------

            int y =
                (Height - boxSize) / 2;

            Rectangle box =
                new Rectangle(
                    1,
                    y,
                    boxSize,
                    boxSize);

            // -----------------------------------------------------
            // Colors
            // -----------------------------------------------------

            Color backgroundColor;

            Color currentBorderColor;

            if (!Enabled)
            {
                backgroundColor =
                    Color.FromArgb(
                        35,
                        36,
                        41);

                currentBorderColor =
                    disabledColor;
            }
            else if (Checked)
            {
                backgroundColor =
                    isHovered
                        ? hoverColor
                        : checkedColor;

                currentBorderColor =
                    checkedBorderColor;
            }
            else
            {
                backgroundColor =
                    isHovered
                        ? Color.FromArgb(
                            45,
                            47,
                            55)
                        : boxColor;

                currentBorderColor =
                    borderColor;
            }

            // -----------------------------------------------------
            // Draw box
            // -----------------------------------------------------

            using (GraphicsPath path =
                   CreateRoundedPath(
                       box,
                       borderRadius))
            {
                using (SolidBrush brush =
                       new SolidBrush(
                           backgroundColor))
                {
                    e.Graphics.FillPath(
                        brush,
                        path);
                }

                if (borderSize > 0)
                {
                    using (Pen pen =
                           new Pen(
                               currentBorderColor,
                               borderSize))
                    {
                        e.Graphics.DrawPath(
                            pen,
                            path);
                    }
                }
            }

            // -----------------------------------------------------
            // Check mark
            // -----------------------------------------------------

            if (Checked)
            {
                DrawCheckMark(
                    e.Graphics,
                    box,
                    Enabled);
            }

            // -----------------------------------------------------
            // Indeterminate
            // -----------------------------------------------------

            if (CheckState ==
                CheckState.Indeterminate)
            {
                DrawIndeterminate(
                    e.Graphics,
                    box);
            }

            // -----------------------------------------------------
            // Text
            // -----------------------------------------------------

            int textX =
                box.Right +
                spacing;

            Rectangle textRect =
                new Rectangle(
                    textX,
                    0,
                    Width - textX,
                    Height);

            Color currentTextColor =
                Enabled
                    ? textColor
                    : disabledColor;

            TextRenderer.DrawText(
                e.Graphics,
                Text,
                Font,
                textRect,
                currentTextColor,
                TextFormatFlags.Left |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis);
        }

        // =========================================================
        // CHECK MARK
        // =========================================================

        private void DrawCheckMark(
            Graphics graphics,
            Rectangle box,
            bool enabled)
        {
            using (Pen pen =
                   new Pen(
                       Color.White,
                       Math.Max(
                           2,
                           boxSize / 7)))
            {
                pen.StartCap =
                    LineCap.Round;

                pen.EndCap =
                    LineCap.Round;

                pen.LineJoin =
                    LineJoin.Round;

                Point p1 =
                    new Point(
                        box.Left +
                        box.Width / 4,
                        box.Top +
                        box.Height / 2);

                Point p2 =
                    new Point(
                        box.Left +
                        box.Width / 2 - 1,
                        box.Bottom -
                        box.Height / 4);

                Point p3 =
                    new Point(
                        box.Right -
                        box.Width / 5,
                        box.Top +
                        box.Height / 4);

                graphics.DrawLines(
                    pen,
                    new[]
                    {
                        p1,
                        p2,
                        p3
                    });
            }
        }

        // =========================================================
        // INDETERMINATE
        // =========================================================

        private void DrawIndeterminate(
            Graphics graphics,
            Rectangle box)
        {
            int margin =
                Math.Max(
                    4,
                    boxSize / 4);

            Rectangle line =
                new Rectangle(
                    box.Left + margin,
                    box.Top +
                    box.Height / 2 - 2,
                    box.Width -
                    margin * 2,
                    4);

            using (SolidBrush brush =
                   new SolidBrush(
                       Color.White))
            {
                graphics.FillRectangle(
                    brush,
                    line);
            }
        }

        // =========================================================
        // ROUNDED PATH
        // =========================================================

        private GraphicsPath CreateRoundedPath(
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

            int d =
                Math.Min(
                    radius * 2,
                    Math.Min(
                        rect.Width,
                        rect.Height));

            path.AddArc(
                rect.X,
                rect.Y,
                d,
                d,
                180,
                90);

            path.AddArc(
                rect.Right - d,
                rect.Y,
                d,
                d,
                270,
                90);

            path.AddArc(
                rect.Right - d,
                rect.Bottom - d,
                d,
                d,
                0,
                90);

            path.AddArc(
                rect.X,
                rect.Bottom - d,
                d,
                d,
                90,
                90);

            path.CloseFigure();

            return path;
        }
    }
}