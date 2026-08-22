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

namespace PMS.Forms.FormationInterest
{
    public partial class PreviewInterest : Form
    {
        // Array [0] = Amount , [1] = Interest
        DataTable TableFormation = new DataTable();
        bool FormationisEnable = true;
        double INTEREST = 0;
        public PreviewInterest(List<String[]> Formation, int interest = -1)
        {
            InitializeComponent();
            if (interest == -1)
            {
                TableFormation.Columns.Add("Amount");
                TableFormation.Columns.Add("Interest");
                if (Formation == null)
                    this.Close();
                else
                    if (Formation.Count != 0)
                    for (int x = 0; x < Formation.Count; x++)
                        TableFormation.Rows.Add(Formation[x][0].ToString(), Formation[x][1].ToString());
            }
            else
            {
                FormationisEnable = false;
                INTEREST = interest;
            }
            if (TableFormation.Rows.Count > 1)
            {
                for (int x = 0; x < TableFormation.Rows.Count; x++)
                {
                    if (Int32.TryParse(TableFormation.Rows[x][0].ToString(), out int Day) && Double.TryParse(TableFormation.Rows[x][1].ToString(), out double Interest))
                    {
                        if (Day > 0 && Interest > 0)
                        {
                            if (DGV.Rows.Count == 0)
                            {
                                DGV.Rows.Add("1", TableFormation.Rows[x][0].ToString(), TableFormation.Rows[x][1].ToString());
                                TBBal.Focus();
                            }
                            else
                                if (Convert.ToInt32(TableFormation.Rows[x][0].ToString()) > Convert.ToInt32(DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value.ToString()))
                                DGV.Rows.Add(Convert.ToInt32(DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value.ToString()) + 1, TableFormation.Rows[x][0].ToString(), TableFormation.Rows[x][1].ToString());
                        }
                    }
                }
            }
            else
                DGV.Rows.Add("1", "-", INTEREST);

        }

        private void PreviewInterest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void TBBal_TextChanged(object sender, EventArgs e)
        {
            PMS.Class.GeneralFuntion.ProtectedTBInt(TBBal);
            BTCal_Click(sender, e);
        }

        private void BTCal_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(TBBal.Text, out int Amount))
            {
                if (Amount > 0)
                {
                    if (FormationisEnable)
                    {
                        if (TableFormation.Rows.Count > 1)
                        {
                            int PosIF = 0;
                            for (int x = 1; x < TableFormation.Rows.Count; x++)
                            {
                                if (Amount <= Convert.ToInt32(TableFormation.Rows[x][0].ToString()) && Amount > Convert.ToInt32(TableFormation.Rows[x - 1][0].ToString()))
                                {
                                    PosIF = x;
                                }
                                else if (Amount >= Convert.ToInt32(TableFormation.Rows[TableFormation.Rows.Count - 1][0].ToString()))
                                {
                                    PosIF = TableFormation.Rows.Count - 1;
                                    break;
                                }
                                else
                                    break;
                            }
                            TBBal.Text = Math.Ceiling(Convert.ToDouble((Convert.ToDouble(TBBal.Text) * (1.0 + (Convert.ToDouble(TableFormation.Rows[PosIF][1].ToString()) / 100))).ToString("N"))).ToString("N");
                        }
                        else
                            PMSMessageBox.Show("รูปแบบข้อมูลที่นำเข้ามาไม่ถูกต้อง กรุณาลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Warning);
                    }
                    else
                    {
                        TBBal.Text = Math.Ceiling(Convert.ToDouble((Convert.ToDouble(TBBal.Text) * (1.0 + (Convert.ToDouble(INTEREST.ToString()) / 100))).ToString("N"))).ToString("N");
                    }
                }
                else
                    PMSMessageBox.Show("กรุณาระบุจำนวนที่มากกว่า 0 ", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Warning);
            }
            else
                PMSMessageBox.Show("โปรดกรอกข้อมูลเป็นตัวเลข", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Warning);
        }
    }
}
