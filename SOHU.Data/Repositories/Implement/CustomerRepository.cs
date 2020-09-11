using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly SOHUContext context;

        public CustomerRepository(SOHUContext context): base(context)
        {
            this.context = context;
        }
    }
}
