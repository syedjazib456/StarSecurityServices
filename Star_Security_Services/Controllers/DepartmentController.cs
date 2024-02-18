using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Star_Security_Services.Models.Data;

namespace Star_Security_Services.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly Application_dbContext _DbContext;
        public DepartmentController(Application_dbContext dbContext)
        {
            this._DbContext = dbContext;
        }
        [HttpGet]
        public IActionResult AddDept()
        {
            return View();
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
       public async Task<IActionResult> AddDept(Department department)
        {
            if (ModelState.IsValid)
            {
                await _DbContext.departments.AddAsync(department);
                await _DbContext.SaveChangesAsync();
                TempData["add"] = "Department Added successfully";
                return RedirectToAction("ViewDept", "Department");
            }
            return View();
        }

        public async Task<IActionResult> ViewDept()
        {
            var dept = await _DbContext.departments.ToListAsync();
            return View(dept);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteDept(int id)
        {
            var dept = await _DbContext.departments.FirstOrDefaultAsync(x=>x.Dept_id==id);
            return View(dept);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDept(Department department)
        {
            _DbContext.departments.Remove(department);
            _DbContext.SaveChangesAsync();
            TempData["del"] = "Deleted Successfully";
            return RedirectToAction("ViewDept", "Department");
        }
        [HttpGet]
        public async Task<IActionResult> EditDept(int id)
        {
            var dept = await _DbContext.departments.FirstOrDefaultAsync(x => x.Dept_id == id);
            return View(dept);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDept(Department department)
        {
            _DbContext.departments.Update(department);
            _DbContext.SaveChangesAsync();
            TempData["edit"] = "Department Edited";
            return RedirectToAction("ViewDept", "Department");
        }

    }
}
