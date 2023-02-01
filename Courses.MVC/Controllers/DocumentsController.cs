using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Courses.MVC.Models;

namespace Courses.MVC.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IConfiguration _configuration;

        public DocumentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            List<DocumentsViewModel> images = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync("Documents/GetAllUploadImages");
                if (result.IsSuccessStatusCode)
                {
                    images = await result.Content.ReadAsAsync<List<DocumentsViewModel>>();
                }
            }
            return View(images);
        }


        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DocumentsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", model.Images.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Images.CopyToAsync(stream);
                }

                model.ImageUrl = "/Images/" + model.Images.FileName;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                var result = await client.PostAsJsonAsync("Documents/CreateImage", model);
                if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return RedirectToAction("Index", "Documents");

                }
            }
            return View(model);

        }
    }
}
