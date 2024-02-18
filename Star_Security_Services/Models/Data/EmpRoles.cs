using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Star_Security_Services.Models.Data
{
    public class EmpRoles
    {
        [Key]
        public int Role_id { get; set; }

        [Required(ErrorMessage = "Please fill the required")]
        [Column(TypeName = "Varchar(70)")]
        public string Role_name { get; set; }
    }
}
