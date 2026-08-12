using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PMS.Controls
{
    public partial class MacWindowButtons : UserControl
    {
        private const int ButtonSize = 13;
        private const int ButtonGap = 8;

        private bool hoverClose;
        private bool hoverMinimize;
        private bool hoverMaximize;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(
            IntPtr hWnd,
            int Msg,
            int wParam,
            int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        public MacWindowButtons()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            BackColor = Color.Transparent;
            Size = new Size(72, 28);
            Cursor = Cursors.Default;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int y = (Height - ButtonSize) / 2;

            DrawButton(
                e.Graphics,
                new Rectangle(5, y, ButtonSize, ButtonSize),
                Color.FromArgb(255, 95, 86),
                hoverClose);

            DrawButton(
                e.Graphics,
                new Rectangle(5 + ButtonSize + ButtonGap, y, ButtonSize, ButtonSize),
                Color.FromArgb(255, 189, 46),
                hoverMinimize);

            DrawButton(
                e.Graphics,
                new Rectangle(
                    5 + (ButtonSize + ButtonGap) * 2,
                    y,
                    ButtonSize,
                    ButtonSize),
                Color.FromArgb(39, 201, 63),
                hoverMaximize);
        }

        private void DrawButton(
            Graphics graphics,
            Rectangle rectangle,
            Color color,
            bool hover)
        {
            using (Brush brush = new SolidBrush(color))
            {
                graphics.FillEllipse(brush, rectangle);
            }

            if (hover)
            {
                using (Pen pen = new Pen(Color.FromArgb(80, Color.Black), 1))
                {
                    graphics.DrawEllipse(
                        pen,
                        rectangle.X,
                        rectangle.Y,
                        rectangle.Width - 1,
                        rectangle.Height - 1);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            hoverClose = IsInsideButton(e.Location, 0);
            hoverMinimize = IsInsideButton(e.Location, 1);
            hoverMaximize = IsInsideButton(e.Location, 2);

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            hoverClose = false;
            hoverMinimize = false;
            hoverMaximize = false;

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button != MouseButtons.Left)
                return;

            if (IsInsideButton(e.Location, 0))
            {
                FindForm()?.Close();
                return;
            }

            if (IsInsideButton(e.Location, 1))
            {
                Form form = FindForm();

                if (form != null)
                    form.WindowState = FormWindowState.Minimized;

                return;
            }

            if (IsInsideButton(e.Location, 2))
            {
                Form form = FindForm();

                if (form != null)
                {
                    if (form.WindowState == FormWindowState.Maximized)
                        form.WindowState = FormWindowState.Normal;
                    else
                        form.WindowState = FormWindowState.Maximized;
                }

                return;
            }

            // ถ้ากดบริเวณว่างของ Control ให้ลาก Form
            Form parentForm = FindForm();

            if (parentForm != null)
            {
                ReleaseCapture();

                SendMessage(
                    parentForm.Handle,
                    WM_NCLBUTTONDOWN,
                    HTCAPTION,
                    0);
            }
        }

        private bool IsInsideButton(Point point, int index)
        {
            int x = 5 + (ButtonSize + ButtonGap) * index;
            int y = (Height - ButtonSize) / 2;

            Rectangle rectangle = new Rectangle(
                x,
                y,
                ButtonSize,
                ButtonSize);

            return rectangle.Contains(point);
        }
    }
}