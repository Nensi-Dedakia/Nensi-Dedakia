using Helperland.Models.Data;
using Helperland.Repository;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HelperlandContext _helperlandContext;

        public CustomerController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;

        }
        public IActionResult CustomerPage()
        {
            var Customer = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            
                var firstname = Customer.FirstName;
                ViewBag.Message = firstname;
            var ID = Customer.UserId;

            System.Threading.Thread.Sleep(2000);
            return View(_helperlandContext.ServiceRequests.Where(address => address.UserId.Equals(ID)).ToList());


           
        }

        public IActionResult CustomerServiceHistory()
        {
            return View();
        }


        public IActionResult CustomerDashboard()
        {
            return View();
        }

        public IActionResult MyProfile()
        {
            var Customer = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            ProfileViewModel mymodel = new ProfileViewModel();
            var ID = Customer.UserId;
            ViewBag.ID = ID;

            mymodel.firstname = Customer.FirstName;
            mymodel.lastname = Customer.LastName;
            mymodel.email = Customer.Email;
            mymodel.Mobile = Customer.Mobile;
            return View(mymodel);

            
        }


        [HttpPost]

        public ActionResult MyProfile(ProfileViewModel vm)
        {


            User profiledetails = new User();

            var data = (from userlist in _helperlandContext.Users
                        where userlist.Email == HttpContext.Session.GetString("Email")
                        select new
                        {
                            userlist.UserId,
                            userlist.FirstName,
                            userlist.LastName,
                            userlist.Email,
                            userlist.Mobile,
                            userlist.DateOfBirth,
                            userlist.Password

                        }).ToList();
            profiledetails.UserId = data[0].UserId;
            profiledetails.FirstName = vm.firstname;
            profiledetails.LastName = vm.lastname;
            profiledetails.Mobile = vm.Mobile;
            profiledetails.DateOfBirth = vm.DateOfBirth;
            profiledetails.Email = data[0].Email;
            profiledetails.UserTypeId = 1;
            profiledetails.IsRegisteredUser = true;
            profiledetails.WorksWithPets = true;
            profiledetails.CreatedDate = DateTime.Now;
            profiledetails.ModifiedDate = DateTime.Now;
            profiledetails.ModifiedBy = 1;
            profiledetails.IsApproved = true;
            profiledetails.IsActive = true;
            profiledetails.IsDeleted = true;
            profiledetails.Password = data[0].Password;


            _helperlandContext.Users.Update(profiledetails);
            _helperlandContext.SaveChanges();
            //  return RedirectToAction("Index", "Home");






            return Ok(Json("true"));

        }








        public IActionResult MyAddress()
        {



            var CustomerId = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            int ID = CustomerId.UserId;

            var AddressID = _helperlandContext.UserAddresses.Where(b => b.UserId.Equals(ID)).FirstOrDefault();
            ViewBag.AddressID = AddressID.AddressId;

            System.Threading.Thread.Sleep(2000);
            return View(_helperlandContext.UserAddresses.Where(address => address.UserId.Equals(ID)).ToList());
        }
        public JsonResult UpdateService(int Id, string ServiceDateTime)
        {
            DateTime Date = DateTime.Parse(ServiceDateTime);
            var details = _helperlandContext.ServiceRequests.Where(b => b.ServiceRequestId.Equals(Id)).FirstOrDefault();
            details.ServiceStartDate = Date;
            _helperlandContext.ServiceRequests.Update(details);
            _helperlandContext.SaveChanges();
            return Json(true);
        }

        public JsonResult CancleService(int Id)
        {
            var details = _helperlandContext.ServiceRequests.Where(b => b.ServiceRequestId.Equals(Id)).FirstOrDefault();
            var detail = _helperlandContext.ServiceRequestAddresses.Where(b => b.ServiceRequestId.Equals(Id)).FirstOrDefault();
            _helperlandContext.ServiceRequests.Remove(details);
            _helperlandContext.ServiceRequestAddresses.Remove(detail);
            _helperlandContext.SaveChanges();
            return Json(true);

        }


        [HttpPost]
        public JsonResult CustomerServiceDetail(int Id)
        {
            var details = _helperlandContext.ServiceRequests.Where(b => b.ServiceRequestId.Equals(Id)).FirstOrDefault();
            DateTime dt = details.ServiceStartDate;
            string datetime = dt.ToString();
            string[] DateTime = datetime.Split(' ');
            string Date = DateTime[0];
            string Time = DateTime[1];
            string[] time = Time.Split(':');
            string clocktime = time[0] + ":" + time[1];
            var ServiceDate = Date;
            var ServiceTime = clocktime;
            var Duration = details.ServiceHours;
            var SrID = details.ServiceRequestId;
            var Extra = details.ExtraHours;
            var NetAmount = details.SubTotal;


            var AddressDetails = _helperlandContext.ServiceRequestAddresses.Where(b => b.ServiceRequestId.Equals(Id)).FirstOrDefault();
            var ServiceAddress = AddressDetails.AddressLine1 + AddressDetails.AddressLine2 + AddressDetails.City + AddressDetails.PostalCode;
            var Phone = AddressDetails.Mobile;
            var Email = AddressDetails.Email;
            // return View(_helperlandContext.ServiceRequests.Where(b => b.ServiceRequestId.Equals(Id)).ToList());
            return Json(new
            {
                id = SrID,
                extra = Extra,
                netamount = NetAmount,
                serviceaddress = ServiceAddress,
                phone = Phone,
                email = Email,
                servicedate = ServiceDate,
                servicetime = ServiceTime,
                duration = Duration
            });

        }





        public JsonResult UpdateAddress(ProfileViewModel model)
        {

            var details = _helperlandContext.UserAddresses.Where(b => b.AddressId.Equals(model.AddressId)).FirstOrDefault();


            if (details == null)
            {
                var Customer = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();

                var ID = Customer.UserId;

                UserAddress use = new UserAddress();

                use.AddressLine1 = model.AddressLine1;
                use.AddressLine2 = model.AddressLine2;
                use.City = model.City;
                use.PostalCode = model.PostalCode;
                use.Mobile = model.Mobile;
                use.UserId = ID;
                _helperlandContext.Add(use);

                _helperlandContext.SaveChanges();
                return Json(true);
            }
            else
            {
                details.AddressId = model.AddressId;
                details.AddressLine1 = model.AddressLine1;
                details.AddressLine2 = model.AddressLine2;
                details.City = model.City;
                details.PostalCode = model.PostalCode;
                details.Mobile = model.Mobile;

                _helperlandContext.UserAddresses.Update(details);
                _helperlandContext.SaveChanges();
                return Json(true);
            }

        }



        public JsonResult RemoveAddress(int Id)
        {
            var details = _helperlandContext.UserAddresses.Where(b => b.AddressId.Equals(Id)).FirstOrDefault();

            _helperlandContext.UserAddresses.Remove(details);

            _helperlandContext.SaveChanges();
            return Json(true);

        }

        //[HttpPost]
        //public IActionResult UpdatePassword(ProfileViewModel pvm)
        //{
        //    var olddetails = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
        //    var oldpwd = olddetails.Password;
        //    //pvm.Password = Crypto.Hash(pvm.Password);
        //    // var pass = Crypto.Hash(pvm.Password);
        //    int ID = olddetails.UserId;

        //    if (string.Compare(Crypto.Hash(pvm.OldPassword), oldpwd) == 0)

        //    {
        //        User view = new User();

        //        var pwd = (from userlist in _helperlandContext.Users
        //                   where userlist.Email == HttpContext.Session.GetString("Email")
        //                   select new
        //                   {
                               
        //                       userlist.FirstName,
        //                       userlist.LastName,
        //                       userlist.Email,
        //                       userlist.Mobile,
        //                       userlist.DateOfBirth,
        //                       userlist.Password,
        //                       userlist.CreatedDate

        //                   }).ToList();
        //        view.UserId = ID;
        //        view.FirstName = pwd[0].FirstName;
        //        view.LastName = pwd[0].LastName;
        //        view.Mobile = pwd[0].Mobile;
        //        view.DateOfBirth = pwd[0].DateOfBirth;
        //        view.Email = pwd[0].Email;
        //        view.UserTypeId = 1;
        //        view.IsRegisteredUser = true;
        //        view.WorksWithPets = true;
        //        view.CreatedDate = pwd[0].CreatedDate;
        //        view.ModifiedDate = DateTime.Now;
        //        view.ModifiedBy = 1;
        //        view.IsApproved = true;
        //        view.IsActive = true;
        //        view.IsDeleted = true;
        //        view.Password = pvm.Password;


        //        _helperlandContext.Users.Update(view);
        //        _helperlandContext.SaveChanges();

        //        // return RedirectToAction("MyProfile", "Customer");
        //        return Ok(Json("true"));
        //    }

        //    else
        //    {
        //        ViewBag.Message = "Invalid Old Password";
        //        return RedirectToAction("MyProfile", "Customer");
        //    }


        //}

        [HttpPost]
        public IActionResult UpdatePassword(ProfileViewModel pv)
        {
            if (ModelState.IsValid)
            {
                User use = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
                var oldpwd = use.Password;
                var userid = use.UserId;

                if (string.Compare(Crypto.Hash(pv.OldPassword), oldpwd) == 0)
                {
                    if (pv.Password == pv.ConfirmPassword)
                    {
                        pv.Password = Crypto.Hash(pv.Password);
                        use.Password = pv.Password;
                        use.ModifiedDate = DateTime.Now;

                        _helperlandContext.Users.Update(use);
                        _helperlandContext.SaveChanges();
                        // return Ok(Json("true"));
                        TempData["Message"] = "Password Update Successfully";
                    }
                    else
                    {
                        TempData["Message"] = "Does not match confirm password and New Password!";
                        // return Ok(Json("false"));
                    }
                }
                else
                {
                    // ViewBag.Pass = "Plz Enter Valid Old Password";
                    TempData["Message"] = "Old Password is invalid";
                    //return Ok(Json("false"));
                }

            }
                return View("MyProfile");
            
        }



        public JsonResult Rating(int id, int OnTimeArrival, int Friendly, int QualityOfService, decimal Ratings, string Comments)
        {
            var sr = _helperlandContext.ServiceRequests.Where(a => a.ServiceRequestId.Equals(id)).FirstOrDefault();
            //var serviceprovider = sr.ServiceProviderId;
            // var customer = sr.UserId;
            Rating ratingtable = _helperlandContext.Ratings.Where(a => a.ServiceRequestId.Equals(id)).FirstOrDefault();
            if (ratingtable != null)
            {

                ratingtable.OnTimeArrival = OnTimeArrival;
                ratingtable.Friendly = Friendly;
                ratingtable.QualityOfService = QualityOfService;
                ratingtable.RatingDate = DateTime.Now;
                ratingtable.Ratings = Ratings;
                _helperlandContext.Ratings.Update(ratingtable);
                _helperlandContext.SaveChanges();


            }

            else
            {

                Rating rate = new Rating();
                rate.ServiceRequestId = sr.ServiceRequestId;
                rate.RatingFrom = sr.UserId;
                rate.RatingTo = Convert.ToInt32(sr.ServiceProviderId);
                rate.Ratings = Ratings;
                rate.OnTimeArrival = OnTimeArrival;
                rate.Friendly = Friendly;
                rate.QualityOfService = QualityOfService;
                rate.RatingDate = DateTime.Now;
                rate.Comments = Comments;

                _helperlandContext.Ratings.Add(rate);
                _helperlandContext.SaveChanges();
            }
            return Json(true);
        }

    }
}
