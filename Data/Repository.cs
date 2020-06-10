using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JsonSerializer;
using System.Collections.Generic;

namespace Buecherwurm.Data
{
    public class Repository
    {
        private static HttpClient _client;
        public string jsonString;
        public Dictionary<string, string>  product;
        public List<Dictionary<string, string>> products;

        public Repository(Actions.Type type)
        {
            init();
            jsonString = GetAllProducts().Result;
            
            var arr = Json.DeserializeArray(jsonString);

            foreach (var p in arr)
            {
                Console.WriteLine(p);
            }

            products = new List<Dictionary<string, string>>();

            foreach(var p in arr){
                Console.WriteLine(p);
                products.Add(ReadDictionary(Json.DeserializeObject(p)));
            }
        }

        public Repository(Actions.Type type, int id)
        {
            init();
            jsonString = GetProductById(id).Result;
            product = ReadDictionary(Json.DeserializeObject(jsonString));
        }

        public Repository(Actions.Type type, string jsonString)
        {
            init();
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

        public Dictionary<string, string> ReadDictionary(Dictionary<string, string> dictionary)
        {
            var result = new Dictionary<string, string>();

            Console.WriteLine(Json.SerializeObject(dictionary));

            foreach(var kvp in dictionary){
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                result[Json.DeserializeString(kvp.Key)] = Json.DeserializeString(kvp.Value);
            }

            foreach (var kvp in result)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            return result;
        }

        private static async Task<string> GetProductById(int id)
        {
            var response = await _client.GetAsync("katalog/" + id);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return "ERROR";
        }

        private static async Task<string> GetAllProducts()
        {
            var response = await _client.GetAsync("katalog/");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return "ERROR";
        }
    }
}
