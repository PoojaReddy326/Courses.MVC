using Courses.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Courses.MVC.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IConfiguration _configuration;
        public CoursesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AllCourses()
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                    var result = await client.PostAsJsonAsync("Course/CreateCourse", course);
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }


        public async Task<IActionResult> Details()
        {
            List<CourseViewModel> course = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync("Course/DisplayCourses");
                if (result.IsSuccessStatusCode)
                {
                    course = await result.Content.ReadAsAsync<List<CourseViewModel>>();
                }
            }
            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            CourseViewModel course = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Course/GetCourseById/{Id}");
                if (result.IsSuccessStatusCode)
                {
                    course = await result.Content.ReadAsAsync<CourseViewModel>();
                    return View(course);
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                    var result = await client.PutAsJsonAsync($"Course/UpdateCourse/{course.CourseId}", course);
                    if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            CourseViewModel courses = await this.CourseById(id);
            if (courses != null)
            {
                return View(courses);
            }
            ModelState.AddModelError("", "Server Error. Please try later");
            return View(courses);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CourseViewModel courses)
        {
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                var result = await client.DeleteAsync($"Course/DeleteCourse/{courses.CourseId}");
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(courses);
        }

        [NonAction]
        public async Task<CourseViewModel> CourseById(int id)
        {
            CourseViewModel course = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Course/GetCourseById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    course = await result.Content.ReadAsAsync<CourseViewModel>();
                }
            }
            return course;
        }
    }
}



        

