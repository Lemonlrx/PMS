using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PMS.Controls
{
    public partial class PMSTextBox : TextBox
    {
        // =========================================================
        // COLORS
        // =========================================================

        private Color borderColor =
            Color.FromArgb(65, 68, 78);

        private Color focusBorderColor =
            Color.FromArgb(75, 110, 255);

        private Color placeholderColor =
            Color.FromArgb(110, 114, 125);

        private Color textBackColor =
            Color.FromArgb(38, 39, 45);

        private int borderRadius = 8;
        private int borderSize = 1;

        // =========================================================
        // PLACEHOLDER
        // =========================================================

        private string placeholderText = "";

        [Category("PMS TextBox")]
        [Description("ข้อความที่แสดงเป็นพื้นหลังเมื่อยังไม่มีข้อความ")]
        public string PlaceholderText
        {
            get => placeholderText;

            set
            {
                placeholderText = value;
                UpdatePlaceholder();
            }
        }

        [Category("PMS TextBox")]
        [Description("สีของ Placeholder")]
        public Color PlaceholderColor
        {
            get => placeholderColor;

            set
            {
                placeholderColor = value;
                Invalidate();
            }
        }

        // =========================================================
        // BORDER
        // =========================================================

        [Category("PMS Border")]
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
        public Color FocusBorderColor
        {
            get => focusBorderColor;

            set
            {
                focusBorderColor = value;
                Invalidate();
            }
        }

        [Category("PMS Border")]
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

        [Category("PMS Border")]
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
        // BACKGROUND
        // =========================================================

        [Category("PMS Theme")]
        public Color TextBackColor
        {
            get => textBackColor;

            set
            {
                textBackColor = value;
                BackColor = value;
                Invalidate();
            }
        }

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public PMSTextBox()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            BorderStyle = BorderStyle.None;

            BackColor = textBackColor;

            ForeColor =
                Color.FromArgb(
                    235,
                    236,
                    240);

            Font =
                new Font(
                    "Segoe UI",
                    10F);

            Padding =
                new Padding(
                    10,
                    7,
                    10,
                    7);

            Height = 38;

            UpdateRegion();

            GotFocus +=
                (s, e) => Invalidate();

            LostFocus +=
                (s, e) => Invalidate();

            Resize +=
                (s, e) =>
                {
                    UpdateRegion();
                    Invalidate();
                };
        }

        // =========================================================
        // REGION
        // =========================================================

        private void UpdateRegion()
        {
            if (Width <= 0 || Height <= 0)
                return;

            using (GraphicsPath path =
                   CreateRoundedPath(
                       new Rectangle(
                           0,
                           0,
                           Width,
                           Height),
                       borderRadius))
            {
                Region =
                    new Region(path);
            }
        }

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

        // =========================================================
        // PAINT BORDER
        // =========================================================

        protected override void WndProc(
            ref Message m)
        {
            const int WM_NCPAINT = 0x0085;

            base.WndProc(ref m);

            if (m.Msg == WM_NCPAINT)
            {
                DrawBorder();
            }
        }

        private void DrawBorder()
        {
            if (Width <= 0 || Height <= 0)
                return;

            using (Graphics g =
                   Graphics.FromHwnd(Handle))
            {
                g.SmoothingMode =
                    SmoothingMode.AntiAlias;

                Color color =
                    Focused
                        ? focusBorderColor
                        : borderColor;

                using (Pen pen =
                       new Pen(
                           color,
                           borderSize))
                {
                    Rectangle rect =
                        new Rectangle(
                            borderSize / 2,
                            borderSize / 2,
                            Width - borderSize,
                            Height - borderSize);

                    using (GraphicsPath path =
                           CreateRoundedPath(
                               rect,
                               borderRadius))
                    {
                        g.DrawPath(
                            pen,
                            path);
                    }
                }
            }
        }

        // =========================================================
        // PLACEHOLDER
        // =========================================================

        private void UpdatePlaceholder()
        {
            if (IsHandleCreated)
            {
                SendMessage(
                    Handle,
                    EM_SETCUEBANNER,
                    0,
                    placeholderText ?? "");
            }
        }

        protected override void OnHandleCreated(
            EventArgs e)
        {
            base.OnHandleCreated(e);

            UpdatePlaceholder();
        }

        // =========================================================
        // WINDOWS CUE BANNER
        // =========================================================

        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll",
            CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(
            IntPtr hWnd,
            int msg,
            int wParam,
            string lParam);
    }
}