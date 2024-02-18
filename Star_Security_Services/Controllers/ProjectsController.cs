using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Star_Security_Services.Models.Data;

namespace Star_Security_Services.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly Application_dbContext _DbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectsController(Application_dbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this._DbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult AddProject()
        {
            ViewBag.client= _DbContext.clients.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576000)]
        [RequestSizeLimit(104857600)]
        public IActionResult AddProject( Projects projects)
        {
         
            if (projects.Pr_image != null)
            {
                string filename = "";
                string updtfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + " " + projects.Pr_image.FileName;
                string filepath = Path.Combine(updtfolder, filename);
                string ext = Path.GetExtension(projects.Pr_image.FileName);

                if (ext.ToLower() == ".jfif" || ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".webp")
                {
                    projects.Pr_image.CopyTo(new FileStream(filepath, FileMode.Create));

                    if (projects.Pr_image.Length <= 1048576000)
                    {
                        Projects prj = new Projects
                        {
                            ProjectName = projects.ProjectName,
                           Project_createdate = projects.Project_createdate,
                            Project_enddate = projects.Project_enddate,
                           ProjectImage = filename,
                          Client_id = projects.Client_id
                        };

                        _DbContext.Projects.Add(prj);
                        _DbContext.SaveChanges();
                        TempData["msg"] = "Project added successfully";
                        return RedirectToAction("ViewProject", "Projects");

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

        public async Task<IActionResult> ViewProject(string search)
        {
            var project = await _DbContext.Projects.Include(x => x.clients).Where(z => z.ProjectName.Contains(search) || search == null).ToListAsync();
            return View(project);

        }

        public async Task<IActionResult> DetailsProject(int pr_id)
        {
            var project = await _DbContext.Projects.Include(x => x.clients).FirstOrDefaultAsync(x=>x.ProjectId==pr_id);
            return View(project);

        }
        [HttpGet]
        public async Task<IActionResult> EditProject(int id)
        {
            var project = await _DbContext.Projects.Include(x => x.clients).FirstOrDefaultAsync(x => x.ProjectId == id);
            return View(project);

        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576000)]
        [RequestSizeLimit(104857600)]
        public async Task<IActionResult> Editproject(Projects project, int id)
        {
       
            if (project.Pr_image != null)
            {
                string filename = "";
                string updtfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + " " + project.Pr_image.FileName;
                string filepath = Path.Combine(updtfolder, filename);
                string ext = Path.GetExtension(project.Pr_image.FileName);

                if (ext.ToLower() == ".jfif" || ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".webp")
                {
                    project.Pr_image.CopyTo(new FileStream(filepath, FileMode.Create));

                    if (project.Pr_image.Length <= 1048576000)
                    {
                        var sp_prj = await _DbContext.Projects.Include(x => x.clients).FirstOrDefaultAsync(y => y.ProjectId == id);
                        {
                            sp_prj.ProjectName = project.ProjectName;
                            sp_prj.Project_createdate = project.Project_createdate;
                            sp_prj.Project_enddate = project.Project_enddate;
                            sp_prj.ProjectImage = filename;
                            sp_prj.Client_id = project.Client_id;
                         
                        };
                        _DbContext.Projects.Update(sp_prj);
                        _DbContext.SaveChanges();
                        TempData["edit"] = "Project updated successfully";
                        return RedirectToAction("ViewProject", "Projects");

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
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _DbContext.Projects.Include(x => x.clients).FirstOrDefaultAsync(x => x.ProjectId == id);
            return View(project);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteProject(Projects project, int id)
        {
            var data = await _DbContext.Projects.FindAsync(id);
            string deleteimg = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string currentrecord = Path.Combine(Directory.GetCurrentDirectory(), deleteimg, data.ProjectImage);

            _DbContext.Projects.Remove(data);

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
            TempData["del_pr"] = "Project is deleted successfully";
            return RedirectToAction("ViewProject", "Projects");
        }
    }
}
