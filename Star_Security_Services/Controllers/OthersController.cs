using Microsoft.AspNetCore.Mvc;
using Star_Security_Services.Models.Data;

namespace Star_Security_Services.Controllers
{
    public class OthersController : Controller
    {
        private readonly Application_dbContext _dbContext;
        public OthersController(Application_dbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IActionResult Vacancies()
        {
            var vacancy = _dbContext.vacancies.ToList();
            return View(vacancy);
        }
        public IActionResult Hiring()
        {
            return View();
        }
    }
}
