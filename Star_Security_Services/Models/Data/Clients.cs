using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Star_Security_Services.Models.Data
{
    public class Clients
    {
        [Key]
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(50)")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(50)")]
        public string ClientProfession { get; set; }
        [Required(ErrorMessage = "Please fill the required field")]
        public string ClientImage { get; set; }
        [NotMapped]
        public IFormFile Client_image { get; set; }

        [Column(TypeName = "Varchar(max)")]
        [Required(ErrorMessage = "Please fill the required field")]
        public string ClientReview { get; set; }
    }
}
