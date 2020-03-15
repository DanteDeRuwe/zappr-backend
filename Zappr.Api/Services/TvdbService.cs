using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Zappr.Api.Services
{
    public static class TvdbService
    {

        public static async Task<string> GetRandomName()
        {
            using HttpClient client = new HttpClient();

            Task<HttpResponseMessage> responseTask = client.GetAsync("https://uinames.com/api/");
            responseTask.Wait();

            HttpResponseMessage result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                dynamic obj = JsonConvert.DeserializeObject(content);
                return obj.name.Value;
            }
            else
            {
                string methodName = new StackTrace().GetFrame(0).GetMethod().Name;
                return $"Error in {methodName}, statuscode: {result.StatusCode}";
            }
        }
    }
}
