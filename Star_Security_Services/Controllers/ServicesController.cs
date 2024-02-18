using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Star_Security_Services.Models.Data;

namespace Star_Security_Services.Controllers
{
    public class ServicesController : Controller
    {
        private readonly Application_dbContext _DbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ServicesController(Application_dbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this._DbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult AddServices()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576000)]
        [RequestSizeLimit(104857600)]
        public async Task<IActionResult> AddServices(Services services)
        {

            if (services.Sr_image != null)
            {
                string filename = "";
                string updtfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + " " + services.Sr_image.FileName;
                string filepath = Path.Combine(updtfolder, filename);
                string ext = Path.GetExtension(services.Sr_image.FileName);

                if (ext.ToLower() == ".jfif" || ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".webp")
                {
                    services.Sr_image.CopyTo(new FileStream(filepath, FileMode.Create));

                    if (services.Sr_image.Length <= 1048576000)
                    {
                        Services srvc = new Services
                        {
                            ServiceName = services.ServiceName,
                            ServiceImage = filename


                        };

                        _DbContext.services.Add(srvc);
                        _DbContext.SaveChangesAsync();
                        TempData["msg"] = "Service added successfully";
                        return RedirectToAction("ViewService", "Services");

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

        public async Task<IActionResult> ViewService(string search)
        {
            var srv = await _DbContext.services.Where(z => z.ServiceName.Contains(search) || search == null).ToListAsync();
            return View(srv);

        }

        public async Task<IActionResult> ServiceDetails(int id)
        {
            var srvc = await _DbContext.services.FirstOrDefaultAsync(x => x.ServiceID == id);
            return View(srvc);

        }
        [HttpGet]
        public async Task<IActionResult> EditService(int id)
        {
            var srvc = await _DbContext.services.FirstOrDefaultAsync(x => x.ServiceID == id);
            return View(srvc);

        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576000)]
        [RequestSizeLimit(104857600)]
        public async Task<IActionResult> EditService(Services services, int id)
        {

            if (services.Sr_image != null)
            {
                string filename = "";
                string updtfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + " " + services.Sr_image.FileName;
                string filepath = Path.Combine(updtfolder, filename);
                string ext = Path.GetExtension(services.Sr_image.FileName);

                if (ext.ToLower() == ".jfif" || ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".webp")
                {
                    services.Sr_image.CopyTo(new FileStream(filepath, FileMode.Create));

                    if (services.Sr_image.Length <= 1048576000)
                    {
                        var sp_srv = await _DbContext.services.FirstOrDefaultAsync(x => x.ServiceID== id);
                        {
                            sp_srv.ServiceName = services.ServiceName;
                      
                            sp_srv.ServiceImage = filename;
                        

                        };
                        _DbContext.services.Update(sp_srv);
                        _DbContext.SaveChanges();
                        TempData["edit"] = "Service updated successfully";
                        return RedirectToAction("ViewService", "Services");

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
        public async Task<IActionResult> DeleteService(int id)
        {
            var sr = await _DbContext.services.FirstOrDefaultAsync(x => x.ServiceID == id);
            return View(sr);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteService(Services services, int id)
        {
            var data = await _DbContext.services.FindAsync(id);
            string deleteimg = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string currentrecord = Path.Combine(Directory.GetCurrentDirectory(), deleteimg, data.ServiceImage);

            _DbContext.services.Remove(data);

            if (await _DbContext.SaveChangesAsync() > 0)
            {
                if (currentrecord != null)
                {
                    if (System.IO.File.Exists(currentrecord))
                    {
                        System.IO.File.Delete(currentrecord);
                    }
                }
            }
            TempData["del_sr"] = "Service is deleted successfully";
            return RedirectToAction("ViewService", "Services");
        }
        public IActionResult ManGuard()
        {
            var service = _DbContext.services.ToList();
            return View(service);
        }
        public IActionResult MobileSecurity()
        {
            var service = _DbContext.services.ToList();
            return View(service);
         
        }
        public IActionResult LifeGuard()
        {
            var service = _DbContext.services.ToList();
            return View(service);
           
        }
        public IActionResult EventSecurity()
        {
            var service = _DbContext.services.ToList();
            return View(service);
            
        }
        public IActionResult Bouncer()
        {
            var service = _DbContext.services.ToList();
            return View(service);
            
        }
        public IActionResult Residential()
        {
            var service = _DbContext.services.ToList();
            return View(service);
            
        }
        public IActionResult SMART()
        {
            var service = _DbContext.services.ToList();
            return View(service);
            
        }
        public IActionResult K9()
        {
            var service = _DbContext.services.ToList();
            return View(service);
            
        }
    }
}
