using SOHU.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Mobile.Services.Membership
{
    public interface IMembershipService
    {
        List<Customer> GetCustomers();

        List<Employee> GetEmployees();

        bool Login(string Account, string Password, out string message);
    }
}
