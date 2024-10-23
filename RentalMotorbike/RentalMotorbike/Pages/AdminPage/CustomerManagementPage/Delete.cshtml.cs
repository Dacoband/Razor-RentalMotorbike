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

namespace RentalMotorbike.Pages.AdminPage.CustomerManagementPage
{
    public class DeleteModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public DeleteModel(IUserRepository context)
        {
            userRepository = context;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                var motorbike = userRepository.GetUserById;
                if (motorbike != null)
                {
                    userRepository.RemoveCustomer(id);
                    TempData["Message"] = "Delete Customer successfully!";
                    return RedirectToPage("./Index");
                }
                else
                {
                    TempData["Message"] = "Customer not found.";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }

            return Page();
        }
    }
}
