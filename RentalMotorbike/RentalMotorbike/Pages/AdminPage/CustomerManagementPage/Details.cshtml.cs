using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RentalMotorbike.BusinessObject;
using RentalMotorbike.Repositories.Interfaces;

namespace RentalMotorbike.Pages.AdminPage.CustomerManagementPage
{
    public class DetailsModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public DetailsModel(IUserRepository context)
        {
            userRepository = context;
        }

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
    }
}
