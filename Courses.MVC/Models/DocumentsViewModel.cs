using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Courses.MVC.Models
{
    public class DocumentsViewModel
    {
        public int Id { get; set; }
        [Required]
        public IFormFile Images { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
    }
}
