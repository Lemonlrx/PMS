using Org.BouncyCastle.Asn1.X509;
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

namespace PMS.Forms.Customer
{
    public partial class CustomerInfo : Form
    {
        /// <summary> 
        /// SQLDefault (MySQL version, ไม่ใช้ stored procedure/CALL)
        /// <para>[0] SELECT Customer INPUT: {WhereExtra} , {Text}  — เงื่อนไข SexID/IsUse ถูกสร้างเป็น {WhereExtra} จากฝั่ง C# ใน BuildSearchWhere()</para> 
        /// <para>[1] Update Info Customer INPUT: {CustomerID} {IsUse} {Note}</para>
        /// </summary> 
        private String[] SQLDefault = new String[]
        { 
         //[0] SELECT Customer INPUT: {AccountID} , {WhereExtra} , {Text}
         // {WhereExtra} คือส่วนเงื่อนไข AND a.IsUse=... / AND a.SexID=... ที่สร้างจาก BuildSearchWhere() ฝั่ง C#
         "SELECT a.IDCard, \r\n " +
         "CONCAT(IFNULL(b.PrefixName,''), ' ', a.FName, ' ', a.LName), \r\n " +
         "c.SexName, a.PhoneNo, a.DateAdd, a.IsUse, a.customerid \r\n " +
         "FROM personal.customer a \r\n " +
         "LEFT JOIN basedata.prefix b ON a.PrefixID = b.PrefixID \r\n " +
         "LEFT JOIN basedata.sex c ON a.SexID = c.SexID \r\n " +
         "WHERE a.userloginid = '{AccountID}' {WhereExtra} \r\n " +
         "AND (a.IDCard LIKE N'%{Text}%' \r\n " +
         "     OR CONCAT(IFNULL(b.PrefixName,''), ' ', a.FName, ' ', a.LName) LIKE N'%{Text}%' \r\n " +
         "     OR a.PhoneNo LIKE N'%{Text}%') \r\n " +
         "LIMIT 50;"
        ,
         //[1] Update Info Customer (เปิด/ปิดใช้งาน) INPUT: {CustomerID} {IsUse} {Note}
         "SET @CustomerID = '{CustomerID}'; \r\n " +
         " \r\n " +
         "INSERT INTO personal.log_changeinfocustomer \r\n " +
         "(CustomerID, IDCardOld, PrefixIDOld, FNameOld, LNameOld, FNameEngOld, LNameEngOld, SexIDOld, BirthDayOld, PhoneNoOld, SubDistrictIDOld, HouseNumOld, MooOld, AlleyNameOld, RoadNameOld, DateCreateCardOld, DateEXPCardOld, ImagePathOld, DateAddOld, IsUseOld, AccountID, DateTimeAdd, Note) \r\n " +
         "SELECT customerid, IDCard, PrefixID, FName, LName, FNameEng, LNameEng, SexID, BirthDay, PhoneNo, SubDistrictID, HouseNum, Moo, AlleyName, RoadName, DateCreateCard, DateEXPCard, ImagePath, DateAdd, IsUse, AccountID, NOW(), N'{Note}' \r\n " +
         "FROM personal.customer WHERE customerid = @CustomerID; \r\n " +
         " \r\n " +
         "UPDATE personal.customer SET IsUse = '{IsUse}' WHERE customerid = @CustomerID; \r\n " +
         " \r\n " +
         "SELECT 'F';"
        };

        /// <summary>
        /// สร้างเงื่อนไข AND a.IsUse=... / AND a.SexID=... สำหรับ SQLDefault[0]
        /// (แทน logic IF/ELSEIF ที่เคยเขียนไว้ใน T-SQL/stored procedure)
        /// </summary>
        private String BuildSearchWhere(String SexID, String IsUse)
        {
            String w = "";
            if (SexID != "")
                w += " AND a.SexID = '" + SexID + "'";
            if (IsUse != "")
                w += " AND a.IsUse = '" + IsUse + "'";
            return w;
        }

