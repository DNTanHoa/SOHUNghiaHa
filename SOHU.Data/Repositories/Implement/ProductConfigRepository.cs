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
    public class ProductConfigRepository : Repository<ProductConfig>, IProductConfigRepository
    {
        private readonly SOHUContext _context;

        public ProductConfigRepository(SOHUContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductConfigDataTransfer> GetDataTransfersByProductIDToList(int ProductID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProductID",ProductID)
            };
            DataTable table = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProductConfigSelectMultipleByProductID", parameters);
            return SQLHelper.ToList<ProductConfigDataTransfer>(table);
        }
    }
}
