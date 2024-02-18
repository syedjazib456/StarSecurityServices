using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Star_Security_Services.Models.Data;

namespace Star_Security_Services.Controllers
{
    public class AdminController : Controller
    {
        private readonly Application_dbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(Application_dbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            this._applicationDbContext = applicationDbContext;
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576000)]
        [RequestSizeLimit(104857600)]
        public async Task<IActionResult> AddAdmin(Admin admin)
        {

            if (admin.Admin_image != null)
            {
                string filename = "";
                string updtfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/AdminImg");
                filename = Guid.NewGuid().ToString() + " " + admin.Admin_image.FileName;
                string filepath = Path.Combine(updtfolder, filename);
                string ext = Path.GetExtension(admin.Admin_image.FileName);

                if (ext.ToLower() == ".jfif" || ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".webp")
                {
                    admin.Admin_image.CopyTo(new FileStream(filepath, FileMode.Create));

                    if (admin.Admin_image.Length <= 1048576000)
                    {
                       Admin adm = new Admin
                        {
                            AdminName = admin.AdminName,
                            AdminContact =admin.AdminContact,
                            AdminImage = filename,
                            AdminEmail = admin.AdminEmail,
                    
                        };

                        _applicationDbContext.Admins.Add(adm);
                        _applicationDbContext.SaveChangesAsync();
                        TempData["msg"] = "Admin added successfully";
                        return RedirectToAction("ViewAdmin", "Admin");

                    }

                    else
                    {
                        TempData["Error"] = "file size is not valid";
                    }
                }
                else
                {
                    TempData["Error"] = "file format is not valid";
                }
            }


            return View();
        }

        public async Task<IActionResult> ViewAdmin(string search)
        {
            var clt = await _applicationDbContext.Admins.Where(z => z.AdminName.Contains(search) || search == null).ToListAsync();
            return View(clt);

        }

        public async Task<IActionResult> DetailsAdmin(int id)
        {
            var adm = await _applicationDbContext.Admins.FirstOrDefaultAsync(x => x.AdminId == id);
            return View(adm);

        }
        [HttpGet]
        public async Task<IActionResult> EditAdmin(int id)
        {
            var adm = await _applicationDbContext.Admins.FirstOrDefaultAsync(x => x.AdminId == id);
            return View(adm);

        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576000)]
        [RequestSizeLimit(104857600)]
        public async Task<IActionResult> EditAdmin(Admin admin, int id)
        {

            if (admin.Admin_image != null)
            {
                string filename = "";
                string updtfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + " " + admin.Admin_image.FileName;
                string filepath = Path.Combine(updtfolder, filename);
                string ext = Path.GetExtension(admin.Admin_image.FileName);

                if (ext.ToLower() == ".jfif" || ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".webp")
                {
                    admin.Admin_image.CopyTo(new FileStream(filepath, FileMode.Create));

                    if (admin.Admin_image.Length <= 1048576000)
                    {
                        var  adm = await _applicationDbContext.Admins.FirstOrDefaultAsync(x => x.AdminId == id);
                        {
                            adm.AdminName = admin.AdminName;
                            adm.AdminContact = admin.AdminContact;
                            adm.AdminImage = filename;
                            adm.AdminEmail = admin.AdminEmail;
                  

                        };
                        _applicationDbContext.Admins.Update(adm);
                        _applicationDbContext.SaveChanges();
                        TempData["edit"] = "Admin updated successfully";
                        return RedirectToAction("ViewAdmin", "Admin");

                    }

                    else
                    {
                        TempData["Error"] = "file size is not valid";
                    }
                }
                else
                {
                    TempData["Error"] = "file format is not valid";
                }
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var clt = await _applicationDbContext.Admins.FirstOrDefaultAsync(x => x.AdminId == id);
            return View(clt);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteAdmin(Admin admin, int id)
        {
            var data = await _applicationDbContext.Admins.FindAsync(id);
            string deleteimg = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string currentrecord = Path.Combine(Directory.GetCurrentDirectory(), deleteimg, data.AdminImage);

            _applicationDbContext.Admins.Remove(data);

            if (await _applicationDbContext.SaveChangesAsync() > 0)
            {
                if (currentrecord != null)
                {
                    if (System.IO.File.Exists(currentrecord))
                    {
                        System.IO.File.Delete(currentrecord);
                    }
                }
            }
            TempData["del"] = "Admin is deleted successfully";
            return RedirectToAction("ViewAdmin", "Admin");
        }
    }
}
