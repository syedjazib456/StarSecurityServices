using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Star_Security_Services.Models.Data
{
    public class Industries
    {
        [Key]
        public int IndustryId { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(50)")]
        public string IndustryName { get; set; }
        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(max)")]
        public string IndustryImage { get; set; }
        [NotMapped]
        public IFormFile Ind_image { get; set; }
    }
}
