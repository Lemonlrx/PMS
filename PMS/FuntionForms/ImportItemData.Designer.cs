namespace PledgenElectronicProject.Forms.Pledge
{
    partial class ImportItemData
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
            DGVCategories = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            DGVType = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            DGVBrand = new DataGridView();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            macWindowButtonsCloseOnly1 = new PMS.Controls.MacWindowButtonsCloseOnly();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)DGVCategories).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DGVType).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DGVBrand).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // DGVCategories
            // 
            DGVCategories.AllowUserToAddRows = false;
            DGVCategories.AllowUserToDeleteRows = false;
            DGVCategories.AllowUserToResizeRows = false;
            DGVCategories.BackgroundColor = Color.FromArgb(38, 39, 44);
            DGVCategories.BorderStyle = BorderStyle.Fixed3D;
            DGVCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVCategories.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            DGVCategories.Location = new Point(15, 24);
            DGVCategories.Margin = new Padding(4, 6, 4, 6);
            DGVCategories.MultiSelect = false;
            DGVCategories.Name = "DGVCategories";
            DGVCategories.RowHeadersVisible = false;
            DGVCategories.Size = new Size(215, 374);
            DGVCategories.TabIndex = 0;
            DGVCategories.CellBeginEdit += DGVCategories_CellBeginEdit;
            DGVCategories.CellClick += DGVCategories_CellClick;
            DGVCategories.CellEndEdit += DGVCategories_CellEndEdit;
            DGVCategories.MouseClick += DGVCategories_MouseClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "ลำดับ";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column1.Width = 50;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column2.HeaderText = "หมวดหมู่";
            Column2.Name = "Column2";
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            Column3.HeaderText = "ID";
            Column3.Name = "Column3";
            Column3.Visible = false;
            // 
            // DGVType
            // 
            DGVType.AllowUserToAddRows = false;
            DGVType.AllowUserToDeleteRows = false;
            DGVType.AllowUserToResizeRows = false;
            DGVType.BackgroundColor = Color.FromArgb(38, 39, 44);
            DGVType.BorderStyle = BorderStyle.Fixed3D;
            DGVType.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVType.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            DGVType.Location = new Point(238, 24);
            DGVType.Margin = new Padding(4, 6, 4, 6);
            DGVType.MultiSelect = false;
            DGVType.Name = "DGVType";
            DGVType.RowHeadersVisible = false;
            DGVType.Size = new Size(215, 374);
            DGVType.TabIndex = 0;
            DGVType.CellBeginEdit += DGVType_CellBeginEdit;
            DGVType.CellClick += DGVType_CellClick;
            DGVType.CellEndEdit += DGVType_CellEndEdit;
            DGVType.MouseClick += DGVType_MouseClick;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "ลำดับ";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn2.HeaderText = "ประเภท";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "ID";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Visible = false;
            // 
            // DGVBrand
            // 
            DGVBrand.AllowUserToAddRows = false;
            DGVBrand.AllowUserToDeleteRows = false;
            DGVBrand.AllowUserToResizeRows = false;
            DGVBrand.BackgroundColor = Color.FromArgb(38, 39, 44);
            DGVBrand.BorderStyle = BorderStyle.Fixed3D;
            DGVBrand.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVBrand.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6 });
            DGVBrand.Location = new Point(461, 24);
            DGVBrand.Margin = new Padding(4, 6, 4, 6);
            DGVBrand.MultiSelect = false;
            DGVBrand.Name = "DGVBrand";
            DGVBrand.RowHeadersVisible = false;
            DGVBrand.Size = new Size(215, 374);
            DGVBrand.TabIndex = 0;
            DGVBrand.CellBeginEdit += DGVBrand_CellBeginEdit;
            DGVBrand.CellDoubleClick += DGVBrand_CellDoubleClick;
            DGVBrand.CellEndEdit += DGVBrand_CellEndEdit;
            DGVBrand.MouseClick += DGVBrand_MouseClick;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "ลำดับ";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn5.HeaderText = "แบรน";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "ID";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.Visible = false;
            // 
            // macWindowButtonsCloseOnly1
            // 
            macWindowButtonsCloseOnly1.BackColor = Color.Transparent;
            macWindowButtonsCloseOnly1.Location = new Point(0, -9);
            macWindowButtonsCloseOnly1.Name = "macWindowButtonsCloseOnly1";
            macWindowButtonsCloseOnly1.Size = new Size(711, 45);
            macWindowButtonsCloseOnly1.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(28, 29, 34);
            panel1.Controls.Add(DGVBrand);
            panel1.Controls.Add(DGVType);
            panel1.Controls.Add(DGVCategories);
            panel1.Location = new Point(14, 37);
            panel1.Name = "panel1";
            panel1.Size = new Size(695, 420);
            panel1.TabIndex = 2;
            // 
            // ImportItemData
            // 
            AutoScaleDimensions = new SizeF(12F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 22);
            ClientSize = new Size(723, 469);
            Controls.Add(panel1);
            Controls.Add(macWindowButtonsCloseOnly1);
            Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 6, 4, 6);
            Name = "ImportItemData";
            Text = "Searching";
            ((System.ComponentModel.ISupportInitialize)DGVCategories).EndInit();
            ((System.ComponentModel.ISupportInitialize)DGVType).EndInit();
            ((System.ComponentModel.ISupportInitialize)DGVBrand).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVCategories;
        private System.Windows.Forms.DataGridView DGVType;
        private System.Windows.Forms.DataGridView DGVBrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private PMS.Controls.MacWindowButtonsCloseOnly macWindowButtonsCloseOnly1;
        private Panel panel1;
    }
}