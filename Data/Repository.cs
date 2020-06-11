using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JsonSerializer;
using System.Collections.Generic;
using System.Text;

namespace Buecherwurm.Data
{
    public class Repository
    {
        private static HttpClient _client;
        public string jsonString;
        public string result;
        public Dictionary<string, string>  product;
        public List<Dictionary<string, string>> products;

        public Repository(Actions.Type type)
        {
            init();
            jsonString = GetAllProducts().Result;
            
            var arr = Json.DeserializeArray(jsonString);

            products = new List<Dictionary<string, string>>();

            foreach(var p in arr){
                Console.WriteLine(p);
                products.Add(ReadDictionary(Json.DeserializeObject(p)));
            }
        }

        public Repository(Actions.Type type, int id)
        {
            switch(type)
            {
                case Actions.Type.catalogueGet:
                init();
                jsonString = GetProductById(id).Result;
                product = ReadDictionary(Json.DeserializeObject(jsonString));
                break;

                case Actions.Type.catalogueDelete:
                init();
                result = catalogueDelete(id).Result;
                break;
            }
        }

        public Repository(Actions.Type type, string data)
        {
            init();
            result = cataloguePost(data).Result;
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

            foreach(var kvp in dictionary){
                result[Json.DeserializeString(kvp.Key)] = Json.DeserializeString(kvp.Value);
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

        private static async Task<string> catalogueDelete(int id)
        {
            var response = await _client.DeleteAsync("katalog/" + id);

            if(response.IsSuccessStatusCode)
            {
                return "OK.";
            }
            return "ERROR";
        }

        private static async Task<string> cataloguePost(string data)
        {
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var responseContent = await content.ReadAsStringAsync();

            Console.WriteLine(responseContent);

            var response = await _client.PostAsync("katalog/buch/", content);

            if (response.IsSuccessStatusCode)
            {
                return "OK.";
            }
            return "ERROR";
        }

        private static async Task<string> cataloguePut(HttpContent data)
        {
            var response = await _client.PutAsync("katalog/", data);

            if (response.IsSuccessStatusCode)
            {
                return "OK.";
            }
            return "ERROR";
        }
    }
}
