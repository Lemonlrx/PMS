using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Class
{
    class UserInfo
    {
        public static String UserID;
        public static String Name;
        public static String ShopName;
        public static String PhoneNo;
        public static String PhoneNo2;
        public static Image UserPic;
        public static Image PictureQR;
        public static String DisPlayName;
        public static String FName;
        public static String LName;
        public static String Email;
        public static Image ProfileImage;


        public static void SetUser(String userid, String name, String shopname, String phoneno, String phoneno2, Image userpic, Image pictureQR, String displayname, String fanme, String lname, String email, Image ProfilePic)
        {
            UserID = userid;
            Name = name;
            ShopName = shopname;
            PhoneNo = phoneno;
            PhoneNo2 = phoneno2;
            PictureQR = pictureQR;
            DisPlayName = displayname;
            FName = fanme;
            LName = lname;
            Email = email;
            ProfileImage = ProfilePic;
            UserPic = userpic;
        }

        public static String GetUserID()
        {
            return UserID;
        }
        public static String GetName()
        {
            return Name;
        }
        public static String GetShopname()
        {
            return ShopName;
        }
        public static String GetPhoneNo()
        {
            return PhoneNo;
        }
        public static String GetPhoneNo2()
        {
            return PhoneNo2;
        }
        public static Image GetUserPic()
        {
            return UserPic;
        }
        public static Image GetImageQR()
        {
            return PictureQR;
        }
        public static String GetDisplayName()
        {
            return DisPlayName;
        }
        public static String GetFName()
        {
            return FName;
        }
        public static String GetLName()
        {
            return LName;
        }
        public static String GetEmail()
        {
            return Email;
        }
        public static Image GetProfilePic()
        {
            return ProfileImage;
        }
    }
}
