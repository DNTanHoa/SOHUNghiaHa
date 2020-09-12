using SOHU.Data.Models;
using System;
using System.Collections.Generic;
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
    }
}
