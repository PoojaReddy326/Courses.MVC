using Courses.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Courses.MVC.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IConfiguration _configuration;
        public FileUploadController(IConfiguration configuration)
        {
            _configuration = configuration;
        }   

        public async Task<IActionResult> Documents()
        {
            List<FileViewModel> files = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync("FileUpload/GetAllImages");
                if (result.IsSuccessStatusCode)
                {
                    files = await result.Content.ReadAsAsync<List<FileViewModel>>();
                }
            }
            return View(files);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(FileViewModel form)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                var result = await client.PostAsJsonAsync("FileUpload/Upload", form);
                if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> Details()
        {

            FileViewModel form = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Documents/GetAllImages");
                if (result.IsSuccessStatusCode)
                {
                    form = await result.Content.ReadAsAsync<FileViewModel>();

                }
            }
            return View(form);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(FileViewModel form)
        {
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.DeleteAsync($"FileUpload/DeleteImage/{form.Id}");
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [NonAction]
        public async Task<FileViewModel> FormById(int id)
        {
            FileViewModel form = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"FileUpload/DeleteImage/{id}");
                if (result.IsSuccessStatusCode)
                {
                    form = await result.Content.ReadAsAsync<FileViewModel>();
                }
            }
            return form;
        }
    }
}
