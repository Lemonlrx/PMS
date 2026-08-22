namespace PMS.Forms.Customer
{
    partial class SearchCustomer
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
            macWindowButtonsCloseOnly1 = new PMS.Controls.MacWindowButtonsCloseOnly();
            panel1 = new Panel();
            DGV = new PMS.Controls.PMSDataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            BTAddCustomer = new PMS.Controls.PMSButton();
            TBSearch = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGV).BeginInit();
            SuspendLayout();
            // 
            // macWindowButtonsCloseOnly1
            // 
            macWindowButtonsCloseOnly1.BackColor = Color.Transparent;
            macWindowButtonsCloseOnly1.Location = new Point(12, 0);
            macWindowButtonsCloseOnly1.Name = "macWindowButtonsCloseOnly1";
            macWindowButtonsCloseOnly1.Size = new Size(663, 41);
            macWindowButtonsCloseOnly1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(33, 34, 39);
            panel1.Controls.Add(DGV);
            panel1.Controls.Add(BTAddCustomer);
            panel1.Controls.Add(TBSearch);
            panel1.Location = new Point(12, 47);
            panel1.Name = "panel1";
            panel1.Size = new Size(632, 276);
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
            DGV.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4 });
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
            DGV.Location = new Point(14, 71);
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
            DGV.Size = new Size(603, 184);
            DGV.TabIndex = 22;
            // 
            // Column1
            // 
            Column1.HeaderText = "รหัสบัตร";
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
            Column3.HeaderText = "เบอร์ติดต่อ";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 150;
            // 
            // Column4
            // 
            Column4.HeaderText = "ID";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Visible = false;
            // 
            // BTAddCustomer
            // 
            BTAddCustomer.BackColor = Color.MediumSeaGreen;
            BTAddCustomer.BorderColor = Color.Transparent;
            BTAddCustomer.DisabledBackColor = Color.FromArgb(45, 46, 52);
            BTAddCustomer.DisabledBorderColor = Color.FromArgb(60, 61, 68);
            BTAddCustomer.DisabledTextColor = Color.FromArgb(110, 110, 120);
            BTAddCustomer.FlatAppearance.BorderSize = 0;
            BTAddCustomer.FlatStyle = FlatStyle.Flat;
            BTAddCustomer.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            BTAddCustomer.ForeColor = Color.Black;
            BTAddCustomer.GradientColor1 = Color.FromArgb(75, 110, 255);
            BTAddCustomer.GradientColor2 = Color.FromArgb(100, 130, 255);
            BTAddCustomer.HoverBackColor = Color.FromArgb(90, 120, 255);
            BTAddCustomer.HoverBorderColor = Color.Transparent;
            BTAddCustomer.Location = new Point(562, 14);
            BTAddCustomer.Name = "BTAddCustomer";
            BTAddCustomer.PressedBackColor = Color.FromArgb(55, 80, 210);
            BTAddCustomer.PressedBorderColor = Color.Transparent;
            BTAddCustomer.Size = new Size(45, 45);
            BTAddCustomer.TabIndex = 21;
            BTAddCustomer.Text = "+";
            BTAddCustomer.TextColor = Color.Black;
            BTAddCustomer.UseVisualStyleBackColor = false;
            BTAddCustomer.Click += TBSearch_TextChanged;
            // 
            // TBSearch
            // 
            TBSearch.Location = new Point(24, 14);
            TBSearch.Multiline = true;
            TBSearch.Name = "TBSearch";
            TBSearch.Size = new Size(520, 39);
            TBSearch.TabIndex = 0;
            TBSearch.TextChanged += TBSearch_TextChanged;
            // 
            // SearchCustomer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(28, 29, 34);
            ClientSize = new Size(669, 335);
            Controls.Add(panel1);
            Controls.Add(macWindowButtonsCloseOnly1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SearchCustomer";
            Text = "SearchCustomer";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DGV).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Controls.MacWindowButtonsCloseOnly macWindowButtonsCloseOnly1;
        private Panel panel1;
        private TextBox TBSearch;
        private Controls.PMSButton BTAddCustomer;
        private Controls.PMSDataGridView DGV;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
    }
}