
using Helperland.Enum;
using Helperland.Models.Data;
using Helperland.Repository;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class BecomeAProviderController : Controller
    {
        private readonly HelperlandContext _helperlandContext;

        public BecomeAProviderController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        public IActionResult ServiceProviderRegistration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ServiceProviderRegistration(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {

                User serviceprovider = new User
                {
                    FirstName = model.firstName,
                    LastName = model.lastName,
                    Email = model.email,
                    Mobile = model.mobile,
                    Password = model.Password,
                    CreatedDate = DateTime.Now,

                    ModifiedDate = DateTime.Now,
                    UserTypeId = (int)UserTypeEnum.ServiceProvider

                };

                _helperlandContext.Add(serviceprovider);
                _helperlandContext.SaveChanges();
                //TempData["Message"] = "Account Created Successfully";
                return RedirectToAction();
            }
            return View();
        }

    }
}
