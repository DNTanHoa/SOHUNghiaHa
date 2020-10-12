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
        public Product GetByMetaTitle(string metaTitle)
        {
            Product model = _context.Set<Product>().FirstOrDefault(item => item.MetaTitle.Equals(metaTitle));
            if (model == null)
            {
                model = _context.Set<Product>().FirstOrDefault(item => item.MetaDescription.Equals(metaTitle));
            }
            return model;
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
        public void InitializationByID(int ID)
        {
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@ID",ID)
                 };
                SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocProductInitializationByID", parameters);
            }
        }
        public void InitializationByIDAndParentID(int ID, int parentID)
        {
            if ((ID > 0) && (parentID > 0))
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@ID",ID),
                new SqlParameter("@ParentID",parentID)
                };
                SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocProductInitializationByIDAndParentID", parameters);
            }
        }
        public void InitializationByIDAndCategoryID(int ID, int categoryID)
        {
            if ((ID > 0) && (categoryID > 0))
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@ID",ID),
                new SqlParameter("@CategoryID",categoryID)
                };
                SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocProductInitializationByIDAndCategoryID", parameters);
            }
        }
        public List<Product> GetByCategoryIDToList(int categoryID)
        {
            return _context.Product.Where(item => item.CategoryID == categoryID).OrderByDescending(item => item.DateUpdated).ToList();
        }
        public List<Product> GetAllOrderByTitleToList()
        {
            return _context.Product.OrderBy(item => item.Title).ToList();
        }
        public List<ProductDataTransfer> GetDataTransferAllOrderByTitleToList()
        {
            DataTable table = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProductSelectAllItems");
            return SQLHelper.ToList<ProductDataTransfer>(table);
        }
    }
}
