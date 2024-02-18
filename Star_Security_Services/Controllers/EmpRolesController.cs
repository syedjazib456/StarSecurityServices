using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Star_Security_Services.Models.Data;

namespace Star_Security_Services.Controllers
{
    public class EmpRolesController : Controller
    {
        private readonly Application_dbContext _DbContext;
        public EmpRolesController(Application_dbContext dbContext)
        {
            this._DbContext = dbContext;
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddRole(EmpRoles roles)
        {
            if (ModelState.IsValid)
            {
                await _DbContext.empRoles.AddAsync(roles);
                await _DbContext.SaveChangesAsync();
                TempData["add"] = "Role Added successfully";
                return RedirectToAction("ViewRole", "EmpRoles");
            }
            return View();
        }

        public async Task<IActionResult> ViewRole()
        {
            var role = await _DbContext.empRoles.ToListAsync();
            return View(role);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _DbContext.empRoles.FirstOrDefaultAsync(x => x.Role_id == id);
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(EmpRoles empRoles)
        {
            _DbContext.empRoles.Remove(empRoles);
            _DbContext.SaveChangesAsync();
            TempData["del"] = "Deleted Successfully";
            return RedirectToAction("ViewRole", "EmpRoles");
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(int id)
        {
            var role = await _DbContext.empRoles.FirstOrDefaultAsync(x => x.Role_id == id);
            return View(role);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(EmpRoles empRoles)
        {
            _DbContext.empRoles.Update(empRoles);
            _DbContext.SaveChangesAsync();
            TempData["edit"] = "Role Edited";
            return RedirectToAction("ViewRole", "EmpRoles");
        }
    }
}
