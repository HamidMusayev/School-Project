using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Models.Classes
{
    public partial class Class
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Sinif adı")]
        public string Name { get; set; }
        [Display(Name = "Passiv")]
        public bool Passive { get; set; }
    }

    public partial class Class
    {
        [NotMapped]
        [Display(Name = "Tələbə sayı")]
        public int StudentCount { get; set; }
    }
}
