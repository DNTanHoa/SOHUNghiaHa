using SOHU.Data.DataTransferObject;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public interface ICartDetailRepository : IRepository<CartDetail>
    {
        public List<CartDetailDataTransfer> GetDataTransferByCartIDToList(int CartID);
    }
}
