using BarcodeStandard;
using PMS.Controls;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using BarcodeLib;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PMS.Class.MethodPrint;

namespace PMS.Class
{
    internal static class PrintDoc
    {
        public static class ContractPledgenDocument
        {
            public static void StartFullPrinting(PrintPageEventArgs e, String IDBill, String Name, String IDCard, String PhoneNo, String Type, String Brand, String Model, String SN, String Addonordefect, String Note, int Money, DateTime Startdate, DateTime nolaterthandate)
            {
                if (e.PageBounds.Width >= 700)
                {
                    ////Detailbill
                    //String IDBill = "123456789854";
                    var bar = new Barcode();

                    ////CustomerInfo
                    //String Name = "นาย ปฏิพัทธ์ วัชราสิน";
                    //String IDCard = "1200901385049";
                    //String PhoneNo = "0610094347";

                    ////DetailItem
                    //String Categories = "เครื่องบินเจ็ท";
                    //String Brand = "ซัมซุง";
                    //String Model = "AE86";
                    //String SN = "กขค 9986";

                    //String Addonordefect = " - ";
                    //String Note = "มาม่าใส่ไข่ 2 ห่อ";
                    //int Money = 5000;
                    //DateTime nolaterthandate = DateTime.Now.AddDays(15);

                    ResetInfoPrint();
                    //Name IDcard PhoneNo
                    List<String> CustomerInfo = new List<String>();
                    CustomerInfo.Add("ข้าพเจ้า " + Name);
                    CustomerInfo.Add("เลขบัตรประจำตัวประชาชน " + IDCard);
                    CustomerInfo.Add("เบอร์ติดต่อ " + PhoneNo);
                    Image barcodeImage = bar.Encode(TYPE.CODE128, IDBill, Color.Black, Color.Transparent, 200, 30);

                    List<String> ItemInfo = new List<string>();
                    ItemInfo.Add("ประเภท " + Type);
                    ItemInfo.Add("ยี่ห้อ " + Brand);
                    ItemInfo.Add("รุ่น  " + Model);
                    ItemInfo.Add("SerialNumber " + SN);

                    LocationHeightIs += 60;
                    NewLineCenter(e, "ใบสัญญาขายฝาก", THsarabun30, BlackBrushes);

                    LocationHeightIs += 40;

                    NewLineRight(e, "เลขบิลที่ " + IDBill, THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineRight(e, "วันที่รับ " + Startdate.ToString("dd/MM/yyyy"), THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    e.Graphics.DrawImage(barcodeImage, new Point(e.PageBounds.Width - e.MarginBounds.X - 180, Convert.ToInt32(LocationHeightIs)));

                    LocationHeightIs += 40;

                    NewLineLeft(e, "ร้าน: " + PMS.Class.UserInfo.ShopName, THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);

                    if (PMS.Class.UserInfo.PhoneNo2 != "")
                        NewLineLeft(e, "เบอร์ติดต่อ: " + PMS.Class.UserInfo.PhoneNo + " - " + PMS.Class.UserInfo.PhoneNo2, THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    else
                        NewLineLeft(e, "เบอร์ติดต่อ: " + PMS.Class.UserInfo.PhoneNo, THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);

                    LocationHeightIs += 20;

                    CreateNewTableColumnInLine(e, CustomerInfo, THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    CreateNewTableColumnInLine(e, ItemInfo, THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "อุปกรณ์เสริม / ตำหนิ " + Addonordefect, THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    //NewLineLeft(e, "หมายเหตุ " + Note, THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "มาขายฝากให้กับร้านเป็นเงินจำนวน " + Money.ToString("#,##0") + " บาท ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "ข้าพเจ้าจะมาซื้อคืนพร้อมค่าบริการภายในวันที่ " + nolaterthandate.ToString("dd/MM/yyyy"), THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);

                    LocationHeightIs += 30;

                    NewLineLeft(e, "     ข้าพเจ้าผู้ขายฝากขอรับรองว่าทรัพย์สินที่นำมาฝากขายไว้เป็นกรรมสิทธิ์โดยชอบธรรมของข้าพเจ้าเอง ซึ่งได้มาโดยชอบด้วยกฎหมาย ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "หากทรัพย์สินดังกล่างข้างต้นนี้ ได้มาโดยมิชอบด้วยกฎหมายในทุกมาตราฯ เช่น การโจรกรรม ลักทรัพย์ ยักยอกทรัพย์ และอื่นๆ ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "ซึ่งมิได้แจ้งให้กับผู้รับฝากทราบ  ข้าพเจ้าผู้ขายฝากยินยอมขอรับผิดชอบตามกฎหมายแต่เพียงผู้เดียวทั้งหมด ขอยินยอมชดใช้ค่าใช้จ่ายต่างๆ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "และค่าใช้จ่ายเกี่ยวเนื่องที่จะเกิดขึ้นทั้งหมด ในทุกกรณี ของทางด้านกฎหมายและผู้รับฝากโดยผู้รับฝากมิต้องมีส่วนร่วมเกี่ยวข้อง", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "และรับผิดชอบทุกสิ่งอย่างใดๆที่จะเกิดขึ้นทั้งสิ้นทุกกรณี", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "    เงื่อนไขการให้บริการ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "1. ) หากครบกำหนดแล้ว ผู้ขายฝากไม่ได้ติดต่อกับผู้ขายฝาก เกิน 5 วันหลังจากวันที่ครบกำหนดตามสัญญาข้างต้น", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "ผู้ขายฝากยินยอมมอบให้เป็น กรรมสิทธิ์ของผู้ขายฝากโดยไม่ต้องบอกกล่าวให้ผู้ฝากขายรับทราบและผู้รับฝากสามารถนำทรัพย์สินดังกล่าว", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "ไปเพื่อจำหน่ายขายทอดตลาดได้ โดยไม่มีข้อเรียกร้องเพิ่มเติมอื่นๆทั้งสิ้น ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "2. ) ใบขายฝากนี้ผู้ขายฝากต้องรักษาอย่างดี หากมีข้อผิดพลาดใดเกิดขึ้นทางผู้รับฝากจะไม่รับผิดชอบใดๆทั้งสิ้น  ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "หากใบขายฝากหายผู้ขายจะไม่สามารถ รับสินค้าคืนได้ นอกเสียจากมีหลักฐานยืนยันการครอบครองทรัพย์มาแจ้ง", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "กับผู้รับฝากและถูกต้องตามกฎหมายเท่านั้น ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "3. ) ผู้ขายฝากต้องนำบัตรประจำตัวประชาชนหรือเอกสารที่ทางราชการเป็นผู้ออกให้ มาขอรับซื้อทรัพย์ดังกล่าวที่ฝากขายคืนด้วยทุกครั้ง ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "4. ) การรับคืนทรัพย์ขายฝาก ผู้รับคืนต้องเป็นผู้ทำสัญญา หรือมอบอำนาจ และใช้เอกสารทางราชการรับรองตัวจริงในการรับคืนทรัพย์เท่านั้น ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);


                    LocationHeightIs += 30;

                    NewLineLeft(e, "    หมายเหตุ  กรณีทางผู้รับฝากทรัพย์เกิดสูญหายหรือ สูญเสีย ซึ่งพิสูจน์ได้ว่าผู้รับฝากได้ถูกโจรกรรมหรือเนื่องจากภัยธรรมชาติ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "ทางผู้รับฝากไม่ต้องชดใช้หรือรับผิดชอบกับทรัพย์ที่รับฝากใดๆทั้งสิ้น ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "     ข้าพเจ้าได้อ่านเงื่อนไขที่กำหนดไว้ ได้ทราบเข้าใจถี่ถ้วนดีแล้ว และตกลงตามเงื่อนไขในเอกสารฉบับนี้ทุกประการ ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "พร้อมทั้งได้รับเงินถูกต้องตาม ต้องชดใช้หรือรับผิดชอบกับทรัพย์ที่รับฝากใดๆทั้งสิ้น ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    NewLineLeft(e, "จำนวนแล้วจึงได้ลงลายมือซึ่งไว้เป็นหลักฐานฯ ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);

                    LocationHeightIs += 50;

                    NewLineLeft(e, "ลงชื่อ (.............................................) ผู้ขายฝาก                                       ลงชื่อ (.............................................) ผู้รับขายฝาก ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);

                    LocationHeightIs += 20;

                    NewLineLeft(e, "ลงชื่อ(.............................................) พยาน                                             ลงชื่อ(.............................................) พยาน ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                    ResetInfoPrint();

                }
                else
                {
                    PMSMessageBox.Show("ไม่สามารถปริ้นได้เนื่องจากขนาดกระดาษ ไม่สมส่วนกับสิ่งที่จะพิมพ์ ", "ระบบ", MessageBoxButtons.OK, PMSMessageBox.PMSMessageIcon.Error);
                    e.Cancel = true;
                }
            }
            public static void StartBillPrinting(PrintPageEventArgs e, String IDBill, String Name, String IDCard, String PhoneNo, String Type, String Brand, String Model, String SN, String Addonordefect, String Note, int Money, DateTime StartDate, DateTime nolaterthandate)
            {

                ////Detailbill
                var bar = new Barcode();
                Image barcodeImage = bar.Encode(TYPE.CODE128, IDBill, Color.Black, Color.Transparent, e.PageBounds.Width - 30, 30);

                ResetInfoPrint();
                //Name IDcard PhoneNo
                List<String> CustomerInfo = new List<String>();
                CustomerInfo.Add("ข้าพเจ้า " + Name);
                CustomerInfo.Add("เลขบัตรประจำตัวประชาชน " + IDCard);
                CustomerInfo.Add("เบอร์ติดต่อ " + PhoneNo);

                List<String> ItemInfo = new List<string>();
                ItemInfo.Add("ประเภท " + Type);
                ItemInfo.Add("ยี่ห้อ " + Brand);
                ItemInfo.Add("รุ่น  " + Model);
                ItemInfo.Add("SerialNumber " + SN);


                NewLineCenterV2(e, "ใบสัญญาขายฝาก", THsarabun22, BlackBrushes);
                NewLineRightV2(e, "เลขบิล " + IDBill, THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                if (StartDate.Hour == 0 && StartDate.Minute == 0)
                    NewLineRightV2(e, "วันที่รับ " + StartDate.ToString("dd/MM/yyyy"), THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                else
                    NewLineRightV2(e, "วันที่รับ " + StartDate.ToString("dd/MM/yyyy เวลา HH:mm:ss"), THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);

                LocationHeightIs += 15;

                if (PMS.Class.UserInfo.PictureQR != null)
                    e.Graphics.DrawImage(PMS.Class.FuntionImage.ResizeImage(PMS.Class.UserInfo.PictureQR, new System.Drawing.Size(85, 85), 85, 85), new Point(e.PageBounds.Width - 100, Convert.ToInt32(LocationHeightIs)));
                LocationHeightIs += 21;
                NewLineLeftV2(e, "ร้าน: " + PMS.Class.UserInfo.ShopName, THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);

                NewLineLeftV2(e, "เบอร์ติดต่อ: " + PMS.Class.UserInfo.PhoneNo, THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                if (PMS.Class.UserInfo.PhoneNo2 != "")
                    NewLineLeftV2(e, "เบอร์ติดต่อสำรอง: " + PMS.Class.UserInfo.PhoneNo2, THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                LocationHeightIs += 35;

                e.Graphics.DrawImage(barcodeImage, new Point((e.PageBounds.Width / 2) - (barcodeImage.Width) / 2, Convert.ToInt32(LocationHeightIs)));
                LocationHeightIs += 60;
                NewLineLeftV2(e, CustomerInfo[0], THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, CustomerInfo[1], THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, CustomerInfo[2], THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "ได้นำสินค้าประเภท " + Type, THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, ItemInfo[1], THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, ItemInfo[2], THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, ItemInfo[3], THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "อุปกรณ์เสริม / ตำหนิ " + Addonordefect, THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "มาขายฝากเป็นจำนวนเงิน " + Money.ToString("#,##0") + " บาท", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "ครบกำหนดวันที่ " + nolaterthandate.ToString("dd/MM/yyyy"), THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);

                LocationHeightIs += 30;

                NewLineLeftV2(e, "เงื่อนไขการให้บริการ", THsarabun14, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "    สินค้าที่ทางร้านได้รับขายฝากดังกล่าว หากครบกำหนดแล้ว", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "ลูกค้าไม่ติดต่อกับทางร้านภายใน 5 วันหลังจากวันที่ครบกำหนดตาม", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "สัญญาข้างต้นผู้ลงนาม (ลูกค้า) ยินยอมยกให้เป็นกรรมสิทของทางร้าน", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "สามารถนำไปเพื่อจำหน่ายขายทอดตลาดได้โดยไม่มีข้อเรียกร้องเพิ่มเติม", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "อื่นๆทั้งสิ้น และ สินทรัพย์ ดังกล่าวนี้ซึ่งได้มา โดยมิชอบด้วยกฏหมายใน", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "ทุกมาตราเช่น การโจรกรรม ลักทรัพย์ ยักยอกทรัพย์ รับของโจรและข้อ", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "อื่นๆซึงได้มาโดยมิได้แจ้งให้ ทางร้านทราบ ผู้ลงนาม (ลูกค้า) ยินยอมเป็น", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "ผู้รับผิดชอบตามกฏหมายด้วยตนเองทั้งหมด รวมทั้งค่าใช้จ่ายต่างๆที่จะ", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "เกิดขึ้นในทุกกรณีกับทางร้าน ผู้ลงนาม (ลูกค้า) ที่เกิดขึ้นโดยทางร้าน", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "ไม่ต้องมีส่วนร่วมเกี่ยวข้องและรับผิดชอบใดๆที่จะเกิดขึ้นทั้งสิ้น", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);

                LocationHeightIs += 15;


                NewLineLeftV2(e, "การรับคืนสินค้าผู้รับคืนต้องเป็นผู้ทำสัญญา หรือมอบอำนาจ", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "และใช้เอกสารรับรองตัวจริงในการรับคืนสินค้าเท่านั้น", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "** กรณีที่ต้องการรับรถยนต์หรือมอเตอร์ไซคืน", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);
                NewLineLeftV2(e, "   กรุณาแจ้งทางร้านก่อน 1 วัน", THsarabun10, BlackBrushes, 0, LocationHeightIs, 0, 0);

                LocationHeightIs += 35;

                NewLineCenterV2(e, "ลงชื่อผู้ขายฝาก", THsarabun14, BlackBrushes);
                LocationHeightIs += 50;
                SignatureLine(e, new Point(0 + 50, Convert.ToInt32(LocationHeightIs)), new Point(e.PageBounds.Width - 50, Convert.ToInt32(LocationHeightIs)), THsarabun10, BlackBrushes);

                NewLineCenterV2(e, "ลงชื่อผู้รับฝาก", THsarabun14, BlackBrushes);
                LocationHeightIs += 50;
                SignatureLine(e, new Point(0 + 50, Convert.ToInt32(LocationHeightIs)), new Point(e.PageBounds.Width - 50, Convert.ToInt32(LocationHeightIs)), THsarabun10, BlackBrushes);



            }
            public static void StartBarCodeSticker(PrintPageEventArgs e, String Barcode, String Name, String Money, DateTime StartDate, String ItemName)
            {
                ResetInfoPrint();
                //Size All = 92 px or 42 mm 
                var qr = new QRCodeGenerator();
                var data = qr.CreateQrCode(Barcode, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(data);
                while (LocationHeightIs < e.PageBounds.Height - 100)
                {
                    SpaceLine = 8;
                    e.Graphics.DrawImage(qrCode.GetGraphic(4), new Point(0, Convert.ToInt32(LocationHeightIs)));
                    LocationHeightIs += 12;
                    NewLineLeft(e, "เลขบิล " + Barcode, THsarabun10, BlackBrushes, 0, LocationHeightIs, 70, 0);
                    NewLineLeft(e, "วันที่รับ " + StartDate.ToString("dd/MM/yyyy"), THsarabun10, BlackBrushes, 0, LocationHeightIs, 70, 0);
                    NewLineLeft(e, "สินค้า : " + ItemName, THsarabun10, BlackBrushes, 0, LocationHeightIs, 70, 0);
                    NewLineLeft(e, "จำนวน " + Convert.ToInt32(Money).ToString("#,##0") + " บาท", THsarabun10, BlackBrushes, 0, LocationHeightIs, 70, 0);
                    NewLineLeft(e, "ชื่อ " + Name, THsarabun10, BlackBrushes, 0, LocationHeightIs, 70, 0);
                    LocationHeightIs += 20;
                    e.Graphics.DrawLine(BlackPen, -999, LocationHeightIs - 4, 999, LocationHeightIs - 4);
                    if (LocationHeightIs >= e.PageBounds.Height - 100)
                        break;
                }
            }
        }
    }
}
