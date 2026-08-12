namespace PMS.Controls
{
    partial class PMSMessageBox
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel messageCard;

        private System.Windows.Forms.PictureBox pictureIcon;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMessage;

        private PMSButton btn1;
        private PMSButton btn2;
        private PMSButton btn3;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            messageCard = new Panel();
            pictureIcon = new PictureBox();
            lblTitle = new Label();
            lblMessage = new Label();
            btn1 = new PMSButton();
            btn2 = new PMSButton();
            btn3 = new PMSButton();
            movingTabs1 = new MovingTabs();
            messageCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureIcon).BeginInit();
            SuspendLayout();
            // 
            // messageCard
            // 
            messageCard.BackColor = Color.FromArgb(28, 29, 34);
            messageCard.Controls.Add(pictureIcon);
            messageCard.Controls.Add(lblTitle);
            messageCard.Controls.Add(lblMessage);
            messageCard.Controls.Add(btn1);
            messageCard.Controls.Add(btn2);
            messageCard.Controls.Add(btn3);
            messageCard.Location = new Point(12, 32);
            messageCard.Name = "messageCard";
            messageCard.Size = new Size(436, 283);
            messageCard.TabIndex = 0;
            // 
            // pictureIcon
            // 
            pictureIcon.Location = new Point(30, 32);
            pictureIcon.Name = "pictureIcon";
            pictureIcon.Size = new Size(55, 55);
            pictureIcon.SizeMode = PictureBoxSizeMode.Zoom;
            pictureIcon.TabIndex = 0;
            pictureIcon.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(100, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(95, 30);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "ข้อความ";
            // 
            // lblMessage
            // 
            lblMessage.Font = new Font("Segoe UI", 10F);
            lblMessage.ForeColor = Color.FromArgb(190, 190, 200);
            lblMessage.Location = new Point(100, 70);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(300, 84);
            lblMessage.TabIndex = 2;
            lblMessage.Text = "ข้อความ";
            // 
            // btn1
            // 
            btn1.BackColor = Color.FromArgb(75, 110, 255);
            btn1.BorderColor = Color.Transparent;
            btn1.DisabledBackColor = Color.FromArgb(45, 46, 52);
            btn1.DisabledBorderColor = Color.FromArgb(60, 61, 68);
            btn1.DisabledTextColor = Color.FromArgb(110, 110, 120);
            btn1.FlatStyle = FlatStyle.Flat;
            btn1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn1.ForeColor = Color.White;
            btn1.GradientColor1 = Color.FromArgb(75, 110, 255);
            btn1.GradientColor2 = Color.FromArgb(100, 130, 255);
            btn1.HoverBackColor = Color.FromArgb(90, 120, 255);
            btn1.HoverBorderColor = Color.Transparent;
            btn1.Location = new Point(177, 193);
            btn1.Name = "btn1";
            btn1.PressedBackColor = Color.FromArgb(55, 80, 210);
            btn1.PressedBorderColor = Color.Transparent;
            btn1.Size = new Size(110, 42);
            btn1.TabIndex = 3;
            btn1.Text = "ตกลง";
            btn1.TextColor = Color.White;
            btn1.UseVisualStyleBackColor = false;
            // 
            // btn2
            // 
            btn2.BackColor = Color.FromArgb(45, 46, 52);
            btn2.BorderColor = Color.FromArgb(70, 71, 80);
            btn2.BorderSize = 1;
            btn2.DisabledBackColor = Color.FromArgb(45, 46, 52);
            btn2.DisabledBorderColor = Color.FromArgb(60, 61, 68);
            btn2.DisabledTextColor = Color.FromArgb(110, 110, 120);
            btn2.FlatStyle = FlatStyle.Flat;
            btn2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn2.ForeColor = Color.White;
            btn2.GradientColor1 = Color.FromArgb(75, 110, 255);
            btn2.GradientColor2 = Color.FromArgb(100, 130, 255);
            btn2.HoverBackColor = Color.FromArgb(55, 56, 64);
            btn2.HoverBorderColor = Color.FromArgb(90, 92, 105);
            btn2.Location = new Point(302, 193);
            btn2.Name = "btn2";
            btn2.PressedBackColor = Color.FromArgb(38, 39, 45);
            btn2.PressedBorderColor = Color.Transparent;
            btn2.Size = new Size(110, 42);
            btn2.TabIndex = 4;
            btn2.Text = "ยกเลิก";
            btn2.TextColor = Color.White;
            btn2.UseVisualStyleBackColor = false;
            // 
            // btn3
            // 
            btn3.BackColor = Color.FromArgb(45, 46, 52);
            btn3.BorderColor = Color.FromArgb(70, 71, 80);
            btn3.BorderSize = 1;
            btn3.DisabledBackColor = Color.FromArgb(45, 46, 52);
            btn3.DisabledBorderColor = Color.FromArgb(60, 61, 68);
            btn3.DisabledTextColor = Color.FromArgb(110, 110, 120);
            btn3.FlatStyle = FlatStyle.Flat;
            btn3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn3.ForeColor = Color.White;
            btn3.GradientColor1 = Color.FromArgb(75, 110, 255);
            btn3.GradientColor2 = Color.FromArgb(100, 130, 255);
            btn3.HoverBackColor = Color.FromArgb(55, 56, 64);
            btn3.HoverBorderColor = Color.FromArgb(90, 92, 105);
            btn3.Location = new Point(302, 193);
            btn3.Name = "btn3";
            btn3.PressedBackColor = Color.FromArgb(38, 39, 45);
            btn3.PressedBorderColor = Color.Transparent;
            btn3.Size = new Size(110, 42);
            btn3.TabIndex = 5;
            btn3.Text = "ยกเลิก";
            btn3.TextColor = Color.White;
            btn3.UseVisualStyleBackColor = false;
            // 
            // movingTabs1
            // 
            movingTabs1.BackColor = Color.FromArgb(25, 25, 29);
            movingTabs1.Location = new Point(4, 2);
            movingTabs1.Name = "movingTabs1";
            movingTabs1.Size = new Size(452, 24);
            movingTabs1.TabIndex = 2;
            // 
            // PMSMessageBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(460, 331);
            Controls.Add(messageCard);
            Controls.Add(movingTabs1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PMSMessageBox";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "PMS MessageBox";
            FormClosing += PMSMessageBox_FormClosing;
            Load += PMSMessageBox_Load;
            messageCard.ResumeLayout(false);
            messageCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureIcon).EndInit();
            ResumeLayout(false);
        }
        private MovingTabs movingTabs1;
    }
}