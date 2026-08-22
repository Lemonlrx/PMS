namespace PMS.Forms.FormationInterest
{
    partial class PreviewInterest
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
            label1 = new Label();
            label2 = new Label();
            TBAmount = new TextBox();
            TBBal = new TextBox();
            DGV = new PMS.Controls.PMSDataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGV).BeginInit();
            SuspendLayout();
            // 
            // macWindowButtonsCloseOnly1
            // 
            macWindowButtonsCloseOnly1.BackColor = Color.Transparent;
            macWindowButtonsCloseOnly1.Location = new Point(2, 12);
            macWindowButtonsCloseOnly1.Name = "macWindowButtonsCloseOnly1";
            macWindowButtonsCloseOnly1.Size = new Size(700, 31);
            macWindowButtonsCloseOnly1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(33, 34, 39);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(TBAmount);
            panel1.Controls.Add(TBBal);
            panel1.Controls.Add(DGV);
            panel1.Location = new Point(12, 49);
            panel1.Name = "panel1";
            panel1.Size = new Size(690, 341);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(180, 180, 190);
            label1.Location = new Point(39, 241);
            label1.Name = "label1";
            label1.Size = new Size(133, 25);
            label1.TabIndex = 27;
            label1.Text = "ดอกที่ต้องชำระ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(180, 180, 190);
            label2.Location = new Point(107, 197);
            label2.Name = "label2";
            label2.Size = new Size(65, 25);
            label2.TabIndex = 27;
            label2.Text = "เงินต้น";
            // 
            // TBAmount
            // 
            TBAmount.BackColor = Color.FromArgb(38, 39, 45);
            TBAmount.BorderStyle = BorderStyle.FixedSingle;
            TBAmount.Enabled = false;
            TBAmount.Font = new Font("Segoe UI", 12F);
            TBAmount.ForeColor = Color.White;
            TBAmount.Location = new Point(178, 242);
            TBAmount.Name = "TBAmount";
            TBAmount.Size = new Size(158, 29);
            TBAmount.TabIndex = 26;
            // 
            // TBBal
            // 
            TBBal.BackColor = Color.FromArgb(38, 39, 45);
            TBBal.BorderStyle = BorderStyle.FixedSingle;
            TBBal.Font = new Font("Segoe UI", 12F);
            TBBal.ForeColor = Color.White;
            TBBal.Location = new Point(178, 198);
            TBBal.Name = "TBBal";
            TBBal.Size = new Size(158, 29);
            TBBal.TabIndex = 26;
            TBBal.TextChanged += TBBal_TextChanged;
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
            DGV.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
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
            DGV.Location = new Point(19, 25);
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
            DGV.Size = new Size(651, 156);
            DGV.TabIndex = 25;
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
            // PreviewInterestlate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(28, 29, 34);
            ClientSize = new Size(714, 402);
            Controls.Add(panel1);
            Controls.Add(macWindowButtonsCloseOnly1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PreviewInterestlate";
            Text = "PreviewInterestlate";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DGV).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Controls.MacWindowButtonsCloseOnly macWindowButtonsCloseOnly1;
        private Panel panel1;
        private Controls.PMSDataGridView DGV;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Label label1;
        private Label label2;
        private TextBox TBAmount;
        private TextBox TBBal;
    }
}