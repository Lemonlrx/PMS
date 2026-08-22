using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMS.Forms.FormationInterest
{
    public partial class SearchFormationInterest : Form
    {
        int SelectIndexRow = -1;
        public static String FORMATIONINTERESTID = "";

        /// <summary> 
        /// SQLDefault (MySQL version, ไม่ใช้ stored procedure/CALL)
        /// <para>schema.table แทน database.dbo.tblXxx ของ SQL Server เดิม, คอลัมน์ AccountID เปลี่ยนเป็น UserLoginID
        /// (ยกเว้น placeholder {AccountID} ที่ถูก .Replace() ด้วยค่าจาก UserInfo.UserID ยังใช้ชื่อเดิม)</para>
        /// <para>[0] Search FormationInterest INPUT: {AccountID} {Text} </para> 
        /// <para>[1] SELECT DetailFormationInterest INPUT: {ID}
        /// — ปรับให้คืนค่า 2 result set เสมอ (เดิมมี IF ทำให้บางกรณีคืนแค่ 1 result set แล้ว ds.Tables[1] จะ error)
        /// Tables[0] = NormalInterest, Tables[1] = รายการ Amount/Interest (ว่างได้ถ้าเป็นดอกเบี้ยแบบอัตราเดียว)</para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Search FormationInterest INPUT: {AccountID} {Text}
           "SELECT Name , Detail , FormationInterestID \r\n " +
          "FROM pms.formationinterest \r\n " +
          "WHERE UserLoginID = '{AccountID}' and CancelDate IS NULL and (Name LIKE N'%{Text}%' or Detail LIKE N'%{Text}%') \r\n " +
          "LIMIT 50;"
           ,
           //[1] SELECT DetailFormationInterest INPUT: {ID}
           // ไม่ใช้ DECLARE/SET/IF แล้ว รันทั้งสอง SELECT ตรง ๆ เสมอ (ตัวที่สองจะว่างเองถ้าไม่มีแถวในตาราง detail)
           "SELECT NormalInterest \r\n " +
          "FROM pms.formationinterest \r\n " +
          "WHERE FormationInterestID = '{ID}' and CancelDate IS NULL; \r\n " +
          " \r\n " +
          "SELECT Amount , Interest \r\n " +
          "FROM pms.formationinterest as a \r\n " +
          "LEFT JOIN pms.detailformationinterest as b on a.FormationInterestID = b.FormationInterestID \r\n " +
          "WHERE a.FormationInterestID = '{ID}' and a.CancelDate IS NULL and b.FormationInterestID IS NOT NULL \r\n " +
          "GROUP BY Amount , Interest \r\n " +
          "ORDER BY Amount;"
           ,

         };
        public SearchFormationInterest()
        {
            InitializeComponent();
            BTInsert.BackColor = Color.SeaGreen;
            FORMATIONINTERESTID = "";
            DataTable dt = SQL.InputMySQLDataTable(SQLDefault[0]
                .Replace("{AccountID}", PMS.Class.UserInfo.UserID)
                .Replace("{Text}", TBSearch.Text));
            if (dt.Rows.Count != 0)
                for (int x = 0; x < dt.Rows.Count; x++)
                    DGV.Rows.Add(dt.Rows[x][0].ToString(), dt.Rows[x][1].ToString(), dt.Rows[x][2].ToString());
        }

        private void TBSearch_TextChanged(object sender, EventArgs e)
        {
            DGV.Rows.Clear();

            // แก้บั๊กจากต้นฉบับ: ของเดิม Replace("%{Text}", ...) ทำให้เผลอตัด % ตัวหน้าออก
            // (กลายเป็นค้นหาแบบ "ขึ้นต้นด้วย" แทนที่จะเป็น "มีคำนี้อยู่ตรงไหนก็ได้") ตอนนี้ใช้ {Text} เหมือน constructor
            DataTable dt = SQL.InputMySQLDataTable(SQLDefault[0]
                .Replace("{AccountID}", PMS.Class.UserInfo.UserID)
                .Replace("{Text}", TBSearch.Text));
            if (dt.Rows.Count != 0)
                for (int x = 0; x < dt.Rows.Count; x++)
                    DGV.Rows.Add(dt.Rows[x][0].ToString(), dt.Rows[x][1].ToString(), dt.Rows[x][2].ToString());
        }

        private void BTInsert_Click(object sender, EventArgs e)
        {
            new PMS.Forms.FormationInterest.IntertInterest().ShowDialog();
        }

        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                FORMATIONINTERESTID = DGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.Close();
            }
        }

        private void DGV_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Check where the user clicked inside the DataGridView
                int currentMouseOverRow = DGV.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow != -1)
                {
                    SelectIndexRow = currentMouseOverRow;

                    // Highlight the right-clicked row
                    DGV.ClearSelection();
                    DGV.Rows[currentMouseOverRow].Selected = true;

                    // FIX: Use modern ContextMenuStrip instead of ContextMenu
                    ContextMenuStrip menu = new ContextMenuStrip();

                    // FIX: Use modern ToolStripMenuItem instead of MenuItem
                    ToolStripMenuItem menuItem = new ToolStripMenuItem("ดูข้อมูลเพิ่มเติม");

                    // Attach the click handler
                    menuItem.Click += new System.EventHandler(this.PreviewData);

                    // Add item to the menu strip
                    menu.Items.Add(menuItem);

                    // FIX: Show using the modern signature (.Show(Control, Point))
                    menu.Show(DGV, new Point(e.X, e.Y));
                }
            }
        }



        private void PreviewData(object sender, EventArgs e)
        {
            DataSet ds = SQL.InputMySQLDataSet(SQLDefault[1]
                .Replace("{ID}", DGV.Rows[SelectIndexRow].Cells[2].Value.ToString()));
            List<String[]> FormationInterest = new List<string[]>();
            if (ds.Tables[1].Rows.Count != 0)
            {
                for (int x = 0; x < ds.Tables[1].Rows.Count; x++)
                    FormationInterest.Add(new string[] { ds.Tables[1].Rows[x][0].ToString(), ds.Tables[1].Rows[x][1].ToString() });
                new PMS.Forms.FormationInterest.PreviewInterest(FormationInterest).ShowDialog();
            }
            else
            {
                FormationInterest.Add(new string[] { ds.Tables[0].Rows[0][0].ToString(), "1" });
                new PMS.Forms.FormationInterest.PreviewInterest(FormationInterest, Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString())).ShowDialog();
            }
        }

        private void SearchFormationInterest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}