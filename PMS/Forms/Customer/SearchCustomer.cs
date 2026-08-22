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
    public partial class SearchCustomer : Form
    {
        public static List<String> Customer = new List<string>();

        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Search Customer INPUT: {Text} {AccountID} </para> 
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Search Customer INPUT: %{Text} {AccountID}
           "SELECT IDCard , CAST(ISNULL(PrefixName,'') + a.FName + ' ' + a.LName as nvarchar(255)) , PhoneNo , CustomerID \r\n " +
          "FROM Persernal.dbo.tblCustomer as a \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as b on a.PrefixID = b.PrefixID \r\n " +
          "WHERE a.IsUse = 1 and (IDCard LIKE N'%{Text}%' or a.FName LIKE N'%{Text}%' or a.LName LIKE N'%{Text}%' or PhoneNo LIKE N'%{Text}%' and a.AccountID = '{AccountID}')"
           ,

         };
        public SearchCustomer()
        {
            InitializeComponent();
            Customer.Clear();
            ReloadDGV();
            BTAddCustomer.BackColor = Color.SeaGreen;
        }

        private void BTAddCustomer_Click(object sender, EventArgs e)
        {
            new PMS.Forms.Customer.CustomerForm().ShowDialog();
            ReloadDGV();
        }

        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                for (int x = 0; x < DGV.Columns.Count; x++)
                    Customer.Add(DGV.Rows[e.RowIndex].Cells[x].Value.ToString());
                if (Customer.Count != 0)
                    this.Close();
            }
        }
        private void ReloadDGV()
        {
            DGV.Rows.Clear();
            DataTable dt = SQL.InputMySQLDataTable(SQLDefault[0]
                .Replace("%{Text}", TBSearch.Text)
                .Replace("{AccountID}", PMS.Class.UserInfo.UserID));
            if (dt.Rows.Count != 0)
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DGV.Rows.Add("");
                    for (int y = 0; y < dt.Columns.Count; y++)
                        DGV.Rows[DGV.Rows.Count - 1].Cells[y].Value = dt.Rows[x][y].ToString();
                    if (DGV.Rows.Count % 2 == 0)
                        DGV.Rows[x].DefaultCellStyle.BackColor = Color.FromArgb(33, 34, 39);
                }
            }
        }

        private void TBSearch_TextChanged(object sender, EventArgs e)
        {
            ReloadDGV();
        }

        private void SearchCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
