﻿using Helperland.Enum;
using Helperland.Models;
using Helperland.Models.Data;
using Helperland.Repository;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Helperland.Controllers
{

    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly HelperlandContext _helperlandContext;

        public HomeController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }




        public IActionResult CreateAnAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAnAccount(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Password = Crypto.Hash(model.Password);

                model.ConfirmPassword = Crypto.Hash(model.ConfirmPassword);

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





        public IActionResult ServiceProviderRegistration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ServiceProviderRegistration(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Password = Crypto.Hash(model.Password);

                model.ConfirmPassword = Crypto.Hash(model.ConfirmPassword);

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




        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel use)
        {
           
            using (HelperlandContext _tc = new HelperlandContext())
            {

                if (ModelState.IsValid)
                {
                    var detail = _helperlandContext.Users.Where(a => a.Email.Equals(use.email)).FirstOrDefault();
                if (detail == null)
                {
                       ViewBag.email = "Invalid Email";
                        TempData["Modal"] = "#myModal";
                        return View();
                }
                HttpContext.Session.SetString("Email", use.email);
                HttpContext.Session.SetString("Password", use.Password);
                if (detail != null)
                {
                    if (string.Compare(Crypto.Hash(use.Password), detail.Password) == 0)
                    {
                        //var check = use.RememberMe;
                        //if (check == true)
                        //{
                        //    string key = use.Password;
                        //    string value = use.email;
                        //    CookieOptions options = new CookieOptions
                        //    {
                        //        Expires = DateTime.Now.AddMinutes(5)
                        //    };
                        //    Response.Cookies.Append(key, value, options);

                        //}
                        if (detail.UserTypeId == 1)
                        {
                            return RedirectToAction("CustomerPage", "Customer");
                        }
                        if (detail.UserTypeId == 2)
                        {
                            return RedirectToAction("ServiceProviderPage", "ServiceProvider");
                        }
                        if(detail.UserTypeId == 3)
                        {
                            return RedirectToAction("AdminRequest", "Admin");
                        }
                        
                        
                    }
                    else
                    {
                        ViewBag.password = "Invalid Password";
                            TempData["Modal"] = "#myModal";
                        }
                }

                }
                TempData["Modal"] = "#myModal";

            }
           
            return View("Index");
           
          

        }
        public IActionResult LogeddIn(CreateAccountViewModel use)
        {
           
            
                if (HttpContext.Session.GetString("Email") == null)
                {
                    string key = use.Password;
                    var CookieValue = Request.Cookies[key];


                    return RedirectToAction("Index");
                }
             
            return View();
        }
        public IActionResult LogeddOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        //[HttpPost]
        //public ActionResult ForgotPassword(string Email)
        //{
        //    string ResetCode = Guid.NewGuid().ToString();

        //    var uriBuilder = new UriBuilder
        //    {
        //        Scheme = Request.Scheme,
        //        Host = Request.Host.Host,
        //        Port = Request.Host.Port ?? -1, //bydefault -1
        //                                        // Path = $"LoginPage/Home/ResetPassword/{ResetCode}"
        //        Path = $"LoginPage/Views/Home/ResetPassword/{ResetCode}"


        //    };
        //    var link = uriBuilder.Uri.AbsoluteUri;
            
        //    using (var context = new HelperlandContext())
        //    {
        //        var getUser = (from s in context.Users where s.Email == Email select s).FirstOrDefault();
        //        if (getUser != null)
        //        {
        //            getUser.ResetPasswordCode = ResetCode;

                    
        //            _helperlandContext.SaveChanges();

        //            var subject = "Password Reset Request";
        //            var body = "Hi " + getUser.FirstName + ", <br/> You recently requested to reset your password for your account. Click the link below to reset it. " +

        //                 " <br/><br/><a href='" + link + "'>" + link + "</a> <br/><br/>" +
        //                 "If you did not request a password reset, please ignore this email or reply to let us know.<br/><br/> Thank you";

        //            SendEmail(getUser.Email, body, subject);

        //            ViewBag.Message = "Reset password link has been sent to your email id.";
        //        }
        //        else
        //        {
        //            ViewBag.Message = "User doesn't exists.";
        //            return RedirectToAction("ResetPassword");
        //        }
        //    }

        //    return RedirectToAction("ResetPassword");
        //}
        [HttpPost]
        public IActionResult ForgotPassword(String email)
        {
            var user = _helperlandContext.Users.Where(X => X.Email.Equals(email)).FirstOrDefault();
            if (ModelState.IsValid)
            {


                if (user != null)
                {
                    var token = Guid.NewGuid().ToString();
                    var UserID = user.UserId;
                    var ResetLink = Url.Action("ResetPassword", "Home", new { ID = UserID, Email = email, Token = token }, Request.Scheme);
                    var subject = "Password Reset Request";
                    var body = "Hi " + user.FirstName + ", <br/> You recently requested to reset your password for your account. Click the link below to reset it. " +

                                   " <br/><br/><a href='" + ResetLink + "'>" + ResetLink + "</a> <br/><br/>" +
                                 "If you did not request a password reset, please ignore this email or reply to let us know.<br/><br/> Thank you";

                    SendEmail(user.Email, body, subject);
                    ViewBag.success = "Reset Password Link has been sent to your email id";
                    TempData["Modal"] = "#forgotModal";
                }
                else
                {
                  ViewBag.alert =  ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>"  + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    ViewBag.email = "Invalid Email";
                    TempData["Modal"] = "#forgotModal";
                    return View();
                }
            }
            TempData["Modal"] = "#forgotModal";
            return RedirectToAction("Index");
        }
        private void SendEmail(string emailAddress, string body, string subject)
        {
            using (MailMessage mm = new MailMessage("18it.nensi.dedakia@gmail.com", emailAddress))
            {
                mm.Subject = subject;
                mm.Body = body;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com";




                smtp.UseDefaultCredentials = false;
                NetworkCredential NetworkCred = new System.Net.NetworkCredential("18it.nensi.dedakia@gmail.com", "9737012809Jayshri@123");
                smtp.Credentials = NetworkCred;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(mm);

            }
        }

        [HttpGet]
        public IActionResult ResetPassword(int ID , string Email , string Token )
        {
            if(Email == null || Token == null)
            {
                ViewBag.error = "Invalid Password Reset Token";
            }
            ViewBag.Email = Email;
            return View();

        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel objuser)
        {
            ViewBag.Email = objuser.Email;
           if(ModelState.IsValid)
           {
                User user = new User();
                var data = (from userlist in _helperlandContext.Users
                            where userlist.Email == objuser.Email
                            select new
                            {
                                userlist.UserId,
                                userlist.FirstName,
                                userlist.LastName,
                                userlist.Email,
                                userlist.Mobile,
                                userlist.Password

                            }).ToList();
                if(data.FirstOrDefault() != null)
                {
                    user.UserId = data[0].UserId;

                    user.Email = data[0].Email;
                    user.FirstName = data[0].FirstName;
                    user.LastName = data[0].LastName;
                    user.Mobile = data[0].Mobile;
                    user.UserTypeId = 1;
                    user.IsRegisteredUser = true;
                    user.WorksWithPets = true;
                    user.CreatedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    user.ModifiedBy = 2;
                    user.IsApproved = true;
                    user.IsActive = true;
                    user.IsDeleted = true;
                    user.Password = objuser.NewPassword;

                    _helperlandContext.Users.Update(user);
                    _helperlandContext.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
                return View(); 
           }
           // return RedirectToAction("Index", "Home");
             return View();
        }
        //[HttpGet]
        //public ActionResult ResetPassword(string id)
        //{

        //    if (string.IsNullOrWhiteSpace(id))
        //    {
        //        return NotFound();
        //    }

        //    using (var context = new HelperlandContext())
        //    {
        //        var user = context.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
        //        if (user != null)
        //        {
        //            ResetPasswordViewModel model = new ResetPasswordViewModel();
        //            model.ResetCode = id;
        //            return View(model);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ResetPassword(ResetPasswordViewModel model)
        //{
        //    var message = "";
        //    if (ModelState.IsValid)
        //    {
        //        using (var context = new HelperlandContext())
        //        {
        //            var user = context.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
        //            if (user != null)
        //            {

        //                user.Password = model.NewPassword;

        //                user.ResetPasswordCode = "";


        //                context.SaveChanges();
        //                message = "New password updated successfully";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        message = "Something invalid";
        //    }
        //    ViewBag.Message = message;
        //    return View(model);
        //}




        public IActionResult faq()
        {
            return View();
        }
        public IActionResult price()
        {
            return View();
        }
        //public IActionResult contact()
        //{
        //    return View();
        //}
        public IActionResult aboutus()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
