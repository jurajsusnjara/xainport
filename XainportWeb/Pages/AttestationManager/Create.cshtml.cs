using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using XainportWeb.Data;
using XainportWeb.Models;

namespace XainportWeb.Pages.AttestationManager
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly XainportWebContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(UserManager<IdentityUser> userManager, XainportWebContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Covid19AttestationModel Covid19AttestationModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Covid19AttestationModel.XainportId = Guid.NewGuid().ToString();

            IdentityUser loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            Covid19AttestationModel.CreatedBy = loggedInUser?.Id;

            _context.Covid19AttestationModel.Add(Covid19AttestationModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
