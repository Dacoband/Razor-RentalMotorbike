using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalMotorbike.BusinessObject;

namespace RentalMotorbike.Pages.CustomerPage.MainPage
{
    public class EditModel : PageModel
    {
        private readonly RentalMotorbike.BusinessObject.RentalMotoBikeContext _context;

        public EditModel(RentalMotorbike.BusinessObject.RentalMotoBikeContext context)
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

            var motorbike =  await _context.Motorbikes.FirstOrDefaultAsync(m => m.MotorbikeId == id);
            if (motorbike == null)
            {
                return NotFound();
            }
            Motorbike = motorbike;
           ViewData["StatusId"] = new SelectList(_context.MotorbikeStatuses, "StatusId", "StatusName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Motorbike).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotorbikeExists(Motorbike.MotorbikeId))
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

        private bool MotorbikeExists(int id)
        {
            return _context.Motorbikes.Any(e => e.MotorbikeId == id);
        }
    }
}
