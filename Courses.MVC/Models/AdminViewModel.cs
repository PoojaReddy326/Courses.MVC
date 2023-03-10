using System.ComponentModel.DataAnnotations;

namespace Courses.MVC.Models
{
    public class AdminViewModel:Login
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //[Required]
        //public string Email { get; set; }
        //[Required]
        //public string Password { get; set; }
    }
    public class Login
    {
        [Required]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Invalid Email Id")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}

