using Courses.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace Courses.MVC.Controllers
{
    public class ApplicationStatusController : Controller
    {
        private readonly IConfiguration _configuration;
        public ApplicationStatusController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            List<StatusViewModel> status = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync("ApplicationStatus/DisplayStatus");
                if (result.IsSuccessStatusCode)
                {
                    status = await result.Content.ReadAsAsync<List<StatusViewModel>>();
                }

            }
            return View(status);
        }
    }
}
