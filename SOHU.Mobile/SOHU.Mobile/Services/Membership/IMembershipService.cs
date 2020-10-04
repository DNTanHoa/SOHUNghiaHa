using SOHU.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Mobile.Services.Membership
{
    public interface IMembershipService
    {
        List<Customer> GetCustomers();

        Customer GetByID(int ID);

        Employee GetEmployeeByID(int ID);

        List<Employee> GetEmployees();

        bool Login(string Account, string Password, out string message);

        bool SaveCustomer(Customer customer, out string message);
        bool SaveEmployee(Employee customer, out string message);
    }
}
