using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RentalMotorbike.BusinessObject;
using RentalMotorbike.Repositories.Interfaces;

namespace RentalMotorbike.Pages.AdminPage.MotorbikeManagementPage
{
    public class DetailsModel : PageModel
    {
        private readonly IMotorbikeRepository _context;

        public DetailsModel(IMotorbikeRepository context)
        {
            _context = context;
        }

        public Motorbike Motorbike { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorbike = _context.GetMotorbikeById(id);
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
    }
}
