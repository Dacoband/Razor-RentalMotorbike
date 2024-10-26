using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RentalMotorbike.BusinessObject;

namespace RentalMotorbike.Pages.CustomerPage.MainPage
{
    public class DeleteModel : PageModel
    {
        private readonly RentalMotorbike.BusinessObject.RentalMotoBikeContext _context;

        public DeleteModel(RentalMotorbike.BusinessObject.RentalMotoBikeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Motorbike Motorbike { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorbike = await _context.Motorbikes.FirstOrDefaultAsync(m => m.MotorbikeId == id);

            if (motorbike == null)
            {
                return NotFound();
            }
            else
            {
                Motorbike = motorbike;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorbike = await _context.Motorbikes.FindAsync(id);
            if (motorbike != null)
            {
                Motorbike = motorbike;
                _context.Motorbikes.Remove(Motorbike);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
