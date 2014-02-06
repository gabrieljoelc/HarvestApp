using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HarvestApp.Core
{
    public class HarvestApi
    {
        public static async Task<T> Get<T>(string uri, string username, string password)
        {
            // https://github.com/harvesthq/harvest_api_samples/blob/master/harvest_api_sample.cs
            var usernamePassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
            
            // from http://stackoverflow.com/a/15034995/34315 but doesn't seem to work
            //var credentials = new NetworkCredential(username, password);
            //var handler = new HttpClientHandler { Credentials = credentials };
            //using (var httpClient = new HttpClient(handler))
            //{
            //    if (handler.SupportsPreAuthenticate())
            //    {
            //        handler.PreAuthenticate = true;
            //    }

            using (var httpClient = new HttpClient())
            {
                // from http://msdn.microsoft.com/en-us/library/windows/apps/hh781239.aspx & http://blogs.msdn.com/b/bclteam/archive/2013/02/18/portable-httpclient-for-net-framework-and-windows-phone.aspx
                httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + usernamePassword);
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
                
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseAsString);
            }
        }
    }
}
