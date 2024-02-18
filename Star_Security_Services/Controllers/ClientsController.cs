using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Star_Security_Services.Models.Data;

namespace Star_Security_Services.Controllers
{
    public class ClientsController : Controller
    {
        private readonly Application_dbContext _DbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClientsController(Application_dbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this._DbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> AddClient()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576000)]
        [RequestSizeLimit(104857600)]
        public async Task<IActionResult> AddClient(Clients clients)
        {

            if (clients.Client_image != null)
            {
                string filename = "";
                string updtfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + " " + clients.Client_image.FileName;
                string filepath = Path.Combine(updtfolder, filename);
                string ext = Path.GetExtension(clients.Client_image.FileName);

                if (ext.ToLower() == ".jfif" || ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".webp")
                {
                    clients.Client_image.CopyTo(new FileStream(filepath, FileMode.Create));

                    if (clients.Client_image.Length <= 1048576000)
                    {
                        Clients client = new Clients
                        {
                            ClientName = clients.ClientName,
                            ClientProfession = clients.ClientProfession,
                            ClientImage = filename,
                            ClientReview=clients.ClientReview
                            
                         
                        };

                        _DbContext.clients.Add(client);
                        _DbContext.SaveChanges();
                        TempData["msg"] = "CLient added successfully";
                        return RedirectToAction("ViewClient", "Clients");

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

       public async Task<IActionResult> ViewClient( string search)
        {
            var clt = await _DbContext.clients.Where(z => z.ClientName.Contains(search) || search == null).ToListAsync();
            return View(clt);
   
        }

        public async Task<IActionResult> DetailsClient(int id)
        {
            var clt = await _DbContext.clients.FirstOrDefaultAsync(x => x.ClientId == id);
            return View(clt);

        }

        [HttpGet]
        public async Task<IActionResult> EditClient(int id)
        {
            var clt = await _DbContext.clients.FirstOrDefaultAsync(x => x.ClientId == id);
            return View(clt);

        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576000)]
        [RequestSizeLimit(104857600)]
        public async Task<IActionResult> EditClient(Clients clients, int id)
        {

            if (clients.Client_image != null)
            {
                string filename = "";
                string updtfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + " " + clients.Client_image.FileName;
                string filepath = Path.Combine(updtfolder, filename);
                string ext = Path.GetExtension(clients.Client_image.FileName);

                if (ext.ToLower() == ".jfif" || ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".webp")
                {
                    clients.Client_image.CopyTo(new FileStream(filepath, FileMode.Create));

                    if (clients.Client_image.Length <= 1048576000)
                    {
                        var sp_clt = await _DbContext.clients.FirstOrDefaultAsync(x => x.ClientId == id);
                        {
                            sp_clt.ClientName = clients.ClientName;
                            sp_clt.ClientProfession = clients.ClientProfession;
                            sp_clt.ClientImage = filename;
                            sp_clt.ClientReview = clients.ClientReview;

                        };
                        _DbContext.clients.Update(sp_clt);
                        _DbContext.SaveChanges();
                        TempData["edit"] = "Project updated successfully";
                        return RedirectToAction("ViewClient", "Clients");

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
        public async Task<IActionResult> DeleteClient(int id)
        {
            var clt = await _DbContext.clients.FirstOrDefaultAsync(x => x.ClientId == id);
            return View(clt);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteClient(Clients clients, int id)
        {
            var data = await _DbContext.clients.FindAsync(id);
            string deleteimg = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string currentrecord = Path.Combine(Directory.GetCurrentDirectory(), deleteimg, data.ClientImage);

            _DbContext.clients.Remove(data);

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
            TempData["del_cl"] = "Client is deleted successfully";
            return RedirectToAction("ViewClient", "Clients");
        }
    }
    }

