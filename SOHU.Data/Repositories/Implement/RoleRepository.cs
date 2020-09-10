using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly SOHUContext _context;

        public RoleRepository(SOHUContext context) : base(context)
        {
            this._context = context;
        }
    }
}
