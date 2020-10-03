using ImTools;
using Newtonsoft.Json;
using SOHU.Mobile.Global;
using SOHU.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SOHU.Mobile.Services.Membership
{
    public class MembershipService : IMembershipService
    {
        public Customer GetByID(int ID)
        {
            using (var client = new HttpClient())
            {
                var uri = AppGlobal.ApiServerEndpoint + "membership/CustomerDetail?ID=" + ID;
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppGlobal.Token);
                    var response = client.GetAsync(uri);
                    if (response.Result.StatusCode == HttpStatusCode.OK)
                    {
                        var content = response.Result.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<SaveResponeModel>(content.Result);
                        return result.Data.ToObject<Customer>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return new Customer();
        }

        public List<Customer> GetCustomers()
        {
            using (var client = new HttpClient())
            {
                var uri = AppGlobal.ApiServerEndpoint + "membership/GetByCustomerParentIDToList";
                try
                {
                    client.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer", AppGlobal.Token);
                    var response = client.GetAsync(uri);
                    if (response.Result.StatusCode == HttpStatusCode.OK)
                    {
                        var content = response.Result.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BaseResponeModel>(content.Result);
                        return result.Data.ToObject<List<Customer>>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return new List<Customer>();
        }

        public List<Employee> GetEmployees()
        {
            using (var client = new HttpClient())
            {
                var uri = AppGlobal.ApiServerEndpoint + "membership/GetByEmployeeParentIDToList";
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppGlobal.Token);
                    var response = client.GetAsync(uri);
                    if (response.Result.StatusCode == HttpStatusCode.OK)
                    {
                        var content = response.Result.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BaseResponeModel>(content.Result);
                        return result.Data.ToObject<List<Employee>>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return new List<Employee>();
        }

        public bool Login(string Account, string Password, out string message)
        {
            using (var client = new HttpClient())
            {
                var uri = AppGlobal.ApiServerEndpoint + "membership/login";
                var parameters = JsonConvert.SerializeObject(new { Account, Password, });
                try
                {
                    var response = client.PostAsync(uri, new StringContent(parameters, Encoding.UTF8, "application/json"));
                    if (response.Result.StatusCode == HttpStatusCode.OK)
                    {
                        var content = response.Result.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<LoginResponeModel>(content.Result);
                        var result = data.Result.ToObject<APIResult>();
                        var token = data.Data.ToObject<TokenResult>();
                        if(result.Message == "success")
                        {
                            message = "Đăng nhập thành công";
                            AppGlobal.Token = token.Token;
                            return true;
                        }
                        else
                        {
                            message = result.Message;
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    message = "Không thể nối với server";
                    return false;
                }
            }
            message = "Đăng nhập thất bại";
            return false;
        }

        public bool SaveCustomer(Customer customer, out string message)
        {
            using (var client = new HttpClient())
            {
                var uri = AppGlobal.ApiServerEndpoint + "membership/SaveCustomer";
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppGlobal.Token);
                    var parameters = JsonConvert.SerializeObject(customer);
                    var response = client.PostAsync(uri, new StringContent(parameters, Encoding.UTF8, "application/json"));
                    if (response.Result.StatusCode == HttpStatusCode.OK)
                    {
                        var content = response.Result.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<SaveResponeModel>(content.Result);
                        var apiResult = result.Result.ToObject<APIResult>();
                        message = apiResult?.Message;
                        if(apiResult.ResultType == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    message = ex.Message;
                    return false;
                }
            }
            message = string.Empty;
            return false;
        }
    }
}
