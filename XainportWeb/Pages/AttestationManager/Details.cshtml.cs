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
    public class DetailsModel : PageModel
    {
        private readonly XainportWeb.Data.XainportWebContext _context;

        public DetailsModel(XainportWeb.Data.XainportWebContext context)
        {
            _context = context;
        }

        public Covid19AttestationModel Covid19AttestationModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Covid19AttestationModel = await _context.Covid19AttestationModel.FirstOrDefaultAsync(m => m.XainportId == id);

            if (Covid19AttestationModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
