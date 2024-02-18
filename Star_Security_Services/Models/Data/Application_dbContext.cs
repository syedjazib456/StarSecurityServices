using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Star_Security_Services.Models.Data
{
    public class Application_dbContext: IdentityDbContext
    {
        public Application_dbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Clients> clients { get; set; }
        public DbSet<Contact> contacts { get; set; }
        public DbSet<Industries> industries { get; set; }
        public DbSet<Vacancies> vacancies { get; set;}
        public DbSet<Services> services { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<EmpRoles> empRoles { get; set; }   

        public DbSet<Admin> Admins { get; set; }
    }
}
