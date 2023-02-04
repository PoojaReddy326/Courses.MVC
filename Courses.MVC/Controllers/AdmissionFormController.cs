using Courses.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Courses.MVC.Controllers
{
    public class AdmissionFormController : Controller
    {
        private readonly IConfiguration _configuration;
        public AdmissionFormController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //Function Used to returning all Application Details
        public async Task<IActionResult> ApplicationDetails()
        {
            List<FormViewModel> forms = new();
            using (var client = new HttpClient())
            {
                //ApiUrl is used for accessing functions from .NET Core api
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                //Passing method name should be same as in api
                var result = await client.GetAsync("ApplicationForms/GetAllForms");
                if (result.IsSuccessStatusCode)
                {
                    forms = await result.Content.ReadAsAsync<List<FormViewModel>>();
                }

            }
            return View(forms);
        }

      //Function Used for Getting Specific details of Student
        [Route("AdmissionForm/Details/{id}")]
        [HttpGet]

        public async Task<IActionResult> Details(int id)
        {

            FormViewModel form = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"ApplicationForms/GetFormsById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    form = await result.Content.ReadAsAsync<FormViewModel>();

                }
            }
            return View(form);
        }
        //Function for application drop
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            FormViewModel form = await this.FormById(id);
            if (form != null)
            {
                return View(form);
            }
            ModelState.AddModelError("", "Server Error. Please try later");
            return View(form);
        }


        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(FormViewModel form)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                    var result = await client.PostAsJsonAsync("ApplicationForms/CreateForm", form);
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("ApplicationDetails");
                    }
                }
            }
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int Id)
        {

            FormViewModel form = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"ApplicationForms/GetFormsById/{Id}");
                if (result.IsSuccessStatusCode)
                {
                    form = await result.Content.ReadAsAsync<FormViewModel>();
                    return View(form);
                }


            }
            return View();
        }

        [HttpPost("AdmissionForm/Edit/{Id}")]

        public async Task<IActionResult> Edit(FormViewModel form)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    
                    client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                    var result = await client.PutAsJsonAsync($"ApplicationForms/UpdateForm/{form.Id}", form);
                    if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return RedirectToAction("ApplicationDetails");
                    }
                }
            }
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Delete(FormViewModel form)
        {
            using (var client = new HttpClient())
            {
                
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.DeleteAsync($"ApplicationForms/DeleteForm/{form.Id}");
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ApplicationDetails");
                }
            }
            return View();
        }
        [NonAction]
        public async Task<FormViewModel> FormById(int id)
        {
            FormViewModel form = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"ApplicationForms/GetFormsById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    form = await result.Content.ReadAsAsync<FormViewModel>();
                }
            }
            return form;
        }
    }
}
