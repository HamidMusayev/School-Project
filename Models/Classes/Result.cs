using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Models.Classes
{
    public class Result
    {
        [Key]
        public int Id { get; set; }
        public Exam Exam { get; set; }
        public User Student { get; set; }
        [Required]
        [Column(TypeName = "decimal(3,2)")]
        public decimal Score { get; set; }
        public bool Passive { get; set; }
    }
}
