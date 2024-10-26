using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentalMotorbike.BusinessObject;

namespace RentalMotorbike.Pages.CustomerPage.MainPage
{
    public class CreateModel : PageModel
    {
        private readonly RentalMotorbike.BusinessObject.RentalMotoBikeContext _context;

        public CreateModel(RentalMotorbike.BusinessObject.RentalMotoBikeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["StatusId"] = new SelectList(_context.MotorbikeStatuses, "StatusId", "StatusName");
            return Page();
        }

        [BindProperty]
        public Motorbike Motorbike { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Motorbikes.Add(Motorbike);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
