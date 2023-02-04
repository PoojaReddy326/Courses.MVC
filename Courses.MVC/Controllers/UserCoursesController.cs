using Courses.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
namespace Courses.MVC.Controllers
{
    public class UserCoursesController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserCoursesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> AllCoursesUser()
        {
            List<CourseViewModel> courses = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync("Course/DisplayCourses");
                if (result.IsSuccessStatusCode)
                {
                    courses = await result.Content.ReadAsAsync<List<CourseViewModel>>();
                }
            }
            return View(courses);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}