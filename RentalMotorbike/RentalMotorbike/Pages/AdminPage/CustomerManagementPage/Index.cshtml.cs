using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RentalMotorbike.BusinessObject;
using RentalMotorbike.Repositories.Interfaces;
using static System.Reflection.Metadata.BlobBuilder;

namespace RentalMotorbike.Pages.AdminPage.CustomerManagementPage
{
    public class IndexModel : PageModel
    {
        private readonly IUserRepository userRepository ;

        public IndexModel(IUserRepository context)
        {
            userRepository = context;
        }

        public IList<User> User { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchCustomer { get; set; }


        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }

        public const int PageSize = 3;

        public async Task OnGetAsync()
        {
            var customers = userRepository.GetCustomers();
            if (!string.IsNullOrEmpty(SearchCustomer))
            {
                customers = customers
                    .Where(c =>
                        (string.IsNullOrEmpty(SearchCustomer) || c.Username.Contains(SearchCustomer, StringComparison.OrdinalIgnoreCase))).ToList();
            }

            TotalPages = (int)Math.Ceiling(customers.Count() / (double)PageSize);
            User = customers.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
