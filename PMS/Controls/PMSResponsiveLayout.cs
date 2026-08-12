using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PMS.Controls
{
    public class PMSResponsiveLayout
    {
        private class ControlInfo
        {
            public Control Control { get; set; }

            public Rectangle Bounds { get; set; }

            public float FontSize { get; set; }

            public FontStyle FontStyle { get; set; }

            public List<ColumnInfo> Columns { get; set; } =
                new List<ColumnInfo>();
        }

        private class ColumnInfo
        {
            public DataGridViewColumn Column { get; set; }

            public int Width { get; set; }
        }

        private Control root;

        private Size designSize;

        private PointF designCenter;

        private List<ControlInfo> controls =
            new List<ControlInfo>();

        private bool isReady;

        private bool scaleFont = true;

        private float minimumScale = 0.5f;

        private float maximumScale = 2.0f;

        // =========================================================
        // Constructor
        // =========================================================

        public PMSResponsiveLayout(
            Control root,
            Size designSize)
        {
            this.root = root;

            this.designSize = designSize;

            designCenter = new PointF(
                designSize.Width / 2f,
                designSize.Height / 2f);

            CaptureControls(root);

            root.Resize += Root_Resize;

            isReady = true;

            Apply();
        }

        // =========================================================
        // Settings
        // =========================================================

        public bool ScaleFont
        {
            get => scaleFont;

            set
            {
                scaleFont = value;

                Apply();
            }
        }

        public float MinimumScale
        {
            get => minimumScale;

            set
            {
                minimumScale =
                    Math.Max(0.1f, value);

                Apply();
            }
        }

        public float MaximumScale
        {
            get => maximumScale;

            set
            {
                maximumScale =
                    Math.Max(
                        minimumScale,
                        value);

                Apply();
            }
        }

        // =========================================================
        // Capture
        // =========================================================

        private void CaptureControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                ControlInfo info =
                    new ControlInfo();

                info.Control = control;

                info.Bounds =
                    control.Bounds;

                info.FontSize =
                    control.Font.Size;

                info.FontStyle =
                    control.Font.Style;

                if (control is DataGridView dgv)
                {
                    foreach (
                        DataGridViewColumn column
                        in dgv.Columns)
                    {
                        info.Columns.Add(
                            new ColumnInfo
                            {
                                Column = column,
                                Width = column.Width
                            });
                    }
                }

                controls.Add(info);

                if (control.HasChildren)
                {
                    CaptureControls(control);
                }
            }
        }

        // =========================================================
        // Resize Event
        // =========================================================

        private void Root_Resize(
            object sender,
            EventArgs e)
        {
            if (!isReady)
                return;

            Apply();
        }

        // =========================================================
        // Apply Layout
        // =========================================================

        public void Apply()
        {
            if (root == null)
                return;

            if (designSize.Width <= 0 ||
                designSize.Height <= 0)
                return;

            float scaleX =
                root.ClientSize.Width /
                (float)designSize.Width;

            float scaleY =
                root.ClientSize.Height /
                (float)designSize.Height;

            // ใช้ Uniform Scale
            // เพื่อไม่ให้ปุ่ม / TextBox / รูปภาพเบี้ยว
            float scale =
                Math.Min(
                    scaleX,
                    scaleY);

            scale =
                Math.Max(
                    minimumScale,
                    Math.Min(
                        maximumScale,
                        scale));

            // =====================================================
            // Center ของหน้าจอปัจจุบัน
            // =====================================================

            PointF currentCenter =
                new PointF(
                    root.ClientSize.Width / 2f,
                    root.ClientSize.Height / 2f);

            foreach (ControlInfo info in controls)
            {
                if (info.Control.IsDisposed)
                    continue;

                Rectangle original =
                    info.Bounds;

                // =================================================
                // หา Center ของ Control
                // =================================================

                float originalControlCenterX =
                    original.X +
                    original.Width / 2f;

                float originalControlCenterY =
                    original.Y +
                    original.Height / 2f;

                // =================================================
                // ระยะจาก Center ของ Form
                // =================================================

                float offsetX =
                    originalControlCenterX -
                    designCenter.X;

                float offsetY =
                    originalControlCenterY -
                    designCenter.Y;

                // =================================================
                // Scale ระยะจาก Center
                // =================================================

                float newCenterX =
                    currentCenter.X +
                    offsetX * scale;

                float newCenterY =
                    currentCenter.Y +
                    offsetY * scale;

                // =================================================
                // Scale Size
                // =================================================

                int newWidth =
                    Math.Max(
                        1,
                        (int)Math.Round(
                            original.Width *
                            scale));

                int newHeight =
                    Math.Max(
                        1,
                        (int)Math.Round(
                            original.Height *
                            scale));

                int newX =
                    (int)Math.Round(
                        newCenterX -
                        newWidth / 2f);

                int newY =
                    (int)Math.Round(
                        newCenterY -
                        newHeight / 2f);

                info.Control.Bounds =
                    new Rectangle(
                        newX,
                        newY,
                        newWidth,
                        newHeight);

                // =================================================
                // Font
                // =================================================

                if (scaleFont)
                {
                    float newFontSize =
                        info.FontSize *
                        scale;

                    newFontSize =
                        Math.Max(
                            6f,
                            newFontSize);

                    info.Control.Font =
                        new Font(
                            info.Control.Font.FontFamily,
                            newFontSize,
                            info.FontStyle);
                }

                // =================================================
                // DataGridView
                // =================================================

                if (info.Control
                    is DataGridView dgv)
                {
                    foreach (
                        ColumnInfo column
                        in info.Columns)
                    {
                        if (column.Column == null)
                            continue;

                        column.Column.Width =
                            Math.Max(
                                20,
                                (int)Math.Round(
                                    column.Width *
                                    scale));
                    }
                }
            }
        }

        // =========================================================
        // Refresh Design
        // =========================================================

        public void ReCapture()
        {
            controls.Clear();

            CaptureControls(root);

            Apply();
        }

        // =========================================================
        // Dispose
        // =========================================================

        public void Dispose()
        {
            if (root != null)
            {
                root.Resize -= Root_Resize;
            }

            controls.Clear();

            root = null;
        }
    }
}