using System.Data.SqlClient;
using SOHU.Data.DataTransferObject;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SOHU.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly SOHUContext _context;

        public ProductRepository(SOHUContext context) : base(context)
        {
            _context = context;
        }
        public bool IsValidByTitle(string title)
        {
            Product item = _context.Set<Product>().FirstOrDefault(item => item.Title.Equals(title));
            return item == null ? true : false;
        }
        public ProductDataTransfer GetDataTransferByID(int ID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ID",ID)
            };
            DataTable table = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProductSelectSingleByID", parameters);
            return SQLHelper.ToList<ProductDataTransfer>(table).FirstOrDefault() ?? new ProductDataTransfer();
        }
        public List<Product> GetByCategoryIDToList(int categoryID)
        {
            return _context.Product.Where(item => item.CategoryId == categoryID).OrderByDescending(item => item.DateUpdated).ToList();
        }
    }
}
