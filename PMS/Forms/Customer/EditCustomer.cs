using PMS.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThaiNationalIDCard;
using static PMS.Controls.PMSMessageBox;

namespace PMS.Forms.Customer
{
    public partial class EditCustomer : Form
    {
        List<String[,]> ListPrefixTH = new List<string[,]>();
        List<String[,]> ListSex = new List<string[,]>();
        List<String[,]> ListProvince = new List<string[,]>();

        List<String[,]> ListDistrict = new List<string[,]>();
        List<String[,]> ListSubDistrict = new List<string[,]>();

        String FilePath = "";

        /// <summary> 
        /// SQLDefault (MySQL version, ไม่ใช้ stored procedure/CALL)
        /// <para>schema.table แทน database.dbo.tblXxx ของ SQL Server เดิม, คอลัมน์ AccountID เปลี่ยนเป็น UserLoginID
        /// (ยกเว้น placeholder {AccountID} ที่ถูก .Replace() ด้วยค่าจาก UserInfo.UserID ยังใช้ชื่อเดิม)</para>
        /// <para>[0] Select Prefix , Sex , province INPUT: -</para> 
        /// <para>[1] Select District INPUT: {ProvinceID}</para>
        /// <para>[2] Select SubDistrict INPUT: {DistrictID}</para>
        /// <para>[3] Insert Customer and Return ID INPUT: {IDCard} {PrefixID} {FName} {LName} {FNameEng} {LNameEng} {SexID} {BirthDay} {PhoneNo} {SubdistrictID} {HouseNum} {MooNum} {AlleyName} {RoadName} {DateCreateCard} {ExpirationDateCard} {Image} {AccountID}</para>
        /// <para>[4] Select ID  INPUT: {IDCard}  </para>
        /// <para>[5] Update Info Customer INPUT: {CustomerID} , {IDCard} , {PrefixID} , {FName} , {LName} , {FNameEng} , {LNameEng} , {SexID} , {BirthDay} , {PhoneNo} , {SubDistrictID} , {HouseNum} , {Moo} , {AlleyName} , {RoadName} , {DateCreateCard} , {DateEXPCard} , {Image}  {Note} {IsUse}</para>
        /// <para>[6]  SELECT CustomerInfo INPUT:  {CustomerID} {AccountID}</para>
        /// <para>[7]  SELECT Location Customer ReadCard INPUT: {ProvinceName} , {Districtname} , {SubDistrictName}</para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Select Prefix , Sex , province INPUT: -
           "SELECT PrefixName , PrefixID  \r\n " +
          " FROM basedata.prefix \r\n " +
          " WHERE IsUse = 1  \r\n " +
          " ORDER BY PrefixName;  \r\n " +
          "   \r\n " +
          " SELECT SexName , SexID  \r\n " +
          " FROM basedata.sex \r\n " +
          " ORDER BY SexName;  \r\n " +
          "   \r\n " +
          " SELECT ProvinceName , ProvinceID  \r\n " +
          " FROM basedata.province \r\n " +
          " ORDER BY ProvinceID;"
           ,

