using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly SOHUContext _context;

        public CartRepository(SOHUContext context) : base(context)
        {
            _context = context;
        }
    }
}
