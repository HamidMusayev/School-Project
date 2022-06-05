using SchoolProject.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models.Classes
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Soyad")]
        public string Surname { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Mobil Nömrə")]
        public string Number { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Tip")]
        public UserType Type { get; set; }
        public Class? Class { get; set; }
        [Display(Name = "Sinif")]
        public int? ClassId { get; set; }
        [Display(Name = "Passiv")]
        public bool Passive { get; set; }
    }
}
