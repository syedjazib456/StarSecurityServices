using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Star_Security_Services.Models.Data;

namespace Star_Security_Services.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly Application_dbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeesController(Application_dbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            this._applicationDbContext = applicationDbContext;
            this._webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> AddEmployee()
        {
            ViewBag.department = await _applicationDbContext.departments.ToListAsync();
            ViewBag.role = await _applicationDbContext.empRoles.ToListAsync();
            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576000)]
        [RequestSizeLimit(1048576000)]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            string filename = "";
            if (employee.Emp_profile != null)
            {
                string updtfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + " " + employee.Emp_profile.FileName;
                string filepath = Path.Combine(updtfolder, filename);
                string extension = Path.GetExtension(employee.Emp_profile.FileName);

                if (extension.ToLower() == ".jfif" || extension.ToLower() == ".png" || extension.ToLower() == ".jpg")
                {
                    employee.Emp_profile.CopyTo(new FileStream(filepath, FileMode.Create));
                    if (employee.Emp_profile.Length <= 1048576000)
                    {


                        Employee emp = new Employee
                        {
                            Emp_name = employee.Emp_name,
                            Emp_email = employee.Emp_email,
                            Emp_contact = employee.Emp_contact,
                            Emp_address=employee.Emp_address,
                            EmpImage = filename,
                            Dep_id = employee.Dep_id,
                            RoleID = employee.RoleID,
                            Emp_Grade=employee.Emp_Grade,
                            Emp_age=employee.Emp_age,
                            Emp_qualification=employee.Emp_qualification
                            
                        };
                        _applicationDbContext.Add(emp);
                        _applicationDbContext.SaveChangesAsync();
                        TempData["msg"] = "Employee added successfully";
                        return RedirectToAction("ViewEmployees", "Employees");
                    }
                    else
                    {
                        TempData["error"] = "File size is not valid";
                    }
                }
                else
                {
                    TempData["error"] = "File size is not validformat is not valid";
                }



            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewEmployees(string search)
        {
            var employees = await _applicationDbContext.employees.Include(x => x.department).Include(x => x.roles).Where(z => z.Emp_name.Contains(search) || search == null).ToListAsync();
            return View(employees);
        }

        public async Task<IActionResult> DetailsEmployee(int id)
        {
            var selemployee = await _applicationDbContext.employees.Include(y => y.department).Include(y => y.roles).FirstOrDefaultAsync(y => y.EmployeeCode == id);
            return View(selemployee);
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var selemployee = await _applicationDbContext.employees.Include(y => y.department).Include(y => y.roles).FirstOrDefaultAsync(y => y.EmployeeCode == id);
            ViewBag.department = await _applicationDbContext.departments.ToListAsync();
            ViewBag.roles = await _applicationDbContext.empRoles.ToListAsync();
            return View(selemployee);
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576000)]
        [RequestSizeLimit(104857600)]
        public async Task<IActionResult> EditEmployee(Employee employee, int id)
        {
            var selemployee = await _applicationDbContext.employees.Include(y => y.department).Include(y => y.roles).FirstOrDefaultAsync(y => y.EmployeeCode == id);
            if (employee.Emp_profile != null)
            {
                string filename = "";
                string updtfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + " " + employee.Emp_profile.FileName;
                string filepath = Path.Combine(updtfolder, filename);
                string ext = Path.GetExtension(employee.Emp_profile.FileName);

                if (ext.ToLower() == ".jfif" || ext.ToLower() == ".png" || ext.ToLower() == ".jpg" || ext.ToLower() == ".webp")
                {
                    employee.Emp_profile.CopyTo(new FileStream(filepath, FileMode.Create));

                    if (employee.Emp_profile.Length <= 1048576000)
                    {


                        {
                            selemployee.Emp_name = employee.Emp_name;
                            selemployee.Emp_email = employee.Emp_email;
                            selemployee.Emp_contact = employee.Emp_contact;
                            selemployee.Emp_address = employee.Emp_address;
                            selemployee.EmpImage = filename;
                            selemployee.Dep_id = employee.Dep_id;
                            selemployee.RoleID = employee.RoleID;
                            selemployee.Emp_Grade = employee.Emp_Grade;
                            selemployee.Emp_age = employee.Emp_age;
                            selemployee.Emp_qualification = employee.Emp_qualification;
                        };
                        _applicationDbContext.employees.Update(selemployee);
                        _applicationDbContext.SaveChanges();
                        TempData["edit"] = "Employee updated successfully";
                        return RedirectToAction("ViewEmployees", "Employees");

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
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var selemployee = await _applicationDbContext.employees.Include(y => y.department).Include(y => y.roles).FirstOrDefaultAsync(y => y.EmployeeCode == id);
            ViewBag.department = await _applicationDbContext.departments.ToListAsync();
            ViewBag.roles = await _applicationDbContext.empRoles.ToListAsync();
            return View(selemployee);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(Employee employee, int id)
        {
            var data = await _applicationDbContext.employees.FindAsync(id);
            string deleteimg = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string currentrecord = Path.Combine(Directory.GetCurrentDirectory(), deleteimg, data.EmpImage);

            _applicationDbContext.employees.Remove(data);

            if (await _applicationDbContext.SaveChangesAsync() > 0)
            {
                if (currentrecord != null)
                {
                    if (System.IO.File.Exists(currentrecord))
                    {
                        System.IO.File.Delete(currentrecord);
                    }
                }
            }
            TempData["del"] = "Employee is deleted successfully";
            return RedirectToAction("ViewEmployees", "Employees");
        }
    }
}
