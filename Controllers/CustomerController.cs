using Helperland.Enum;
using Helperland.Models.Data;
using Helperland.Repository;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            IEnumerable<ServiceRequest> use = _helperlandContext.ServiceRequests.Include(x => x.ServiceProvider).ThenInclude(x=>x.RatingRatingToNavigations).Where(x => x.UserId.Equals(ID)).ToList();
            var User = _helperlandContext.ServiceRequests.Where(address => address.UserId.Equals(ID)).ToList();
            System.Threading.Thread.Sleep(2000);
            return View(use);
            

           
        }

        public IActionResult CustomerServiceHistory()
        {
            return View();
        }


        public IActionResult CustomerDashboard()
        {
            return View();
        }

        
        public IActionResult abcdef(int? Id)
        {
            if(Id != null)
            {
                var ratingdata = _helperlandContext.Ratings.Where(x => x.RatingTo.Equals(Id)).ToList();
                if(ratingdata != null)
                {
                    var ratingavg = ratingdata.Average(x => x.Ratings);
                    return Json(ratingavg);
                }
            }
            return Json(null);
        }
        public IActionResult MyProfile()
        {
            var Customer = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            var FirstName = Customer.FirstName;
            ViewBag.FirstName = FirstName;
            ProfileViewModel mymodel = new ProfileViewModel();
            var ID = Customer.UserId;
            ViewBag.ID = ID;

            mymodel.firstname = Customer.FirstName;
            mymodel.lastname = Customer.LastName;
            mymodel.email = Customer.Email;
            mymodel.Mobile = Customer.Mobile;
           
           
            string datetime = Customer.DateOfBirth.ToString();
            string[] DateTime = datetime.Split(' ');
            string Date = DateTime[0];
            string[] dob = Date.Split('-');
            if(Customer.DateOfBirth != null)
            {
                mymodel.BirthYear = Int32.Parse(dob[2]);
                mymodel.BirthMonth = dob[1];
                mymodel.BirthDate = Int32.Parse(dob[0]);

            }
           

            return View(mymodel);

            
        }


        [HttpPost]

        public ActionResult MyProfile(ProfileViewModel vm)
        {
            if (ModelState.IsValid)
            { 
            vm.DateOfBirth = Convert.ToDateTime(vm.BirthDate + "-" + vm.BirthMonth + "-" + vm.BirthYear);
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




        }

            return View();

        }
        public IActionResult MyPassword()
        {
            return View();
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

        public JsonResult RescheduleGetDetail(int Id)
        {
            
            var servicerequest = _helperlandContext.ServiceRequests.Where(b => b.ServiceRequestId.Equals(Id)).FirstOrDefault();

            var servicedate = servicerequest.ServiceStartDate.ToShortDateString();
            var servicetime = servicerequest.ServiceStartDate.ToLongTimeString();

            string[] times = servicetime.Split(' ');
            return Json(new
            {
                date = servicedate,
                time = times[0],
               
            });

        }
        //public JsonResult UpdateService(int Id, string ServiceDateTime)
        //{
        //    DateTime Date = DateTime.Parse(ServiceDateTime);
        //    var details = _helperlandContext.ServiceRequests.Where(b => b.ServiceRequestId.Equals(Id)).FirstOrDefault();
        //    details.ServiceStartDate = Date;
        //    _helperlandContext.ServiceRequests.Update(details);
        //    _helperlandContext.SaveChanges();
        //    return Json(true);
        //}
        public JsonResult UpdateService(int Id, string ServiceDateTime)
        {
            bool conflictService = false;
            ServiceRequest serviceRequest = _helperlandContext.ServiceRequests.Include(x => x.ServiceProvider).FirstOrDefault(x => x.ServiceRequestId.Equals(Id));
            DateTime newdate = DateTime.Parse(ServiceDateTime);
            if (serviceRequest.ServiceProvider == null)
            {
                serviceRequest.ServiceStartDate = newdate;
                serviceRequest.ModifiedDate = DateTime.Now;
                _helperlandContext.ServiceRequests.Update(serviceRequest);
                _helperlandContext.SaveChanges();
                return Json(true);
            }
            else
            {
                var newstarttime = newdate;
                var extra = Convert.ToDouble(serviceRequest.ExtraHours);
                var newendtime = newstarttime.AddHours(serviceRequest.ServiceHours + extra);
                IEnumerable<ServiceRequest> service = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == serviceRequest.ServiceProviderId && x.Status == 2 && x.ServiceRequestId != serviceRequest.ServiceRequestId).ToList();
                foreach (var request in service)
                {
                    var oldStartTime = request.ServiceStartDate;
                    var oldextra = Convert.ToDouble(request.ExtraHours);
                    var oldEndTime = oldStartTime.AddHours(request.ServiceHours + oldextra);
                    conflictService = false;
                    //check time conflicts
                    if ((request.ServiceStartDate == newdate) || (((newstarttime > oldStartTime) && (newstarttime < oldEndTime)) || ((newendtime > oldStartTime) && (newendtime < oldEndTime))))
                    {
                        conflictService = true;
                        break;
                    }
                }



                if (conflictService)
                {

                    return Json(false);
                }
                else
                {
                    serviceRequest.ServiceStartDate = newdate;
                    serviceRequest.ModifiedDate = DateTime.Now;

                    _helperlandContext.ServiceRequests.Update(serviceRequest);
                    _helperlandContext.SaveChanges();

                    var subject = "";
                    var body = "";

                    if (serviceRequest.ServiceProviderId != null)
                    {
                        subject = "Detail Update";
                        body = "Hi " + "<b>" + serviceRequest.ServiceProvider.FirstName + "</b>" + ", <br/>" +
                            " Service Request Id :" + Id + "This Service is Rescheduled by customer";

                        SendEmail(serviceRequest.ServiceProvider.Email, body, subject);
                    }
                    return Json(true);

                }
            }


        }

        public JsonResult CancleService(int Id)
        {
            var subject = "";
            var body = "";
            var details = _helperlandContext.ServiceRequests.Where(b => b.ServiceRequestId.Equals(Id)).FirstOrDefault();
            User s = _helperlandContext.Users.Where(a => a.UserId.Equals(details.ServiceProviderId)).FirstOrDefault();
            
            details.Status =(int) ServiceStatus.Cancelled;
            details.ModifiedDate = DateTime.Now;
            details.ModifiedBy = (int)UserTypeEnum.Customer;
            _helperlandContext.ServiceRequests.Update(details);
            //_helperlandContext.ServiceRequestAddresses.Remove(detail);
            _helperlandContext.SaveChanges();
            

            if (details.ServiceProviderId != null)
            {
                subject = "Detail Update";
                body = "Hi " + "<b>"  + "</b>" + ", <br/>" +
                    " Service Request Id :" + Id + "This Service is cancelled by customer";
                   
                SendEmail(s.Email, body, subject);
            }
            return Json(true);

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

        public JsonResult GetAddressDetails(int Id)
        {
            var Useraddress = _helperlandContext.UserAddresses.Where(b => b.AddressId.Equals(Id)).FirstOrDefault();

            var Address1 = Useraddress.AddressLine1;
            var Address2 = Useraddress.AddressLine2;
            var Mobile = Useraddress.Mobile;
            var Postal = Useraddress.PostalCode;
            var City = Useraddress.City;

            return Json(new
            {
                line1 = Address1,
                line2 = Address2,
                mobile = Mobile,
                zip = Postal,
                city = City,
               
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
        public IActionResult UpdatePassword(CustomerPasswordViewModel pv)
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




        public FileResult DownloadExcel()
        {
            var Customer = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            var ID = Customer.UserId;
            IEnumerable<ServiceRequest> use = _helperlandContext.ServiceRequests.Include(x => x.ServiceProvider).ThenInclude(x => x.RatingRatingToNavigations).Where(x => x.UserId.Equals(ID) && (x.Status==1 || x.Status == 3)).ToList();

           
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "Service Details";
            Sheet.Cells["B1"].Value = "Service Provider";
            Sheet.Cells["C1"].Value = "Payment";
            Sheet.Cells["D1"].Value = "Status";
            int row = 2;
            foreach (var item in use)
            {
                Sheet.Cells[String.Format("A{0}", row)].Value = item.ServiceStartDate.ToShortDateString() + "\n" + item.ServiceStartDate.ToShortTimeString() + " - " + item.ServiceStartDate.AddHours(Convert.ToDouble(item.SubTotal)).ToShortTimeString();
                Sheet.Cells[String.Format("B{0}", row)].Value = item.ServiceProvider.FirstName + item.ServiceProvider.LastName;
                Sheet.Cells[String.Format("C{0}", row)].Value = item.SubTotal;
                if(item.Status == 1)
                {
                    Sheet.Cells[String.Format("D{0}", row)].Value = "Completed";
                }
                if(item.Status == 3)
                {
                    Sheet.Cells[String.Format("D{0}", row)].Value = "Cancelled";
                }
               
                row++;
            }
             
            Sheet.Cells["A:AZ"].AutoFitColumns();
            using (MemoryStream stream = new MemoryStream())
            {
                Ep.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerServiceHistory.xlsx");
            }



        }

    }
}
