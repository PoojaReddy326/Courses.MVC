using System.ComponentModel.DataAnnotations;

namespace Courses.MVC.Models
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter 16 digit Reference Number")]
        public string ReferenceNumber { get; set; }
        [Required(ErrorMessage ="Please Enter the amount")]
        [Range(2000,3000)]
        public double Amount { get; set; }

    }
}
