using Microsoft.AspNetCore.Razor.Language;
using SOHU.Data.DataTransferObject;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SOHU.Data.Repositories
{
    public class ConfigRepository : Repository<Config>, IConfigRepository
    {
        private readonly SOHUContext _context;

        public ConfigRepository(SOHUContext context) : base(context)
        {
            _context = context;
        }
        public bool IsValidByGroupNameAndCodeAndCodeName(string groupName, string code, string codeName)
        {
            Config item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.CodeName.Equals(codeName));
            return item == null ? true : false;
        }
        public bool IsValidByGroupNameAndCodeAndCodeNameAndParentID(string groupName, string code, string codeName, int parentID)
        {
            return _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.CodeName.Equals(codeName) && item.ParentID == parentID) == null ? true : false;
        }
        public List<Config> GetByCodeToList(string code)
        {
            return _context.Config.Where(item => item.Code.Equals(code)).ToList();
        }
        public List<Config> GetMenuTopByParentIDToList(int parentID)
        {
            return _context.Config.Where(item => item.ParentID == parentID).OrderBy(item => item.SortOrder).ToList();
        }
        public List<Config> GetByGroupNameAndCodeToList(string groupName, string code)
        {
            return _context.Config.Where(item => item.GroupName.Equals(groupName) && item.Code.Equals(code)).OrderBy(item => item.CodeName).ToList();
        }
        public List<ConfigDataTransfer> GetDataTransferByParentIDToList(int parentID)
        {
            List<ConfigDataTransfer> list = new List<ConfigDataTransfer>();
            if (parentID > 0)
            {

                SqlParameter[] parameters =
                {
                new SqlParameter("@ParentID",parentID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocConfigSelectByParentID", parameters);
                list = SQLHelper.ToList<ConfigDataTransfer>(dt).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Parent = new ModelTemplate();
                    list[i].Parent.ID = int.Parse(dt.Rows[i]["ParentID"].ToString());
                    list[i].Parent.TextName = list[i].ParentName;
                }
            }
            return list;
        }
        public List<Config> GetByCRMAndProductCategoryToTree()
        {
            List<Config> list = new List<Config>();

            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocConfigSelectByCRMAndProductCategoryToTree");
            list = SQLHelper.ToList<Config>(dt).ToList();
            return list;
        }
        public string MenuTopSub(int parentID)
        {
            string menuTop = "";
            List<Config> list = GetMenuTopByParentIDToList(parentID);
            if (list.Count > 0)
            {
                StringBuilder txt = new StringBuilder();
                txt.AppendLine(@"<ul class='nav nav-pills'>");
                foreach (Config item in list)
                {
                    string url = AppGlobal.Domain + item.CodenameSub + "-" + item.ID;
                    if (GetMenuTopByParentIDToList(item.ID).Count > 0)
                    {
                        txt.AppendLine(@"<li class='dropdown'>");
                        txt.AppendLine(@"<a href='#' class='nav-link dropdown-toggle' style='font-size:14px; color:#000000;'>" + item.CodeName + "</a>");
                        txt.AppendLine(MenuTopSub(item.ID));
                        txt.AppendLine(@"</li>");
                    }
                    else
                    {
                        txt.AppendLine(@"<li>");
                        txt.AppendLine(@"<a href='" + url + "' class='nav-link' style='font-size:14px; color:#000000;'>" + item.CodeName + "</a>");
                        txt.AppendLine(@"</li>");
                    }

                }
                txt.AppendLine(@"</ul>");
                menuTop = txt.ToString();
            }
            return menuTop;
        }
        public string MenuTop()
        {
            string menuTop = MenuTopSub(40);
            return menuTop;
        }
    }
}
