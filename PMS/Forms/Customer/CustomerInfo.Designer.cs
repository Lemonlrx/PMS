namespace PMS.Forms.Customer
{
    partial class CustomerInfo
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            panel1 = new Panel();
            DGV = new PMS.Controls.PMSDataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            BTSearch = new PMS.Controls.PMSButton();
            CBStatus = new ComboBox();
            CBSex = new ComboBox();
            label6 = new Label();
            label5 = new Label();
            label18 = new Label();
            lblUsername = new Label();
            TBSearch = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGV).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(33, 34, 39);
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(DGV);
            panel1.Controls.Add(BTSearch);
            panel1.Controls.Add(CBStatus);
            panel1.Controls.Add(CBSex);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label18);
            panel1.Controls.Add(lblUsername);
            panel1.Controls.Add(TBSearch);
            panel1.Location = new Point(9, 9);
            panel1.Name = "panel1";
            panel1.Size = new Size(1100, 680);
            panel1.TabIndex = 1;
            // 
            // DGV
            // 
            DGV.AllowUserToAddRows = false;
            DGV.AllowUserToDeleteRows = false;
            DGV.AlternateRowBackground = Color.FromArgb(31, 32, 38);
            dataGridViewCellStyle1.BackColor = Color.FromArgb(31, 32, 38);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(220, 222, 228);
            DGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DGV.BackgroundColor = Color.FromArgb(28, 29, 34);
            DGV.BorderColor = Color.FromArgb(65, 68, 78);
            DGV.BorderRadius = 10;
            DGV.BorderStyle = BorderStyle.None;
            DGV.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(38, 40, 47);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(235, 236, 240);
            dataGridViewCellStyle2.Padding = new Padding(10, 0, 10, 0);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGV.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6, Column7 });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(28, 29, 34);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(220, 222, 228);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(55, 75, 130);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DGV.DefaultCellStyle = dataGridViewCellStyle3;
            DGV.EnableHeadersVisualStyles = false;
            DGV.Font = new Font("Segoe UI", 9.5F);
            DGV.GridBackground = Color.FromArgb(28, 29, 34);
            DGV.GridColor = Color.FromArgb(48, 50, 58);
            DGV.GridLineColor = Color.FromArgb(48, 50, 58);
            DGV.HeaderBackground = Color.FromArgb(38, 40, 47);
            DGV.HeaderForeground = Color.FromArgb(235, 236, 240);
            DGV.Location = new Point(20, 150);
            DGV.MultiSelect = false;
            DGV.Name = "DGV";
            DGV.ReadOnly = true;
            DGV.RowForeground = Color.FromArgb(220, 222, 228);
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(38, 40, 47);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(235, 236, 240);
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            DGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            DGV.RowHeadersVisible = false;
            DGV.RowTemplate.Height = 42;
            DGV.SelectionBackground = Color.FromArgb(55, 75, 130);
            DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV.Size = new Size(1061, 515);
            DGV.TabIndex = 21;
            DGV.MouseClick += DGV_MouseClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "รหัสบัตรประชาชน";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 200;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column2.HeaderText = "ชื่อ - นามสกุล";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.HeaderText = "เพศ";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 120;
            // 
            // Column4
            // 
            Column4.FillWeight = 200F;
            Column4.HeaderText = "เบอร์ติดต่อ";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 150;
            // 
            // Column5
            // 
            Column5.FillWeight = 200F;
            Column5.HeaderText = "วันที่สมัคร";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 150;
            // 
            // Column6
            // 
            Column6.FillWeight = 250F;
            Column6.HeaderText = "สถาณะการใช้งาน";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.Width = 180;
            // 
            // Column7
            // 
            Column7.HeaderText = "CustomerID";
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            Column7.Visible = false;
            // 
            // BTSearch
            // 
            BTSearch.BackColor = Color.MediumSeaGreen;
            BTSearch.BorderColor = Color.Transparent;
            BTSearch.DisabledBackColor = Color.FromArgb(45, 46, 52);
            BTSearch.DisabledBorderColor = Color.FromArgb(60, 61, 68);
            BTSearch.DisabledTextColor = Color.FromArgb(110, 110, 120);
            BTSearch.FlatAppearance.BorderSize = 0;
            BTSearch.FlatStyle = FlatStyle.Flat;
            BTSearch.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            BTSearch.ForeColor = Color.Black;
            BTSearch.GradientColor1 = Color.FromArgb(75, 110, 255);
            BTSearch.GradientColor2 = Color.FromArgb(100, 130, 255);
            BTSearch.HoverBackColor = Color.FromArgb(90, 120, 255);
            BTSearch.HoverBorderColor = Color.Transparent;
            BTSearch.Location = new Point(514, 88);
            BTSearch.Name = "BTSearch";
            BTSearch.PressedBackColor = Color.FromArgb(55, 80, 210);
            BTSearch.PressedBorderColor = Color.Transparent;
            BTSearch.Size = new Size(45, 45);
            BTSearch.TabIndex = 20;
            BTSearch.Text = "+";
            BTSearch.TextColor = Color.Black;
            BTSearch.UseVisualStyleBackColor = false;
            BTSearch.Click += BTSearch_Click;
            // 
            // CBStatus
            // 
            CBStatus.BackColor = SystemColors.Control;
            CBStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            CBStatus.Font = new Font("Segoe UI", 12F);
            CBStatus.ForeColor = Color.Black;
            CBStatus.FormattingEnabled = true;
            CBStatus.Location = new Point(945, 98);
            CBStatus.Name = "CBStatus";
            CBStatus.Size = new Size(128, 29);
            CBStatus.TabIndex = 2;
            CBStatus.Visible = false;
            CBStatus.SelectedIndexChanged += CBStatus_SelectedIndexChanged;
            // 
            // CBSex
            // 
            CBSex.BackColor = SystemColors.Control;
            CBSex.DropDownStyle = ComboBoxStyle.DropDownList;
            CBSex.Font = new Font("Segoe UI", 12F);
            CBSex.ForeColor = Color.Black;
            CBSex.FormattingEnabled = true;
            CBSex.Location = new Point(630, 97);
            CBSex.Name = "CBSex";
            CBSex.Size = new Size(125, 29);
            CBSex.TabIndex = 1;
            CBSex.Visible = false;
            CBSex.SelectedIndexChanged += CBSex_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(180, 180, 190);
            label6.Location = new Point(784, 98);
            label6.Name = "label6";
            label6.Size = new Size(155, 25);
            label6.TabIndex = 5;
            label6.Text = "สถาณะการใช้งาน";
            label6.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(180, 180, 190);
            label5.Location = new Point(579, 97);
            label5.Name = "label5";
            label5.Size = new Size(45, 25);
            label5.TabIndex = 5;
            label5.Text = "เพศ";
            label5.Visible = false;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.ForeColor = Color.FromArgb(180, 180, 190);
            label18.Location = new Point(10, 15);
            label18.Name = "label18";
            label18.Size = new Size(510, 65);
            label18.TabIndex = 5;
            label18.Text = "Customer Infomation";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.ForeColor = Color.FromArgb(180, 180, 190);
            lblUsername.Location = new Point(60, 97);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(71, 25);
            lblUsername.TabIndex = 5;
            lblUsername.Text = "Search";
            // 
            // TBSearch
            // 
            TBSearch.BackColor = Color.FromArgb(38, 39, 45);
            TBSearch.BorderStyle = BorderStyle.FixedSingle;
            TBSearch.Font = new Font("Segoe UI", 12F);
            TBSearch.ForeColor = Color.White;
            TBSearch.Location = new Point(137, 98);
            TBSearch.Name = "TBSearch";
            TBSearch.Size = new Size(371, 29);
            TBSearch.TabIndex = 0;
            TBSearch.TextChanged += TBSearch_TextChanged;
            // 
            // CustomerInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(28, 29, 34);
            ClientSize = new Size(1119, 699);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CustomerInfo";
            Text = "CustomerInfo";
            SizeChanged += CustomerInfo_Resize;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DGV).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Controls.PMSButton BTSearch;
        private ComboBox CBStatus;
        private ComboBox CBSex;
        private Label label6;
        private Label label5;
        private Label label18;
        private Label lblUsername;
        private TextBox TBSearch;
        private Controls.PMSDataGridView DGV;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
    }
}