           //[1] Select District INPUT: {ProvinceID}
           "SELECT DistrictName , DistrictID \r\n " +
          "FROM basedata.district \r\n " +
          "WHERE ProvinceID = '{ProvinceID}' \r\n"+
          "ORDER BY DistrictName;"
           ,
           //[2] Select SubDistrict INPUT: {DistrictID}
           "SELECT SubDistrictName , SubDistrictID \r\n " +
          "FROM basedata.subdistrict \r\n " +
          "WHERE DistrictID = '{DistrictID}' \r\n " +
          "ORDER BY SubDistrictName;"
           ,
           //[3] Insert Customer and Return ID (ฟอร์มนี้ไม่ได้เรียกใช้จริง แต่คงไว้ตามเดิม)
           // INPUT: {IDCard} {PrefixID} {FName} {LName} {FNameEng} {LNameEng} {SexID} {BirthDay} {PhoneNo} {SubdistrictID} {HouseNum} {MooNum} {AlleyName} {RoadName} {DateCreateCard} {ExpirationDateCard} {Image} {AccountID}
           "INSERT INTO personal.customer \r\n " +
          "(IDCard, PrefixID, FName, LName, FNameEng, LNameEng, SexID, BirthDay, PhoneNo, SubDistrictID, HouseNum, Moo, AlleyName, RoadName, DateCreateCard, DateEXPCard, Image, DateAdd, IsUse, UserLoginID) \r\n " +
          "VALUES('{IDCard}', '{PrefixID}', N'{FName}', N'{LName}', '{FNameEng}', '{LNameEng}', '{SexID}', '{BirthDay}', '{PhoneNo}', '{SubdistrictID}', N'{HouseNum}', N'{MooNum}', N'{AlleyName}', N'{RoadName}', '{DateCreateCard}', '{ExpirationDateCard}', {Image}, NOW(), 1, '{AccountID}'); \r\n " +
          " \r\n " +
          "SELECT LAST_INSERT_ID();"
           ,
        //[4] Select ID  INPUT: {IDCard}  (ฟอร์มนี้ไม่ได้เรียกใช้จริง แต่คงไว้ตามเดิม)
         "SELECT CustomerID \r\n " +
         "FROM personal.customer \r\n " +
         "WHERE IDCard = '{IDCard}';"
        ,
         //[5] Update Info Customer INPUT: {CustomerID} , {IDCard} , {PrefixID} , {FName} , {LName} , {FNameEng} , {LNameEng} , {SexID} , {BirthDay} , {PhoneNo} , {SubDistrictID} , {HouseNum} , {Moo} , {AlleyName} , {RoadName} , {DateCreateCard} , {DateEXPCard} , {Image} , {Note} , {IsUse}
         // ใช้ user variable (@..) แทน DECLARE ของ T-SQL ได้เลยโดยไม่ต้องมี stored procedure
         "SET @CustomerID = '{CustomerID}'; \r\n " +
         " \r\n " +
         "INSERT INTO personal.log_changeinfocustomer \r\n " +
         "(CustomerID, IDCardOld, PrefixIDOld, FNameOld, LNameOld, FNameEngOld, LNameEngOld, SexIDOld, BirthDayOld, PhoneNoOld, SubDistrictIDOld, HouseNumOld, MooOld, AlleyNameOld, RoadNameOld, DateCreateCardOld, DateEXPCardOld, DateAddOld, IsUseOld, UserLoginID, DateTimeAdd, Note) \r\n " +
         "SELECT CustomerID, IDCard, PrefixID, FName, LName, FNameEng, LNameEng, SexID, BirthDay, PhoneNo, SubDistrictID, HouseNum, Moo, AlleyName, RoadName, DateCreateCard, DateEXPCard, DateAdd, IsUse, UserLoginID, NOW(), N'{Note}' \r\n " +
         "FROM personal.customer WHERE CustomerID = @CustomerID; \r\n " +
         " \r\n " +
         "UPDATE personal.customer \r\n " +
         "SET IDCard = '{IDCard}', PrefixID = '{PrefixID}', FName = N'{FName}', LName = N'{LName}', FNameEng = '{FNameEng}', LNameEng = '{LNameEng}', " +
         "SexID = '{SexID}', BirthDay = '{BirthDay}', PhoneNo = '{PhoneNo}', SubDistrictID = '{SubDistrictID}', HouseNum = N'{HouseNum}', Moo = N'{Moo}', " +
         "AlleyName = N'{AlleyName}', RoadName = N'{RoadName}', DateCreateCard = '{DateCreateCard}', DateEXPCard = '{DateEXPCard}', ImagePath = {Image}, IsUse = '{IsUse}', DateAdd = NOW() \r\n " +
         "WHERE CustomerID = @CustomerID; \r\n " +
         " \r\n " +
         "SELECT 'F';"
        ,
          //[6]  SELECT CustomerInfo INPUT:  {CustomerID} {AccountID}
          // แก้บั๊กจากต้นฉบับ: ของเดิม WHERE ไปเทียบ IDCard = '{CustomerID}' ซึ่งผิด (ค่า CustomerID ที่ส่งมาเป็นเลข CustomerID ไม่ใช่เลขบัตร)
          // เปลี่ยนเป็นเทียบกับคอลัมน์ CustomerID ให้ถูกต้อง
       "SELECT IDCard , PrefixID , FName , LName , FNameEng , LNameEng , SexID , BirthDay , PhoneNo , d.ProvinceID , c.DistrictID , a.SubDistrictID , HouseNum , Moo , AlleyName , RoadName , DateCreateCard , DateEXPCard , Imagepath , IsUse \r\n " +
         "      FROM personal.customer AS a \r\n " +
         "      LEFT JOIN basedata.subdistrict AS b ON a.SubDistrictID = b.SubDistrictID \r\n " +
         "      LEFT JOIN basedata.district AS c ON b.DistrictID = c.DistrictID \r\n " +
         "      LEFT JOIN basedata.province AS d ON c.ProvinceID = d.ProvinceID \r\n " +
         "      WHERE a.CustomerID = '{CustomerID}' AND a.UserLoginID = '{AccountID}';"
        ,
       
