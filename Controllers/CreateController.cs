using Helperland.Enum;
using Helperland.Models.Data;
using Helperland.Repository;
using Helperland.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class CreateController : Controller
    {
        private readonly HelperlandContext _helperlandContext;

        public CreateController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        //public IActionResult CreateAnAccount()
        //{
        //    User use = new User();
        //    return View(use);
        //}

        //[HttpPost]

        //public IActionResult CreateAnAccount(User use)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        use.CreatedDate = DateTime.Now;
        //        use.ModifiedDate = DateTime.Now;



        //        _helperlandContext.Users.Add(use);
        //        _helperlandContext.SaveChanges();

        //        return RedirectToAction("CreateAnAccount");


        //    }
        //    return View();
        //}
        public IActionResult CreateAnAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAnAccount(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {

                User user = new User
                {
                    FirstName = model.firstName,
                    LastName = model.lastName,
                    Email = model.email,
                    Mobile = model.mobile,
                    Password = model.Password,
                    CreatedDate = DateTime.Now,
                    
                    ModifiedDate = DateTime.Now,
                    UserTypeId = (int)UserTypeEnum.Customer


                };

                _helperlandContext.Add(user);
                _helperlandContext.SaveChanges();
                return RedirectToAction();
            }
            return View();
        }

        //public IActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]

        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await SignInManager

        //        _helperlandContext.Add(result);
        //        _helperlandContext.SaveChanges();
        //        return RedirectToAction();
        //    }
        //    return View();
        //}
    }
}
