using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Star_Security_Services.Models.Data
{
    public class Projects
    {
        [Key]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(50)")]
        public string ProjectName { get; set; }
        public string ProjectImage { get; set; }
        [NotMapped]
        public IFormFile Pr_image { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        public DateTime Project_createdate { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        public DateTime Project_enddate { get; set; }

        public int Client_id { get; set; }
        [ForeignKey("Client_id")]
        public Clients clients{ get; set; }

    }
}
