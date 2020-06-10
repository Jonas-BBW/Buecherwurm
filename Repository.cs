using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Buecherwurm.Data
{
    public class Repository
    {
        private static HttpClient _client;
        public string jsonString;

        public Repository()
        {
            init();
            jsonString = GetItemById(1).Result;
        }

        private void init()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://10.4.35.202/api/")
            };

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add
            (
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            _client = client;
        }

        private static async Task<string> GetItemById(int id)
        {
            var response = await _client.GetAsync("katalog/" + id);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return "ERROR";
        }
    }
}
