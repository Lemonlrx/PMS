using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PMS.Class
{
    internal class GeneralFuntion
    {

        public static void OpenFilesPreview(PictureBox PTB, Image I)
        {
            if (I != null)
            {
                Size S = new Size(0, 0);

                int ImageWidth = I.Width;
                int ImageHeight = I.Height;

                int MaxWidth = PTB.Width;
                int MaxHeight = PTB.Height;

                if (ImageWidth > ImageHeight)
                    S = new Size(MaxWidth, MaxHeight / 2 + MaxHeight / 5);
                else if (ImageWidth < ImageHeight)
                    S = new Size(MaxWidth / 2 + MaxWidth / 5, MaxHeight);
                else
                    S = new Size(MaxWidth, MaxHeight);
                PTB.Image = (Image)(new Bitmap(I, S));
            }
            else
                Console.WriteLine("Don't Found Image File.");
        }

        public static Image ResizeImageWindowScreen(Image I)
        {
            while (I.Size.Width > Screen.PrimaryScreen.Bounds.Width - 100 || I.Size.Height > Screen.PrimaryScreen.Bounds.Height - 100)
            {
                I = (Image)(new Bitmap(I, new Size(I.Size.Width / 2 + I.Size.Width / 4, I.Size.Height / 2 + I.Size.Height / 4)));
            }
            if (I.Size.Width < 200 || I.Size.Height < 200)
            {
                I = (Image)(new Bitmap(I, new Size(I.Size.Width + 300, I.Size.Height + 300)));
            }
            return I;
        }

        // TextChange Event 
        public static void ProtectedTBInt(System.Windows.Forms.TextBox TB)
        {
            if (TB.Text.Length > 0)
            {
                if (Int32.TryParse(TB.Text, out int x) && x <= 0)
                {
                    TB.Text = "";
                }
                else if (!(Int32.TryParse(TB.Text, out int y)))
                {
                    TB.Text = "";
                }
            }
        }
        //public static void ProtectedTBIntVRJ(RJTextBox TB)
        //{
        //    if (TB.Text.Length > 0)
        //    {
        //        if (Int32.TryParse(TB.Text, out int x) && x <= 0)
        //        {
        //            TB.Text = "";
        //        }
        //        else if (!(Int32.TryParse(TB.Text, out int y)))
        //        {
        //            TB.Text = "";
        //        }
        //    }
        //}
        public static void ProtectedTBIntZeroAllow(System.Windows.Forms.TextBox TB)
        {
            if (TB.Text.Length > 0)
            {
                if (Int32.TryParse(TB.Text, out int x) && x < 0)
                {
                    TB.Text = "";
                }
                else if (!(Int32.TryParse(TB.Text, out int y)))
                {
                    TB.Text = "";
                }
            }
        }
        //public static void ProtectedTBIntZeroAllowVRJ(RJTextBox TB)
        //{
        //    if (TB.Text.Length > 0)
        //    {
        //        if (Int32.TryParse(TB.Text, out int x) && x < 0)
        //        {
        //            TB.Text = "";
        //        }
        //        else if (!(Int32.TryParse(TB.Text, out int y)))
        //        {
        //            TB.Text = "";
        //        }
        //    }
        //}

        // TextChange Event 
        public static void ProtectedTBBDouble(System.Windows.Forms.TextBox TB)
        {
            bool Lastinputisjud = false;
            if (TB.Text.Length > 0)
            {
                Lastinputisjud = TB.Text.Substring(TB.Text.Length - 1, 1).Contains(".");
                if (Lastinputisjud)
                {
                    String Text = TB.Text;
                    Text += "1";
                    if (Double.TryParse(Text, out double x) && x <= 0)
                    {
                        TB.Text = "";
                    }
                    else if (!(Double.TryParse(Text, out double y)))
                    {
                        TB.Text = "";
                    }
                }
                else
                {
                    if (Double.TryParse(TB.Text, out double x) && x <= 0)
                    {
                        TB.Text = "";
                    }
                    else if (!(Double.TryParse(TB.Text, out double y)))
                    {
                        TB.Text = "";
                    }

                }
            }
        }
        // TextChange Event 
        public static void ProtectedTBPercent(System.Windows.Forms.TextBox TB)
        {
            bool Lastinputisjud = false;
            if (TB.Text.Length > 0)
            {
                Lastinputisjud = TB.Text.Substring(TB.Text.Length - 1, 1).Contains(".");
                if (Lastinputisjud)
                {
                    String Text = TB.Text;
                    Text += "1";
                    if (Double.TryParse(Text, out double x) && x <= 0)
                    {
                        TB.Text = "";
                    }
                    else if (!(Double.TryParse(Text, out double y)))
                    {
                        TB.Text = "";
                    }
                    if (Double.TryParse(Text, out double v) && v > 100)
                        TB.Text = TB.Text.Replace(".", "");
                }
                else
                {
                    if (Double.TryParse(TB.Text, out double x) && x < 0)
                    {
                        TB.Text = "";
                    }
                    else if (!(Double.TryParse(TB.Text, out double y)))
                    {
                        TB.Text = "";
                    }

                }

                if (TB.Text.Contains("."))
                {
                    int Pos = 0;
                    for (int x = 0; x < TB.Text.Length; x++)
                    {
                        if (TB.Text.Substring(x, 1).ToString() == ".")
                        {
                            Pos = x;
                            break;
                        }
                    }
                    if (Pos - 1 > 2)
                    {
                        TB.Text = TB.Text.Substring(0, 2);
                    }
                    if (TB.Text.Length - Pos > 3)
                    {
                        TB.Text = TB.Text.Substring(0, TB.Text.Length - 1);
                    }
                    if (Pos == 0)
                        TB.Text = "";
                }
                else
                {
                    if (TB.Text.Length >= 3)
                        TB.Text = TB.Text.Substring(0, 3);
                }
                if (Double.TryParse(TB.Text, out double value) && value > 100)
                    TB.Text = "100";
            }
        }

        public static void ChangeSizePanal(Form myForm, Panel myPanal)
        {
            myPanal.Location = new System.Drawing.Point(myForm.Width / 2 - myPanal.Size.Width / 2,
            myForm.Height / 2 - myPanal.Size.Height / 2);
        }
        public static void ChangeSizeMaxPanel(Form myForm, Panel myPanal)
        {
            myPanal.Size = new Size(myForm.Size.Width - 20, myForm.Size.Height - 20);
            myPanal.Location = new System.Drawing.Point(myForm.Width / 2 - myPanal.Size.Width / 2, myForm.Height / 2 - myPanal.Size.Height / 2);
        }
        public static void CopyAndPlaceImageLoc(Image Image, String PathPlaceLocation, String FileName, String File_Extension = ".jpg")
        {
            if (Image != null)
            {
                Image.Save(PathPlaceLocation + "\\" + FileName + File_Extension);
            }
        }

    }
}
