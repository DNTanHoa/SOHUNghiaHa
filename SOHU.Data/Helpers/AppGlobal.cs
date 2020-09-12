using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SOHU.Data.Helpers
{
    public class AppGlobal
    {
        #region Init
        public static DateTime InitDateTime => DateTime.Now;

        public static string InitString => string.Empty;

        public static string DateTimeCode => DateTime.Now.ToString("ddMMyyyyHHmmss");

        public static string InitGuiCode => Guid.NewGuid().ToString();
        #endregion

        #region AppSettings 
        public static string URLEmail
        {
            get
            {
                return "https://mail.google.com/mail/u/0/?view=cm&fs=1&to=" + EmailContact + "&su=" + Title + "&body=" + Domain + "&tf=1";
            }
        }
        public static string URLGoogleMap
        {
            get
            {
                return "https://www.google.com/maps/d/embed?mid=" + GoogleMap;
            }
        }
        public static string GoogleMap
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("GoogleMap").Value;
            }
        }
        public static string PhoneDisplay
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("PhoneDisplay").Value;
            }
        }
        public static string URLImages
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("URLImages").Value;
            }
        }
        public static string URLImagesProduct
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("URLImagesProduct").Value;
            }
        }
        public static string EditSuccess
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("EditSuccess").Value;
            }
        }

        public static string EditFail
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("EditFail").Value;
            }
        }

        public static string CreateSuccess
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CreateSuccess").Value;
            }
        }

        public static string CreateFail
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CreateFail").Value;
            }
        }

        public static string DeleteSuccess
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("DeleteSuccess").Value;
            }
        }

        public static string DeleteFail
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("DeleteFail").Value;
            }
        }

        public static string Success
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Success").Value;
            }
        }

        public static string Fail
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Fail").Value;
            }
        }

        public static string RedirectDefault
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("RedirectDefault").Value;
            }
        }

        public static string LoginFail
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("LoginFail").Value;
            }
        }

        public static string FileFTP
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("FileFTP").Value;
            }
        }

        public static string ConectionString
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("ConectionString").Value;
            }
        }

        public static string Domain
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Domain").Value;
            }
        }
        public static string DomainWebsite
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("DomainWebsite").Value;
            }
        }
        public static string SitemapFTP
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("SitemapFTP").Value;
            }
        }

        public static string CMSTitle
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CMSTitle").Value;
            }
        }

        public static string MD5Key
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MD5Key").Value;
            }
        }

        public static string PhoneContact
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("PhoneContact").Value;
            }
        }

        public static string EmailContact
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("EmailContact").Value;
            }
        }

        public static string AddressContact
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("AddressContact").Value;
            }
        }

        public static string Title
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Title").Value;
            }
        }

        public static string Facebook
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Facebook").Value;
            }
        }

        public static string Twitter
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Twitter").Value;
            }
        }

        public static string Youtube
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Youtube").Value;
            }
        }

        public static string FacebookTitle
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("FacebookTitle").Value;
            }
        }

        public static string TwitterTitle
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("TwitterTitle").Value;
            }
        }

        public static string YoutubeTitle
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("YoutubeTitle").Value;
            }
        }

        public static string TopMenuCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("TopMenuCode").Value;
            }
        }

        public static string MenuLeftCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MenuLeftCode").Value;
            }
        }

        public static string CarouselCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CarouselCode").Value;
            }
        }

        public static int ProductPageSize
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("ProductPageSize").Value);
            }
        }
        public static int AboutID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("AboutID").Value);
            }
        }

        public static string CategoryCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CategoryCode").Value;
            }
        }

        public static string PriceUnit
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("PriceUnit").Value;
            }
        }

        public static string MailUsername
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MailUsername").Value;
            }
        }

        public static string MailPassword
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MailPassword").Value;
            }
        }

        public static int MailSTMPPort
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("MailSTMPPort").Value);
            }
        }

        public static string TagCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("TagCode").Value;
            }
        }

        public static string TokenSecretKey
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("TokenSecretKey").Value;
            }
        }

        public static int TokenExpireMinute
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("TokenExpireMinute").Value);
            }
        }

        public static string Keywords
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Keywords").Value;
            }
        }

        public static string Description
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Description").Value;
            }
        }

        public static string Author
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Author").Value;
            }
        }

        public static string ServiceCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("ServiceCode").Value;
            }
        }

        public static string Slogan
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Slogan").Value;
            }
        }

        public static string Introduction
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Introduction").Value;
            }
        }

        #endregion
        #region Functions 
        public static string SetName(string fileName)
        {
            string fileNameReturn = fileName;
            fileNameReturn = fileNameReturn.ToLower();
            fileNameReturn = fileNameReturn.Replace("’", "-");
            fileNameReturn = fileNameReturn.Replace("“", "-");
            fileNameReturn = fileNameReturn.Replace("--", "-");
            fileNameReturn = fileNameReturn.Replace("+", "-");
            fileNameReturn = fileNameReturn.Replace("/", "-");
            fileNameReturn = fileNameReturn.Replace(@"\", "-");
            fileNameReturn = fileNameReturn.Replace(":", "-");
            fileNameReturn = fileNameReturn.Replace(";", "-");
            fileNameReturn = fileNameReturn.Replace("%", "-");
            fileNameReturn = fileNameReturn.Replace("`", "-");
            fileNameReturn = fileNameReturn.Replace("~", "-");
            fileNameReturn = fileNameReturn.Replace("#", "-");
            fileNameReturn = fileNameReturn.Replace("$", "-");
            fileNameReturn = fileNameReturn.Replace("^", "-");
            fileNameReturn = fileNameReturn.Replace("&", "-");
            fileNameReturn = fileNameReturn.Replace("*", "-");
            fileNameReturn = fileNameReturn.Replace("(", "-");
            fileNameReturn = fileNameReturn.Replace(")", "-");
            fileNameReturn = fileNameReturn.Replace("|", "-");
            fileNameReturn = fileNameReturn.Replace("'", "-");
            fileNameReturn = fileNameReturn.Replace(",", "-");
            fileNameReturn = fileNameReturn.Replace(".", "-");
            fileNameReturn = fileNameReturn.Replace("?", "-");
            fileNameReturn = fileNameReturn.Replace("<", "-");
            fileNameReturn = fileNameReturn.Replace(">", "-");
            fileNameReturn = fileNameReturn.Replace("]", "-");
            fileNameReturn = fileNameReturn.Replace("[", "-");
            fileNameReturn = fileNameReturn.Replace(@"""", "-");
            fileNameReturn = fileNameReturn.Replace(@" ", "-");
            fileNameReturn = fileNameReturn.Replace("á", "a");
            fileNameReturn = fileNameReturn.Replace("à", "a");
            fileNameReturn = fileNameReturn.Replace("ả", "a");
            fileNameReturn = fileNameReturn.Replace("ã", "a");
            fileNameReturn = fileNameReturn.Replace("ạ", "a");
            fileNameReturn = fileNameReturn.Replace("ă", "a");
            fileNameReturn = fileNameReturn.Replace("ắ", "a");
            fileNameReturn = fileNameReturn.Replace("ằ", "a");
            fileNameReturn = fileNameReturn.Replace("ẳ", "a");
            fileNameReturn = fileNameReturn.Replace("ẵ", "a");
            fileNameReturn = fileNameReturn.Replace("ặ", "a");
            fileNameReturn = fileNameReturn.Replace("â", "a");
            fileNameReturn = fileNameReturn.Replace("ấ", "a");
            fileNameReturn = fileNameReturn.Replace("ầ", "a");
            fileNameReturn = fileNameReturn.Replace("ẩ", "a");
            fileNameReturn = fileNameReturn.Replace("ẫ", "a");
            fileNameReturn = fileNameReturn.Replace("ậ", "a");
            fileNameReturn = fileNameReturn.Replace("í", "i");
            fileNameReturn = fileNameReturn.Replace("ì", "i");
            fileNameReturn = fileNameReturn.Replace("ỉ", "i");
            fileNameReturn = fileNameReturn.Replace("ĩ", "i");
            fileNameReturn = fileNameReturn.Replace("ị", "i");
            fileNameReturn = fileNameReturn.Replace("ý", "y");
            fileNameReturn = fileNameReturn.Replace("ỳ", "y");
            fileNameReturn = fileNameReturn.Replace("ỷ", "y");
            fileNameReturn = fileNameReturn.Replace("ỹ", "y");
            fileNameReturn = fileNameReturn.Replace("ỵ", "y");
            fileNameReturn = fileNameReturn.Replace("ó", "o");
            fileNameReturn = fileNameReturn.Replace("ò", "o");
            fileNameReturn = fileNameReturn.Replace("ỏ", "o");
            fileNameReturn = fileNameReturn.Replace("õ", "o");
            fileNameReturn = fileNameReturn.Replace("ọ", "o");
            fileNameReturn = fileNameReturn.Replace("ô", "o");
            fileNameReturn = fileNameReturn.Replace("ố", "o");
            fileNameReturn = fileNameReturn.Replace("ồ", "o");
            fileNameReturn = fileNameReturn.Replace("ổ", "o");
            fileNameReturn = fileNameReturn.Replace("ỗ", "o");
            fileNameReturn = fileNameReturn.Replace("ộ", "o");
            fileNameReturn = fileNameReturn.Replace("ơ", "o");
            fileNameReturn = fileNameReturn.Replace("ớ", "o");
            fileNameReturn = fileNameReturn.Replace("ờ", "o");
            fileNameReturn = fileNameReturn.Replace("ở", "o");
            fileNameReturn = fileNameReturn.Replace("ỡ", "o");
            fileNameReturn = fileNameReturn.Replace("ợ", "o");
            fileNameReturn = fileNameReturn.Replace("ú", "u");
            fileNameReturn = fileNameReturn.Replace("ù", "u");
            fileNameReturn = fileNameReturn.Replace("ủ", "u");
            fileNameReturn = fileNameReturn.Replace("ũ", "u");
            fileNameReturn = fileNameReturn.Replace("ụ", "u");
            fileNameReturn = fileNameReturn.Replace("ư", "u");
            fileNameReturn = fileNameReturn.Replace("ứ", "u");
            fileNameReturn = fileNameReturn.Replace("ừ", "u");
            fileNameReturn = fileNameReturn.Replace("ử", "u");
            fileNameReturn = fileNameReturn.Replace("ữ", "u");
            fileNameReturn = fileNameReturn.Replace("ự", "u");
            fileNameReturn = fileNameReturn.Replace("é", "e");
            fileNameReturn = fileNameReturn.Replace("è", "e");
            fileNameReturn = fileNameReturn.Replace("ẻ", "e");
            fileNameReturn = fileNameReturn.Replace("ẽ", "e");
            fileNameReturn = fileNameReturn.Replace("ẹ", "e");
            fileNameReturn = fileNameReturn.Replace("ê", "e");
            fileNameReturn = fileNameReturn.Replace("ế", "e");
            fileNameReturn = fileNameReturn.Replace("ề", "e");
            fileNameReturn = fileNameReturn.Replace("ể", "e");
            fileNameReturn = fileNameReturn.Replace("ễ", "e");
            fileNameReturn = fileNameReturn.Replace("ệ", "e");
            fileNameReturn = fileNameReturn.Replace("đ", "d");
            fileNameReturn = fileNameReturn.Replace("--", "-");
            return fileNameReturn;
        }
        public static string SetImagesFileName(string content)
        {
            string result = "";
            content = content.Replace("src=", "src=~");
            for (int i = 1; i < content.Split('~').Length; i++)
            {
                string text001 = content.Split('~')[i];
                if (text001.Split('"').Length > 1)
                {
                    text001 = text001.Split('"')[1];
                    if (text001.Split('.').Length > 1)
                    {
                        string extension = text001.Split('.')[text001.Split('.').Length - 1];
                        if ((extension == "jpg") || (extension == "png") || (extension == "gif") || (extension == "bmp"))
                        {
                            result = result + "~" + text001;
                        }
                    }

                }
            }
            result = result.Replace(@"~", @"");
            return result;
        }
        #endregion
    }
}
