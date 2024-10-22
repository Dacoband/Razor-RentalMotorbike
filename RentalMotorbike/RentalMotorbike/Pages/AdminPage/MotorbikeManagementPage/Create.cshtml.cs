using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentalMotorbike.BusinessObject;
using RentalMotorbike.Repositories.Implements;
using RentalMotorbike.Repositories.Interfaces;

namespace RentalMotorbike.Pages.AdminPage.MotorbikeManagementPage
{
    public class CreateModel : PageModel
    {
        private readonly IMotorbikeRepository _motorbikeRepository;

        public CreateModel(IMotorbikeRepository context)
        {
            _motorbikeRepository = context;
        }

        public IActionResult OnGet()
        {
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
            if (!Regex.IsMatch(Motorbike.LicensePlate, "^[0-9]{2}[A-Z]{1}-[0-9]{5}$"))
            {
                ModelState.AddModelError("Motorbike.LicensePlate", "License plate is not in correct format. Example: 29A-12345");
                return Page();
            }
            if (Motorbike.RentalPricePerDay < 0)
            {
                ModelState.AddModelError("Motorbike.Price", "Price must be greater than 0.");
                return Page();
            }

            _motorbikeRepository.AddMotorbike(Motorbike);
            TempData["Message"] = "Create Motorbike successfully!";
            return RedirectToPage("./Index");
        }
    }
}
