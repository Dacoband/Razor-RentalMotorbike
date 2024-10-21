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

namespace RentalMotorbike.Pages.AdminPage.MotorbikeManagementPage
{
    public class DeleteModel : PageModel
    {
        private readonly IMotorbikeRepository _motorbikeRepository;

        public DeleteModel(IMotorbikeRepository context)
        {
            _motorbikeRepository = context;
        }

        [BindProperty]
        public Motorbike Motorbike { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorbike = _motorbikeRepository.GetMotorbikeById(id);

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

        public IActionResult OnPost(int id)
        {
            try
            {
                var motorbike = _motorbikeRepository.GetMotorbikeById(id);
                if (motorbike != null)
                {
                    _motorbikeRepository.RemoveMotorbike(id);
                    TempData["Message"] = "Delete Motorbike successfully!";
                    return RedirectToPage("./Index");
                }
                else
                {
                    TempData["Message"] = "Motorbike not found.";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }

            return Page(); // Nếu có lỗi thì return về lại trang hiện tại.
        }

    }
}
