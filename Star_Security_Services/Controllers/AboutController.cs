using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Star_Security_Services.Models.Data;

namespace Star_Security_Services.Controllers
{
    public class AboutController : Controller
    {
        private readonly Application_dbContext _dbContext;
        public AboutController(Application_dbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IActionResult History()
        {
            return View();
        }
        public IActionResult BOD()
        {
            return View();
        }
        public IActionResult OurNetwork()
        {
            var industry = _dbContext.industries.ToList();
            return View(industry);
        }
    }
}
