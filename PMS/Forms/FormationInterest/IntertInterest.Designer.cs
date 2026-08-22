namespace PMS.Forms.FormationInterest
{
    partial class IntertInterest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            macWindowButtonsCloseOnly1 = new PMS.Controls.MacWindowButtonsCloseOnly();
            panel1 = new Panel();
            DGV = new PMS.Controls.PMSDataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            BTTest = new PMS.Controls.PMSButton();
            BTSave = new PMS.Controls.PMSButton();
            BTMessage = new PMS.Controls.PMSButton();
            BTClear = new PMS.Controls.PMSButton();
            BTaddInfoToDGV = new PMS.Controls.PMSButton();
            RDBInterestNormal = new RadioButton();
            RDBInterestif = new RadioButton();
            label1 = new Label();
            label3 = new Label();
            label2 = new Label();
            lblUsername = new Label();
            TBDetail = new TextBox();
            TBInterestPer100 = new TextBox();
            TBAmount = new TextBox();
            TBName = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGV).BeginInit();
            SuspendLayout();
            // 
            // macWindowButtonsCloseOnly1
            // 
            macWindowButtonsCloseOnly1.BackColor = Color.Transparent;
            macWindowButtonsCloseOnly1.Location = new Point(2, 0);
            macWindowButtonsCloseOnly1.Name = "macWindowButtonsCloseOnly1";
            macWindowButtonsCloseOnly1.Size = new Size(801, 36);
            macWindowButtonsCloseOnly1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(33, 34, 39);
            panel1.Controls.Add(DGV);
            panel1.Controls.Add(BTTest);
            panel1.Controls.Add(BTSave);
            panel1.Controls.Add(BTMessage);
            panel1.Controls.Add(BTClear);
            panel1.Controls.Add(BTaddInfoToDGV);
            panel1.Controls.Add(RDBInterestNormal);
            panel1.Controls.Add(RDBInterestif);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(lblUsername);
            panel1.Controls.Add(TBDetail);
            panel1.Controls.Add(TBInterestPer100);
            panel1.Controls.Add(TBAmount);
            panel1.Controls.Add(TBName);
            panel1.Location = new Point(12, 31);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 412);
            panel1.TabIndex = 1;
            // 
            // DGV
            // 
            DGV.AllowUserToAddRows = false;
            DGV.AllowUserToDeleteRows = false;
            DGV.AlternateRowBackground = Color.FromArgb(31, 32, 38);
            dataGridViewCellStyle9.BackColor = Color.FromArgb(31, 32, 38);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(220, 222, 228);
            DGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            DGV.BackgroundColor = Color.FromArgb(28, 29, 34);
            DGV.BorderColor = Color.FromArgb(65, 68, 78);
            DGV.BorderRadius = 10;
            DGV.BorderStyle = BorderStyle.None;
            DGV.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = Color.FromArgb(38, 40, 47);
            dataGridViewCellStyle10.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle10.ForeColor = Color.FromArgb(235, 236, 240);
            dataGridViewCellStyle10.Padding = new Padding(10, 0, 10, 0);
            dataGridViewCellStyle10.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
            DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGV.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = Color.FromArgb(28, 29, 34);
            dataGridViewCellStyle11.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle11.ForeColor = Color.FromArgb(220, 222, 228);
            dataGridViewCellStyle11.SelectionBackColor = Color.FromArgb(55, 75, 130);
            dataGridViewCellStyle11.SelectionForeColor = Color.White;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.False;
            DGV.DefaultCellStyle = dataGridViewCellStyle11;
            DGV.EnableHeadersVisualStyles = false;
            DGV.Font = new Font("Segoe UI", 9.5F);
            DGV.GridBackground = Color.FromArgb(28, 29, 34);
            DGV.GridColor = Color.FromArgb(48, 50, 58);
            DGV.GridLineColor = Color.FromArgb(48, 50, 58);
            DGV.HeaderBackground = Color.FromArgb(38, 40, 47);
            DGV.HeaderForeground = Color.FromArgb(235, 236, 240);
            DGV.Location = new Point(10, 178);
            DGV.MultiSelect = false;
            DGV.Name = "DGV";
            DGV.ReadOnly = true;
            DGV.RowForeground = Color.FromArgb(220, 222, 228);
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = Color.FromArgb(38, 40, 47);
            dataGridViewCellStyle12.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle12.ForeColor = Color.FromArgb(235, 236, 240);
            dataGridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = DataGridViewTriState.True;
            DGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            DGV.RowHeadersVisible = false;
            DGV.RowTemplate.Height = 42;
            DGV.SelectionBackground = Color.FromArgb(55, 75, 130);
            DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV.Size = new Size(748, 156);
            DGV.TabIndex = 24;
            // 
            // Column1
            // 
            Column1.HeaderText = "เงินต้น";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 250;
            // 
            // Column2
            // 
            Column2.HeaderText = "จนถึง";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 250;
            // 
            // Column3
            // 
            Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column3.HeaderText = "อัตราคิดร้อยละ";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // BTTest
            // 
            BTTest.BackColor = SystemColors.ButtonHighlight;
            BTTest.BorderColor = Color.Transparent;
            BTTest.DisabledBackColor = Color.FromArgb(45, 46, 52);
            BTTest.DisabledBorderColor = Color.FromArgb(60, 61, 68);
            BTTest.DisabledTextColor = Color.FromArgb(110, 110, 120);
            BTTest.FlatAppearance.BorderSize = 0;
            BTTest.FlatStyle = FlatStyle.Flat;
            BTTest.Font = new Font("Segoe UI", 16.25F, FontStyle.Bold);
            BTTest.ForeColor = Color.Black;
            BTTest.GradientColor1 = Color.FromArgb(75, 110, 255);
            BTTest.GradientColor2 = Color.FromArgb(100, 130, 255);
            BTTest.HoverBackColor = Color.FromArgb(90, 120, 255);
            BTTest.HoverBorderColor = Color.Transparent;
            BTTest.Location = new Point(350, 340);
            BTTest.Name = "BTTest";
            BTTest.PressedBackColor = Color.FromArgb(55, 80, 210);
            BTTest.PressedBorderColor = Color.Transparent;
            BTTest.Size = new Size(264, 45);
            BTTest.TabIndex = 23;
            BTTest.Text = "ทำสอบอัตราการคิดดอกเบี้ย";
            BTTest.TextColor = Color.Black;
            BTTest.UseVisualStyleBackColor = false;
            BTTest.Click += BTTest_Click;
            // 
            // BTSave
            // 
            BTSave.BackColor = Color.MediumSeaGreen;
            BTSave.BorderColor = Color.Transparent;
            BTSave.DisabledBackColor = Color.FromArgb(45, 46, 52);
            BTSave.DisabledBorderColor = Color.FromArgb(60, 61, 68);
            BTSave.DisabledTextColor = Color.FromArgb(110, 110, 120);
            BTSave.FlatAppearance.BorderSize = 0;
            BTSave.FlatStyle = FlatStyle.Flat;
            BTSave.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BTSave.ForeColor = Color.Black;
            BTSave.GradientColor1 = Color.FromArgb(75, 110, 255);
            BTSave.GradientColor2 = Color.FromArgb(100, 130, 255);
            BTSave.HoverBackColor = Color.FromArgb(90, 120, 255);
            BTSave.HoverBorderColor = Color.Transparent;
            BTSave.Location = new Point(630, 340);
            BTSave.Name = "BTSave";
            BTSave.PressedBackColor = Color.FromArgb(55, 80, 210);
            BTSave.PressedBorderColor = Color.Transparent;
            BTSave.Size = new Size(128, 45);
            BTSave.TabIndex = 23;
            BTSave.Text = "บันทึก";
            BTSave.TextColor = Color.Black;
            BTSave.UseVisualStyleBackColor = false;
            BTSave.Click += BTSave_Click;
            // 
            // BTMessage
            // 
            BTMessage.BackColor = SystemColors.ButtonHighlight;
            BTMessage.BorderColor = Color.Transparent;
            BTMessage.DisabledBackColor = Color.FromArgb(45, 46, 52);
            BTMessage.DisabledBorderColor = Color.FromArgb(60, 61, 68);
            BTMessage.DisabledTextColor = Color.FromArgb(110, 110, 120);
            BTMessage.FlatAppearance.BorderSize = 0;
            BTMessage.FlatStyle = FlatStyle.Flat;
            BTMessage.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BTMessage.ForeColor = Color.Black;
            BTMessage.GradientColor1 = Color.FromArgb(75, 110, 255);
            BTMessage.GradientColor2 = Color.FromArgb(100, 130, 255);
            BTMessage.HoverBackColor = Color.FromArgb(90, 120, 255);
            BTMessage.HoverBorderColor = Color.Transparent;
            BTMessage.Location = new Point(10, 340);
            BTMessage.Name = "BTMessage";
            BTMessage.PressedBackColor = Color.FromArgb(55, 80, 210);
            BTMessage.PressedBorderColor = Color.Transparent;
            BTMessage.Size = new Size(128, 45);
            BTMessage.TabIndex = 23;
            BTMessage.Text = "หมายเหตุ";
            BTMessage.TextColor = Color.Black;
            BTMessage.UseVisualStyleBackColor = false;
            BTMessage.Click += BTMessage_Click;
            // 
            // BTClear
            // 
            BTClear.BackColor = Color.LightCoral;
            BTClear.BorderColor = Color.Transparent;
            BTClear.DisabledBackColor = Color.FromArgb(45, 46, 52);
            BTClear.DisabledBorderColor = Color.FromArgb(60, 61, 68);
            BTClear.DisabledTextColor = Color.FromArgb(110, 110, 120);
            BTClear.FlatAppearance.BorderSize = 0;
            BTClear.FlatStyle = FlatStyle.Flat;
            BTClear.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            BTClear.ForeColor = Color.Black;
            BTClear.GradientColor1 = Color.FromArgb(75, 110, 255);
            BTClear.GradientColor2 = Color.FromArgb(100, 130, 255);
            BTClear.HoverBackColor = Color.FromArgb(90, 120, 255);
            BTClear.HoverBorderColor = Color.Transparent;
            BTClear.Location = new Point(635, 115);
            BTClear.Name = "BTClear";
            BTClear.PressedBackColor = Color.FromArgb(55, 80, 210);
            BTClear.PressedBorderColor = Color.Transparent;
            BTClear.Size = new Size(95, 45);
            BTClear.TabIndex = 23;
            BTClear.Text = "ล้าง";
            BTClear.TextColor = Color.Black;
            BTClear.UseVisualStyleBackColor = false;
            BTClear.Click += BTClear_Click;
            // 
            // BTaddInfoToDGV
            // 
            BTaddInfoToDGV.BackColor = Color.MediumSeaGreen;
            BTaddInfoToDGV.BorderColor = Color.Transparent;
            BTaddInfoToDGV.DisabledBackColor = Color.FromArgb(45, 46, 52);
            BTaddInfoToDGV.DisabledBorderColor = Color.FromArgb(60, 61, 68);
            BTaddInfoToDGV.DisabledTextColor = Color.FromArgb(110, 110, 120);
            BTaddInfoToDGV.FlatAppearance.BorderSize = 0;
            BTaddInfoToDGV.FlatStyle = FlatStyle.Flat;
            BTaddInfoToDGV.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            BTaddInfoToDGV.ForeColor = Color.Black;
            BTaddInfoToDGV.GradientColor1 = Color.FromArgb(75, 110, 255);
            BTaddInfoToDGV.GradientColor2 = Color.FromArgb(100, 130, 255);
            BTaddInfoToDGV.HoverBackColor = Color.FromArgb(90, 120, 255);
            BTaddInfoToDGV.HoverBorderColor = Color.Transparent;
            BTaddInfoToDGV.Location = new Point(569, 115);
            BTaddInfoToDGV.Name = "BTaddInfoToDGV";
            BTaddInfoToDGV.PressedBackColor = Color.FromArgb(55, 80, 210);
            BTaddInfoToDGV.PressedBorderColor = Color.Transparent;
            BTaddInfoToDGV.Size = new Size(45, 45);
            BTaddInfoToDGV.TabIndex = 22;
            BTaddInfoToDGV.Text = "+";
            BTaddInfoToDGV.TextColor = Color.Black;
            BTaddInfoToDGV.UseVisualStyleBackColor = false;
            BTaddInfoToDGV.Click += BTaddInfoToDGV_Click;
            // 
            // RDBInterestNormal
            // 
            RDBInterestNormal.AutoSize = true;
            RDBInterestNormal.Font = new Font("Segoe UI", 14.25F);
            RDBInterestNormal.ForeColor = Color.FromArgb(180, 180, 190);
            RDBInterestNormal.Location = new Point(635, 21);
            RDBInterestNormal.Name = "RDBInterestNormal";
            RDBInterestNormal.Size = new Size(95, 29);
            RDBInterestNormal.TabIndex = 8;
            RDBInterestNormal.Text = "ดอกปกติ";
            RDBInterestNormal.UseVisualStyleBackColor = true;
            // 
            // RDBInterestif
            // 
            RDBInterestif.AutoSize = true;
            RDBInterestif.Checked = true;
            RDBInterestif.Font = new Font("Segoe UI", 14.25F);
            RDBInterestif.ForeColor = Color.FromArgb(180, 180, 190);
            RDBInterestif.Location = new Point(507, 21);
            RDBInterestif.Name = "RDBInterestif";
            RDBInterestif.Size = new Size(111, 29);
            RDBInterestif.TabIndex = 8;
            RDBInterestif.TabStop = true;
            RDBInterestif.Text = "ดอกเงื่อนไข";
            RDBInterestif.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(180, 180, 190);
            label1.Location = new Point(10, 70);
            label1.Name = "label1";
            label1.Size = new Size(102, 25);
            label1.TabIndex = 7;
            label1.Text = "รายละเอียด";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(180, 180, 190);
            label3.Location = new Point(291, 125);
            label3.Name = "label3";
            label3.Size = new Size(98, 25);
            label3.TabIndex = 7;
            label3.Text = "ดอกร้อยละ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(180, 180, 190);
            label2.Location = new Point(46, 125);
            label2.Name = "label2";
            label2.Size = new Size(65, 25);
            label2.TabIndex = 7;
            label2.Text = "เงินต้น";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.ForeColor = Color.FromArgb(180, 180, 190);
            lblUsername.Location = new Point(10, 23);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(101, 25);
            lblUsername.TabIndex = 7;
            lblUsername.Text = "ชื่อดอกเบี้ย";
            // 
            // TBDetail
            // 
            TBDetail.BackColor = Color.FromArgb(38, 39, 45);
            TBDetail.BorderStyle = BorderStyle.FixedSingle;
            TBDetail.Font = new Font("Segoe UI", 12F);
            TBDetail.ForeColor = Color.White;
            TBDetail.Location = new Point(117, 71);
            TBDetail.Name = "TBDetail";
            TBDetail.Size = new Size(613, 29);
            TBDetail.TabIndex = 6;
            // 
            // TBInterestPer100
            // 
            TBInterestPer100.BackColor = Color.FromArgb(38, 39, 45);
            TBInterestPer100.BorderStyle = BorderStyle.FixedSingle;
            TBInterestPer100.Font = new Font("Segoe UI", 12F);
            TBInterestPer100.ForeColor = Color.White;
            TBInterestPer100.Location = new Point(395, 126);
            TBInterestPer100.Name = "TBInterestPer100";
            TBInterestPer100.Size = new Size(158, 29);
            TBInterestPer100.TabIndex = 6;
            TBInterestPer100.TextChanged += TBInterest_TextChanged;
            // 
            // TBAmount
            // 
            TBAmount.BackColor = Color.FromArgb(38, 39, 45);
            TBAmount.BorderStyle = BorderStyle.FixedSingle;
            TBAmount.Font = new Font("Segoe UI", 12F);
            TBAmount.ForeColor = Color.White;
            TBAmount.Location = new Point(117, 125);
            TBAmount.Name = "TBAmount";
            TBAmount.Size = new Size(158, 29);
            TBAmount.TabIndex = 6;
            TBAmount.TextChanged += TBDay_TextChanged;
            // 
            // TBName
            // 
            TBName.BackColor = Color.FromArgb(38, 39, 45);
            TBName.BorderStyle = BorderStyle.FixedSingle;
            TBName.Font = new Font("Segoe UI", 12F);
            TBName.ForeColor = Color.White;
            TBName.Location = new Point(117, 24);
            TBName.Name = "TBName";
            TBName.Size = new Size(371, 29);
            TBName.TabIndex = 6;
            // 
            // IntertInterestlate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(28, 29, 34);
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(macWindowButtonsCloseOnly1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "IntertInterestlate";
            Text = "IntertInterestlate";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DGV).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Controls.MacWindowButtonsCloseOnly macWindowButtonsCloseOnly1;
        private Panel panel1;
        private RadioButton RDBInterestNormal;
        private RadioButton RDBInterestif;
        private Label lblUsername;
        private TextBox TBName;
        private Label label1;
        private Label label3;
        private Label label2;
        private TextBox TBDetail;
        private TextBox TBInterestPer100;
        private TextBox TBAmount;
        private Controls.PMSButton BTClear;
        private Controls.PMSButton BTaddInfoToDGV;
        private Controls.PMSDataGridView DGV;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Controls.PMSButton BTTest;
        private Controls.PMSButton BTSave;
        private Controls.PMSButton BTMessage;
    }
}