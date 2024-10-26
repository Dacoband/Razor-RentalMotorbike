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
            var userId = HttpContext.Session.GetInt32("CustomerId");

            if (userId != null)
            {
                
                Rental = _context.GetRentalsByUserId(userId.Value);
            }
            else
            {
                Rental = new List<Rental>();
            }
        }
        public IActionResult OnPost()
        {
            if(Request.Form["handler"] == "Back")
            {
                return RedirectToPage("/CustomerPage/MainPage/Index");
            }
            return Page();
        }
    }
}
