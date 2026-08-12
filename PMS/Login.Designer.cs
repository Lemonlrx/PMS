namespace PMS
{
    partial class Login
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel loginCard;

        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;

        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;

        private System.Windows.Forms.Button btnLogin;

        private System.Windows.Forms.Label lblForgot;
        private System.Windows.Forms.Label lblVersion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            loginCard = new Panel();
            cbremember = new CheckBox();
            lblTitle = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnLogin = new Button();
            lblForgot = new Label();
            lblVersion = new Label();
            macWindowButtons1 = new PMS.Controls.MacWindowButtons();
            loginCard.SuspendLayout();
            SuspendLayout();
            // 
            // loginCard
            // 
            loginCard.BackColor = Color.FromArgb(28, 29, 34);
            loginCard.BorderStyle = BorderStyle.FixedSingle;
            loginCard.Controls.Add(cbremember);
            loginCard.Controls.Add(lblTitle);
            loginCard.Controls.Add(lblUsername);
            loginCard.Controls.Add(txtUsername);
            loginCard.Controls.Add(lblPassword);
            loginCard.Controls.Add(txtPassword);
            loginCard.Controls.Add(btnLogin);
            loginCard.Controls.Add(lblForgot);
            loginCard.Controls.Add(lblVersion);
            loginCard.Location = new Point(21, 52);
            loginCard.Name = "loginCard";
            loginCard.Size = new Size(420, 500);
            loginCard.TabIndex = 0;
            // 
            // cbremember
            // 
            cbremember.AutoSize = true;
            cbremember.Checked = true;
            cbremember.CheckState = CheckState.Checked;
            cbremember.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cbremember.ForeColor = Color.FromArgb(180, 180, 190);
            cbremember.Location = new Point(50, 291);
            cbremember.Name = "cbremember";
            cbremember.Size = new Size(97, 19);
            cbremember.TabIndex = 9;
            cbremember.Text = "จดจำรหัสผ่าน";
            cbremember.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(50, 36);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(317, 90);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Pawn Management \r\nSystem";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(180, 180, 190);
            lblUsername.Location = new Point(50, 145);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(71, 15);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "USERNAME";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(38, 39, 45);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.ForeColor = Color.White;
            txtUsername.Location = new Point(50, 170);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(315, 29);
            txtUsername.TabIndex = 3;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(180, 180, 190);
            lblPassword.Location = new Point(50, 225);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(73, 15);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "PASSWORD";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(38, 39, 45);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.ForeColor = Color.White;
            txtPassword.Location = new Point(50, 250);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(315, 29);
            txtPassword.TabIndex = 5;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(75, 110, 255);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(50, 320);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(315, 48);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "เข้าสู่ระบบ";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // lblForgot
            // 
            lblForgot.AutoSize = true;
            lblForgot.Font = new Font("Segoe UI", 9F);
            lblForgot.ForeColor = Color.FromArgb(120, 140, 255);
            lblForgot.Location = new Point(50, 390);
            lblForgot.Name = "lblForgot";
            lblForgot.Size = new Size(62, 15);
            lblForgot.TabIndex = 7;
            lblForgot.Text = "ลืมรหัสผ่าน?";
            lblForgot.Click += lblForgot_Click;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 8F);
            lblVersion.ForeColor = Color.FromArgb(100, 100, 110);
            lblVersion.Location = new Point(50, 450);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(183, 13);
            lblVersion.TabIndex = 8;
            lblVersion.Text = "Pawn Management System • v1.6.4";
            // 
            // macWindowButtons1
            // 
            macWindowButtons1.BackColor = Color.Transparent;
            macWindowButtons1.Location = new Point(12, 2);
            macWindowButtons1.Name = "macWindowButtons1";
            macWindowButtons1.Size = new Size(440, 33);
            macWindowButtons1.TabIndex = 1;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 22);
            ClientSize = new Size(464, 564);
            Controls.Add(macWindowButtons1);
            Controls.Add(loginCard);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pawn Management System";
            Resize += Form1_Resize;
            loginCard.ResumeLayout(false);
            loginCard.PerformLayout();
            ResumeLayout(false);
        }
        private Controls.MacWindowButtons macWindowButtons1;
        private CheckBox cbremember;
    }
}