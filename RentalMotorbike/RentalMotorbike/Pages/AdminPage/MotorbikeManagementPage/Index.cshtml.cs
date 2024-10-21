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
    public class IndexModel : PageModel
    {
        private readonly IMotorbikeRepository _context;

        public IndexModel(IMotorbikeRepository context)
        {
            _context = context;
        }

        public IList<Motorbike> Motorbike { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Motorbike = _context.GetAllMotorbikes();
        }
    }
}
