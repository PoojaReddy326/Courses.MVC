using System.ComponentModel.DataAnnotations;

namespace Courses.MVC.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Enter the course Name"), StringLength(50)]
        public string CourseName { get; set; }
    }
}
