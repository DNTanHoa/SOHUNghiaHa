using System.Data.SqlClient;
using SOHU.Data.DataTransferObject;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SOHU.Data.Repositories
{
    public class CartDetailRepository : Repository<CartDetail>, ICartDetailRepository
    {
        private readonly SOHUContext _context;

        public CartDetailRepository(SOHUContext context) : base(context)
        {
            _context = context;
        }

        public List<CartDetailDataTransfer> GetDataTransferByCartIDToList(int CartID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CartID",CartID)
            };
            DataTable table = SQLHelper.Fill(AppGlobal.ConectionString, "sprocCartDetailSelectByCartID", parameters);
            return SQLHelper.ToList<CartDetailDataTransfer>(table);
        }
    }
}
