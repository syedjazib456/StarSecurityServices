using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Star_Security_Services.Models.Data
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(50)")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(max)")]
        [EmailAddress]
        public string AdminEmail { get; set; }


        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(50)")]
        public string AdminContact { get; set; }
        [Required(ErrorMessage = "Please fill the required field")]
        public string AdminImage { get; set; }
        [NotMapped]
        public IFormFile Admin_image { get; set; }

    }
}
