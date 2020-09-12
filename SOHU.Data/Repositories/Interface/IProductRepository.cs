using SOHU.Data.DataTransferObject;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public bool IsValidByTitle(string title);
        public ProductDataTransfer GetDataTransferByID(int ID);
        public List<Product> GetByCategoryIDToList(int categoryID);
    }
}
