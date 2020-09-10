using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        private readonly SOHUContext _context;

        public MenuRepository(SOHUContext context) : base(context)
        {
            this._context = context;
        }
    }
}
