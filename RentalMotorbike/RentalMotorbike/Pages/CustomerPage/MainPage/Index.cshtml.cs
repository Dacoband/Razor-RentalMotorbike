using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RentalMotorbike.BusinessObject;
using RentalMotorbike.Repositories.Implements;
using RentalMotorbike.Repositories.Interfaces;

namespace RentalMotorbike.Pages.CustomerPage.MainPage
{
    public class IndexModel : PageModel
    {
        private readonly IMotorbikeRepository motorbikeRepository;
        private readonly IRentalRepository rentalRepository;
        public IList<Motorbike> MotorbikesList { get; set; } = new List<Motorbike>();

        [BindProperty]
        public string SearchText { get; set; } = string.Empty;

        public IndexModel(IMotorbikeRepository motorbikeRepo, IRentalRepository rentalRepo)
        {
            motorbikeRepository = motorbikeRepo;
            rentalRepository = rentalRepo;
        }

        public async Task OnGetAsync()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");

            if (customerId.HasValue)
            {
                MotorbikesList = motorbikeRepository.GetMotorbikesAvailableForCustomer(customerId.Value).ToList();
            }
            else
            {
                throw new InvalidOperationException("Customer ID not found in session. Please log in.");
            }
        }

        public async Task<IActionResult> OnPostRentalAsync(int id)
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");

            if (customerId == null)
            {
                return RedirectToPage("/LoginPage/Index");
            }

            var selectedMotorbike = motorbikeRepository.GetMotorbikeById(id);
            if (selectedMotorbike == null || selectedMotorbike.StatusId != 1)  //Available
            {
                return NotFound("Motorbike not available for rental.");
            }

            var rental = new Rental
            {
                MotorbikeId = selectedMotorbike.MotorbikeId,
                UserId = customerId.Value,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                TotalPrice = selectedMotorbike.RentalPricePerDay
            };

            selectedMotorbike.StatusId = 3;  //set to rented
            motorbikeRepository.UpdateMotorbike(selectedMotorbike);
            rentalRepository.AddRental(rental);

            TempData["Message"] = $"Rental successful: {selectedMotorbike.LicensePlate}";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                var allMotorbikes = motorbikeRepository.GetAllMotorbikes();
                MotorbikesList = allMotorbikes
                    .Where(m => m.Brand.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                m.Model.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                m.LicensePlate.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                await OnGetAsync();  
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Request.Form["handler"] == "Logout")
            {
                HttpContext.Session.Clear();
                return RedirectToPage("/LoginPage/Index");
            }
            return Page();
        }
    }
}
