using Newtonsoft.Json;
using SOHU.Mobile.Global;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SOHU.Mobile.Services.Membership
{
    public class MembershipService : IMembershipService
    {
        public bool Login(string account, string password, out string token)
        {
            using (var client = new HttpClient())
            {
                var uri = AppGlobal.ApiServerEndpoint + "masterconfig/getbycode?";
                var parameters = new Dictionary<string, string> { { "Code", Code } };
                var encodedContent = new FormUrlEncodedContent(parameters).ReadAsStringAsync().Result;
                try
                {
                    var response = client.GetAsync(uri + encodedContent);
                    if (response.Result.StatusCode == HttpStatusCode.OK)
                    {
                        var content = response.Result.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<MasterConfig>>(content.Result);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return new List<MasterConfig>();
        }
    }
}
