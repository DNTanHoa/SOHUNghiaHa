using SOHU.Data.DataTransferObject;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public Product GetByMetaTitle(string metaTitle);
        public List<Product> GetAllOrderByTitleToList();
        public List<ProductDataTransfer> GetDataTransferAllOrderByTitleToList();
        public void InitializationByIDAndCategoryID(int ID, int categoryID);
        public void InitializationByIDAndParentID(int ID, int parentID);
        public void InitializationByID(int ID);
        public bool IsValidByTitle(string title);
        public ProductDataTransfer GetDataTransferByID(int ID);
        public List<Product> GetByCategoryIDToList(int categoryID);
    }
}
