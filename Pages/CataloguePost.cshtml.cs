using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using JsonSerializer;

namespace Buecherwurm.Pages
{
    public class CataloguePostModel : PageModel
    {
        private readonly ILogger<CataloguePostModel> _logger;
        public string result;
        public int id;

        public CataloguePostModel(ILogger<CataloguePostModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Post();
        }
        
        public void Post(){
            

            var obj = Json.CreateNewObject();
            obj = Json.AddKeyValuePair(obj, "title", "test", true);
            obj = Json.AddKeyValuePair(obj, "author", "test", true);
            obj = Json.AddKeyValuePair(obj, "country", "test", true);
            obj = Json.AddKeyValuePair(obj, "link", "test", true);
            obj = Json.AddKeyValuePair(obj, "language", "test", true);
            obj = Json.AddKeyValuePair(obj, "pages", "10", true);
            obj = Json.AddKeyValuePair(obj, "year", "2020", true);
            obj = Json.AddKeyValuePair(obj, "category", "0", true);
            obj = Json.AddKeyValuePair(obj, "imageLink", "test", true);
            obj = Json.AddKeyValuePair(obj, "lendTime", "1", true);
            obj = Json.AddKeyValuePair(obj, "lendType", "0", true);

            Console.WriteLine(obj);

            var repo = new Data.Repository
            (
                Data.Actions.Type.cataloguePost,
                obj

            );

            result = repo.result;
        }
    }
}
