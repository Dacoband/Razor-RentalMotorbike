using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace RentalMotorbike.Pages.AdminPage.JsonPage
{
    public class UploadJsonModel : PageModel
    {
        public static string DataFilePath { get; private set; } = Path.Combine(Directory.GetCurrentDirectory(), "Data", "default.txt");

        public async Task<IActionResult> OnPostAsync()
        {
            var file = Request.Form.Files["fileUpload"];

            if (file != null && file.Length > 0)
            {
                DataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", file.FileName);

                using (var stream = new FileStream(DataFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                if (Path.GetExtension(file.FileName).ToLower() == ".json")
                {
                    using (var reader = new StreamReader(DataFilePath))
                    {
                        var fileContent = await reader.ReadToEndAsync();

                        if (fileContent.Contains("\"UserId\""))
                        {
                            return RedirectToPage("UserFilePage");
                        }
                        else if (fileContent.Contains("\"MotorbikeId\""))
                        {
                            return RedirectToPage("MotorbikeFilePage");
                        }
                    }
                }
            }

            return RedirectToPage("UploadJson");
        }
    }
}
