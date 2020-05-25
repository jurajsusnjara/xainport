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
    public class DeleteModel : PageModel
    {
        private readonly XainportWeb.Data.XainportWebContext _context;

        public DeleteModel(XainportWeb.Data.XainportWebContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Covid19AttestationModel = await _context.Covid19AttestationModel.FindAsync(id);

            if (Covid19AttestationModel != null)
            {
                _context.Covid19AttestationModel.Remove(Covid19AttestationModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
