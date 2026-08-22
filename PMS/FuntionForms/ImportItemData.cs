using PMS;
using PMS.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMS.Forms.Pledge
{   
    public partial class ImportItemData : Form
    {
        public static String BrandReturnID = null;
        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Select Categories INPUT: {AccountID}' </para> 
        /// <para>[1] INSERT Categories INPUT: {CategorieName} {AccountID}</para>
        /// <para>[2] Select Type INPUT:  {CategorieID}</para>
        /// <para>[3] Insert Type INPUT: {TypeName} {CategorieID}</para>
        /// <para>[4] SELECT Brand INPUT: {TypeID}</para>
        /// <para>[5] Insert Brand INPUT: {BrandName} {TypeID}</para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Select Categories INPUT: {AccountID}'
           "SELECT CategorieName , CategorieID \r\n " +
          "FROM Itemdata.dbo.tblCategories \r\n " +
          "WHERE IsUse = 1 and AccountID = '{AccountID}'"
           ,
           //[1] INSERT Categories INPUT: {CategorieName} {AccountID}
           "DECLARE @CategorieID INT; \r\n " +
          " \r\n " +
          "SET @CategorieID = (SELECT CategorieID FROM Itemdata.dbo.tblCategories WHERE CategorieName = '{CategorieName}' and IsUse = 1) \r\n " +
          " \r\n " +
          "IF(@CategorieID IS NULL) \r\n " +
          "BEGIN \r\n " +
          " \r\n " +
          "INSERT INTO Itemdata.dbo.tblCategories ( CategorieName ,DateAdd , IsUse , AccountID) \r\n " +
          "VALUES(N'{CategorieName}',CURRENT_TIMESTAMP , 1,'{AccountID}') \r\n " +
          " \r\n " +
          "SET @CategorieID = SCOPE_IDENTITY(); \r\n " +
          " \r\n " +
          "IF(@CategorieID IS NOT NULL) \r\n " +
          " \r\n " +
          "BEGIN \r\n " +
          "SELECT @CategorieID \r\n " +
          "END \r\n " +
          " \r\n " +
          "ELSE \r\n " +
          " \r\n " +
          "BEGIN \r\n " +
          "SELECT 'Fail' \r\n " +
          "END \r\n " +
          " \r\n " +
          "END \r\n " +
          " \r\n " +
          "ELSE \r\n " +
          "BEGIN \r\n " +
          " \r\n " +
          "SELECT 'Found' \r\n " +
          " \r\n " +
          "END"
           ,
           //[2] Select Type INPUT:  {CategorieID}
           "SELECT TypeName , TypeID \r\n " +
          "FROM Itemdata.dbo.tblType \r\n " +
          "WHERE CategorieID = '{CategorieID}' and IsUse = 1"
           ,
           //[3] Insert Type INPUT: {TypeName} {CategorieID}
           "DECLARE @TypeID INT; \r\n " +
          " \r\n " +
          "SET @TypeID = (SELECT TypeID FROM Itemdata.dbo.tblType WHERE TypeName = N'{TypeName}' and IsUse = 1 and CategorieID = '{CategorieID}') \r\n " +
          " \r\n " +
          "IF(@TypeID IS NULL) \r\n " +
          "BEGIN \r\n " +
          " \r\n " +
          "INSERT INTO Itemdata.dbo.tblType (TypeName , DateAdd , CategorieID , IsUse) \r\n " +
          "VALUES(N'{TypeName}' , CURRENT_TIMESTAMP , '{CategorieID}' , 1) \r\n " +
          "SET @TypeID = SCOPE_IDENTITY(); \r\n " +
          " \r\n " +
          "IF(@TypeID IS NOT NULL) \r\n " +
          "BEGIN \r\n " +
          "SELECT @TypeID \r\n " +
          "END \r\n " +
          " \r\n " +
          "ELSE \r\n " +
          "BEGIN \r\n " +
          "SELECT 'Fail' \r\n " +
          "END \r\n " +
          " \r\n " +
          "END \r\n " +
          " \r\n " +
          "ELSE \r\n " +
          "BEGIN \r\n " +
          "SELECT 'Found' \r\n " +
          "END \r\n " +
          ""
           ,
           //[4] SELECT Brand INPUT: {TypeID}
           "SELECT BrandName , BrandID \r\n " +
          "FROM Itemdata.dbo.tblBrand \r\n " +
          "WHERE IsUse = 1 and TypeID = '{TypeID}'"
           ,
           //[5] Insert Brand INPUT: {BrandName} {TypeID}
           "DECLARE @BrandID INT; \r\n " +
          " \r\n " +
          "SET @BrandID = (SELECT BrandID FROM Itemdata.dbo.tblBrand WHERE IsUse = 1 and BrandName = '{BrandName}' and TypeID = '{TypeID}') \r\n " +
          " \r\n " +
          "IF(@BrandID IS NULL) \r\n " +
          "	BEGIN \r\n " +
          "	INSERT INTO Itemdata.dbo.tblBrand (BrandName , DateAdd , TypeID , IsUse) \r\n " +
          "	VALUES (N'{BrandName}',CURRENT_TIMESTAMP , '{TypeID}', 1); \r\n " +
          "	SET @BrandID = SCOPE_IDENTITY(); \r\n " +
          "	IF(@BrandID IS NOT NULL) \r\n " +
          "		BEGIN \r\n " +
          "		SELECT @BrandID; \r\n " +
          "		END \r\n " +
          "	ELSE \r\n " +
          "		BEGIN \r\n " +
          "		SELECT 'Fail' \r\n " +
          "		END \r\n " +
          "	END \r\n " +
          " \r\n " +
          " \r\n " +
          "ELSE \r\n " +
          "	BEGIN \r\n " +
          "	SELECT 'Found'; \r\n " +
          "	END"
           ,

         };
        DataGridView DGVFocus = null;
        int CurrentCategories = 0;
        int CurrentType = 0;
        public ImportItemData()
        {
            InitializeComponent();
            BrandReturnID = null;
            DataTable dt = SQL.InputMySQLDataTable(SQLDefault[0]
                .Replace("{AccountID}", PMS.Class.UserInfo.UserID));
            if (dt.Rows.Count != 0)
                for (int x = 0; x < dt.Rows.Count; x++)
                    DGVCategories.Rows.Add(x+1,dt.Rows[x][0].ToString() , dt.Rows[x][1].ToString());
            DGVCategories.ClearSelection();
            if (DGVCategories.Rows.Count != 0)
                DGVCategories_CellClick(new object() , new DataGridViewCellEventArgs(0,0));
        }

        private void DGVCategories_MouseClick(object sender, MouseEventArgs e)
        {
            DGVFocus = DGVCategories;
            if(e.Button == MouseButtons.Right)
            {
                DGVType.Rows.Clear();
                DGVBrand.Rows.Clear();
                InsertSTM(e, "เพิ่มหมวดหมู่");
            }
        }
        private void InsertSTM(MouseEventArgs e, string ctmText)
        {
            if (DGVFocus != null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    // 1. เปลี่ยนมาใช้ ContextMenuStrip
                    ContextMenuStrip ctm = new ContextMenuStrip();
                    ctm.Items.Clear();

                    // 2. ใช้ ToolStripMenuItem และผูก Event Click ไปที่ DGVAddRows ทันทีในบรรทัดเดียว
                    ToolStripMenuItem menuItem = new ToolStripMenuItem(ctmText, null, new System.EventHandler(this.DGVAddRows));
                    ctm.Items.Add(menuItem);

                    // 3. สั่งแสดงผลเมนูตรงพิกัดเมาส์
                    ctm.Show(DGVFocus, new Point(e.X, e.Y));
                }
            } 
        }

    private void DGVAddRows(object sender, EventArgs e)
        {
            if(DGVFocus != null)
            {
                DataGridView DGV = DGVFocus;
                if(DGV.Rows.Count != 0)
                {
                    if (DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value.ToString().Replace(" ", "") != "")
                    {
                        DGV.Rows.Add(DGV.Rows.Count + 1, "");
                        DGV.CurrentCell = DGV.Rows[DGV.Rows.Count - 1].Cells[1];
                        DGV.BeginEdit(true);
                    }
                    else
                    {
                        DGV.CurrentCell = DGV.Rows[DGV.Rows.Count - 1].Cells[1];
                        DGV.BeginEdit(true);
                    }
                }
                else
                {
                    DGV.Rows.Add(DGV.Rows.Count+1,"");
                    DGV.CurrentCell = DGV.Rows[DGV.Rows.Count - 1].Cells[1];
                    DGV.BeginEdit(true);
                }
            }
        }
        private void DGVBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            var DGV = DGVFocus;
            if (e.RowIndex != -1)
            {
                if (DGV.Rows[e.RowIndex].Cells[1].Value.ToString().Replace(" ", "").Replace("-", "") != "" && DGV.Rows[e.RowIndex].Cells[2].Value != null)
                    e.Cancel = true;
            }
        }
        private void DGVEndEdit(int RowIndex , String FormationText ,String SQLInsert )
        {
            var DGV = DGVFocus;
            if(DGV != null)
            {
                if (DGV.Rows[RowIndex].Cells[1].Value.ToString().Replace(" ", "").Replace("-", "") != "")
                {
                    var dr = PMSMessageBox.Show($"คุณต้องการที่จะเพิ่ม{FormationText} \"{DGV.Rows[RowIndex].Cells[1].Value.ToString()}\" ใช่หรือไม่ \r\n โปรดตรวจสอบก่อนกดยืนยัน", "ระบบ", MessageBoxButtons.YesNo, PMSMessageBox.PMSMessageIcon.Warning);
                    if (DialogResult.Yes == dr)
                    {
                        //รอแก้ จาก dt เป็ฯ ds 
                        var ds = SQL.InputMySQLDataSet(SQLInsert);
                        if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                        {
                            String ReturnText = ds.Tables[0].Rows[0][0].ToString();
                            if (Int32.TryParse(ReturnText, out int ID))
                            {
                                DGV.Rows[RowIndex].Cells[2].Value = ID;
                                PMSMessageBox.Show($"เพิ่ม{FormationText}สำเร็จแล้ว", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Information);
                            }
                            else if (ReturnText == "Fail")
                            {
                                DGV.Rows.RemoveAt(RowIndex);
                                PMSMessageBox.Show($"เพิ่ม{FormationText}ไม่สำเร็จ โปรดลองใหม่อีกครั้งภายหลัง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
                            }
                            else if (ReturnText == "Found")
                            {
                                DGV.Rows.RemoveAt(RowIndex);
                                PMSMessageBox.Show($"เพิ่ม{FormationText}ไม่สำเร็จ! \r\n เนื่องจากมี{FormationText}ชื่อดังกล่าวอยู่แล้ว", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
                            }
                            else
                            {
                                DGV.Rows.RemoveAt(RowIndex);
                                PMSMessageBox.Show($"เพิ่ม{FormationText}ไม่สำเร็จ โปรดลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
                            }
                        }
                        else
                        {
                            DGV.Rows.RemoveAt(RowIndex);
                            PMSMessageBox.Show($"เพิ่ม{FormationText}ไม่สำเร็จ โปรดลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
                        }
                    }
                    else
                    {
                        try
                        {
                            DGV.Rows.RemoveAt(DGV.Rows.Count - 1);
                        }
                        catch
                        {
                            // กัน Error ที่เพิ่มรายการแล้วไปกดที่ Cell อื่น หลัง Edit จะ Error 
                        }
                    }
                    DGV.ClearSelection();
                }
            }
        }
        private void DGVCategories_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DGVBeginEdit(e);
        }

        private void DGVCategories_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(DGVCategories.Rows[e.RowIndex].Cells[1].Value != null)
            {
                DGVEndEdit(e.RowIndex, "หมวดหมู่", SQLDefault[1]
                    .Replace("{CategorieName}", DGVCategories.Rows[e.RowIndex].Cells[1].Value.ToString())
                    .Replace("{AccountID}", PMS.Class.UserInfo.UserID));
                if (DGVCategories.Rows.Count != 0)
                {
                    if (DGVCategories.CurrentRow != null)
                    {
                        DGVCategories.Rows[DGVCategories.CurrentRow.Index].Cells[DGVCategories.CurrentCell.ColumnIndex].Selected = true;
                        DGVCategories_CellClick(new object(), new DataGridViewCellEventArgs(DGVCategories.CurrentCell.ColumnIndex, DGVCategories.CurrentRow.Index));
                    }
                    else
                    {
                        DGVCategories.Rows[0].Cells[0].Selected = true;
                        DGVCategories_CellClick(new object(), new DataGridViewCellEventArgs(0, 0));
                    }
                }
            }

        }

        private void DGVCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DGVType.Rows.Clear();
                DGVBrand.Rows.Clear();
                if (DGVCategories.Rows[e.RowIndex].Cells[2].Value != null)
                {
                    CurrentCategories = Convert.ToInt32(DGVCategories.Rows[e.RowIndex].Cells[2].Value.ToString());
                    DataTable dt = SQL.InputMySQLDataTable(SQLDefault[2]
                        .Replace("{CategorieID}", CurrentCategories.ToString()));
                    if (dt.Rows.Count != 0)
                        for (int x = 0; x < dt.Rows.Count; x++)
                            DGVType.Rows.Add(DGVType.Rows.Count+1, dt.Rows[x][0].ToString(), dt.Rows[x][1].ToString());
                    if (DGVType.Rows.Count != 0)
                        DGVType_CellClick(new object(), new DataGridViewCellEventArgs(0, 0));
                }
            }
        }

        private void DGVType_MouseClick(object sender, MouseEventArgs e)
        {
            DGVFocus = DGVType;
            if(DGVCategories.Rows.Count != 0 && DGVCategories.CurrentRow != null && DGVCategories.Rows[DGVCategories.CurrentRow.Index].Cells[2].Value != null)
            {
                if(e.Button == MouseButtons.Right)
                {
                    DGVBrand.Rows.Clear();
                    InsertSTM(e, "เพิ่มประเภท");
                }
            }
        }

        private void DGVType_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DGVBeginEdit(e);
        }

        private void DGVType_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVType.Rows[e.RowIndex].Cells[1].Value != null)
            {
                DGVEndEdit(e.RowIndex, "ประเภท", SQLDefault[3]
                .Replace("{TypeName}", DGVType.Rows[e.RowIndex].Cells[1].Value.ToString())
                .Replace("{CategorieID}", CurrentCategories.ToString()));

                if (DGVType.Rows.Count != 0)
                {
                    if (DGVType.CurrentRow != null)
                    {
                        DGVType.Rows[DGVType.CurrentRow.Index].Cells[DGVType.CurrentCell.ColumnIndex].Selected = true;
                        DGVType_CellClick(new object(), new DataGridViewCellEventArgs(DGVType.CurrentCell.ColumnIndex, DGVType.CurrentRow.Index));
                    }
                    else
                    {
                        DGVType.Rows[0].Cells[0].Selected = true;
                        DGVType_CellClick(new object(), new DataGridViewCellEventArgs(0, 0));
                    }
                }
            }
        }

        private void DGVType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DGVBrand.Rows.Clear();
                if (DGVType.Rows[e.RowIndex].Cells[2].Value != null)
                {
                    CurrentType = Convert.ToInt32(DGVType.Rows[e.RowIndex].Cells[2].Value.ToString());
                    DataTable dt = SQL.InputMySQLDataTable(SQLDefault[4]
                        .Replace("{TypeID}", CurrentType.ToString()));
                    if (dt.Rows.Count != 0)
                        for (int x = 0; x < dt.Rows.Count; x++)
                            DGVBrand.Rows.Add(DGVBrand.Rows.Count + 1, dt.Rows[x][0].ToString(), dt.Rows[x][1].ToString());
                }
            }
        }

        private void DGVBrand_MouseClick(object sender, MouseEventArgs e)
        {

            if (DGVCategories.Rows.Count != 0 && DGVCategories.CurrentRow != null && DGVType.Rows.Count != 0 && DGVType.CurrentRow != null && DGVType.Rows[DGVType.CurrentRow.Index].Cells[2].Value != null)
            {
                DGVFocus = DGVBrand;
                InsertSTM(e, "เพิ่มแบรน");
            }
        }

        private void DGVBrand_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DGVBeginEdit(e);
        }

        private void DGVBrand_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVBrand.Rows[e.RowIndex].Cells[1].Value != null)
            {
                DGVEndEdit(e.RowIndex, "แบรน", SQLDefault[5]
                .Replace("{BrandName}", DGVBrand.Rows[e.RowIndex].Cells[1].Value.ToString())
                .Replace("{TypeID}", CurrentType.ToString()));
            }
        }

        private void DGVBrand_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                if(DGVBrand.Rows[e.RowIndex].Cells[2].Value != null)
                {
                    BrandReturnID = DGVBrand.Rows[e.RowIndex].Cells[2].Value.ToString();
                    this.Close();
                }
            }
        }
    }
}
