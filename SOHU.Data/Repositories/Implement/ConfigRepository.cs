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
        public List<Config> GetByCodeToList(string code)
        {
            return _context.Config.Where(item => item.Code.Equals(code)).ToList();
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
    }
}