        int SelectIndexRow = -1;

        public CustomerInfo()
        {
            InitializeComponent();
            SearchCustomer();
            if (CBSex.Items.Count != 0)
                CBSex.SelectedIndex = 0;
            if (CBStatus.Items.Count != 0)
                CBStatus.SelectedIndex = 0;
            BTSearch.BackColor = Color.SeaGreen;
        }

        private void CustomerInfo_Resize(object sender, EventArgs e)
        {
            Class.GeneralFuntion.ChangeSizePanal(this, panel1);
        }
        private void SearchCustomer()
        {
            DGV.Rows.Clear();
            String SexID = "";
            String IsUse = "";
            String Text = "";

            if (CBSex.Items.Count != 0)
            {
                if (CBSex.SelectedIndex == 1)
                    SexID = "1";
                else if (CBSex.SelectedIndex == 2)
                    SexID = "2";
            }

            if (CBStatus.Items.Count != 0)
            {
                if (CBStatus.SelectedIndex == 1)
                    IsUse = "1";
                else if (CBStatus.SelectedIndex == 2)
                    IsUse = "0";
            }
            if (TBSearch.Text != "")
            {
                // ไม่ต้องเติม % เองแล้ว เพราะ query ใส่ % ครอบให้ทั้งสองด้านอยู่แล้ว (N'%{Text}%')
                Text = TBSearch.Text;
            }

            var ds = SQL.InputMySQLDataSet(SQLDefault[0]
                             .Replace("{WhereExtra}", BuildSearchWhere(SexID, IsUse))
                             .Replace("{Text}", Text)
                             .Replace("{AccountID}", PMS.Class.UserInfo.UserID));
            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        String Status = "ใช้งาน";
                        if (ds.Tables[0].Rows[x][5].ToString() == "0")
                            Status = "ยกเลิก";
                        DGV.Rows.Add(ds.Tables[0].Rows[x][0].ToString(), ds.Tables[0].Rows[x][1].ToString(), ds.Tables[0].Rows[x][2].ToString(), ds.Tables[0].Rows[x][3].ToString(), Convert.ToDateTime(ds.Tables[0].Rows[x][4].ToString()).ToString("dd/MM/yyyy"), Status, ds.Tables[0].Rows[x][6].ToString());
                        if (DGV.Rows.Count % 2 == 0)
                            DGV.Rows[x].DefaultCellStyle.BackColor = Color.FromArgb(33, 34, 39);
                    }
                }
            }
        }

        private void CBSex_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchCustomer();
        }

        private void CBStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchCustomer();
        }

        private void TBSearch_TextChanged(object sender, EventArgs e)
        {
            SearchCustomer();
        }
        private void DGV_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = DGV.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow != -1)
                {
                    SelectIndexRow = currentMouseOverRow;

                    // เปลี่ยนมาใช้ ContextMenuStrip ของ .NET 8
                    ContextMenuStrip m = new ContextMenuStrip();

                    // ตรวจสอบค่า Null เพื่อป้องกันโปรแกรม Crash (กรณีใน Cell เป็นค่าว่าง)
                    string status = DGV.Rows[currentMouseOverRow].Cells[5].Value?.ToString() ?? "";

                    if (status == "ใช้งาน")
                    {
                        // สร้าง Menu Item แต่ละตัว
                        ToolStripMenuItem itemEdit = new ToolStripMenuItem("แก้ไขข้อมูลลูกค้า");
                        ToolStripMenuItem itemHistory = new ToolStripMenuItem("ดูประวัติการทำรายการลูกค้า");
                        ToolStripMenuItem itemCancel = new ToolStripMenuItem("ตั้งค่าสถาณะเป็นยกเลิกสมาชิก");

                        // ผูก Event ให้เรียบร้อยก่อนแสดงเมนู
                        itemEdit.Click += new System.EventHandler(this.EditCustomerInfo);
                        itemHistory.Click += new System.EventHandler(this.Customerhistory);
                        itemCancel.Click += new System.EventHandler(this.CancelCustomer);

                        // เพิ่มปุ่มเข้าไปในเมนูหลัก
                        m.Items.Add(itemEdit);
                        m.Items.Add(itemHistory);
                        m.Items.Add(itemCancel);
                    }
                    else
                    {
                        // สร้าง Menu Item สำหรับกรณีสถานะอื่นๆ
                        ToolStripMenuItem itemEdit = new ToolStripMenuItem("แก้ไขข้อมูลลูกค้า");
                        ToolStripMenuItem itemHistory = new ToolStripMenuItem("ดูประวัติการทำรายการลูกค้า");
                        ToolStripMenuItem itemActivate = new ToolStripMenuItem("ตั้งค่าสถาณะเป็นใช้งาน");

                        // ผูก Event ให้เรียบร้อยก่อนแสดงเมนู
                        itemEdit.Click += new System.EventHandler(this.EditCustomerInfo);
                        itemHistory.Click += new System.EventHandler(this.Customerhistory);
                        itemActivate.Click += new System.EventHandler(this.ActivateCustomer);

                        // เพิ่มปุ่มเข้าไปในเมนูหลัก
                        m.Items.Add(itemEdit);
                        m.Items.Add(itemHistory);
                        m.Items.Add(itemActivate);
                    }

                    // แสดงเมนูออกมาที่ตำแหน่งเมาส์
                    m.Show(DGV, new Point(e.X, e.Y));
                }
            }
        }

        private void EditCustomerInfo(object sender, EventArgs e)
        {
            var Form = new PMS.Forms.Customer.EditCustomer(DGV.Rows[SelectIndexRow].Cells[6].Value.ToString());
            Form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Form.StartPosition = FormStartPosition.CenterScreen;
            Form.MaximumSize = new Size(1201, 743);
            Form.MinimumSize = new Size(1201, 743);
            Form.Size = new Size(1201, 743);
            Form.ShowDialog();

            SearchCustomer();
            SelectIndexRow = -1;
        }
        private void Customerhistory(object sender, EventArgs e)
        {

            SelectIndexRow = -1;
        }
        private void CancelCustomer(object sender, EventArgs e)
        {
            var ds = SQL.InputMySQLDataSet(SQLDefault[1]
                .Replace("{CustomerID}", DGV.Rows[SelectIndexRow].Cells[6].Value.ToString())
                .Replace("{IsUse}", 0.ToString())
                .Replace("{Note}", "ยกเลิกสมาชิก"));
            if (ds.Tables.Count != 0)
                if (ds.Tables[0].Rows.Count != 0)
                    if (ds.Tables[0].Rows[0][0].ToString() == "F")
                        PMSMessageBox.Show("แก้ไขสถาณะเสร็จสิ้น", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Information);
                    else
                        PMSMessageBox.Show("เกิดข้อผิดพลาดโปรดลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
            SearchCustomer();
            SelectIndexRow = -1;
        }
        private void ActivateCustomer(object sender, EventArgs e)
        {
            var ds = SQL.InputMySQLDataSet(SQLDefault[1]
                .Replace("{CustomerID}", DGV.Rows[SelectIndexRow].Cells[6].Value.ToString())
                .Replace("{IsUse}", 1.ToString())
                .Replace("{Note}", "เปิดใช้งานสมาชิก"));
            if (ds.Tables.Count != 0)
                if (ds.Tables[0].Rows.Count != 0)
                    if (ds.Tables[0].Rows[0][0].ToString() == "F")
                        PMSMessageBox.Show("แก้ไขสถาณะเสร็จสิ้น", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Information);
                    else
                        PMSMessageBox.Show("เกิดข้อผิดพลาดโปรดลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
            SearchCustomer();
            SelectIndexRow = -1;
        }

        private void BTSearch_Click(object sender, EventArgs e)
        {

        }
    }
}