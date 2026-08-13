using PMS.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PMS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            //autofill when save user pass
            if (Properties.Settings.Default.User != "")
            {
                txtUsername.Text = Properties.Settings.Default.User;
                txtPassword.Text = Properties.Settings.Default.Pass;
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show(
                    "กรุณากรอก Username",
                    "Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "กรุณากรอกรหัสผ่าน",
                    "Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtPassword.Focus();
                return;
            }

            // TODO:
            // ตรวจสอบ Username / Password
            // จาก Database ตรงนี้
            var dt = SQL.InputMySQLDataTable($"SELECT * FROM user.userlogin WHERE username = '{username}' and password = '{password}'");
            if (dt.Rows.Count != 0)
            {
                if (cbremember.Checked)
                {
                    Properties.Settings.Default.User = username;
                    Properties.Settings.Default.Pass = password;
                }
                Properties.Settings.Default.Save();

                PMS.Class.UserInfo.DisPlayName = dt.Rows[0][5].ToString();
                PMS.Class.UserInfo.UserID = dt.Rows[0][0].ToString();

                PMSMessageBox.Show(
                  "เข้าสู่ระบบสำเร็จ",
                  "Login",
                  MessageBoxButtons.OK,
                  PMSMessageBox.PMSMessageIcon.Success
              );
                this.Hide();
                new MainForm().Show();
            }
            else
            {
                PMSMessageBox.Show(
                   "เข้าสู่ระบบไม่สำเร็จ",
                   "Login",
                   MessageBoxButtons.OK,
                   PMSMessageBox.PMSMessageIcon.Error
               );
            }

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterLoginCard();
        }

        private void CenterLoginCard()
        {
            if (loginCard == null)
                return;

            loginCard.Left =
                (ClientSize.Width - loginCard.Width) / 2;

            loginCard.Top =
                (ClientSize.Height - loginCard.Height) / 2;
        }

        private void lblForgot_Click(object sender, EventArgs e)
        {
            PMSMessageBox.Show("", "", MessageBoxButtons.YesNo, PMSMessageBox.PMSMessageIcon.Warning);
            PMSMessageBox.Show("", "", MessageBoxButtons.YesNo, PMSMessageBox.PMSMessageIcon.Information);
            PMSMessageBox.Show("", "", MessageBoxButtons.YesNo, PMSMessageBox.PMSMessageIcon.Question);
            PMSMessageBox.Show("", "", MessageBoxButtons.YesNo, PMSMessageBox.PMSMessageIcon.None);
        }
    }
}