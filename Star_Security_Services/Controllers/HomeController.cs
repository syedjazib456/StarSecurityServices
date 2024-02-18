using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Star_Security_Services.Models;
using Star_Security_Services.Models.Data;
using System.Diagnostics;

namespace Star_Security_Services.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Application_dbContext _DbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(Application_dbContext dbContext, IWebHostEnvironment webHostEnvironment, ILogger<HomeController> logger)
        {
            this._DbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var getdetails=_DbContext.Projects.Include(x=>x.clients).ToList();
            return View(getdetails);
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _DbContext.contacts.AddAsync(contact);
                await _DbContext.SaveChangesAsync();
                TempData["msg"] = "Your message has sent successfully";
                return RedirectToAction("Contact", "Home");

            }
            return View();
        }
        public async Task<IActionResult> ViewContact()
        {
            var contact = await _DbContext.contacts.ToListAsync();
            return View(contact);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteMsg(int id)
        {
            var cont = await _DbContext.contacts.FirstOrDefaultAsync(x => x.Id == id);
            return View(cont);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMsg(Contact contact)
        {
            _DbContext.contacts.Remove(contact);
            _DbContext.SaveChangesAsync();
            TempData["del"] = "Deleted Successfully";
            return RedirectToAction("ViewContact", "Home");
        }
        public IActionResult OurClient()
        {
            var client = _DbContext.clients.ToList();
            return View(client);
        }

        public IActionResult Dashboard()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}