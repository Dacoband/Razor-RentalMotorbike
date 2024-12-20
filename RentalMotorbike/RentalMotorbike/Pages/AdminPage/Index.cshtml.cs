﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentalMotorbike.Pages.AdminPage
{
    public class IndexModel : PageModel
    {
        public IActionResult OnPost()
        {
            if (Request.Form["handler"] == "MotorbikeManagementPage")
            {
                return RedirectToPage("/AdminPage/MotorbikeManagementPage/Index");
            }
            else if (Request.Form["handler"] == "CustomerManagementPage")
            {
                return RedirectToPage("/AdminPage/CustomerManagementPage/Index");
            }
            else if (Request.Form["handler"] == "UploadJson")
            {
                return RedirectToPage("/AdminPage/JsonPage/UploadJson");
            }
            else if (Request.Form["handler"] == "Logout")
            {
                HttpContext.Session.Clear();
                return RedirectToPage("/LoginPage/Index");
            }

            return Page(); 
        }
    }
}
