using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentalMotorbike.Repositories.Interfaces;

namespace RentalMotorbike.Pages.LoginPage
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string password { get; set; }
        private readonly IUserRepository _userRepository;
        public IndexModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Please enter email and password!");
                return Page();
            }

            var user = _userRepository.GetUserByEmailAndPassword(email, password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password!");
                return Page();
            }
            if (user.Role.RoleId == 1)
            {
                HttpContext.Session.SetInt32("RoleID", user.Role.RoleId);
                return RedirectToPage("/AdminPage/Index");
            }
            else if (user.Role.RoleId == 2)
            {
                HttpContext.Session.SetInt32("RoleID", user.Role.RoleId);
                return RedirectToPage("/EmployeePage/Index");
            }
            else if (user.Role.RoleId == 3)
            {
                HttpContext.Session.SetInt32("CustomerId", user.UserId);
                return RedirectToPage("/CustomerPage/MainPage/Index");
            }

            ModelState.AddModelError("", "Invalid email or password!");
            return Page();
        }
    }
}
