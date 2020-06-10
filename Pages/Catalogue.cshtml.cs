using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Buecherwurm.Pages
{
    public class CatalogueModel : PageModel
    {
        private readonly ILogger<CatalogueModel> _logger;
        public int id;
        public string jsonString;
        public Dictionary<string, string> product;
        public List<Dictionary<string, string>> products;

        public CatalogueModel(ILogger<CatalogueModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int id)
        {
            this.id = id;

            if(id >= 1)
            {
                var repo = new Data.Repository
                (
                    Data.Actions.Type.catalogueGet,
                    id
                );
                jsonString = string.Empty;
                product = repo.product;
            }
            else if(id == 0)
            {
                var repo = new Data.Repository(Data.Actions.Type.catalogueGet);
                products = repo.products;
            }
            else
            {
                jsonString = "Diese ID ist ungültig.";
            }
        }
    }
}
