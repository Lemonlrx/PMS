using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace PMS.Controls
{
    public partial class PMSNavigation : UserControl
    {
        // =========================================================
        // MENU ITEM
        // =========================================================
        public class PMSMenuItem
        {
            public string Text { get; set; } = string.Empty;
            public Image Icon { get; set; } = null!;
            public Func<Form> CreateForm { get; set; } = null!;
            public List<PMSMenuItem> Children { get; set; } = new();

            public bool IsCategory =>
                Children != null && Children.Count > 0;

            public PMSMenuItem() { }

            public PMSMenuItem(string text, Image icon, Func<Form> createForm)
            {
                Text = text;
                Icon = icon;
                CreateForm = createForm;
            }

            public PMSMenuItem(string text, Image icon, params PMSMenuItem[] children)
            {
                Text = text;
                Icon = icon;
                if (children != null)
                    Children.AddRange(children);
            }
        }

        // =========================================================
        // BACKWARD COMPATIBILITY
        // =========================================================
        public class PMSPage : PMSMenuItem
        {
            public PMSPage(string text, Image icon, Func<Form> createForm)
                : base(text, icon, createForm) { }
        }

        // =========================================================
        // CONSTANTS
        // =========================================================
        // Extra transparent space (px) painted to the right of every menu
        // icon so the label text never sits flush against the icon.
        private const int IconTextGap = 10;

        // Icon square sizes for top level / sub level menu items.
        private const int MainIconSize = 24;
        private const int SubIconSize = 21;

        // Width reserved on the right of menu items for the vertical
        // scrollbar so content never fights it for space (see GetMenuWidth).
        private static readonly int ScrollBarReserve = SystemInformation.VerticalScrollBarWidth + 2;

        // =========================================================
        // THEME
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
        public Color NavigationColor
        {
            get => navigationColor;
            set { navigationColor = value; ApplyTheme(); }
        }

        [Category("PMS Theme")]
        public Color HoverColor
        {
            get => hoverColor;
            set => hoverColor = value;
        }

        [Category("PMS Theme")]
        public Color SelectedColor
        {
            get => selectedColor;
            set => selectedColor = value;
        }

        [Category("PMS Theme")]
        public Color SubMenuColor
        {
            get => subMenuColor;
            set => subMenuColor = value;
        }

        [Category("PMS Theme")]
        public Color AccentColor
        {
            get => accentColor;
            set
            {
                accentColor = value;
                if (btnNotification != null)
                    btnNotification.ForeColor = value;
                Invalidate();
            }
        }

        [Category("PMS Theme")]
        public Color TextColor
        {
            get => textColor;
            set { textColor = value; ApplyTheme(); }
        }

        [Category("PMS Theme")]
        public Color SecondaryTextColor
        {
            get => secondaryTextColor;
            set => secondaryTextColor = value;
        }

        // =========================================================
        // LAYOUT
        // =========================================================
        private int sidebarWidth = 270;
        private int menuHeight = 56;
        private int subMenuHeight = 48;
        private float menuFontSize = 13.5F;
        private float subMenuFontSize = 12F;

        [Category("PMS Layout")]
        public int SidebarWidth
        {
            get => sidebarWidth;
            set
            {
                sidebarWidth = Math.Max(220, value);
                Width = sidebarWidth;
                RefreshMenuLayout();
            }
        }

        [Category("PMS Layout")]
        public int MenuHeight
        {
            get => menuHeight;
            set
            {
                menuHeight = Math.Max(40, value);
                RefreshMenuLayout();
            }
        }

        [Category("PMS Layout")]
        public float MenuFontSize
        {
            get => menuFontSize;
            set
            {
                menuFontSize = Math.Max(8F, value);
                RefreshMenuLayout();
            }
        }

        [Category("PMS Layout")]
        public float SubMenuFontSize
        {
            get => subMenuFontSize;
            set
            {
                subMenuFontSize = Math.Max(8F, value);
                RefreshMenuLayout();
            }
        }

        // =========================================================
        // CONTROLS
        // =========================================================
        private Panel headerPanel = null!;
        private FlowLayoutPanel menuPanel = null!;
        private Panel bottomPanel = null!;
        private Label lblLogo = null!;
        private Label lblSubtitle = null!;
        private Button btnNotification = null!;
        private Label lblNotification = null!;
        private Button btnUser = null!;
        private Button btnLogout = null!;
        private Button btnExit = null!;
        private Button selectedButton = null!;

        private readonly Dictionary<Button, int> buttonLevels = new();

        // =========================================================
        // USER
        // =========================================================
        private string currentUser = "ผู้ใช้งาน";

        [Category("PMS User")]
        public string CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = string.IsNullOrWhiteSpace(value) ? "ผู้ใช้งาน" : value;
                if (btnUser != null)
                    btnUser.Text = "●    " + currentUser;
            }
        }

        // =========================================================
        // NOTIFICATION
        // =========================================================
        private int notificationCount;

        [Category("PMS Notification")]
        public int NotificationCount
        {
            get => notificationCount;
            set
            {
                notificationCount = Math.Max(0, value);
                UpdateNotification();
            }
        }

        public event EventHandler LogoutClicked = delegate { };
        public event EventHandler NotificationClicked = delegate { };

        // =========================================================
        // CONSTRUCTOR
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

            // Double buffering on the control (and its scrollable menu
            // panel) is what removes the flicker/"jumping" feeling when
            // buttons repaint or the panel relayouts.
            DoubleBuffered = true;
            SetDoubleBuffered(this);

            CreateHeader();
            CreateBottomPanel();
            CreateMenuPanel();

            headerPanel.SendToBack();
            bottomPanel.SendToBack();
            menuPanel.BringToFront();

            UpdateNotification();
        }

        /// <summary>
        /// Enables double buffering on controls (like FlowLayoutPanel) that
        /// don't expose the DoubleBuffered property publicly.
        /// </summary>
        private static void SetDoubleBuffered(Control control)
        {
            if (SystemInformation.TerminalServerSession)
                return;

            typeof(Control)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(control, true, null);
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

            MacWindowButtons macButtons = new MacWindowButtons
            {
                Location = new Point(10, 6),
                Size = new Size(500, 30)
            };
            headerPanel.Controls.Add(macButtons);

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

            lblSubtitle = new Label
            {
                AutoSize = false,
                Text = "Pawn Management System",
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = secondaryTextColor,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(12, 64),
                Size = new Size(210, 22)
            };
            headerPanel.Controls.Add(lblSubtitle);

            btnNotification = new Button
            {
                Size = new Size(40, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(sidebarWidth - 55, 48),
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
            menuPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = false,
                AutoScroll = true,
                BackColor = navigationColor,
                Padding = new Padding(8, 8, 8, 12)
            };

            Controls.Add(menuPanel);
            SetDoubleBuffered(menuPanel);
            menuPanel.BringToFront();

            // Only vertical scrolling is ever wanted here; re-assert this
            // on every layout pass (see RefreshMenuLayout) so it can't
            // creep back on.
            menuPanel.HorizontalScroll.Maximum = 0;
            menuPanel.HorizontalScroll.Visible = false;
        }

        // =========================================================
        // PAGE / CATEGORY HELPERS
        // =========================================================
        public PMSPage Page(string text, Image icon, Func<Form> createForm)
        {
            return new PMSPage(text, icon, createForm);
        }

        public PMSMenuItem Category(string text, Image icon, params PMSMenuItem[] children)
        {
            return new PMSMenuItem(text, icon, children);
        }

        // =========================================================
        // ADD PAGE
        // =========================================================
        public void AddPage(string text, Image icon, Func<Form> createForm)
        {
            AddPage(new PMSPage(text, icon, createForm));
        }

        public void AddPage(PMSMenuItem page)
        {
            if (page == null)
                return;

            RenderMenuItem(menuPanel, page, 0);
        }

        // =========================================================
        // ADD CATEGORY
        // =========================================================
        public void AddCategory(string text, Image icon, params PMSMenuItem[] children)
        {
            AddCategory(new PMSMenuItem(text, icon, children));
        }

        public void AddCategory(PMSMenuItem category)
        {
            if (category == null)
                return;

            RenderMenuItem(menuPanel, category, 0);
        }

        // =========================================================
        // RENDER MENU
        // =========================================================
        private void RenderMenuItem(FlowLayoutPanel parent, PMSMenuItem item, int level)
        {
            if (item == null)
                return;

            if (!item.IsCategory)
            {
                Button pageButton = CreatePageButton(item, level);
                pageButton.Tag = item;
                pageButton.Click += PageButton_Click;

                AddToPanel(parent, pageButton);
                return;
            }

            RenderCategory(parent, item, level);
        }

        /// <summary>
        /// Builds a collapsible category: a header button plus a child
        /// panel that is shown/hidden on click.
        /// </summary>
        private void RenderCategory(FlowLayoutPanel parent, PMSMenuItem item, int level)
        {
            FlowLayoutPanel category = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = level == 0 ? navigationColor : subMenuColor,
                Margin = new Padding(0, 0, 0, 4),
                Padding = new Padding(level * 8, 0, 0, 0),
                Tag = item
            };

            category.Width = GetMenuWidth(parent);

            parent.Controls.Add(category);

            Button categoryButton = CreateCategoryButton(item, level);
            category.Controls.Add(categoryButton);

            FlowLayoutPanel childPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = subMenuColor,
                Visible = false,
                Margin = new Padding(0),
                Padding = new Padding(6, 2, 0, 4)
            };

            childPanel.Width = Math.Max(100, category.Width - 8);
            category.Controls.Add(childPanel);

            foreach (PMSMenuItem child in item.Children)
                RenderMenuItem(childPanel, child, level + 1);

            categoryButton.Click += (sender, e) =>
                ToggleCategory(item, categoryButton, category, childPanel, parent);

            category.Resize += (sender, e) =>
            {
                categoryButton.Width = category.ClientSize.Width;
                childPanel.Width = Math.Max(100, category.ClientSize.Width - 8);
            };
        }

        /// <summary>
        /// Expands/collapses a category's child panel while keeping the
        /// sidebar's scroll position fixed, so the whole menu doesn't
        /// visibly jump up or down when you click a category.
        /// </summary>
        private void ToggleCategory(
            PMSMenuItem item,
            Button categoryButton,
            FlowLayoutPanel category,
            FlowLayoutPanel childPanel,
            FlowLayoutPanel parent)
        {
            Point scrollPosition = menuPanel.AutoScrollPosition;

            menuPanel.SuspendLayout();
            parent.SuspendLayout();
            category.SuspendLayout();

            bool expand = !childPanel.Visible;
            childPanel.Visible = expand;
            categoryButton.Text = BuildCategoryText(item.Text, expand);

            category.ResumeLayout(true);
            parent.ResumeLayout(true);
            menuPanel.ResumeLayout(true);

            // AutoScrollPosition is stored as a negative offset; restoring
            // it explicitly stops the panel from snapping back to the top.
            menuPanel.AutoScrollPosition = new Point(-scrollPosition.X, -scrollPosition.Y);
        }

        private void AddToPanel(FlowLayoutPanel panel, Control control)
        {
            control.Width = GetMenuWidth(panel);
            panel.Controls.Add(control);
        }

        /// <summary>
        /// Target width for menu items inside <paramref name="panel"/>.
        /// Deliberately uses the panel's outer Width (fixed) rather than
        /// ClientSize.Width, which shrinks or grows depending on whether
        /// the vertical scrollbar happens to be visible at that instant.
        /// Basing the calculation on ClientSize created a feedback loop:
        /// every OnResize (e.g. triggered by opening a page) recalculated
        /// widths from an already-shrunk ClientSize, which nudged the
        /// horizontal AutoScrollMinSize a little wider each time - so the
        /// sidebar's horizontal scroll range kept growing the more pages
        /// you opened. Reserving a fixed ScrollBarReserve up front avoids
        /// that entirely.
        /// </summary>
        private int GetMenuWidth(Control panel)
        {
            int width = panel.Width - panel.Padding.Horizontal - ScrollBarReserve;
            return Math.Max(100, width);
        }

        // =========================================================
        // PAGE BUTTON
        // =========================================================
        private Button CreatePageButton(PMSMenuItem item, int level)
        {
            bool main = level == 0;

            Button button = new Button
            {
                Height = main ? menuHeight : subMenuHeight,
                Width = 250,
                FlatStyle = FlatStyle.Flat,
                BackColor = main ? navigationColor : subMenuColor,
                ForeColor = main ? textColor : secondaryTextColor,
                Font = new Font(
                    "Segoe UI",
                    main ? menuFontSize : subMenuFontSize,
                    main ? FontStyle.Bold : FontStyle.Regular),
                Text = BuildPageText(item.Text, level),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12 + level * 10, 0, 10, 0),
                Margin = new Padding(0, 0, 0, 3),
                Cursor = Cursors.Hand,
                TabStop = false
            };

            button.FlatAppearance.BorderSize = 0;

            ApplyIcon(button, item.Icon, level);
            AddHoverEvents(button);
            RegisterLevel(button, level);

            return button;
        }

        // =========================================================
        // CATEGORY BUTTON
        // =========================================================
        private Button CreateCategoryButton(PMSMenuItem item, int level)
        {
            bool main = level == 0;

            Button button = new Button
            {
                Height = main ? menuHeight : subMenuHeight,
                Width = 250,
                FlatStyle = FlatStyle.Flat,
                BackColor = main ? navigationColor : subMenuColor,
                ForeColor = textColor,
                Font = new Font(
                    "Segoe UI",
                    main ? menuFontSize : subMenuFontSize,
                    FontStyle.Bold),
                Text = BuildCategoryText(item.Text, false),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12 + level * 10, 0, 10, 0),
                Margin = new Padding(0, 0, 0, 3),
                Cursor = Cursors.Hand,
                TabStop = false
            };

            button.FlatAppearance.BorderSize = 0;

            ApplyIcon(button, item.Icon, level);
            AddHoverEvents(button);
            RegisterLevel(button, level);

            return button;
        }

        private void RegisterLevel(Button button, int level)
        {
            buttonLevels[button] = level;
        }

        // =========================================================
        // ICON
        // =========================================================
        private void ApplyIcon(Button button, Image icon, int level)
        {
            if (icon == null)
                return;

            int size = level == 0 ? MainIconSize : SubIconSize;

            button.Image = ResizeIcon(icon, size, size);
            button.ImageAlign = ContentAlignment.MiddleLeft;
            button.TextImageRelation = TextImageRelation.ImageBeforeText;
        }

        /// <summary>
        /// Resizes the icon and pads it with transparent space on the
        /// right (IconTextGap px) so there is visible breathing room
        /// between the icon and the button text.
        /// </summary>
        private Image ResizeIcon(Image source, int width, int height)
        {
            if (source == null)
                return null!;

            Bitmap padded = new Bitmap(width + IconTextGap, height);

            using (Graphics g = Graphics.FromImage(padded))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(source, 0, 0, width, height);
            }

            return padded;
        }

        // =========================================================
        // TEXT
        // =========================================================
        private string BuildPageText(string text, int level)
        {
            return new string(' ', 2 + level * 3) + text;
        }

        private string BuildCategoryText(string text, bool expanded)
        {
            return text + (expanded ? "   ˅" : "   ›");
        }

        // =========================================================
        // HOVER / SELECT
        // =========================================================
        private void AddHoverEvents(Button button)
        {
            button.MouseEnter += (sender, e) =>
            {
                if (button != selectedButton)
                    button.BackColor = hoverColor;
            };

            button.MouseLeave += (sender, e) =>
            {
                if (button != selectedButton)
                    button.BackColor = GetNormalBackColor(button);
            };
        }

        private Color GetNormalBackColor(Button button)
        {
            if (buttonLevels.TryGetValue(button, out int level))
                return level == 0 ? navigationColor : subMenuColor;

            return navigationColor;
        }

        private void SelectButton(Button button)
        {
            if (selectedButton != null)
                selectedButton.BackColor = GetNormalBackColor(selectedButton);

            selectedButton = button;
            selectedButton.BackColor = selectedColor;
            selectedButton.ForeColor = textColor;
            selectedButton.Invalidate();
        }

        // =========================================================
        // PAGE CLICK
        // =========================================================
        private void PageButton_Click(object sender, EventArgs e)
        {
            if (sender is not Button button)
                return;

            if (button.Tag is not PMSMenuItem item)
                return;

            SelectButton(button);

            if (item.CreateForm == null)
                return;

            Form form = item.CreateForm();
            if (form == null)
                return;

            OpenForm(form);
        }

        // =========================================================
        // OPEN FORM
        // =========================================================
        /// <summary>
        /// Swaps the active MDI child form. All layout-affecting
        /// properties are set before the form is shown, and the parent
        /// form's layout is suspended for the whole swap, so the new
        /// form docks straight into place instead of flashing at its
        /// design-time size and then snapping to fill the client area.
        /// </summary>
        private void OpenForm(Form form)
        {
            Form parent = FindForm();

            if (parent == null)
            {
                form.Show();
                return;
            }

            if (parent.IsMdiContainer)
            {
                parent.SuspendLayout();

                try
                {
                    foreach (Form child in parent.MdiChildren)
                        child.Close();

                    // Configure appearance/behavior first, dock last -
                    // this avoids the form briefly rendering at its
                    // original design size before it snaps to fill.
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.ControlBox = false;
                    form.Text = string.Empty;
                    form.MdiParent = parent;
                    form.Dock = DockStyle.Fill;
                }
                finally
                {
                    parent.ResumeLayout(true);
                }

                form.Show();
                return;
            }

            form.Show(parent);
        }

        // =========================================================
        // BOTTOM PANEL
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

            Panel separator = new Panel
            {
                Dock = DockStyle.Top,
                Height = 1,
                BackColor = separatorColor
            };
            bottomPanel.Controls.Add(separator);

            btnExit = CreateBottomButton("⏻    ปิดโปรแกรม");
            btnExit.Dock = DockStyle.Bottom;
            btnExit.Height = 34;
            btnExit.ForeColor = dangerColor;
            btnExit.Click += BtnExit_Click;
            bottomPanel.Controls.Add(btnExit);

            btnLogout = CreateBottomButton("⎋    ออกจากระบบ");
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.Height = 34;
            btnLogout.Click += BtnLogout_Click;
            bottomPanel.Controls.Add(btnLogout);

            btnUser = CreateBottomButton("●    " + currentUser);
            btnUser.Dock = DockStyle.Bottom;
            btnUser.Height = 42;
            btnUser.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
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
                Font = new Font("Segoe UI", 10.5F),
                Cursor = Cursors.Hand,
                TabStop = false
            };

            button.FlatAppearance.BorderSize = 0;

            button.MouseEnter += (s, e) =>
            {
                button.BackColor = hoverColor;
                if (button != btnLogout && button != btnExit)
                    button.ForeColor = textColor;
            };

            button.MouseLeave += (s, e) =>
            {
                button.BackColor = navigationColor;

                if (button == btnExit)
                    button.ForeColor = dangerColor;
                else if (button == btnUser)
                    button.ForeColor = textColor;
                else
                    button.ForeColor = secondaryTextColor;
            };

            return button;
        }

        // =========================================================
        // LOGOUT / EXIT
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
                FindForm()?.Close();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = PMSMessageBox.Show(
                "คุณต้องการปิดโปรแกรมหรือไม่?",
                "ปิดโปรแกรม",
                MessageBoxButtons.YesNo,
                PMSMessageBox.PMSMessageIcon.Question);

            if (result == DialogResult.Yes)
                Application.Exit();
        }

        // =========================================================
        // NOTIFICATION
        // =========================================================
        private void UpdateNotification()
        {
            if (lblNotification == null)
                return;

            lblNotification.Visible = notificationCount > 0;

            if (notificationCount > 0)
            {
                lblNotification.Text =
                    notificationCount > 99
                        ? "99+"
                        : notificationCount.ToString();

                PositionNotification();
            }
        }

        private void PositionNotification()
        {
            if (btnNotification == null || lblNotification == null)
                return;

            lblNotification.Location = new Point(
                btnNotification.Right - 12,
                btnNotification.Top - 2);

            lblNotification.BringToFront();
        }

        // =========================================================
        // THEME
        // =========================================================
        private void ApplyTheme()
        {
            BackColor = navigationColor;

            if (headerPanel != null)
                headerPanel.BackColor = navigationColor;

            if (menuPanel != null)
                menuPanel.BackColor = navigationColor;

            if (bottomPanel != null)
                bottomPanel.BackColor = navigationColor;

            if (lblLogo != null)
                lblLogo.ForeColor = textColor;

            if (lblSubtitle != null)
                lblSubtitle.ForeColor = secondaryTextColor;

            if (btnNotification != null)
                btnNotification.ForeColor = accentColor;

            if (btnUser != null)
                btnUser.ForeColor = textColor;

            if (btnExit != null)
                btnExit.ForeColor = dangerColor;

            Invalidate();
        }

        // =========================================================
        // RESIZE
        // =========================================================
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (headerPanel != null && btnNotification != null)
            {
                btnNotification.Location = new Point(
                    headerPanel.ClientSize.Width - btnNotification.Width - 16,
                    48);

                PositionNotification();
            }

            RefreshMenuLayout();
        }

        private void RefreshMenuLayout()
        {
            if (menuPanel == null)
                return;

            menuPanel.SuspendLayout();
            ResizeFlowPanel(menuPanel, GetMenuWidth(menuPanel));
            menuPanel.ResumeLayout(true);

            // Belt-and-braces: never let a horizontal scrollbar appear.
            // The sidebar only ever needs to scroll vertically; forcing
            // this after every layout pass stops any horizontal scroll
            // range from creeping up over repeated opens/resizes.
            menuPanel.HorizontalScroll.Maximum = 0;
            menuPanel.HorizontalScroll.Visible = false;
            menuPanel.AutoScrollMinSize = new Size(0, menuPanel.AutoScrollMinSize.Height);
        }

        /// <summary>
        /// Recursively sets every menu control to a stable target width.
        /// Nested panels use their own fixed Width (not ClientSize.Width)
        /// for the same reason described in GetMenuWidth - it keeps the
        /// calculation stable instead of compounding on every pass.
        /// </summary>
        private void ResizeFlowPanel(FlowLayoutPanel panel, int targetWidth)
        {
            if (panel == null)
                return;

            foreach (Control control in panel.Controls)
            {
                control.Width = targetWidth;

                if (control is FlowLayoutPanel childPanel)
                {
                    int childWidth = Math.Max(100, targetWidth - childPanel.Padding.Horizontal - 8);
                    ResizeFlowPanel(childPanel, childWidth);
                }
            }
        }

        // =========================================================
        // BYTE[] -> IMAGE
        // =========================================================
        public Image ConvertByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null!;

            using (MemoryStream ms = new MemoryStream(byteArray))
            using (Image temp = Image.FromStream(ms))
            {
                return new Bitmap(temp);
            }
        }
    }
}