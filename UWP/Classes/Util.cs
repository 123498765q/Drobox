using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace UWP.Classes
{
    static class Util
    {
        public static void PostUser(UserInfo userInfo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:57769/");
                    var response = client.PostAsJsonAsync("api/add-user", userInfo).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
