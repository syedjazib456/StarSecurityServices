using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Star_Security_Services.Models.Data
{
    public class Services
    {

        [Key]
        public int ServiceID { get; set; }
        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(150)")]
        public string ServiceName { get; set; }
        [Required(ErrorMessage = "Please fill the required field")]
        public string ServiceImage { get; set; }
        [NotMapped]
        public IFormFile Sr_image { get; set; }
    }
}
