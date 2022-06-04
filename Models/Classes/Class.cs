using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models.Classes
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public bool Passive { get; set; }
    }
}
