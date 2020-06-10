using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Buecherwurm.Pages
{
    public class LendModel : PageModel
    {
        private readonly ILogger<LendModel> _logger;
        public int id;

        public LendModel(ILogger<LendModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int id)
        {
            this.id = id;
        }
    }
}
