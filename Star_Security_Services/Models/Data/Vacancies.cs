using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Star_Security_Services.Models.Data
{
    public class Vacancies
    {
        [Key]
       public int ID { get; set; }
        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(150)")]
        public string PostName { get; set; }
        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(max)")]
        public string JobDescription { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(50)")]
        public string Task1 { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(50)")]
        public string Task2 { get; set; }

        [Required(ErrorMessage = "Please fill the required field")]
        [Column(TypeName = "Varchar(50)")]
        public string Task3 { get; set; }

    }
}
