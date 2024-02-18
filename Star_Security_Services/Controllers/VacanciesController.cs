using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Star_Security_Services.Models.Data;

namespace Star_Security_Services.Controllers
{
    public class VacanciesController : Controller
    {
        private readonly Application_dbContext _DbContext;


        public VacanciesController(Application_dbContext dbContext)
        {
            this._DbContext = dbContext;
       
        }
        [HttpGet]
        public IActionResult AddVacancy()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddVacancy(Vacancies vacancies)
        {

            if (ModelState.IsValid)
            {
                await _DbContext.vacancies.AddAsync(vacancies);
                await _DbContext.SaveChangesAsync();
                TempData["msg"] = "Added successfully";
                return RedirectToAction("ViewVacancy", "Vacancies");

            }
            return View();
        }
        public async Task<IActionResult> ViewVacancy(string search)
        {
            var vacancy =await _DbContext.vacancies.Where(z => z.PostName.Contains(search) || search == null).ToListAsync();
            return View(vacancy);
        }

        public async Task<IActionResult> VacancyDetails(int id)
        {
            var sel_vac = await _DbContext.vacancies.FirstOrDefaultAsync(z => z.ID == id);
            return View(sel_vac);
        }
        public async Task<IActionResult> EditVacancy(int id)
        {
            var sel_vac = await _DbContext.vacancies.FirstOrDefaultAsync(z => z.ID == id);
            return View(sel_vac);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVacancy(Vacancies vacancies)
        {
            if (ModelState.IsValid)
            {
                _DbContext.vacancies.Update(vacancies);
                await _DbContext.SaveChangesAsync();
                TempData["edit"] = " Updated Successfully";
                return RedirectToAction("ViewVacancy", "Vacancies");

            }
            return View();
        }
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            var sel_vac = await _DbContext.vacancies.FirstOrDefaultAsync(z => z.ID == id);
            return View(sel_vac);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteVacancy(Vacancies vacancy)
        {
            _DbContext.vacancies.Remove(vacancy);
            await _DbContext.SaveChangesAsync();
            TempData["del_vac"] = "Deleted successfully";
            return RedirectToAction("ViewVacancy", "Vacancies");
        }
    }
}
