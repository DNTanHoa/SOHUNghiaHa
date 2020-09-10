using SOHU.Data.Enum;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public interface IMembershipRepository : IRepository<Membership>
    {
        public bool IsValid(string account, string password);

        public Membership GetByAccount(string Username);

        public void InitBeforeSave(Membership model, InitType initType);
    }
}
