using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models.Classes
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        public Class Class { get; set; }
        public User Teacher { get; set; }
        public bool Passive { get; set; }
    }
}
