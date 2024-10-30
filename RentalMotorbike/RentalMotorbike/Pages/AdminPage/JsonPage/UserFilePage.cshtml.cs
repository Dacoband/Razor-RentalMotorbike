using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RentalMotorbike.BusinessObject;
using RentalMotorbike.Respositories.Interfaces;

namespace RentalMotorbike.Pages.AdminPage.JsonPage
{
    public class UserFilePageModel : PageModel
    {
        private readonly IFileService<User> _fileServices;
        private readonly string _filePath = UploadJsonModel.DataFilePath;

        public List<User> Users { get; set; } = new List<User>();

        public UserFilePageModel(IFileService<User> fileServices)
        {
            _fileServices = fileServices;
        }

        public async Task OnGetAsync()
        {
            Users = await _fileServices.ReadFileAsync(_filePath);
            Console.WriteLine("Data read from file:");
            foreach (var customer in Users)
            {
                Console.WriteLine($"ID: {customer.UserId}, NAME: {customer.Username}");
            }
        }

        public async Task<IActionResult> OnPostCreateAsync(int UserId,string Username, string Email, string PasswordHash, int RoleId)
        {
            Users = await _fileServices.ReadFileAsync(_filePath);

            var newCustomer = new User()
            {
                UserId = UserId,
                Username = Username,
                Email = Email,
                PasswordHash = PasswordHash,
                RoleId = RoleId
            };

            Users.Add(newCustomer);
            await _fileServices.WriteFileAsync(Users, _filePath);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            Users = await _fileServices.ReadFileAsync(_filePath);
            var customerToDelete = Users.FirstOrDefault(s => s.UserId == id);
            if (customerToDelete != null)
            {
                Users.Remove(customerToDelete);
                await _fileServices.WriteFileAsync(Users, _filePath);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(int UserId, string Username, string Email, string PasswordHash, int RoleId)
        {
            Users = await _fileServices.ReadFileAsync(_filePath);
            var customerToEdit = Users.FirstOrDefault(s => s.UserId == UserId);
            if (customerToEdit != null)
            {
                customerToEdit.Username = Username;
                customerToEdit.Email = Email;
                customerToEdit.PasswordHash = PasswordHash;
                customerToEdit.RoleId = RoleId;
                await _fileServices.WriteFileAsync(Users, _filePath);
            }
            return RedirectToPage();
        }
    }
}
