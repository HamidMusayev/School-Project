﻿using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models.Classes
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        [Display(Name = "Dərs adı")]
        public string Name { get; set; }
        [Display(Name = "Sinif")]
        public Class? Class { get; set; }
        public int ClassId { get; set; }
        [Display(Name = "Müəllim")]
        public User? Teacher { get; set; }
        public int TeacherId { get; set; }
        [Display(Name = "Passiv")]
        public bool Passive { get; set; }
    }
}
