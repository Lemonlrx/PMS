using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PMS.Controls
{
    public partial class MovingTabs : UserControl
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