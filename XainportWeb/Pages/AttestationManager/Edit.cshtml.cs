using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XainportWeb.Data;
using XainportWeb.Models;

namespace XainportWeb.Pages.AttestationManager
{
    public class EditModel : PageModel
    {
        private readonly XainportWeb.Data.XainportWebContext _context;

        public EditModel(XainportWeb.Data.XainportWebContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Covid19AttestationModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Covid19AttestationModelExists(Covid19AttestationModel.XainportId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool Covid19AttestationModelExists(string id)
        {
            return _context.Covid19AttestationModel.Any(e => e.XainportId == id);
        }
    }
}
