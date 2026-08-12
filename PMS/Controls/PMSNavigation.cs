using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PMS.Controls
{
    public partial class PMSNavigation : UserControl
    {
        // =========================================================
        // PAGE DEFINITION
        // =========================================================

        public class PMSPage
        {
            public string Text { get; set; } = string.Empty;
            public Image Icon { get; set; } = null!;
            public Func<Form> CreateForm { get; set; } = null!;

            public PMSPage(string text, Image icon, Func<Form> createForm)
            {
                Text = text;
                Icon = icon;
                CreateForm = createForm;
            }
        }

        // =========================================================
        // COLORS
        // =========================================================

        private Color navigationColor = Color.FromArgb(20, 21, 26);
        private Color hoverColor = Color.FromArgb(31, 33, 40);
        private Color selectedColor = Color.FromArgb(38, 41, 51);
        private Color subMenuColor = Color.FromArgb(25, 26, 32);
        private Color accentColor = Color.FromArgb(75, 110, 255);
        private Color textColor = Color.FromArgb(235, 236, 240);
        private Color secondaryTextColor = Color.FromArgb(155, 158, 168);
        private Color separatorColor = Color.FromArgb(48, 50, 58);
        private Color dangerColor = Color.FromArgb(235, 90, 90);

        [Category("PMS Theme")]
        [Description("สีพื้นหลัง Navigation")]
        public Color NavigationColor
        {
            get => navigationColor;
            set
            {
                navigationColor = value;
                ApplyTheme();
            }
        }

        [Category("PMS Theme")]
        [Description("สีเมื่อ Mouse Hover")]
        public Color HoverColor
        {
            get => hoverColor;
            set => hoverColor = value;
        }

        [Category("PMS Theme")]
        [Description("สีเมนูที่ถูกเลือก")]
        public Color SelectedColor
        {
            get => selectedColor;
            set => selectedColor = value;
        }

        [Category("PMS Theme")]
        [Description("สีพื้นหลัง Submenu")]
        public Color SubMenuColor
        {
            get => subMenuColor;
            set => subMenuColor = value;
        }

        [Category("PMS Theme")]
        [Description("สีหลักของระบบ")]
        public Color AccentColor
        {
            get => accentColor;
            set
            {
                accentColor = value;
                if (btnNotification != null)
                {
                    btnNotification.ForeColor = accentColor;
                }
                Refresh();
            }
        }

        [Category("PMS Theme")]
        [Description("สีตัวอักษรหลัก")]
        public Color TextColor
        {
            get => textColor;
            set
            {
                textColor = value;
                ApplyTheme();
            }
        }

        [Category("PMS Theme")]
        [Description("สีตัวอักษรรอง")]
        public Color SecondaryTextColor
        {
            get => secondaryTextColor;
            set => secondaryTextColor = value;
        }

        // =========================================================
        // SIZE
        // =========================================================

        private int sidebarWidth = 270;
        private int menuHeight = 52;
        private int subMenuHeight = 44;
        private float menuFontSize = 11F;
        private float subMenuFontSize = 10F;

        [Category("PMS Layout")]
        [Description("ความกว้าง Sidebar")]
        public int SidebarWidth
        {
            get => sidebarWidth;
            set
            {
                sidebarWidth = Math.Max(220, value);
                Width = sidebarWidth;
                RefreshLayout();
            }
        }

        [Category("PMS Layout")]
        [Description("ความสูงของเมนูหลัก")]
        public int MenuHeight
        {
            get => menuHeight;
            set
            {
                menuHeight = Math.Max(40, value);
                RefreshLayout();
            }
        }

        [Category("PMS Layout")]
        [Description("ขนาด Font เมนูหลัก")]
        public float MenuFontSize
        {
            get => menuFontSize;
            set
            {
                menuFontSize = Math.Max(8F, value);
                RefreshLayout();
            }
        }

        [Category("PMS Layout")]
        [Description("ขนาด Font Submenu")]
        public float SubMenuFontSize
        {
            get => subMenuFontSize;
            set
            {
                subMenuFontSize = Math.Max(8F, value);
                RefreshLayout();
            }
        }

        // =========================================================
        // CONTROLS
        // =========================================================

        private Panel headerPanel = null!;
        private Panel menuPanel = null!;
        private Panel bottomPanel = null!;
        private Label lblLogo = null!;
        private Label lblSubtitle = null!;
        private Button btnNotification = null!;
        private Label lblNotification = null!;
        private Button btnUser = null!;
        private Button btnLogout = null!;
        private Button btnExit = null!;
        private Button selectedButton = null!;

        // =========================================================
        // CURRENT USER
        // =========================================================

        private string currentUser = "ผู้ใช้งาน";

        [Category("PMS User")]
        [Description("ชื่อผู้ใช้งานปัจจุบัน")]
        public string CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = string.IsNullOrWhiteSpace(value) ? "ผู้ใช้งาน" : value;
                if (btnUser != null)
                {
                    btnUser.Text = "●    " + currentUser;
                }
            }
        }

        // =========================================================
        // NOTIFICATION
        // =========================================================

        private int notificationCount;

        [Category("PMS Notification")]
        [Description("จำนวน Notification")]
        public int NotificationCount
        {
            get => notificationCount;
            set
            {
                notificationCount = Math.Max(0, value);
                UpdateNotification();
            }
        }

        // =========================================================
        // EVENTS
        // =========================================================

        public event EventHandler LogoutClicked = delegate { };
        public event EventHandler NotificationClicked = delegate { };

        // =========================================================
        // CONSTRUCTOR & INITIALIZE
        // =========================================================

        public PMSNavigation()
        {
            InitializeNavigation();
        }

        private void InitializeNavigation()
        {
            Dock = DockStyle.Left;
            Width = sidebarWidth;
            BackColor = navigationColor;
            DoubleBuffered = true;

            CreateHeader();
            CreateBottomPanel();
            CreateMenuPanel();

            // ส่ง Header และ Bottom ไปหลังสุด เพื่อให้ Dock.Top และ Dock.Bottom จองพื้นที่ขอบก่อน
            headerPanel.SendToBack();
            bottomPanel.SendToBack();

            // ดึง menuPanel ขึ้นหน้าสุด เพื่อให้ Dock.Fill เติมเต็มเฉพาะพื้นที่ที่เหลือตรงกลาง
            menuPanel.BringToFront();

            UpdateNotification();
        }

        // =========================================================
        // HEADER
        // =========================================================

        private void CreateHeader()
        {
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = navigationColor
            };
            Controls.Add(headerPanel);

            // MacWindowButtons (วางไว้มุมซ้ายบน)
            MacWindowButtons macButtons = new MacWindowButtons
            {
                Location = new Point(10, 6),
                Size = new Size(440, 30)
            };
            headerPanel.Controls.Add(macButtons);

            // LOGO
            lblLogo = new Label
            {
                AutoSize = false,
                Text = "PMS",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = textColor,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(12, 34),
                Size = new Size(140, 30)
            };
            headerPanel.Controls.Add(lblLogo);

            // SUBTITLE
            lblSubtitle = new Label
            {
                AutoSize = false,
                Text = "Pawn Management System",
                Font = new Font("Segoe UI", 10F),
                ForeColor = secondaryTextColor,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(12, 64),
                Size = new Size(180, 22)
            };
            headerPanel.Controls.Add(lblSubtitle);

            // NOTIFICATION BUTTON
            btnNotification = new Button
            {
                Size = new Size(40, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(sidebarWidth - 45, 50),
                Text = "♢",
                Font = new Font("Segoe UI Symbol", 18F, FontStyle.Bold),
                ForeColor = accentColor,
                BackColor = Color.FromArgb(27, 29, 36),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnNotification.FlatAppearance.BorderSize = 0;
            btnNotification.Click += BtnNotification_Click;
            headerPanel.Controls.Add(btnNotification);

            // NOTIFICATION BADGE
            lblNotification = new Label
            {
                AutoSize = false,
                Size = new Size(22, 22),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(235, 90, 90),
                Visible = false
            };
            lblNotification.BringToFront();
            headerPanel.Controls.Add(lblNotification);

            PositionNotification();
        }

        private void BtnNotification_Click(object sender, EventArgs e)
        {
            NotificationClicked(this, EventArgs.Empty);
        }

        // =========================================================
        // MENU PANEL
        // =========================================================

        private void CreateMenuPanel()
        {
            menuPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = navigationColor,
                Padding = new Padding(8)
            };

            Controls.Add(menuPanel);
            menuPanel.BringToFront();
        }

        // =========================================================
        // ADD PAGE & CATEGORY
        // =========================================================

        public void AddPage(string text, Image icon, Func<Form> createForm)
        {
            AddPage(new PMSPage(text, icon, createForm));
        }

        public void AddPage(PMSPage page)
        {
            if (page == null) return;

            // ส่ง false เพื่อไม่ให้แสดงลูกศร >
            Button button = CreateMenuButton(page.Text, page.Icon, hasSubmenu: false);
            button.Tag = page;
            button.Click += PageButton_Click;
            button.Dock = DockStyle.Top;

            menuPanel.Controls.Add(button);
            button.BringToFront();
        }

        public void AddCategory(string text, Image icon, params PMSPage[] pages)
        {
            if (pages == null) pages = Array.Empty<PMSPage>();

            Panel category = new Panel
            {
                Dock = DockStyle.Top,
                Height = menuHeight,
                BackColor = navigationColor
            };

            menuPanel.Controls.Add(category);
            category.BringToFront();

            // ส่ง true เพื่อให้แสดงลูกศร › สำหรับ Category
            Button categoryButton = CreateMenuButton(text, icon, hasSubmenu: true);
            categoryButton.Dock = DockStyle.None;
            categoryButton.Location = new Point(0, 0);
            categoryButton.Size = new Size(Math.Max(100, menuPanel.ClientSize.Width - 16), menuHeight);
            category.Controls.Add(categoryButton);

            // CHILD PANEL
            Panel childPanel = new Panel
            {
                Dock = DockStyle.None,
                Location = new Point(0, menuHeight),
                Size = new Size(Math.Max(100, menuPanel.ClientSize.Width - 16), 0),
                Visible = false,
                BackColor = subMenuColor
            };
            category.Controls.Add(childPanel);

            // CHILDREN
            for (int i = 0; i < pages.Length; i++)
            {
                PMSPage page = pages[i];
                if (page == null) continue;

                Button child = CreateChildButton(page.Text, page.Icon);
                child.Tag = page;
                child.Click += PageButton_Click;

                childPanel.Controls.Add(child);
                child.BringToFront();
            }

            // CATEGORY CLICK
            categoryButton.Click += (sender, e) =>
            {
                bool expand = !childPanel.Visible;
                childPanel.Visible = expand;

                int childHeight = pages.Length * subMenuHeight;
                childPanel.Height = expand ? childHeight : 0;
                category.Height = menuHeight + childPanel.Height;

                categoryButton.Text = BuildMenuText(text, expand);

                category.PerformLayout();
                menuPanel.PerformLayout();
            };

            category.Resize += (sender, e) =>
            {
                int width = Math.Max(100, category.ClientSize.Width);
                categoryButton.Size = new Size(width, menuHeight);
                childPanel.Location = new Point(0, menuHeight);
                childPanel.Width = width;
            };
        }

        // =========================================================
        // PAGE CLICK & FORM MANAGEMENT
        // =========================================================

        private void PageButton_Click(object sender, EventArgs e)
        {
            if (sender is not Button button || button.Tag is not PMSPage page)
                return;

            SelectButton(button);

            if (page.CreateForm == null)
                return;

            Form form = page.CreateForm();
            if (form == null)
                return;

            OpenForm(form);
        }

        private void OpenForm(Form form)
        {
            Form parent = FindForm();
            if (parent == null)
            {
                form.Show();
                return;
            }

            // 1. ปิด Form ลูกเดิมที่เปิดอยู่ก่อนหน้า
            if (parent.IsMdiContainer)
            {
                foreach (Form child in parent.MdiChildren)
                {
                    child.Close();
                }

                form.MdiParent = parent;
                form.FormBorderStyle = FormBorderStyle.None;
                form.ControlBox = false;
                form.Text = string.Empty; // ล้างข้อความเพื่อป้องกันระบบ MDI ดึงไปสร้าง Title Bar
                form.Dock = DockStyle.Fill;

                // **จุดสำคัญ:** ใช้ Normal ร่วมกับ Dock.Fill แทน Maximized 
                // จะขยายฟอร์มเต็มพื้นที่โดยไม่กระตุ้นปุ่ม _ ❐ X ให้ลอยไปมุมขวาบน
                form.WindowState = FormWindowState.Normal;

                form.Show();
                return;
            }

            // 2. หากไม่ได้เปิด IsMdiContainer ให้ค้นหา Panel ใดก็ได้ในหน้าหลักอัตโนมัติ
            Panel targetPanel = null;
            foreach (Control c in parent.Controls)
            {
                if (c is Panel p && c != this)
                {
                    targetPanel = p;
                    break;
                }
            }

            if (targetPanel != null)
            {
                targetPanel.Controls.Clear();
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                targetPanel.Controls.Add(form);
                form.Show();
            }
            else
            {
                form.Show(parent);
            }
        }

        // =========================================================
        // BUTTON CREATION HELPERS
        // =========================================================

        private Button CreateMenuButton(string text, Image icon, bool hasSubmenu = false)
        {
            if (string.IsNullOrWhiteSpace(text))
                text = "เมนู";

            Button button = new Button
            {
                Height = menuHeight,
                Width = sidebarWidth - 20,
                FlatStyle = FlatStyle.Flat,
                BackColor = navigationColor,
                ForeColor = textColor,
                Font = new Font("Segoe UI", menuFontSize, FontStyle.Bold),
                // ถ้ามี Submenu ให้ใช้ BuildMenuText (ใส่ลูกศร) ถ้าไม่มีให้ใส่แค่เว้นวรรค
                Text = hasSubmenu ? BuildMenuText(text, false) : "        " + text,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(14, 0, 10, 0),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            button.FlatAppearance.BorderSize = 0;

            if (icon != null)
            {
                button.Image = ResizeIcon(icon, 22, 22);
                button.ImageAlign = ContentAlignment.MiddleLeft;
                button.TextImageRelation = TextImageRelation.ImageBeforeText;
                button.Padding = new Padding(14, 0, 10, 0);
            }

            AddHoverEvents(button);
            return button;
        }

        private Button CreateChildButton(string text, Image icon)
        {
            Button button = new Button
            {
                Height = subMenuHeight,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                BackColor = subMenuColor,
                ForeColor = secondaryTextColor,
                Font = new Font("Segoe UI", subMenuFontSize),
                Text = "        " + text,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 10, 0),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            button.FlatAppearance.BorderSize = 0;

            if (icon != null)
            {
                button.Image = ResizeIcon(icon, 18, 18);
                button.ImageAlign = ContentAlignment.MiddleLeft;
                button.TextImageRelation = TextImageRelation.ImageBeforeText;
                button.Padding = new Padding(32, 0, 10, 0);
            }

            AddHoverEvents(button);
            return button;
        }

        private Image ResizeIcon(Image source, int width, int height)
        {
            if (source == null) return null!;
            return new Bitmap(source, new Size(width, height));
        }

        private void AddHoverEvents(Button button)
        {
            button.MouseEnter += (sender, e) =>
            {
                if (button != selectedButton)
                {
                    button.BackColor = hoverColor;
                }
            };

            button.MouseLeave += (sender, e) =>
            {
                if (button != selectedButton)
                {
                    button.BackColor = (button.Parent != null && button.Parent.BackColor == subMenuColor)
                        ? subMenuColor
                        : navigationColor;
                }
            };
        }

        // =========================================================
        // SELECT
        // =========================================================

        private void SelectButton(Button button)
        {
            if (selectedButton != null)
            {
                selectedButton.BackColor = (selectedButton.Parent != null && selectedButton.Parent.BackColor == subMenuColor)
                    ? subMenuColor
                    : navigationColor;
            }

            selectedButton = button;
            selectedButton.BackColor = selectedColor;
            selectedButton.ForeColor = textColor;
            selectedButton.Invalidate();
        }

        // =========================================================
        // TEXT BUILDER
        // =========================================================

        private string BuildMenuText(string text, bool expanded)
        {
            if (string.IsNullOrWhiteSpace(text))
                text = "เมนู";

            return "        " + text + (expanded ? "                 ˅" : "                 ›");
        }

        // =========================================================
        // USER AREA & BOTTOM PANEL
        // =========================================================

        private void CreateBottomPanel()
        {
            bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 120,
                BackColor = navigationColor
            };

            Controls.Add(bottomPanel);
            bottomPanel.BringToFront();

            // SEPARATOR
            Panel separator = new Panel
            {
                Dock = DockStyle.Top,
                Height = 1,
                BackColor = separatorColor
            };
            bottomPanel.Controls.Add(separator);

            // EXIT
            btnExit = CreateBottomButton("⏻    ปิดโปรแกรม");
            btnExit.Dock = DockStyle.Bottom;
            btnExit.Height = 34;
            btnExit.ForeColor = dangerColor;
            btnExit.Click += BtnExit_Click;
            bottomPanel.Controls.Add(btnExit);

            // LOGOUT
            btnLogout = CreateBottomButton("⎋    ออกจากระบบ");
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.Height = 34;
            btnLogout.Click += BtnLogout_Click;
            bottomPanel.Controls.Add(btnLogout);

            // USER
            btnUser = CreateBottomButton("●    " + currentUser);
            btnUser.Dock = DockStyle.Bottom;
            btnUser.Height = 42;
            btnUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUser.ForeColor = textColor;
            btnUser.Cursor = Cursors.Default;
            bottomPanel.Controls.Add(btnUser);
        }

        private Button CreateBottomButton(string text)
        {
            Button button = new Button
            {
                Text = "   " + text,
                TextAlign = ContentAlignment.MiddleLeft,
                FlatStyle = FlatStyle.Flat,
                BackColor = navigationColor,
                ForeColor = secondaryTextColor,
                Font = new Font("Segoe UI", 9.5F),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            button.FlatAppearance.BorderSize = 0;

            button.MouseEnter += (s, e) =>
            {
                button.BackColor = hoverColor;
                if (button != btnLogout && button != btnExit)
                {
                    button.ForeColor = textColor;
                }
            };

            button.MouseLeave += (s, e) =>
            {
                button.BackColor = navigationColor;
                if (button == btnExit)
                {
                    button.ForeColor = dangerColor;
                }
                else if (button == btnUser)
                {
                    button.ForeColor = textColor;
                }
                else
                {
                    button.ForeColor = secondaryTextColor;
                }
            };

            return button;
        }

        // =========================================================
        // LOGOUT & EXIT
        // =========================================================

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (LogoutClicked != null)
            {
                LogoutClicked(this, EventArgs.Empty);
                return;
            }

            DialogResult result = PMSMessageBox.Show(
                "คุณต้องการออกจากระบบหรือไม่?",
                "ออกจากระบบ",
                MessageBoxButtons.YesNo,
                PMSMessageBox.PMSMessageIcon.Question);

            if (result == DialogResult.Yes)
            {
                Form parent = FindForm();
                parent?.Close();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = PMSMessageBox.Show(
                "คุณต้องการปิดโปรแกรมหรือไม่?",
                "ปิดโปรแกรม",
                MessageBoxButtons.YesNo,
                PMSMessageBox.PMSMessageIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // =========================================================
        // NOTIFICATION
        // =========================================================

        private void UpdateNotification()
        {
            if (lblNotification == null)
                return;

            if (notificationCount <= 0)
            {
                lblNotification.Visible = false;
                return;
            }

            lblNotification.Visible = true;
            lblNotification.Text = notificationCount > 99 ? "99+" : notificationCount.ToString();
            PositionNotification();
        }

        private void PositionNotification()
        {
            if (btnNotification == null || lblNotification == null)
                return;

            lblNotification.Location = new Point(btnNotification.Right - 12, btnNotification.Top - 2);
            lblNotification.BringToFront();
        }

        // =========================================================
        // THEME & LAYOUT REFRESH
        // =========================================================

        private void ApplyTheme()
        {
            BackColor = navigationColor;

            if (headerPanel != null) headerPanel.BackColor = navigationColor;
            if (menuPanel != null) menuPanel.BackColor = navigationColor;
            if (bottomPanel != null) bottomPanel.BackColor = navigationColor;
            if (lblLogo != null) lblLogo.ForeColor = textColor;
            if (lblSubtitle != null) lblSubtitle.ForeColor = secondaryTextColor;
            if (btnNotification != null) btnNotification.ForeColor = accentColor;
            if (btnUser != null) btnUser.ForeColor = textColor;
            if (btnExit != null) btnExit.ForeColor = dangerColor;

            Refresh();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (headerPanel == null)
                return;

            if (btnNotification != null)
            {
                btnNotification.Location = new Point(
                    headerPanel.ClientSize.Width - btnNotification.Width - 16,
                    btnNotification.Height - 2);

                PositionNotification();
            }

            ResizeCategoryControls();
        }

        private void RefreshLayout()
        {
            if (menuPanel == null)
                return;

            foreach (Control control in menuPanel.Controls)
            {
                if (control is Button button)
                {
                    button.Width = Math.Max(100, menuPanel.ClientSize.Width - 16);
                    button.Height = menuHeight;
                    button.Font = new Font("Segoe UI", menuFontSize);
                }

                if (control is Panel category)
                {
                    category.Width = Math.Max(100, menuPanel.ClientSize.Width - 16);

                    foreach (Control child in category.Controls)
                    {
                        if (child is Button categoryButton)
                        {
                            categoryButton.Width = category.ClientSize.Width;
                            categoryButton.Height = menuHeight;
                            categoryButton.Font = new Font("Segoe UI", menuFontSize);
                        }

                        if (child is Panel childPanel)
                        {
                            childPanel.Width = category.ClientSize.Width;

                            foreach (Control subChild in childPanel.Controls)
                            {
                                if (subChild is Button subButton)
                                {
                                    subButton.Height = subMenuHeight;
                                    subButton.Font = new Font("Segoe UI", subMenuFontSize);
                                }
                            }
                        }
                    }
                }
            }

            PositionNotification();
            menuPanel.PerformLayout();
            menuPanel.Invalidate();
        }

        private void ResizeCategoryControls()
        {
            if (menuPanel == null)
                return;

            int width = Math.Max(100, menuPanel.ClientSize.Width - 16);

            foreach (Control control in menuPanel.Controls)
            {
                if (control is not Panel category)
                    continue;

                category.Width = width;

                foreach (Control child in category.Controls)
                {
                    if (child is Button categoryButton)
                    {
                        categoryButton.Location = Point.Empty;
                        categoryButton.Width = width;
                        categoryButton.Height = menuHeight;
                    }

                    if (child is Panel childPanel)
                    {
                        childPanel.Width = width;
                        childPanel.Location = new Point(0, menuHeight);
                    }
                }
            }
        }

        // =========================================================
        // HELPERS
        // =========================================================

        public PMSPage Page(string text, Image icon, Func<Form> createForm)
        {
            return new PMSPage(text, icon, createForm);
        }

        public Image ConvertByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return null!;
            }

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                using (Image temp = Image.FromStream(ms))
                {
                    return new Bitmap(temp);
                }
            }
        }
    }
}