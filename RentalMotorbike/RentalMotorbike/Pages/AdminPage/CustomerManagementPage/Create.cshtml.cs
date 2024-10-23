using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentalMotorbike.BusinessObject;
using RentalMotorbike.Repositories.Interfaces;

namespace RentalMotorbike.Pages.AdminPage.CustomerManagementPage
{
    public class CreateModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public CreateModel(IUserRepository context)
        {
            userRepository = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            userRepository.AddCustomer(User);
            TempData["Message"] = "Create Customer successfully!";
            return RedirectToPage("./Index");
        }
    }
}
