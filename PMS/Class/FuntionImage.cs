using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace PMS.Class
{
    internal class FuntionImage
    {
        public static void OpenFilesPreview(PictureBox PTB, Image I)
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
        //public static void OpenFilesPreviewVRJ(RJPictureBox PTB, Image I)
        //{
        //    Size S = new Size(0, 0);
        //    int ImageWidth = I.Width;
        //    int ImageHeight = I.Height;

        //    int MaxWidth = PTB.Width;
        //    int MaxHeight = PTB.Height;

        //    if (ImageWidth > ImageHeight)
        //        S = new Size(MaxWidth, MaxHeight / 2 + MaxHeight / 5);
        //    else if (ImageWidth < ImageHeight)
        //        S = new Size(MaxWidth / 2 + MaxWidth / 5, MaxHeight);
        //    else
        //        S = new Size(MaxWidth, MaxHeight);
        //    PTB.Image = (Image)(new Bitmap(I, S));

        //}
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

        public static String SaveImageFromPTB(PictureBox PTB, String Path, String FileName)
        {
            Path += FileName + ".jpg";
            var ImageBit = new Bitmap(PTB.Image);
            ImageBit.Save(Path);
            Bitmap bmp = new Bitmap(PTB.Image.Width, PTB.Image.Height);

            ImageBit.Save(Path, System.Drawing.Imaging.ImageFormat.Jpeg);

            return Path;
        }

        /// <summary>
        /// <para>[Size Image]</para>
        /// <para>Full HD = 1920 x 1080 pixel</para>
        /// <para>720p = 1280 x 720 pixel</para>
        /// <para>480p = 854 x 480 pixel</para>
        /// <para>240p = 426 x 240 pixel</para>
        /// <para>144p = 256 x 144 pixel</para>
        /// </summary>
        public static Image ResizeImage(Image I, Size ResizeTo, int X = 50, int Y = 50)
        {

            int HeightImage = -1;
            int WidthImage = -1;
            HeightImage = I.Height;
            WidthImage = I.Width;
            if (HeightImage > WidthImage)
                ResizeTo = new Size(ResizeTo.Height, ResizeTo.Width);
            else if (WidthImage > HeightImage)
                ResizeTo = new Size(ResizeTo.Width, ResizeTo.Height);
            else
                ResizeTo = new Size(X, Y);
            Image NewImage = (Image)(new Bitmap(I, ResizeTo));
            return NewImage;
        }

        public static String ResizeFile(String LocalImage, String ID, String PathCreate)
        {
            String PathCreate2 = PathCreate + @"\" + ID + ".jpg";
            int x = 0;
            while (true)
            {
                if (!File.Exists(PathCreate2))
                {
                    break;
                }
                else if (x >= 0)
                {
                    PathCreate2 = PathCreate + ID + "_" + x + ".jpg";
                    x++;
                }
            }
            Bitmap BMP = new Bitmap(LocalImage);
            ImageCodecInfo jpgEncode = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            //ลดคุณภาพลงประมาณ 50 %
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            String newPathImage = PathCreate;
            BMP.Save(newPathImage, jpgEncode, myEncoderParameters);
            return newPathImage;
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static byte[] ReadImageToBinary(String LocationImage)
        {
            byte[] Image = null;
            FileStream FS = new FileStream(LocationImage, FileMode.Open, FileAccess.Read);
            BinaryReader BR = new BinaryReader(FS);
            Image = BR.ReadBytes((int)FS.Length);
            FS.Close();
            BR.Close();
            return Image;
        }
        public static byte[] ReadImageINPTBToBinary(Image I)
        {
            int x = 0;
            var dir = @"C:\LEMONLR-PROJECT";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            while (true)
            {
                using (Bitmap bmb = new Bitmap(I))
                {
                    try
                    {
                        dir = @"C:\LEMONLR-PROJECT\" + x + ".jpg";
                        bmb.Save(dir, bmb.RawFormat);
                        break;
                    }
                    catch
                    {
                        x++;
                    }
                }
            }
            byte[] Image = null;
            FileStream FS = new FileStream(dir, FileMode.Open, FileAccess.Read);
            BinaryReader BR = new BinaryReader(FS);
            Image = BR.ReadBytes((int)FS.Length);
            BR.Close();
            FS.Close();
            return Image;
        }
        public static byte[] ReadLocalToBinary(String LocalFile)
        {

            byte[] Image = null;
            FileStream FS = new FileStream(LocalFile, FileMode.Open, FileAccess.Read);
            BinaryReader BR = new BinaryReader(FS);
            Image = BR.ReadBytes((int)FS.Length);
            BR.Close();
            FS.Close();
            return Image;
        }
        public static Byte[] ImageToByteArray(Image imageIn)
        {
            var ms = new System.IO.MemoryStream(); // สร้างตัวรับข้อมูล Byte
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // แปลงภาพเป็น   bytes
            var Buffer = ms.GetBuffer(); // เก็บ Byte
            ms.Close();
            return Buffer;
        }
        public static Image ByteArrayToImage(dynamic bytesArr)
        {
            try
            {
                var returnType = bytesArr.GetType().Name; // ชื่อ ประเภทตัวเเปร ใน dynamic
                if (bytesArr.GetType() != typeof(byte[]) && bytesArr.GetType() == typeof(Bitmap)) // เช็คประเภทตัวเเปร ว่าเป็น  bytes หรือ ไม่
                {
                    bytesArr = ImageToByteArray(bytesArr);
                }
                if (bytesArr.GetType() == typeof(byte[]))
                {
                    var ms = new System.IO.MemoryStream();
                    ms.Write(bytesArr, 0, bytesArr.Length);
                    Image imgFromStream = Image.FromStream(ms);
                    return imgFromStream;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// <para> How to use  </para>
        /// <para> INPUT : image | variables : byte or imagr </para>
        /// </summary>
        public static void ImageToPB(dynamic image, System.Windows.Forms.PictureBox P)
        {
            P.Image = Class.FuntionImage.ByteArrayToImage(image);
            PMS.Class.GeneralFuntion.OpenFilesPreview(P, P.Image);
        }

    }
}
