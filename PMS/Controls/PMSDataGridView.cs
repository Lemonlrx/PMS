using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PMS.Controls
{
    public partial class PMSDataGridView : DataGridView
    {
        // =========================================================
        // COLORS
        // =========================================================

        private Color gridBackground =
            Color.FromArgb(28, 29, 34);

        private Color headerBackground =
            Color.FromArgb(38, 40, 47);

        private Color headerForeground =
            Color.FromArgb(235, 236, 240);

        private Color rowForeground =
            Color.FromArgb(220, 222, 228);

        private Color alternateRowBackground =
            Color.FromArgb(31, 32, 38);

        private Color selectionBackground =
            Color.FromArgb(55, 75, 130);

        private Color gridLineColor =
            Color.FromArgb(48, 50, 58);

        private Color borderColor =
            Color.FromArgb(65, 68, 78);

        private int borderRadius = 10;

        // =========================================================
        // THEME
        // =========================================================

        [Category("PMS Theme")]
        public Color GridBackground
        {
            get => gridBackground;

            set
            {
                gridBackground = value;
                ApplyTheme();
            }
        }

        [Category("PMS Theme")]
        public Color HeaderBackground
        {
            get => headerBackground;

            set
            {
                headerBackground = value;
                ApplyTheme();
            }
        }

        [Category("PMS Theme")]
        public Color HeaderForeground
        {
            get => headerForeground;

            set
            {
                headerForeground = value;
                ApplyTheme();
            }
        }

        [Category("PMS Theme")]
        public Color RowForeground
        {
            get => rowForeground;

            set
            {
                rowForeground = value;
                ApplyTheme();
            }
        }

        [Category("PMS Theme")]
        public Color AlternateRowBackground
        {
            get => alternateRowBackground;

            set
            {
                alternateRowBackground = value;
                ApplyTheme();
            }
        }

        [Category("PMS Theme")]
        public Color SelectionBackground
        {
            get => selectionBackground;

            set
            {
                selectionBackground = value;
                ApplyTheme();
            }
        }

        [Category("PMS Theme")]
        public Color GridLineColor
        {
            get => gridLineColor;

            set
            {
                gridLineColor = value;
                ApplyTheme();
            }
        }

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
        public int BorderRadius
        {
            get => borderRadius;

            set
            {
                borderRadius =
                    Math.Max(0, value);

                UpdateRegion();
                Invalidate();
            }
        }

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public PMSDataGridView()
        {
            DoubleBuffered = true;

            BorderStyle =
                BorderStyle.None;

            BackgroundColor =
                gridBackground;

            Font =
                new Font(
                    "Segoe UI",
                    9.5F);

            RowTemplate.Height = 42;

            AllowUserToAddRows = false;

            EnableHeadersVisualStyles = false;

            AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            ColumnHeadersHeight = 44;

            RowHeadersVisible = false;

            SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            MultiSelect = false;

            CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            GridColor =
                gridLineColor;

            UpdateRegion();

            ApplyTheme();

            Resize +=
                (s, e) =>
                {
                    UpdateRegion();
                    Invalidate();
                };
        }

        // =========================================================
        // THEME
        // =========================================================

        private void ApplyTheme()
        {
            BackgroundColor =
                gridBackground;

            GridColor =
                gridLineColor;

            DefaultCellStyle.BackColor =
                gridBackground;

            DefaultCellStyle.ForeColor =
                rowForeground;

            DefaultCellStyle.SelectionBackColor =
                selectionBackground;

            DefaultCellStyle.SelectionForeColor =
                Color.White;

            DefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9.5F);

            AlternatingRowsDefaultCellStyle.BackColor =
                alternateRowBackground;

            AlternatingRowsDefaultCellStyle.ForeColor =
                rowForeground;

            ColumnHeadersDefaultCellStyle.BackColor =
                headerBackground;

            ColumnHeadersDefaultCellStyle.ForeColor =
                headerForeground;

            ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9.5F,
                    FontStyle.Bold);

            ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            ColumnHeadersDefaultCellStyle.Padding =
                new Padding(10, 0, 10, 0);

            RowHeadersDefaultCellStyle.BackColor =
                headerBackground;

            RowHeadersDefaultCellStyle.ForeColor =
                headerForeground;

            Invalidate();
        }

        // =========================================================
        // ROUNDED REGION
        // =========================================================

        private void UpdateRegion()
        {
            if (Width <= 0 ||
                Height <= 0)
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
        // BORDER
        // =========================================================

        protected override void OnPaint(
            PaintEventArgs e)
        {
            base.OnPaint(e);

            if (borderRadius <= 0)
                return;

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            using (Pen pen =
                   new Pen(
                       borderColor,
                       1))
            {
                Rectangle rect =
                    new Rectangle(
                        0,
                        0,
                        Width - 1,
                        Height - 1);

                using (GraphicsPath path =
                       CreateRoundedPath(
                           rect,
                           borderRadius))
                {
                    e.Graphics.DrawPath(
                        pen,
                        path);
                }
            }
        }
    }
}