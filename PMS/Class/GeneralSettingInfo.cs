using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Class
{
    internal class GeneralSettingInfo
    {
        public static class GetInfoPrinter
        {
            public static String PrinterMain;
            public static String PrinterBill;
            public static String PrinterBarcode;

            public static String WidMain;
            public static String WidBill;
            public static String WidBarCode;

            public static String HeigMain;
            public static String HeigBill;
            public static String HeigBarCode;

            public static String MarginMain;
            public static String MarginBill;
            public static String MarginBarCode;

            public static void SetUser(String printermain, String printerbill, String printrbarcode, String widmain, String widbill, String widbarcode, String heigmain, String heigbill, String heigbarcode, String marginmain, String marginbill, String marginbarcode)
            {
                PrinterMain = printermain;
                PrinterBill = printerbill;
                PrinterBarcode = printrbarcode;

                WidMain = widmain;
                WidBill = widbill;
                WidBarCode = widbarcode;

                HeigMain = heigmain;
                HeigBill = heigbill;
                HeigBarCode = heigbarcode;

                MarginMain = marginmain;
                MarginBill = marginbill;
                MarginBarCode = marginbarcode;

            }

            public static String[] PrinterMainInfo()
            {
                return new string[] { PrinterMain, WidMain, HeigMain, MarginMain };
            }
            public static String[] PrinterBilleInfo()
            {
                return new String[] { PrinterBill, WidBill, HeigBill, MarginBill };
            }
            public static String[] PrinterBarCodeInfo()
            {
                return new String[] { PrinterBarcode, WidBarCode, HeigBarCode, MarginBarCode };
            }
        }
    }
}
