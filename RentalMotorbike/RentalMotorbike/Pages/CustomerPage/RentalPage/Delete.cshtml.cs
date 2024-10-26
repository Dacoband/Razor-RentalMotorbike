using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RentalMotorbike.BusinessObject;

namespace RentalMotorbike.Pages.CustomerPage.RentalPage
{
    public class DeleteModel : PageModel
    {
        private readonly RentalMotorbike.BusinessObject.RentalMotoBikeContext _context;

        public DeleteModel(RentalMotorbike.BusinessObject.RentalMotoBikeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rental Rental { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FirstOrDefaultAsync(m => m.RentalId == id);

            if (rental == null)
            {
                return NotFound();
            }
            else
            {
                Rental = rental;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                Rental = rental;
                _context.Rentals.Remove(Rental);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
