using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models.Classes
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        public Class Class { get; set; }
        public User Teacher { get; set; }
        [Required]
        public DateTime ExamStartDate { get; set; }
        [Required]
        public DateTime ExamEndDate { get; set; }
        public bool IsCanceled { get; set; }
    }
}
