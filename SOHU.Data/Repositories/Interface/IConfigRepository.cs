using SOHU.Data.DataTransferObject;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public interface IConfigRepository : IRepository<Config>
    {
        public string MenuTop();
        public List<Config> GetMenuTopByParentIDToList(int parentID);
        public bool IsValidByGroupNameAndCodeAndCodeNameAndParentID(string groupName, string code, string codeName, int parentID);
        public bool IsValidByGroupNameAndCodeAndCodeName(string groupName, string code, string codeName);
        public List<ConfigDataTransfer> GetDataTransferByParentIDToList(int parentID);
        public List<Config> GetByCRMAndProductCategoryToTree();
        public List<Config> GetByCodeToList(string code);
        public List<Config> GetByGroupNameAndCodeToList(string groupName, string code);
    }
}
