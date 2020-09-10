using SOHU.Data.DataTransferObject;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public interface IConfigRepository : IRepository<Config>
    {
        public List<Config> GetByCodeToList(string Code);
    }
}
