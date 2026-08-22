using Google.Protobuf;
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
using System.Xml.Linq;
using static PMS.Controls.PMSMessageBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PMS.Forms.FormationInterest
{
    public partial class IntertInterest : Form
    {
        /// <summary> 
        /// SQLDefault (MySQL version, ไม่ใช้ stored procedure/CALL)
        /// <para>schema.table แทน database.dbo.tblXxx ของ SQL Server เดิม, คอลัมน์ AccountID เปลี่ยนเป็น UserLoginID
        /// (ยกเว้น placeholder {AccountID} ที่ถูก .Replace() ด้วยค่าจาก UserInfo.UserID ยังใช้ชื่อเดิม)</para>
        /// <para>[0] Insert FormationInterest INPUT: {Name} {Detail} {NormalInterest} {AccountID}</para>
        /// <para>[1] INSERT FormationInterest Multi INPUT: {Name} {AccountID} {Plus}</para>
        /// <para>[2] Plus SQLDEFAULT [1] INPUT: {Amount} {Interest}</para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         {  
           //[0] Insert FormationInterest INPUT: {Name} {Detail} {NormalInterest} {AccountID}
           "INSERT INTO pms.formationinterest (Name , Detail , NormalInterest , UserLoginID , DateAdd ) \r\n " +
          "VALUES (N'{Name}' , N'{Detail}' , '{NormalInterest}' , '{AccountID}' , NOW()); \r\n " +
          " \r\n " +
          "SELECT LAST_INSERT_ID();"
           ,
           //[1] INSERT FormationInterest Multi INPUT: {Name} {AccountID} {Plus}
           // ใช้ user variable (@..) แทน DECLARE ของ T-SQL ได้เลยโดยไม่ต้องมี stored procedure
           // ตัดเงื่อนไข IF(@FormationinterestID IS NOT NULL) ออก เพราะ LAST_INSERT_ID() หลัง INSERT ที่สำเร็จจะไม่มีทาง NULL อยู่แล้ว
           "INSERT INTO pms.formationinterest (Name , Detail , NormalInterest , UserLoginID , DateAdd ) \r\n " +
          "VALUES (N'{Name}' , N'{Detail}' , '{NormalInterest}' , '{AccountID}' , NOW()); \r\n " +
          " \r\n " +
          "SELECT LAST_INSERT_ID(); \r\n " +
          "SET @FormationinterestID = LAST_INSERT_ID(); \r\n " +
          " \r\n {Plus}" +
          " \r\n " +
          "SELECT 'F';"
           ,
           //[2] Plus SQLDEFAULT [1] INPUT: {Amount} {Interest}
          "INSERT INTO pms.detailformationinterest (Amount, Interest , FormationInterestID) \r\n " +
          "VALUES('{Amount}','{Interest}',@FormationinterestID); \r\n \r\n"
           ,
         };
        public IntertInterest()
        {
            InitializeComponent();
            BTaddInfoToDGV.BackColor = Color.LightSeaGreen;
            BTClear.BackColor = Color.Maroon;
            BTMessage.BackColor = Color.PaleVioletRed;
            BTSave.BackColor = Color.LightSeaGreen;
        }

        private void RDBInterestif_CheckedChanged(object sender, EventArgs e)
        {
            if (RDBInterestif.Checked)
            {
                DGV.Enabled = true;
                BTaddInfoToDGV.Enabled = true;
                TBAmount.Enabled = true;
            }
            else
            {
                BTaddInfoToDGV.Enabled = false;
                DGV.Enabled = false;
                TBAmount.Enabled = false;
                TBAmount.Text = "";
                DGV.Rows.Clear();
            }
        }

        private void TBDay_TextChanged(object sender, EventArgs e)
        {
            PMS.Class.GeneralFuntion.ProtectedTBInt(TBAmount);
        }

        private void TBInterest_TextChanged(object sender, EventArgs e)
        {
            PMS.Class.GeneralFuntion.ProtectedTBPercent(TBInterestPer100);
        }

        private void BTSave_Click(object sender, EventArgs e)
        {
            if (RDBInterestif.Checked)
            {
                if (TBName.Text != "" && TBDetail.Text != "" && DGV.Rows.Count != 0)
                {
                    String SQLCommand = SQLDefault[1]
                        .Replace("{Name}", TBName.Text)
                        .Replace("{Detail}", TBDetail.Text)
                        .Replace("{NormalInterest}", "")
                        .Replace("{AccountID}", PMS.Class.UserInfo.UserID);
                    String Plus = "";
                    for (int x = 0; x < DGV.Rows.Count; x++)
                    {
                        Plus += SQLDefault[2]
                            .Replace("{Amount}", DGV.Rows[x].Cells[1].Value.ToString())
                            .Replace("{Interest}", DGV.Rows[x].Cells[2].Value.ToString());
                    }
                    SQLCommand = SQLCommand.Replace("{Plus}", Plus);
                    DataSet ds = SQL.InputMySQLDataSet(SQLCommand);
                    if (ds.Tables.Count != 0 && ds.Tables[1].Rows[0][0].ToString() == "F")
                    {
                        PMSMessageBox.Show("บันทึกสำเร็จ", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Information);
                        this.Close();
                    }
                    else
                        PMSMessageBox.Show("บันทึกไม่สำเร็จโปรดลองใหม่อีกครั้งภายหลัง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Warning);
                }
                else
                    PMSMessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วนก่อนทำรายการ", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Warning);
            }
            else
            {
                if (TBName.Text != "" && TBDetail.Text != "" && TBAmount.Text != "" && TBInterestPer100.Text != "")
                {
                    DataSet ds = SQL.InputMySQLDataSet(SQLDefault[0]
                        .Replace("{Name}", TBName.Text)
                        .Replace("{Detail}", TBDetail.Text)
                        .Replace("{NormalInterest}", TBInterestPer100.Text)
                        .Replace("{AccountID}", PMS.Class.UserInfo.UserID));
                    if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        PMSMessageBox.Show("บันทึกสำเร็จ", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Information);
                        this.Close();
                    }
                    else
                        PMSMessageBox.Show("บันทึกไม่สำเร็จโปรดลองใหม่อีกครั้งภายหลัง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Warning);
                }
                else
                    PMSMessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วนก่อนทำรายการ", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Warning);
            }
        }

        private void BTTest_Click(object sender, EventArgs e)
        {
            List<String[]> Formation = new List<string[]>();
            if (RDBInterestif.Checked)
            {
                if (DGV.Rows.Count != 0)
                {
                    for (int x = 0; x < DGV.Rows.Count; x++)
                        Formation.Add(new string[] { DGV.Rows[x].Cells[1].Value.ToString(), DGV.Rows[x].Cells[2].Value.ToString() });
                    new PMS.Forms.FormationInterest.PreviewInterest(Formation).ShowDialog();
                }
            }
            else if (TBInterestPer100.Text != "")
            {
                Formation.Add(new string[] { TBAmount.Text, TBInterestPer100.Text });
                new PMS.Forms.FormationInterest.PreviewInterest(Formation, Convert.ToInt32(TBInterestPer100.Text)).ShowDialog();
            }
            else
            {
                PMSMessageBox.Show("ไม่พบข้อมูลกรุณากรอกข้อมูลลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
            }
        }

        private void BTMessage_Click(object sender, EventArgs e)
        {
            PMSMessageBox.Show("**หมายเหตุ** \r\n" +
            "การใส่ ยอดเงินตั้งต้นให้ใส่จากจำนวนที่มากที่สุดของเงื่อนไขนั้นๆ" +
            "ตามการเรียงลำดับของตาราง บรรทัดแรกจะคิดตั้งแต่ 0 - ยอดที่ใส่ในบรรทัดแรก \r\n" +
            "บรรทัดที่ 2 - รองสุดท้าย จะเป็นเงื่อนไขที่ใส่ได้เรื่อยๆ และจะเริ่มนับยอดตั้งต้นของบรรทัดก่อนหน้าตามลำดับ \r\n" +
            "ส่วนบรรทัดสุดท้ายจะคิดเป็นยอด ตั้งแต่ รองสุดท้าย - ∞ ( 0x0000221E ) ", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Question);
        }

        private void BTaddInfoToDGV_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(TBAmount.Text, out int Amount) && Double.TryParse(TBInterestPer100.Text, out double Interest))
            {
                if (Amount > 0 && Interest > 0)
                {
                    if (DGV.Rows.Count == 0)
                    {
                        DGV.Rows.Add("1", TBAmount.Text, TBInterestPer100.Text);
                        TBAmount.Text = "0";
                        TBInterestPer100.Text = "0";
                        TBAmount.Focus();
                    }
                    else
                    {
                        if (Convert.ToInt32(TBAmount.Text) > Convert.ToInt32(DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value.ToString()))
                        {
                            DGV.Rows.Add(Convert.ToInt32(DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value.ToString()) + 1, TBAmount.Text, TBInterestPer100.Text);
                            TBAmount.Text = "0";
                            TBInterestPer100.Text = "0";
                            TBAmount.Focus();
                        }
                        else
                            PMSMessageBox.Show("ไม่สามารถเพิ่มเงื่อนไขได้เนื่องจากจำนวนเงินต้นน้อยกว่าเงื่อนไขแรก", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Warning);
                    }
                }
                else
                    PMSMessageBox.Show("โปรดกรอกข้อมูลให้ถูกต้อง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Warning);
            }
            else
                PMSMessageBox.Show("โปรดกรอกข้อมูลให้ถูกต้อง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Warning);
        }

        private void BTClear_Click(object sender, EventArgs e)
        {
            DGV.Rows.Clear();
        }

        private void InsertInterest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}