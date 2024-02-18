using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Star_Security_Services.Models.Data
{
    public class Department
    {
        [Key]
        public int Dept_id { get; set; }

        [Required(ErrorMessage = "Please fill the required")]
        [Column(TypeName = "Varchar(70)")]
        public string Dept_name { get; set; }

        [Required(ErrorMessage = "Please fill the required")]
        [Column(TypeName = "Varchar(70)")]
        public string Dept_location { get; set; }
    }
}
