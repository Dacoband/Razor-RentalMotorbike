using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RentalMotorbike.BusinessObject;
using RentalMotorbike.Repositories.Interfaces;

namespace RentalMotorbike.Pages.CustomerPage.RentalPage
{
    public class IndexModel : PageModel
    {
        private readonly IRentalRepository _context;

        public IndexModel(IRentalRepository context)
        {
            _context = context;
        }

        public IList<Rental> Rental { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                
                Rental = _context.GetRentalsByUserId(userId);
            }
            else
            {
                Rental = new List<Rental>();
            }
        }
    }
}
