using PMS.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThaiNationalIDCard;
using static PMS.Controls.PMSMessageBox;
using static QRCoder.Base64QRCode;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PMS.Forms.Customer
{
    public partial class CustomerForm : Form
    {
        List<String[,]> ListPrefixTH = new List<string[,]>();
        List<String[,]> ListSex = new List<string[,]>();
        List<String[,]> ListProvince = new List<string[,]>();

        List<String[,]> ListDistrict = new List<string[,]>();
        List<String[,]> ListSubDistrict = new List<string[,]>();

        String FilePath = "";

        /// <summary>
        /// โฟลเดอร์เก็บรูปลูกค้า (แทนการเก็บรูปเป็น binary ในฐานข้อมูลแบบเดิม
        /// ตอนนี้เซฟไฟล์ไว้ในเครื่อง แล้วเก็บแค่ "path" ลงฐานข้อมูล)
        /// </summary>
        private const string CustomerImageFolder = @"C:\PMS\CustomerProto\";

        /// <summary>
        /// SQLDefault (MySQL version)
        /// <para>schema.table แทน database.dbo.tblXxx ของ SQL Server เดิม</para>
        /// <para>[0] Select Prefix , Sex , province INPUT: -</para>
        /// <para>[1] Select District INPUT: {ProvinceID}</para>
        /// <para>[2] Select SubDistrict INPUT: {DistrictID}</para>
        /// <para>[3] Insert Customer and Return ID (CALL sp_insert_customer) INPUT: {IDCard} {PrefixID} {FName} {LName} {FNameEng} {LNameEng} {SexID} {BirthDay} {PhoneNo} {SubdistrictID} {HouseNum} {MooNum} {AlleyName} {RoadName} {DateCreateCard} {ExpirationDateCard} {ImagePath} {idUserLogin}</para>
        /// <para>[4] Select ID  INPUT: {IDCard} {idUserLogin} </para>
        /// <para>[5] Update Info Customer (CALL sp_update_customer) INPUT: {CustomerID} , {IDCard} , {PrefixID} , {FName} , {LName} , {FNameEng} , {LNameEng} , {SexID} , {BirthDay} , {PhoneNo} , {SubDistrictID} , {HouseNum} , {Moo} , {AlleyName} , {RoadName} , {DateCreateCard} , {DateEXPCard} , {ImagePath} , {Note} </para>
        /// <para>[6] SELECT PathFile  INPUT: {idUserLogin}  </para>
        /// <para>[7]  SELECT Location Customer ReadCard (CALL sp_get_location_ids) INPUT: {ProvinceName} , {Districtname} , {SubDistrictName}</para>
        /// </summary>
        private String[] SQLDefault = new String[]
         {
           //[0] Select Prefix , Sex , province INPUT: -
           "SELECT PrefixName , idPrefix \r\n " +
          " FROM BaseData.Prefix \r\n " +
          " WHERE IsUse = 1 \r\n " +
          " ORDER BY PrefixName; \r\n " +
          "  \r\n " +
          " SELECT SexName , idSex \r\n " +
          " FROM BaseData.Sex \r\n " +
          " ORDER BY SexName; \r\n " +
          "  \r\n " +
          " SELECT provincename , idprovince \r\n " +
          " FROM BaseData.Province \r\n " +
          " ORDER BY idprovince;"
           ,

           //[1] Select District INPUT: {ProvinceID}
           "SELECT DistrictName , idDistrict \r\n " +
          "FROM BaseData.District \r\n " +
          "WHERE idProvince = '{ProvinceID}' \r\n" +
          "ORDER BY DistrictName;"
           ,
           //[2] Select SubDistrict INPUT: {DistrictID}
           "SELECT SubdistrictName , idSubdistrict \r\n " +
          "FROM BaseData.Subdistrict \r\n " +
          "WHERE iddistrict = '{DistrictID}' \r\n " +
          "ORDER BY SubdistrictName;"
           ,
           //[3] Insert Customer and Return ID INPUT: {IDCard} {PrefixID} {FName} {LName} {FNameEng} {LNameEng} {SexID} {BirthDay} {PhoneNo} {SubdistrictID} {HouseNum} {MooNum} {AlleyName} {RoadName} {DateCreateCard} {ExpirationDateCard} {ImagePath} {idUserLogin}
           "CALL Personal.sp_insert_customer(" +
           "'{IDCard}', '{PrefixID}', '{FName}', '{LName}', '{FNameEng}', '{LNameEng}', " +
           "'{SexID}', '{BirthDay}', '{PhoneNo}', '{SubdistrictID}', '{HouseNum}', '{MooNum}', " +
           "'{AlleyName}', '{RoadName}', '{DateCreateCard}', '{ExpirationDateCard}', '{ImagePath}', '{idUserLogin}');"
           ,
           //[4] Select ID  INPUT: {IDCard} {idUserLogin}
           "      SELECT idCustomer\r\n " +
           "      FROM Personal.Customer\r\n " +
           "      WHERE IDCard = '{IDCard}' and idUserLogin = '{idUserLogin}' and IsUse = 1 "
           ,
           //[5] Update Info Customer INPUT: {CustomerID} , {IDCard} , {PrefixID} , {FName} , {LName} , {FNameEng} , {LNameEng} , {SexID} , {BirthDay} , {PhoneNo} , {SubDistrictID} , {HouseNum} , {Moo} , {AlleyName} , {RoadName} , {DateCreateCard} , {DateEXPCard} , {ImagePath} , {Note}
           "CALL Personal.sp_update_customer(" +
           "'{CustomerID}', '{IDCard}', '{PrefixID}', '{FName}', '{LName}', '{FNameEng}', '{LNameEng}', " +
           "'{SexID}', '{BirthDay}', '{PhoneNo}', '{SubDistrictID}', '{HouseNum}', '{Moo}', " +
           "'{AlleyName}', '{RoadName}', '{DateCreateCard}', '{DateEXPCard}', '{ImagePath}', '{Note}');"
           ,
           //[6] SELECT PathFile  INPUT: {idUserLogin}
           "       SELECT PathCustomerImage\r\n " +
           "      FROM PMS.DefaultSettingChargePreview\r\n " +
           "      WHERE idUserLogin = '{idUserLogin}'\r\n " +
           "      ORDER BY DefaultSettingChargeID DESC \r\n " +
           "      LIMIT 1 "
           ,
           //[7]  SELECT Location Customer ReadCard INPUT: {ProvinceName} , {Districtname} , {SubDistrictName}
           "      SELECT p.idprovince , d.idDistrict , s.idSubdistrict\r\n " +
           "      FROM BaseData.Province p\r\n " +
           "      LEFT JOIN BaseData.District d ON d.idProvince = p.idprovince AND d.DistrictName = '{Districtname}'\r\n " +
           "      LEFT JOIN BaseData.Subdistrict s ON s.idDistrict = d.idDistrict AND s.SubdistrictName = '{SubDistrictName}'\r\n " +
           "      WHERE p.provincename = '{ProvinceName}' "
           ,

         };
        public CustomerForm()
        {
            InitializeComponent();

            BTReadCard2.BackColor = Color.RoyalBlue;
            BTReadCard2.BorderColor = Color.RoyalBlue;
            BTClear2.BackColor = Color.Maroon;
            BTClear2.BorderColor = Color.Maroon;
            BTSave2.BackColor = Color.SeaGreen;
            BTSave2.BorderColor = Color.SeaGreen;

            PMS.Class.ReadIDCard.UsbNotification.RegisterUsbDeviceNotification(this.Handle);
            DataSet ds = SQL.InputMySQLDataSet(SQLDefault[0]);
            System.Windows.Forms.ComboBox[] cb = { CBPrefixThai, CBSex, CBProvince };
            List<String[,]>[] ls = { ListPrefixTH, ListSex, ListProvince };
            for (int y = 0; y < cb.Length; y++)
                if (ds.Tables.Count != 0)
                {
                    if (ds.Tables[y].Rows.Count != 0)
                        for (int z = 0; z < ds.Tables[y].Rows.Count; z++)
                        {
                            cb[y].Items.Add(new PMS.Class.ComboboxInfo(ds.Tables[y].Rows[z][0].ToString(), ds.Tables[y].Rows[z][1].ToString()));
                            ls[y].Add(new string[,] { { ds.Tables[y].Rows[z][0].ToString(), ds.Tables[y].Rows[z][1].ToString() } });
                        }
                }
            Directory.CreateDirectory(CustomerImageFolder);
        }

        private void AddCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            // แก้บัค: เดิม if/else ซ้อนกันผิดจนทำให้ F5 (บันทึก) และ Esc (ปิดฟอร์ม)
            // ไม่เคยถูกเรียกใช้งานจริง
            if (e.KeyCode == Keys.F4)
            {
                BTReadCard_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                BTSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (TBIDCard.Text != "")
                    BTClear_Click(sender, e);
                else
                    this.Close();
            }
        }

        private void BTReadCard_Click(object sender, EventArgs e)
        {
            var idcard = new ThaiIDCard();
            try
            {
                Refresh();
                Personal personal = idcard.readAllPhoto();
                if (personal != null)
                {
                    TBIDCard.Text = personal.Citizenid; //IDCard
                    DTPBirthDay.Value = personal.Birthday; //Birthday
                    TBFirstNameThai.Text = personal.Th_Firstname; //FnameThai
                    TBLastNameThai.Text = personal.Th_Lastname; //LnameThai
                    TBFirstNameEng.Text = personal.En_Firstname; //FNameEng
                    TBLastNameEng.Text = personal.En_Lastname; //LNameEng
                    DTPCreateCard.Value = personal.Issue; //Create Card Date
                    DTPEXPCard.Value = personal.Expire; //EXP Card
                    TBMoo.Text = personal.addrVillageNo.Replace("หมู่ที่", "");

                    if (personal.addrLane == "")
                        TBSoi.Text = "-";
                    else
                        TBSoi.Text = personal.addrLane;

                    if (personal.addrRoad == "")
                        TBRoad.Text = "-";
                    else
                        TBRoad.Text = personal.addrRoad;

                    TBHouseNum.Text = personal.addrHouseNo;


                    for (int x = 0; x < CBSex.Items.Count; x++)
                    {
                        if (CBSex.Items[x].ToString() == PMS.Class.ReadIDCard.ConvertingSexReadIDCard(Convert.ToInt32(personal.Sex)))
                        {
                            CBSex.SelectedIndex = x;
                            break;
                        }
                    }
                    String Prefix = personal.Th_Prefix;
                    if (Prefix == "น.ส.")
                        Prefix = "นางสาว";
                    else if (Prefix == "ด.ช.")
                        Prefix = "เด็กชาย";
                    else if (Prefix == "ด.ญ.")
                        Prefix = "เด็กหญิง";
                    for (int x = 0; x < CBPrefixThai.Items.Count; x++)
                    {
                        if (CBPrefixThai.Items[x].ToString() == Prefix)
                        {
                            CBPrefixThai.SelectedIndex = x;
                            break;
                        }
                    }
                    DataSet ds = SQL.InputMySQLDataSet(SQLDefault[7]
                         .Replace("{ProvinceName}", personal.addrProvince.Replace("จังหวัด", ""))
                         .Replace("{Districtname}", personal.addrAmphur.Replace("อำเภอ", "").Replace("เขค", ""))
                         .Replace("{SubDistrictName}", personal.addrTambol.Replace("ตำบล", ""))
                             );
                    if (ds.Tables.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            for (int ProvinceLoop = 0; ProvinceLoop < CBProvince.Items.Count; ProvinceLoop++)
                            {
                                var ProvinceInfo = (CBProvince.Items[ProvinceLoop] as PMS.Class.ComboboxInfo);
                                if (ProvinceInfo.ID == ds.Tables[0].Rows[0][0].ToString())
                                {
                                    CBProvince.SelectedIndex = ProvinceLoop;
                                    for (int DistrictLoop = 0; DistrictLoop < CBDistrict.Items.Count; DistrictLoop++)
                                    {
                                        var DistrictInfo = (CBDistrict.Items[DistrictLoop] as PMS.Class.ComboboxInfo);
                                        if (DistrictInfo.ID == ds.Tables[0].Rows[0][1].ToString())
                                        {
                                            CBDistrict.SelectedIndex = DistrictLoop;
                                            for (int SubDistrictLoop = 0; SubDistrictLoop < CBSubDistrict.Items.Count; SubDistrictLoop++)
                                            {
                                                var SubDistrictInfo = (CBSubDistrict.Items[SubDistrictLoop] as PMS.Class.ComboboxInfo);
                                                if (SubDistrictInfo.ID == ds.Tables[0].Rows[0][2].ToString())
                                                {
                                                    CBSubDistrict.SelectedIndex = SubDistrictLoop;
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    PTB.Image = personal.PhotoBitmap;
                    TBPhoneNo.Focus();
                }
                else if (idcard.ErrorCode() > 0)
                {
                    PMSMessageBox.Show("ไม่พบบัตรกรุณาลองใหม่อีกครั้ง", "System", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
                }
            }
            catch
            {
                PMSMessageBox.Show("ไม่สามารถสแกนได้กรุณาลองใหม่อีกครั้ง", "System", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
            }
        }

        private void CBProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBDistrict.Items.Clear();
            CBSubDistrict.Items.Clear();
            ListDistrict.Clear();
            ListSubDistrict.Clear();
            if (CBProvince.SelectedIndex != -1)
            {
                var Info = (CBProvince.SelectedItem as PMS.Class.ComboboxInfo);
                DataTable dt = SQL.InputMySQLDataTable(SQLDefault[1]
                    .Replace("{ProvinceID}", Info.ID));
                if (dt.Rows.Count != 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        CBDistrict.Items.Add(new PMS.Class.ComboboxInfo(dt.Rows[x][0].ToString(), dt.Rows[x][1].ToString()));
                        ListDistrict.Add(new string[,] { { dt.Rows[x][0].ToString(), dt.Rows[x][1].ToString() } });
                    }
                    CBDistrict.Enabled = true;
                }
            }
            else
            {
                CBDistrict.Text = "";
                CBDistrict.Enabled = false;
                CBDistrict.SelectedIndex = -1;
                CBSubDistrict.Text = "";
                CBSubDistrict.Enabled = false;
                CBSubDistrict.SelectedIndex = -1;
            }
        }
        private void CBDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBSubDistrict.Items.Clear();
            ListSubDistrict.Clear();
            if (CBDistrict.SelectedIndex != -1)
            {
                var Info = (CBDistrict.SelectedItem as PMS.Class.ComboboxInfo);
                DataTable dt = SQL.InputMySQLDataTable(SQLDefault[2]
                    .Replace("{DistrictID}", Info.ID));
                if (dt.Rows.Count != 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        CBSubDistrict.Items.Add(new PMS.Class.ComboboxInfo(dt.Rows[x][0].ToString(), dt.Rows[x][1].ToString()));
                        ListSubDistrict.Add(new string[,] { { dt.Rows[x][0].ToString(), dt.Rows[x][1].ToString() } });
                    }
                    CBSubDistrict.Enabled = true;
                }
            }
            else
            {
                CBSubDistrict.Text = "";
                CBSubDistrict.Enabled = false;
                CBSubDistrict.SelectedIndex = -1;
            }
        }

        private void BTRemovePicture_Click(object sender, EventArgs e)
        {
            PTB.Image = null;
            FilePath = "";
            BTRemovePicture.Enabled = false;
            BTUploadPicture.Enabled = true;
        }

        private void BTUploadPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog odf = new OpenFileDialog() { Filter = "Image Files (*.PNG;*.JPG) |*.PNG;*.JPG" };
            odf.ShowDialog();
            if (odf.FileName != "")
            {
                FilePath = odf.FileName;
                PMS.Class.FuntionImage.OpenFilesPreview(PTB, Image.FromFile(odf.FileName));
                BTRemovePicture.Enabled = true;
            }
        }

        private void BTClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            TBFirstNameEng.Text = "";
            TBFirstNameThai.Text = "";
            TBHouseNum.Text = "";
            TBIDCard.Text = "";
            TBLastNameEng.Text = "";
            TBLastNameThai.Text = "";
            TBMoo.Text = "";
            TBPhoneNo.Text = "";
            TBRoad.Text = "-";
            TBSoi.Text = "-";
            PTB.Image = null;

            CBProvince.SelectedIndex = -1;
            CBDistrict.SelectedIndex = -1;
            CBSubDistrict.SelectedIndex = -1;
            CBDistrict.Enabled = false;
            CBSubDistrict.Enabled = false;
            CBPrefixThai.SelectedIndex = -1;
            CBSex.SelectedIndex = -1;


            DTPBirthDay.Value = DateTime.Now;
            DTPCreateCard.Value = DateTime.Now;
            DTPEXPCard.Value = DateTime.Now;
        }

        /// <summary>
        /// เซฟรูปลูกค้าลงไฟล์ที่ C:\PMS\CustomerProto\ แล้วคืนค่า path ไฟล์
        /// (แทนการแปลงรูปเป็น binary แล้วยัดลงฐานข้อมูลตรง ๆ แบบเดิม)
        /// </summary>
        private string SaveCustomerImage(Image image, string idCard)
        {
            try
            {
                Directory.CreateDirectory(CustomerImageFolder);
                string fileName = $"{idCard}_{DateTime.Now:yyyyMMddHHmmss}.jpg";
                //string fullPath = Path.Combine(CustomerImageFolder, fileName);
                string fullPath = fileName;
                // สร้าง Bitmap ตัวใหม่จาก image เพื่อปลดล็อก Stream/File Lock ของ GDI+
                using (Bitmap tempBitmap = new Bitmap(image))
                {
                    tempBitmap.Save(fullPath, ImageFormat.Jpeg);
                }

                return fullPath;
            }
            catch
            {
                PMSMessageBox.Show("บันทึกรูปภาพไม่สำเร็จ กรุณาตรวจสอบสิทธิ์เข้าถึงโฟลเดอร์ " + CustomerImageFolder, "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
                return "";
            }
        }

        private void BTSave_Click(object sender, EventArgs e)
        {
            if (TBFirstNameEng.Text != "" && TBFirstNameThai.Text != "" && TBHouseNum.Text != "" && TBIDCard.Text != "" &&
                TBLastNameEng.Text != "" && TBLastNameThai.Text != "" && TBMoo.Text != "" && TBPhoneNo.Text != "" && TBRoad.Text != "" &&
                TBSoi.Text != "" && CBSubDistrict.SelectedIndex != -1 &&
                CBSex.SelectedIndex != -1 && CBPrefixThai.SelectedIndex != -1)
            {
                DataTable dt = SQL.InputMySQLDataTable(SQLDefault[4].Replace("{IDCard}", TBIDCard.Text).Replace("{idUserLogin}", PMS.Class.UserInfo.UserID));
                var SexInfo = (CBSex.SelectedItem as PMS.Class.ComboboxInfo);
                var PrefixInfo = (CBPrefixThai.SelectedItem as PMS.Class.ComboboxInfo);
                var SubDistrict = (CBSubDistrict.SelectedItem as PMS.Class.ComboboxInfo);

                // เซฟรูป (ถ้ามี) เป็นไฟล์ก่อน แล้วค่อยได้ path ไปใส่ในคำสั่ง SQL
                // ใช้ path เดียวกันได้ทั้งกรณี insert ลูกค้าใหม่ และ update ลูกค้าเดิม
                string imagePath = "";
                if (PTB.Image != null)
                {
                    imagePath = SaveCustomerImage(PTB.Image, TBIDCard.Text);
                    if (imagePath == "")
                        return; // เซฟรูปไม่สำเร็จ หยุดการบันทึก
                }

                if (dt.Rows.Count == 0)
                {
                    DataSet ds = SQL.InputMySQLDataSet(SQLDefault[3]
                        .Replace("{IDCard}", TBIDCard.Text)
                        .Replace("{PrefixID}", PrefixInfo.ID)
                        .Replace("{FName}", TBFirstNameThai.Text)
                        .Replace("{LName}", TBLastNameThai.Text)
                        .Replace("{FNameEng}", TBFirstNameEng.Text)
                        .Replace("{LNameEng}", TBLastNameEng.Text)
                        .Replace("{SexID}", SexInfo.ID)
                        .Replace("{BirthDay}", DTPBirthDay.Value.ToString("yyyy-MM-dd"))
                        .Replace("{PhoneNo}", TBPhoneNo.Text)
                        .Replace("{SubdistrictID}", SubDistrict.ID)
                        .Replace("{HouseNum}", TBHouseNum.Text)
                        .Replace("{MooNum}", TBMoo.Text)
                        .Replace("{AlleyName}", TBSoi.Text)
                        .Replace("{RoadName}", TBRoad.Text)
                        .Replace("{DateCreateCard}", DTPCreateCard.Value.ToString("yyyy-MM-dd"))
                        .Replace("{ExpirationDateCard}", DTPEXPCard.Value.ToString("yyyy-MM-dd"))
                        .Replace("{ImagePath}", imagePath)
                        .Replace("{idUserLogin}", PMS.Class.UserInfo.UserID));

                    if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0 && ds.Tables[0].Rows[0][0] != DBNull.Value)
                    {
                        PMSMessageBox.Show("บันทึกข้อมูลสำเร็จ", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Information);
                        Clear();
                    }
                    else
                        PMSMessageBox.Show("บันทึกข้อมูลไม่สำเร็จโปรดลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
                }
                else
                {
                    DialogResult dr = PMSMessageBox.Show("มีลูกค้าท่านนี้ในระบบแล้ว ต้องการอัพเดทข้อมูลนี้เป็นข้อมูลล่าสุดหรือไม่", "ระบบ", MessageBoxButtons.YesNo, PMSMessageBox.PMSMessageIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        // ถ้าไม่ได้เลือกรูปใหม่ ให้ใช้รูปเดิมต่อ (ไม่ล้าง path ทิ้ง)
                        DataSet ds = SQL.InputMySQLDataSet(SQLDefault[5]
                            .Replace("{CustomerID}", dt.Rows[0][0].ToString())
                            .Replace("{IDCard}", TBIDCard.Text)
                            .Replace("{PrefixID}", PrefixInfo.ID)
                            .Replace("{FName}", TBFirstNameThai.Text)
                            .Replace("{LName}", TBLastNameThai.Text)
                            .Replace("{FNameEng}", TBFirstNameEng.Text)
                            .Replace("{LNameEng}", TBLastNameEng.Text)
                            .Replace("{SexID}", SexInfo.ID)
                            .Replace("{BirthDay}", DTPBirthDay.Value.ToString("yyyy-MM-dd"))
                            .Replace("{PhoneNo}", TBPhoneNo.Text)
                            .Replace("{SubDistrictID}", SubDistrict.ID)
                            .Replace("{HouseNum}", TBHouseNum.Text)
                            .Replace("{Moo}", TBMoo.Text)
                            .Replace("{AlleyName}", TBSoi.Text)
                            .Replace("{RoadName}", TBRoad.Text)
                            .Replace("{DateCreateCard}", DTPCreateCard.Value.ToString("yyyy-MM-dd"))
                            .Replace("{DateEXPCard}", DTPEXPCard.Value.ToString("yyyy-MM-dd"))
                            .Replace("{ImagePath}", imagePath)
                            .Replace("{Note}", "อัพเดทข้อมูลลูกค้าให้เป็นข้อมูลล่าสุด"));

                        if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                        {
                            PMSMessageBox.Show("บันทึกข้อมูลสำเร็จ", "ระบบ", MessageBoxButtons.OK, PMSMessageIcon.Information);
                            Clear();
                        }
                        else
                            PMSMessageBox.Show("บันทึกข้อมูลไม่สำเร็จโปรดลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, PMSMessageIcon.Error);
                    }
                }
            }
            else
                PMSMessageBox.Show("โปรดกรอกข้อมูลให้ครบถ้วนก่อนทำรายการ", "ระบบ", MessageBoxButtons.OK, PMSMessageIcon.Warning);
        }
        private void CustomerForm_Resize(object sender, EventArgs e)
        {
            PMS.Class.GeneralFuntion.ChangeSizePanal(this, this.panel1);
        }
    }
}