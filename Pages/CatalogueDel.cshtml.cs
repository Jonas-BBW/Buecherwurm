using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Buecherwurm.Pages
{
    public class CatalogueDelModel : PageModel
    {
        private readonly ILogger<CatalogueDelModel> _logger;
        public string result;
        public int id;

        public CatalogueDelModel(ILogger<CatalogueDelModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int id)
        {
            var repo = new Data.Repository
            (
                Data.Actions.Type.catalogueDelete,
                id
            );
            this.id = id;
            result = repo.result;
        }
    }
}