         //[7]  SELECT Location Customer ReadCard INPUT: {ProvinceName} , {Districtname} , {SubDistrictName}  
         // ใช้ user variable แทน DECLARE ของ T-SQL
         "SET @ProvinceID = (SELECT ProvinceID FROM basedata.province WHERE ProvinceName = N'{ProvinceName}' LIMIT 1); \r\n " +
         "SET @District = (SELECT DistrictID FROM basedata.district WHERE DistrictName = N'{Districtname}' AND ProvinceID = @ProvinceID LIMIT 1); \r\n " +
         "SET @SubDistrict = (SELECT SubDistrictID FROM basedata.subdistrict WHERE SubDistrictName = N'{SubDistrictName}' AND DistrictID = @District LIMIT 1); \r\n " +
         " \r\n " +
         "SELECT @ProvinceID , @District , @SubDistrict;"
        ,
         };
        String CUSTOMERID = "";
        private const string CustomerImageFolder = @"C:\PMS\CustomerProto\";
        public EditCustomer(String CustomerID)
        {
            InitializeComponent();

            BTReadCard2.BackColor = Color.RoyalBlue;
            BTReadCard2.BorderColor = Color.RoyalBlue;
            BTClear2.BackColor = Color.Maroon;
            BTClear2.BorderColor = Color.Maroon;
            BTSave2.BackColor = Color.SeaGreen;
            BTSave2.BorderColor = Color.SeaGreen;

            CUSTOMERID = CustomerID;
            PMS.Class.ReadIDCard.UsbNotification.RegisterUsbDeviceNotification(this.Handle);
            DataSet ds = SQL.InputMySQLDataSet(SQLDefault[0]);
            System.Windows.Forms.ComboBox[] cb = { CBPrefixThai, CBSex, CBProvince };
            List<String[,]>[] ls = { ListPrefixTH, ListSex, ListProvince };
            for (int y = 0; y < cb.Length; y++)
                if (ds.Tables[y].Rows.Count != 0)
                    for (int z = 0; z < ds.Tables[y].Rows.Count; z++)
                    {
                        cb[y].Items.Add(new PMS.Class.ComboboxInfo(ds.Tables[y].Rows[z][0].ToString(), ds.Tables[y].Rows[z][1].ToString()));
                        ls[y].Add(new string[,] { { ds.Tables[y].Rows[z][0].ToString(), ds.Tables[y].Rows[z][1].ToString() } });
                    }
            var dt = SQL.InputMySQLDataTable(SQLDefault[6]
                .Replace("{AccountID}", PMS.Class.UserInfo.UserID)
                .Replace("{CustomerID}", CustomerID));
            if (dt.Rows.Count != 0)
            {
                TBIDCard.Text = dt.Rows[0][0].ToString();
                loopSelectID(CBPrefixThai, dt.Rows[0][1].ToString());
                TBFirstNameThai.Text = dt.Rows[0][2].ToString();
                TBLastNameThai.Text = dt.Rows[0][3].ToString();
                TBFirstNameEng.Text = dt.Rows[0][4].ToString();
                TBLastNameEng.Text = dt.Rows[0][5].ToString();
                loopSelectID(CBSex, dt.Rows[0][6].ToString());
                DTPBirthDay.Value = Convert.ToDateTime(dt.Rows[0][7].ToString());
                TBPhoneNo.Text = dt.Rows[0][8].ToString();
                loopSelectID(CBProvince, dt.Rows[0][9].ToString());
                loopSelectID(CBDistrict, dt.Rows[0][10].ToString());
                loopSelectID(CBSubDistrict, dt.Rows[0][11].ToString());
                TBHouseNum.Text = dt.Rows[0][12].ToString();
                TBMoo.Text = dt.Rows[0][13].ToString();
                TBSoi.Text = dt.Rows[0][14].ToString();
                TBRoad.Text = dt.Rows[0][15].ToString();
                DTPCreateCard.Value = Convert.ToDateTime(dt.Rows[0][16].ToString());
                DTPEXPCard.Value = Convert.ToDateTime(dt.Rows[0][17].ToString());
                if (dt.Rows[0][18].ToString() != "")
                {
                    try
                    {
                        PTB.Image = Image.FromFile(CustomerImageFolder + dt.Rows[0][18].ToString());
                    }
                    catch
                    {
                        Console.WriteLine("Not Found Image");
                    }
                }
                CBStatus.SelectedIndex = Convert.ToBoolean(dt.Rows[0][19]) ? 1 : 0;
            }
        }
        private static void loopSelectID(System.Windows.Forms.ComboBox cb, String ID)
        {
            for (int x = 0; x < cb.Items.Count; x++)
            {
                var Info = (cb.Items[x] as PMS.Class.ComboboxInfo);
                if (Info.ID == ID)
                {
                    cb.SelectedIndex = x;
                    break;
                }
            }
        }
        private void AddCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
                BTReadCard_Click(sender, e);
            else if (e.KeyCode == Keys.Escape)
                if (TBIDCard.Text != "")
                    BTClear_Click(sender, e);
                else if (e.KeyCode == Keys.F5)
                    BTSave_Click(sender, e);
                else if (e.KeyCode == Keys.Escape)
                    this.Close();
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
                DataTable dt = SQL.InputMySQLDataTable(SQLDefault[4].Replace("{IDCard}", TBIDCard.Text).Replace("{AccountID}", PMS.Class.UserInfo.UserID));
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

