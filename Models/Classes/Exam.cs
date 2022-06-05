using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models.Classes
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Dərs")]
        public Lesson? Lesson { get; set; }
        public int LessonId { get; set; }
        [Required]
        [Display(Name = "Başlama Tarixi")]
        public DateTime ExamStartDate { get; set; }
        [Required]
        [Display(Name = "Bitmə Tarixi")]
        public DateTime ExamEndDate { get; set; }
        [Display(Name = "Ləğv olunub")]
        public bool IsCanceled { get; set; }
    }
}
