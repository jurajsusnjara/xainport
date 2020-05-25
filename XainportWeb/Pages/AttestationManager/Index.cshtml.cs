using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using XainportWeb.Data;
using XainportWeb.Models;

namespace XainportWeb.Pages.AttestationManager
{
    public class IndexModel : PageModel
    {
        private readonly XainportWeb.Data.XainportWebContext _context;

        public IndexModel(XainportWeb.Data.XainportWebContext context)
        {
            _context = context;
        }

        public IList<Covid19AttestationModel> Covid19AttestationModel { get;set; }

        public async Task OnGetAsync()
        {
            Covid19AttestationModel = await _context.Covid19AttestationModel.ToListAsync();
        }
    }
}
