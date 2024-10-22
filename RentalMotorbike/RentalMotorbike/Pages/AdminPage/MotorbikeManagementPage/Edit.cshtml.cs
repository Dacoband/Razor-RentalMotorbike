using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalMotorbike.BusinessObject;
using RentalMotorbike.Repositories.Implements;
using RentalMotorbike.Repositories.Interfaces;

namespace RentalMotorbike.Pages.AdminPage.MotorbikeManagementPage
{
    public class EditModel : PageModel
    {
        private readonly IMotorbikeRepository motorbikeRepository;

        public EditModel(IMotorbikeRepository context)
        {
            motorbikeRepository = context;
        }

        [BindProperty]
        public Motorbike Motorbike { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorbike = motorbikeRepository.GetMotorbikeById(id);

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                    motorbikeRepository.UpdateMotorbike(Motorbike);
                    TempData["Message"] = "Delete Motorbike successfully!";
                    return RedirectToPage("./Index");         

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
            return motorbikeRepository.GetMotorbikeById(id) != null;
        }
    }
}
