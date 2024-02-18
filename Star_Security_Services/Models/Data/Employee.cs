using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Star_Security_Services.Models.Data
{
    public class Employee
    {

        [Key]
        public int EmployeeCode { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(50)")]
        public string Emp_name { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(max)")]
        [EmailAddress]
        public string Emp_email { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(50)")]
        public string Emp_contact { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(max)")]
        public string Emp_address { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(max)")]
        public string Emp_qualification { get; set; }

       

        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(max)")]
        public string Emp_Grade { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        public int Emp_age { get; set; }

        [NotMapped]
        public IFormFile Emp_profile { get; set; }
        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(max)")]
        public string EmpImage { get; set; }

        public int Dep_id { get; set; }
        [ForeignKey("Dep_id")]
        public Department department { get; set; }

        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public EmpRoles roles { get; set; }
    }
}
