using PMS.Controls;
using PMS.Forms;
using PMS.Forms.Customer;
using PMS.Forms.Pawn;
using PMS.Forms.Report;
using System;
using System.Drawing;
using System.Windows.Forms;
using static PMS.Controls.PMSMessageBox;

namespace PMS
{
    public partial class MainForm : Form
    {
        private PMS.Controls.PMSNavigation navigation;

        public MainForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            SetupMDI();

            CreateNavigation();

            CreateMenus();
        }

        // =========================================================
        // MDI
        // =========================================================

        private void SetupMDI()
        {
            IsMdiContainer = true;

            BackColor = Color.FromArgb(245,246,248);
        }

        // =========================================================
        // NAVIGATION
        // =========================================================

        private void CreateNavigation()
        {
            navigation = new PMS.Controls.PMSNavigation();
            navigation.Dock = DockStyle.Left;
            navigation.CurrentUser = PMS.Class.UserInfo.DisPlayName;
            navigation.NotificationCount =98;
            navigation.NotificationClicked += Navigation_NotificationClicked;
            navigation.LogoutClicked +=Navigation_LogoutClicked;
            Controls.Add(navigation);
            navigation.BringToFront();
        }

        // =========================================================
        // MENU CONFIGURATION
        // =========================================================

        private void CreateMenus()
        {
            navigation.AddPage("Dashboard",Properties.Resources.dashboard,() => new PMS.Forms.DashboardForm());

            navigation.AddCategory("รับจำนำ", Properties.Resources.pawn,

                navigation.Category("จัดการบิล", null,
                    navigation.Page("เปิดบิล", null, () => new PawnCreateForm()),
                    navigation.Page("ต่อดอก", null, () => new PawnInterestForm()),
                    navigation.Page("ไถ่ถอน", null, () => new PawnCreateForm()),
                    navigation.Page("แก้ไข้เงินต้น", null, () => new PawnCreateForm()),
                    navigation.Page("ดูช้อมูลบิล", null, () => new PawnHistoryForm()),
                    navigation.Page("รายการรอตีหลุด", null, () => new PawnCreateForm())
                    ),
                navigation.Category("การขาย", null,
                    navigation.Page("ขายสินค้า", null, () => new PawnCreateForm()),
                    navigation.Page("ยกเลิกบิลการขาย", null, () => new PawnCreateForm()),
                    navigation.Page("ดูข้อมูลการขาย", null, () => new PawnCreateForm())
                    ),
                navigation.Category("รับซื้อสินค้า", null,
                    navigation.Page("รับซื้อสินค้า", null, () => new PawnCreateForm()),
                    navigation.Page("ยกเลิกบิลรับซื้อ", null, () => new PawnCreateForm()),
                    navigation.Page("ดูข้อมูลการรับซื้อ", null, () => new PawnCreateForm())
                    ),
                navigation.Page("เช็คสินค้าในสต็อก", null, () => new PawnCreateForm())
            );

            navigation.AddCategory("สมาชิก",Properties.Resources.customer,
                navigation.Page("สมัครสมาชิก", null, () => new CustomerForm()),
                navigation.Page("ดูข้อมูลสมาชิก", null, () => new CustomerForm())
                );

            navigation.AddCategory("การเงิน", Properties.Resources.interest,
                navigation.Page("เพิ่มเงินเข้า", null, () => new CustomerForm()),
                navigation.Page("ถอนเงินออก", null, () => new CustomerForm()),
                navigation.Page("เช็คประวัติการทำรายการ", null, () => new CustomerForm())
                );

            navigation.AddCategory("รายงาน",Properties.Resources.report,

                navigation.Page("รายงานรับจำนำ", null, () => new PawnReportForm()),
                navigation.Page("รายงานไถ่ถอน", null,() => new RedeemReportForm()),
                navigation.Page("รายงานการเงิน", null, () => new FinanceReportForm())
            );

            navigation.AddCategory("การตั้งค่า", Properties.Resources.property,

                navigation.Page("ตั้งค่าระบบ", null, () => new PawnReportForm()),
                navigation.Page("ตั้งค่า Profile ร้านค้า", null, () => new RedeemReportForm())
            );
        }

        // =========================================================
        // NOTIFICATION
        // =========================================================

        private void Navigation_NotificationClicked(
            object sender,
            EventArgs e)
        {
            using (PMSNotificationForm form =
                   new PMSNotificationForm())
            {
                form.ShowDialog(this);
            }
        }

        // =========================================================
        // LOGOUT
        // =========================================================

        private void Navigation_LogoutClicked(
            object sender,
            EventArgs e)
        {
            DialogResult result =
                PMS.Controls.PMSMessageBox.Show(
                    "คุณต้องการออกจากระบบหรือไม่?",
                    "ออกจากระบบ",
                    MessageBoxButtons.YesNo,
                    PMSMessageBox.PMSMessageIcon.Question);

            if (result ==
                DialogResult.Yes)
            {
                // ตรงนี้ภายหลังค่อยกลับไป LoginForm
                Close();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Exit Application
            Application.Exit();
        }
    }
}