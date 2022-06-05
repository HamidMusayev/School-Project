using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Models.Classes
{
    public partial class Lesson
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        [Display(Name = "Dərs adı")]
        public string Name { get; set; }
        public Class? Class { get; set; }
        [Display(Name = "Sinif")]
        public int ClassId { get; set; }
        public User? Teacher { get; set; }
        [Display(Name = "Müəllim")]
        public int TeacherId { get; set; }
        [Display(Name = "Passiv")]
        public bool Passive { get; set; }
    }

    public partial class Lesson
    {
        [NotMapped]
        public string ExamAddDropdownName => $"Sinif: {Class?.Name}, Fənn: {Name}";
    }
}
