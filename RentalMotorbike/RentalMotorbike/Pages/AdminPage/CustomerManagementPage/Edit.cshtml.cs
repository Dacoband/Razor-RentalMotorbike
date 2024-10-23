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

namespace RentalMotorbike.Pages.AdminPage.CustomerManagementPage
{
    public class EditModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public EditModel(IUserRepository context)
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

            var user =  userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
          
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                userRepository.UpdateCustomer(User);
                TempData["Message"] = "Update Customer successfully!";
                return RedirectToPage("./Index");

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool UserExists(int id)
        {
            return userRepository.GetUserById(id) != null;
        }
    }
}
