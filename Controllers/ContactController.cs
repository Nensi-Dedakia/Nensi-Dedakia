using Helperland.Models.Data;
using Helperland.Repository;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class ContactController : Controller
    {
       
        private readonly HelperlandContext _helperlandContext;

        private readonly IHostingEnvironment hostingEnvironment;


        public ContactController(HelperlandContext helperlandContext,IHostingEnvironment hostingEnvironment)
        {
            _helperlandContext = helperlandContext;
            this.hostingEnvironment = hostingEnvironment;
        }

        //public IActionResult Index()
        //{
        //    ContactU contacts = new ContactU();
        //    return View(contacts);
        //}

        //[HttpPost]
        //public IActionResult Index(ContactU contacts)
        //{
        //    if (ModelState.IsValid)
        //    {


        //        _helperlandContext.ContactUs.Add(contacts);

        //        _helperlandContext.SaveChanges();


        //    }
        //    return View();
        //}
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactUSViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if(model.uploadFileName != null)
                {
                  string uploadFolder =   Path.Combine(hostingEnvironment.WebRootPath, "filestore");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.uploadFileName.FileName;
                    string filepath = Path.Combine(uploadFolder, uniqueFileName);
                    model.uploadFileName.CopyTo(new FileStream(filepath, FileMode.Create));
                }
                ContactU contactus = new ContactU
                {
                    Name = model.firstName + "  " + model.lastName,
                    Email = model.email,
                    PhoneNumber = model.phoneNumber,
                    Subject = model.subject,
                    Message = model.message,
                    UploadFileName = uniqueFileName
                
                };

                _helperlandContext.Add(contactus);
                _helperlandContext.SaveChanges();
                //TempData["Message"] = "Account Created Successfully";
                return RedirectToAction();
            }
            return View();
        }
    }
}

