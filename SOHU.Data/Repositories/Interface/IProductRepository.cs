using SOHU.Data.DataTransferObject;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public ProductDataTransfer GetDataTransferByID(int ID);
    }
}
