using Newtonsoft.Json;
using SOHU.Mobile.Global;
using SOHU.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SOHU.Mobile.Services.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        public Project GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public List<Project> GetProjects(int year, int month)
        {
            using (var client = new HttpClient())
            {
                var parameter = JsonConvert.SerializeObject(new { year, month });
                var uri = AppGlobal.ApiServerEndpoint + "Invoice/GetByDuAnAndYearAndMonthToList";
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppGlobal.Token);
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(uri),
                        Content = new StringContent(parameter, Encoding.UTF8, "application/json"),
                    };
                    var response = client.SendAsync(request);
                    if (response.Result.StatusCode == HttpStatusCode.OK)
                    {
                        var content = response.Result.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BaseResponeModel>(content.Result);
                        return result.Data.ToObject<List<Project>>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return new List<Project>();
        }
    }
}
