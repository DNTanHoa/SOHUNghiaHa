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

        public static string DateTimeCode => DateTime.Now.ToString("yyyyMMddHHmmss");

        public static string InitGuiCode => Guid.NewGuid().ToString();
        #endregion

        #region AppSettings 
        public static string URLImagesCustomer
        {
            get
            {
                string result = AppGlobal.Images + "/Project";
                return result;
            }
        }
        public static string URLImagesProduct
        {
            get
            {
                string result = AppGlobal.Images + "/Product";
                return result;
            }
        }
        public static string LogoURLFull
        {
            get
            {
                string result = AppGlobal.Domain + AppGlobal.Images + "/" + AppGlobal.Logo;
                return result;
            }
        }
        public static string LoadingURLFull
        {
            get
            {
                string result = AppGlobal.Domain + AppGlobal.Images + "/" + AppGlobal.Loading;
                return result;
            }
        }
        public static string EAN13CountryCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("EAN13CountryCode").Value;
            }
        }
        public static string EAN13ManufacturerCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("EAN13ManufacturerCode").Value;
            }
        }
        public static string Images
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Images").Value;
            }
        }
        public static string Logo
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Logo").Value;
            }
        }
        public static string Loading
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Loading").Value;
            }
        }
        public static int GuestID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("GuestID").Value);
            }
        }
        public static int SellID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("SellID").Value);
            }
        }
        public static int NghiaHaID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("NghiaHaID").Value);
            }
        }
        public static int EuronailsupplyID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("EuronailsupplyID").Value);
            }
        }
        public static int ProductCategoryRootID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("ProductCategoryRootID").Value);
            }
        }
        public static int InvoiceImportID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("InvoiceImportID").Value);
            }
        }
        public static int InvoiceExportID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("InvoiceExportID").Value);
            }
        }
        public static int ShoppingCartID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("ShoppingCartID").Value);
            }
        }
        public static int UnitID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("UnitID").Value);
            }
        }
        public static int GBPID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("GBPID").Value);
            }
        }
        public static int USDID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("USDID").Value);
            }
        }
        public static int LanID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("LanID").Value);
            }
        }
        public static int NhanCongID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("NhanCongID").Value);
            }
        }
        public static int ThiCongID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("ThiCongID").Value);
            }
        }
        public static int NhanSuID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("NhanSuID").Value);
            }
        }

        public static int ChamCongID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("ChamCongID").Value);
            }
        }
        public static int DuAnID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DuAnID").Value);
            }
        }
        public static int ChaoGiaID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("ChaoGiaID").Value);
            }
        }
        public static int DuToanID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DuToanID").Value);
            }
        }
        public static int Tax
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("Tax").Value);
            }
        }
        public static int DateBegin
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DateBegin").Value);
            }
        }
        public static int DateEnd
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DateEnd").Value);
            }
        }
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
                return "https://www.google.com/maps/d/embed?mID=" + GoogleMap;
            }
        }
        public static string InvoiceCategory
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("InvoiceCategory").Value;
            }
        }
        public static string CustomerCategory
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CustomerCategory").Value;
            }
        }
        public static string ProductCategory
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("ProductCategory").Value;
            }
        }
        public static string Unit
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Unit").Value;
            }
        }
        public static string CRM
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CRM").Value;
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

        public static string Error
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Error").Value;
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
        public static int InvoiceInputID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("InvoiceInputID").Value);
            }
        }
        public static int CustomerParentID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("CustomerParentID").Value);
            }
        }
        public static int SupplierParentID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("SupplierParentID").Value);
            }
        }
        public static int EmployeeParentID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("EmployeeParentID").Value);
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
            if (!string.IsNullOrEmpty(fileNameReturn))
            {
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
            }
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
        public static string ConvertDecimalToString(decimal number)
        {
            string s = number.ToString("#");
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;
            string str = " ";
            bool booAm = false;
            decimal decS = 0;
            //Tung addnew
            try
            {
                decS = Convert.ToDecimal(s.ToString());
            }
            catch
            {
            }
            if (decS < 0)
            {
                decS = -decS;
                s = decS.ToString();
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                str = so[0] + str;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        str = hang[j] + str;
                    j++;
                    if (j > 3) j = 1;
                    if ((donvi == 1) && (chuc > 1))
                    {
                        if (str.Length > 0)
                        {
                            str = "mốt " + str;
                        }
                        else
                        {
                            str = "một " + str;
                        }
                    }
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            str = "lăm " + str;
                        else if (donvi > 0)
                            str = so[donvi] + " " + str;
                    }
                    if (chuc < 0)
                        break;
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                        if (chuc == 1) str = "mười " + str;
                        if (chuc > 1) str = so[chuc] + " mươi " + str;
                    }
                    if (tram < 0) break;
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                    }
                    str = " " + str;
                }
            }
            if (booAm) str = "Âm " + str;
            return str + "đồng chẵn";
        }
        #endregion
    }
    public class YearFinance
    {
        public int Display { get; set; }
        public YearFinance()
        {
        }
        public static List<YearFinance> GetAllToList()
        {
            List<YearFinance> list = new List<YearFinance>();
            for (int i = AppGlobal.DateBegin; i <= AppGlobal.DateEnd; i++)
            {
                YearFinance model = new YearFinance();
                model.Display = i;
                list.Add(model);
            }
            return list;
        }
    }
    public class MonthFinance
    {
        public int Display { get; set; }
        public MonthFinance()
        {
        }
        public static List<MonthFinance> GetAllToList()
        {
            List<MonthFinance> list = new List<MonthFinance>();
            for (int i = 1; i <= 12; i++)
            {
                MonthFinance model = new MonthFinance();
                model.Display = i;
                list.Add(model);
            }
            return list;
        }
    }
}
