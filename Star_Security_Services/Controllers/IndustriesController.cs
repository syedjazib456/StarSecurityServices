using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Star_Security_Services.Models.Data;

namespace Star_Security_Services.Controllers
{
    public class IndustriesController : Controller
    {
        private readonly Application_dbContext _DbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndustriesController(Application_dbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this._DbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> AddIndustry()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576000)]
        [RequestSizeLimit(104857600)]
        public async Task<IActionResult> AddIndustry(Industries industry)
        {

            if (industry.Ind_image != null)
            {
                string filename = "";
                string updtfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + " " + industry.Ind_image.FileName;
                string filepath = Path.Combine(updtfolder, filename);
                string ext = Path.GetExtension(industry.Ind_image.FileName);

                if (ext.ToLower() == ".jfif" || ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".webp")
                {
                    industry.Ind_image.CopyTo(new FileStream(filepath, FileMode.Create));

                    if (industry.Ind_image.Length <= 1048576000)
                    {
                        Industries ind = new Industries
                        {
                            IndustryName = industry.IndustryName,
                            IndustryImage = filename,


                        };

                        _DbContext.industries.Add(ind);
                        _DbContext.SaveChanges();
                        TempData["msg"] = "Added successfully";
                        return RedirectToAction("ViewIndustry", "Industries");

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

        public async Task<IActionResult> ViewIndustry(string search)
        {
            var industry = await _DbContext.industries.Where(z => z.IndustryName.Contains(search) || search == null).ToListAsync();
            return View(industry);

        }
        public async Task<IActionResult> DetailsIndustry(int id)
        {
            var industry = await _DbContext.industries.FirstOrDefaultAsync(x => x.IndustryId == id);
            return View(industry);

        }

        [HttpGet]
        public async Task<IActionResult> EditIndustry(int id)
        {
            var industry = await _DbContext.industries.FirstOrDefaultAsync(x => x.IndustryId == id);
            return View(industry);

        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576000)]
        [RequestSizeLimit(104857600)]
        public async Task<IActionResult> EditIndustry(Industries industries, int id)
        {

            if (industries.Ind_image != null)
            {
                string filename = "";
                string updtfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + " " + industries.Ind_image.FileName;
                string filepath = Path.Combine(updtfolder, filename);
                string ext = Path.GetExtension(industries.Ind_image.FileName);

                if (ext.ToLower() == ".jfif" || ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".webp")
                {
                    industries.Ind_image.CopyTo(new FileStream(filepath, FileMode.Create));

                    if (industries.Ind_image.Length <= 1048576000)
                    {
                        var sp_ind= await _DbContext.industries.FirstOrDefaultAsync(x => x.IndustryId == id);
                        {
                            sp_ind.IndustryName = industries.IndustryName;
                            sp_ind.IndustryImage = filename;

                        };
                        _DbContext.industries.Update(sp_ind);
                        _DbContext.SaveChanges();
                        TempData["edit"] = " Updated successfully";
                        return RedirectToAction("ViewIndustry", "Industries");

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
        public async Task<IActionResult> DeleteIndustry(int id)
        {
            var ind = await _DbContext.industries.FirstOrDefaultAsync(x => x.IndustryId == id);
            return View(ind);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteIndustry(Industries industries, int id)
        {
            var ind = await _DbContext.industries.FindAsync(id);
            string deleteimg = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string currentrecord = Path.Combine(Directory.GetCurrentDirectory(), deleteimg, ind.IndustryImage);

            _DbContext.industries.Remove(ind);

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
            TempData["del_ind"] = "Record deleted successfully";
            return RedirectToAction("ViewIndustry", "Industries");
        }


    }
}
