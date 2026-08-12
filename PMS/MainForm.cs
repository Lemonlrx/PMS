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

            BackColor =
                Color.FromArgb(
                    245,
                    246,
                    248);
        }

        // =========================================================
        // NAVIGATION
        // =========================================================

        private void CreateNavigation()
        {
            navigation =
                new PMS.Controls.PMSNavigation();

            navigation.Dock =
                DockStyle.Left;

            navigation.CurrentUser =
                "Admin";

            navigation.NotificationCount =
                98;

            navigation.NotificationClicked +=
                Navigation_NotificationClicked;

            navigation.LogoutClicked +=
                Navigation_LogoutClicked;

            Controls.Add(
                navigation);

            navigation.BringToFront();
        }

        // =========================================================
        // MENU CONFIGURATION
        // =========================================================

        private void CreateMenus()
        {
            // =====================================================
            // DASHBOARD
            // =====================================================

            navigation.AddPage(
                "Dashboard",
                Properties.Resources.dashboard,
                () => new PMS.Forms.DashboardForm());


            // =====================================================
            // ระบบจำนำ
            // =====================================================

            navigation.AddCategory(
                "ระบบจำนำ",
                Properties.Resources.pawn,

                navigation.Page(
                    "รับจำนำ",
                    Properties.Resources.pawn,
                    () => new PawnCreateForm()),

                navigation.Page(
                    "ไถ่ถอน",
                    Properties.Resources.redeem,
                    () => new PawnRedeemForm()),

                navigation.Page(
                    "ต่อดอก",
                    Properties.Resources.interest,
                    () => new PawnInterestForm()),

                navigation.Page(
                    "ประวัติรายการ",
                    Properties.Resources.history,
                    () => new PawnHistoryForm())
            );


            // =====================================================
            // ลูกค้า
            // =====================================================

            navigation.AddPage(
                "ลูกค้า",
                Properties.Resources.customer,
                () => new CustomerForm());


            // =====================================================
            // รายงาน
            // =====================================================

            navigation.AddCategory(
                "รายงาน",
                Properties.Resources.report,

                navigation.Page(
                    "รายงานรับจำนำ",
                    Properties.Resources.report,
                    () => new PawnReportForm()),

                navigation.Page(
                    "รายงานไถ่ถอน",
                    Properties.Resources.report,
                    () => new RedeemReportForm()),

                navigation.Page(
                    "รายงานการเงิน",
                    Properties.Resources.report,
                    () => new FinanceReportForm())
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