using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMS.Class
{
    internal class MethodPrint
    {
        static Char[] Word = { 'ำ', 'ะ', 'ั', 'ี', 'ุ', 'ึ', 'เ', '็', '้', '๋', 'ิ', 'ื', '์', '.', ' ' };
        static Char[] ReplaceWord = { '่', '้', '๊', '๋', 'ุ', 'ู', 'ิ', 'ี', 'ึ', 'ื', '๋', '็', 'ั', 'ี', 'ุ', 'ึ', '็', '้', '๋', 'ิ', 'ื', '์' };

        public static Font THsarabun30 = new Font("TH Sarabun New", 30, FontStyle.Bold);
        public static Font THsarabun22 = new Font("TH Sarabun New", 22, FontStyle.Bold);
        public static Font THsarabun18 = new Font("TH Sarabun New", 18, FontStyle.Bold);
        public static Font THsarabun16 = new Font("TH Sarabun New", 16, FontStyle.Bold);
        public static Font THsarabun14 = new Font("TH Sarabun New", 14, FontStyle.Bold);
        public static Font THsarabun10 = new Font("TH Sarabun New", 10, FontStyle.Bold);
        public static Brush BlackBrushes = Brushes.Black;

        public static Pen BlackPen = new Pen(Color.Black);
        public static Pen RedPen = new Pen(Color.Red);
        public static Pen GreenPen = new Pen(Color.Green);

        //ขึ้นหน้าใหม่
        //e.HasMorePages = true;

        public static float OldLocationHeightIs = 0;
        public static float LocationHeightIs = 0;
        public static float LocationWidthIs = 0;
        public static int SpaceLine = 10;
        public static SizeF Size = new SizeF(0, 0);
        public static int NowRowsCount = 0;
        public static List<Point[]> PositionAddLine = new List<Point[]>();



        public static int[] FineCenterLocationInPage(PrintPageEventArgs e)
        {
            int[] Loc = new int[] { 0, 0 };
            Loc[0] = e.MarginBounds.Width / 2;
            Loc[1] = e.MarginBounds.Height / 2;
            return Loc;
        }
        public static void NewLineCenter(PrintPageEventArgs e, String Text, Font F, Brush B)
        {
            LocationWidthIs = 0;
            SizeF SizeString = e.Graphics.MeasureString(Text, F);
            float StartLoc = e.PageBounds.Width / 2 - SizeString.Width / 2;
            String ReplaceText = Text;
            for (int x = 0; x < ReplaceWord.Length; x++)
            {
                ReplaceText.Replace(ReplaceWord[x].ToString(), "");
            }
            e.Graphics.DrawString(Text, F, B, new PointF(StartLoc, LocationHeightIs));
            Size = e.Graphics.MeasureString(ReplaceText, F);
            OldLocationHeightIs = LocationHeightIs;
            LocationHeightIs += Size.Height / 2 + SpaceLine;
            LocationWidthIs = Size.Width;
        }

        public static void PrintOther(String[] PrinterInfo, PrintDocument PrintDoc, PrintDialog PrintDia)
        {
            try
            {
                String PrinterName = PrinterInfo[0].ToString();
                int WidPrinter = Convert.ToInt32(PrinterInfo[1].ToString());
                int HeigPrinter = Convert.ToInt32(PrinterInfo[2].ToString());
                int MarginPrinter = Convert.ToInt32(PrinterInfo[3].ToString());
                PrintDia.Document = PrintDoc;
                PrintDoc.DefaultPageSettings.PrinterSettings.PrinterName = PrinterName;
                PrintDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("MyPage", WidPrinter, HeigPrinter);
                PrintDoc.DefaultPageSettings.Margins = new Margins(MarginPrinter, MarginPrinter, MarginPrinter, MarginPrinter);
                PrintDoc.Print();
            }
            catch
            {
                Console.Write("Error X 459003425");
            }
        }

        public static void NewLineRight(PrintPageEventArgs e, string Text, Font F, Brush B, float X, float Y, float Pointplus, float Pointdelete)
        {
            LocationWidthIs = 0;
            SizeF SizeString = e.Graphics.MeasureString(Text, F);
            X = e.PageBounds.Width - e.MarginBounds.X - SizeString.Width;
            String ReplaceText = Text;
            for (int x = 0; x < ReplaceWord.Length; x++)
            {
                ReplaceText.Replace(ReplaceWord[x].ToString(), "");
            }
            e.Graphics.DrawString(Text, F, B, new PointF(Pointplus + X - Pointdelete, Y));
            Size = e.Graphics.MeasureString(ReplaceText, F);
            OldLocationHeightIs = LocationHeightIs;
            LocationHeightIs += Size.Height / 2 + SpaceLine;
            LocationWidthIs = Size.Width;
        }
        public static void NewLineLeft(PrintPageEventArgs e, string Text, Font F, Brush B, float X, float Y, float Pointplus, float Pointdelete)
        {
            LocationWidthIs = 0;
            Size = e.Graphics.MeasureString(Text, F);
            while (Size.Width > e.PageBounds.Size.Width)
            {
                String T1 = Text.Substring(0, e.PageBounds.Size.Width);
                String T2 = Text.Substring(e.PageBounds.Size.Width, Text.Length);
                Text = T1 + "\r\n" + T2;
            }
            String ReplaceText = Text;
            for (int x = 0; x < ReplaceWord.Length; x++)
            {
                ReplaceText.Replace(ReplaceWord[x].ToString(), "");
            }


            e.Graphics.DrawString(Text, F, B, new PointF(e.MarginBounds.X + Pointplus + X - Pointdelete, Y));
            Size = e.Graphics.MeasureString(ReplaceText, F);
            OldLocationHeightIs = LocationHeightIs;
            LocationHeightIs += Size.Height / 2 + SpaceLine;
            LocationWidthIs = Size.Width;
        }
        public static void NewLineLeftV2(PrintPageEventArgs e, string Text, Font F, Brush B, float X, float Y, float Pointplus, float Pointdelete)
        {
            LocationWidthIs = 0;
            Size = e.Graphics.MeasureString(Text, F);
            while (Size.Width > e.PageBounds.Size.Width)
            {
                try
                {
                    String T1 = Text.Substring(0, e.PageBounds.Size.Width);
                    String T2 = Text.Substring(e.PageBounds.Size.Width, Text.Length);
                    Text = T1 + "\r\n" + T2;
                }
                catch
                {
                    break;
                }
            }
            String ReplaceText = Text;
            for (int x = 0; x < ReplaceWord.Length; x++)
            {
                ReplaceText.Replace(ReplaceWord[x].ToString(), "");
            }


            e.Graphics.DrawString(Text, F, B, new PointF(Pointplus + X - Pointdelete, Y));
            Size = e.Graphics.MeasureString(ReplaceText, F);
            OldLocationHeightIs = LocationHeightIs;
            LocationHeightIs += Size.Height / 2 + SpaceLine;
            LocationWidthIs = Size.Width;
        }
        public static void ConcatTextInLine(PrintPageEventArgs e, String Text, Font F, Brush B, float Y, float Xplus, float Pointdelete)
        {
            String ReplaceText = Text;
            for (int x = 0; x < ReplaceWord.Length; x++)
            {
                ReplaceText.Replace(ReplaceWord[x].ToString(), "");
            }
            e.Graphics.DrawString(Text, F, B, new PointF(LocationWidthIs + 2 + Xplus, Y));
            Size = e.Graphics.MeasureString(ReplaceText, F);
            LocationWidthIs = Size.Width + LocationWidthIs;
        }
        public static void DrawingTable(PrintPageEventArgs e, DataTable dt, List<String> HeaderTable, List<float> HeaderTableLengthRatioMaxTen)
        {
            if (dt != null)
            {
                if (HeaderTable.Count != 0 && HeaderTableLengthRatioMaxTen.Count != 0)
                {
                    float SumHeaderTableLengthRatioMaxTen = 0;
                    for (int x = 0; x < HeaderTableLengthRatioMaxTen.Count; x++)
                        SumHeaderTableLengthRatioMaxTen += HeaderTableLengthRatioMaxTen[x];
                    if (SumHeaderTableLengthRatioMaxTen == 10)
                    {
                        //find the ratio
                        double ratio = e.MarginBounds.Width / 10.0;
                        //TopLine
                        e.Graphics.DrawLine(BlackPen, new Point(0, Convert.ToInt32(LocationHeightIs)), new Point(e.MarginBounds.Width, Convert.ToInt32(LocationHeightIs)));

                        PositionAddLine.Add(new Point[] { new Point(0, Convert.ToInt32(LocationHeightIs)), new Point(0, (int)(LocationHeightIs + 100)) });

                        for (int Rowss = 0; Rowss < dt.Columns.Count; Rowss++)
                            PlaceTextInTable(e, ratio, dt.Columns[Rowss].ToString(), THsarabun14, (int)HeaderTableLengthRatioMaxTen[Rowss]);

                        Size = e.Graphics.MeasureString(dt.Columns[0].ToString(), THsarabun14);
                        LocationHeightIs += Size.Height / 2 + SpaceLine;

                        for (int Row = 0; Row < dt.Rows.Count; Row++)
                        {
                            LocationWidthIs = 0;
                            for (int Col = 0; Col < dt.Columns.Count; Col++)
                                PlaceTextInTable(e, ratio, dt.Rows[Row][Col].ToString(), THsarabun14, (int)HeaderTableLengthRatioMaxTen[Col]);
                            Size = e.Graphics.MeasureString(dt.Rows[0][0].ToString(), THsarabun14);
                            LocationHeightIs += Size.Height / 2 + SpaceLine;
                        }
                        for (int PosLine = 0; PosLine < PositionAddLine.Count; PosLine++)
                            e.Graphics.DrawLine(BlackPen, PositionAddLine[PosLine][0], PositionAddLine[PosLine][1]);

                    }
                    else
                        Console.WriteLine("Input Length != 10");
                }
            }
        }
        public static void CreateNewTableColumnInLine(PrintPageEventArgs e, List<String> Text, Font F, Brush B, float PosX, float PosY, float Pointplus, float Pointdelete)
        {
            String SF = "อู่";
            float MaxSizeHeightString = e.Graphics.MeasureString(SF, F).Height;
            List<float> PositionTextDevide = new List<float>();
            float SizeColumnDivede = e.MarginBounds.Width / Text.Count;


            bool CheckCharBeyone = false;
            float CountBeyone = 0;
            for (int x = 0; x < Text.Count; x++)
            {
                if (CheckCharBeyone)
                {
                    if (e.Graphics.MeasureString(Text[x].ToString(), F).Width > SizeColumnDivede - CountBeyone && x != Text.Count - 1)
                    {
                        PositionTextDevide.Add((SizeColumnDivede * x));
                        CheckCharBeyone = true;
                        CountBeyone = e.Graphics.MeasureString(Text[x].ToString(), F).Width - (SizeColumnDivede - CountBeyone);
                        continue;
                    }
                    else
                    {
                        PositionTextDevide.Add(((SizeColumnDivede * x + CountBeyone)));
                        CheckCharBeyone = false;
                        CountBeyone = 0;
                    }
                }
                else
                {
                    if (e.Graphics.MeasureString(Text[x].ToString(), F).Width > SizeColumnDivede && x != Text.Count - 1)
                    {
                        PositionTextDevide.Add((SizeColumnDivede * x));
                        CheckCharBeyone = true;
                        CountBeyone = e.Graphics.MeasureString(Text[x].ToString(), F).Width - SizeColumnDivede;
                        continue;
                    }
                    else
                        PositionTextDevide.Add((SizeColumnDivede * x));

                }
            }
            if (Text.Count != 0)
                for (int x = 0; x < Text.Count; x++)
                {
                    e.Graphics.DrawString(Text[x], F, B, new PointF(e.MarginBounds.X + PositionTextDevide[x], PosY));
                }
            LocationHeightIs += Size.Height / 2 + SpaceLine;
        }
        public static void PlaceTextInTable(PrintPageEventArgs e, double ratio, String Text, Font F, float UnitRatio)
        {
            LocationWidthIs += float.Parse(ratio.ToString()) * UnitRatio;
            ratio = LocationWidthIs / 2 - Text.Length / 2;
            e.Graphics.DrawString(Text, F, BlackBrushes, new Point((int)ratio, (int)LocationHeightIs));
        }
        public static Font SetFontTHSaraban(int SizeFont, FontStyle FontStyle = FontStyle.Bold)
        {
            return new Font("TH Sarabun New", SizeFont, FontStyle);
        }
        public static void ResetInfoPrint()
        {
            OldLocationHeightIs = 0;
            LocationHeightIs = 0;
            LocationWidthIs = 0;
            SpaceLine = 10;
            Size = new SizeF(0, 0);
            NowRowsCount = 0;
            PositionAddLine.Clear();
        }
        public static void NewLineCenterV2(PrintPageEventArgs e, String Text, Font F, Brush B)
        {
            LocationWidthIs = 0;
            SizeF SizeString = e.Graphics.MeasureString(Text, F);
            float StartLoc = e.PageBounds.Width / 2 - SizeString.Width / 2;
            String ReplaceText = Text;
            for (int x = 0; x < ReplaceWord.Length; x++)
            {
                ReplaceText.Replace(ReplaceWord[x].ToString(), "");
            }
            e.Graphics.DrawString(Text, F, B, new PointF(StartLoc, LocationHeightIs));
            Size = e.Graphics.MeasureString(ReplaceText, F);
            OldLocationHeightIs = LocationHeightIs;
            LocationHeightIs += Size.Height / 2 + SpaceLine;
            LocationWidthIs = Size.Width;
        }
        public static void NewLineRightV2(PrintPageEventArgs e, string Text, Font F, Brush B, float X, float Y, float Pointplus, float Pointdelete)
        {
            LocationWidthIs = 0;
            String ReplaceText = Text;
            for (int x = 0; x < ReplaceWord.Length; x++)
            {
                ReplaceText = ReplaceText.Replace(ReplaceWord[x].ToString(), "");
            }
            SizeF SizeString = e.Graphics.MeasureString(ReplaceText, F);
            X = e.PageBounds.Width - SizeString.Width;
            e.Graphics.DrawString(Text, F, B, new PointF(Pointplus + X - Pointdelete - 30, Y));
            Size = e.Graphics.MeasureString(ReplaceText, F);
            OldLocationHeightIs = LocationHeightIs;
            LocationHeightIs += Size.Height / 2 + SpaceLine;
            LocationWidthIs = Size.Width;
        }
        public static void SignatureLine(PrintPageEventArgs e, Point StartLine, Point EndLine, Font F, Brush B)
        {
            LocationWidthIs = 0;
            Size = e.Graphics.MeasureString("()", F);
            e.Graphics.DrawString("(", F, B, StartLine);
            e.Graphics.DrawString(")", F, B, EndLine);
            e.Graphics.DrawLine(Pens.Black, new Point(StartLine.X + 3, StartLine.Y + Convert.ToInt32(Size.Height) - 8), new Point(EndLine.X + 3, EndLine.Y + Convert.ToInt32(Size.Height) - 8));
            LocationHeightIs += Size.Height / 2 + 50;
            LocationWidthIs = EndLine.X;
        }
    }
}