                    if (dt.Rows.Count != 0)
                    {
                        DataSet ds = SQL.InputMySQLDataSet(SQLDefault[5]
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
                            .Replace("{Image}", imagePath)
                            .Replace("{AccountID}", PMS.Class.UserInfo.UserID)
                            .Replace("{CustomerID}", CUSTOMERID)
                            .Replace("{Note}", "อัพเดทข้อมูลลูกค้าให้เป็นข้อมูลล่าสุด")
                            .Replace("{IsUse}", CBStatus.SelectedIndex.ToString()));
                        if (ds.Tables.Count != 0)
                        {
                            if (ds.Tables[0].Rows.Count != 0)
                            {
                                PMSMessageBox.Show("บันทึกข้อมูลสำเร็จ", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Information);
                                this.Close();
                            }
                            else
                                PMSMessageBox.Show("บันทึกข้อมูลไม่สำเร็จโปรดลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
                        }
                        else
                            PMSMessageBox.Show("บันทึกข้อมูลไม่สำเร็จโปรดลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
                        //ทำระบบแก้ไขสมาาชิก อย่าลืมเก็บ log // หมายเหตุการแก้ไขจะเป็น อัพเดทข้อมูลบัตรประชาชน
                    }
                }
                else
                {
                    // ไม่ได้เลือกรูปใหม่ -> ส่ง NULL ตรง ๆ (ไม่มีเครื่องหมายคำพูดครอบแล้ว เพราะ SQLDefault[5] ใช้ Image = {Image} ตรง ๆ)
                    DataSet ds = SQL.InputMySQLDataSet(SQLDefault[5]
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
                        .Replace("{Image}", "NULL")
                        .Replace("{AccountID}", PMS.Class.UserInfo.UserID)
                        .Replace("{CustomerID}", CUSTOMERID)
                        .Replace("{Note}", "อัพเดทข้อมูลลูกค้าให้เป็นข้อมูลล่าสุด")
                        .Replace("{IsUse}", CBStatus.SelectedIndex.ToString()));
                    if (ds.Tables.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            PMSMessageBox.Show("บันทึกข้อมูลสำเร็จ", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Information);
                            this.Close();
                        }
                        else
                            PMSMessageBox.Show("บันทึกข้อมูลไม่สำเร็จโปรดลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
                    }
                    else
                        PMSMessageBox.Show("บันทึกข้อมูลไม่สำเร็จโปรดลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
                    //ทำระบบแก้ไขสมาาชิก อย่าลืมเก็บ log // หมายเหตุการแก้ไขจะเป็น อัพเดทข้อมูลบัตรประชาชน
                }
            }

        }

        private void CBPrefixThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void EditCustomer_Load(object s, EventArgs e)
        {

        }
        private void EditCustomer_Resize(object s, EventArgs e)
        {
            PMS.Class.GeneralFuntion.ChangeSizePanal(this, panel1);
        }
    }
}