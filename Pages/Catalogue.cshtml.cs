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

        public CatalogueModel(ILogger<CatalogueModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int id)
        {
            this.id = id;
            var repo = new Data.Repository();
            jsonString = repo.jsonString;
        }
    }
}
