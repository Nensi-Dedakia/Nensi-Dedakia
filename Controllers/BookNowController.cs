using Helperland.Models.Data;
using Helperland.Repository;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class BookNowController : Controller
    {
        private readonly HelperlandContext _helperlandContext;

        public BookNowController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;

        }
        [HttpGet]

        public IActionResult BookNow()
        {
            return View();
        }

        [HttpPost]

        
        public ActionResult ZipCodeValue(BookNowViewModel model)
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                var postal = _helperlandContext.Users.Where(b => b.ZipCode.Equals(model.ZipCode) && b.UserTypeId == 2).FirstOrDefault();
                if (postal != null)
                {
                    HttpContext.Session.SetString("ZipCode", model.ZipCode);
                    return Ok(Json("true"));
                }
                else
                {
                    return Ok(Json("false"));
                }
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }

           
        }


        [HttpPost]

        public IActionResult Schedule(BookNowViewModel schedule)
        {
            // if(ModelState.IsValid)

            return Ok(Json("true"));

            //else
            //{
            //    return Ok(Json("false"));
            //}
        }

        [HttpGet]

        public IActionResult Address()
        {
            var CustomerId = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            int ID = CustomerId.UserId;

            System.Threading.Thread.Sleep(2000);

            return View(_helperlandContext.UserAddresses.Where(address => address.UserId.Equals(ID)).ToList());



        }






        [HttpPost]

        public IActionResult AddNewAddress(BookNowViewModel address)

        {
            var user = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            var userid = user.UserId;
            UserAddress add = new UserAddress
            {
                UserId = userid,
                AddressLine1 = address.addressline1,
                AddressLine2 = address.addressline2,
                City = address.city,
                State = address.state,
                PostalCode = address.postalcode,
                Mobile = address.mobile,
                
            };


            _helperlandContext.Add(add);

            _helperlandContext.SaveChanges();
            return Ok(Json("true"));
          
        }

        [HttpPost]
        public IActionResult YourDetail(int radio)
        {

            HttpContext.Session.SetInt32("AddressID", radio);

            return Ok(Json("true"));
        }



        [HttpPost]

        public ActionResult CompleteBooking(BookNowViewModel booking)
        {
            var CustomerId = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            int ID = CustomerId.UserId;

            var ZipValue = HttpContext.Session.GetString("ZipCode");

            var day = booking.ServiceDate.ToString("dd-MM-yyyy");
            var time = booking.ServiceTime.ToString("hh:mm:ss");
            var actual = day + " " + time;
            DateTime dt = DateTime.Parse(actual);
            booking.ServiceStartDate = dt;

            ServiceRequest service = new ServiceRequest
            {
                ZipCode = ZipValue,
                UserId = ID,
                ServiceStartDate = booking.ServiceStartDate,
                ServiceHourlyRate = booking.ServiceHourlyRate,
                ExtraHours = booking.ExtraHours,
                SubTotal = booking.SubTotal,
                ServiceHours = booking.ServiceHours,

                Discount = booking.Discount,
                TotalCost = booking.TotalCost,
                Comments = booking.Comments,
                HasPets = booking.HasPets,
                PaymentDone= booking.PaymentDone,
                RecordVersion = Guid.NewGuid(),
            };

            _helperlandContext.Add(service);

            _helperlandContext.SaveChanges();

            var AddressRadio = HttpContext.Session.GetInt32("AddressID");
            var AddressData = _helperlandContext.UserAddresses.Where(a => a.AddressId.Equals(AddressRadio)).FirstOrDefault();

            ServiceRequestAddress serviceaddress = new ServiceRequestAddress
            {
                AddressLine1 = AddressData.AddressLine1,
                AddressLine2 = AddressData.AddressLine2,
                City = AddressData.City,
                PostalCode = AddressData.PostalCode,
                Mobile = AddressData.Mobile,
                ServiceRequestId = service.ServiceRequestId,

            };
            _helperlandContext.Add(serviceaddress);

            _helperlandContext.SaveChanges();

            List<User> user = new List<User>();
            var sp = _helperlandContext.FavoriteAndBlockeds.Where(a => a.TargetUserId.Equals(ID) && a.IsBlocked == true).ToList();
            if (sp != null)
            {
                foreach (var item in sp)
                {
                    user.AddRange(_helperlandContext.Users.Where(a => a.UserId != item.UserId && a.UserTypeId == 2 && a.ZipCode == AddressData.PostalCode).ToList()); ;
                }


                foreach (var EmailMessage in user)
                {
                    var subject = "New Request Arrived";
                    var body = "Hi " + EmailMessage.FirstName + ", <br/> Customer Wants to book a service on this aera .Can you take this service ? " + "<br> Thank you";




                    SendEmail(EmailMessage.Email, body, subject);
                }
            }
            else
            {
                var emailmessage = _helperlandContext.Users.Where(b => b.ZipCode.Equals(AddressData.PostalCode) && b.UserTypeId == 2).ToList();
                foreach (var EmailMessage in emailmessage)
                {
                    var subject = "New Request Arrived";
                    var body = "Hi " + EmailMessage.FirstName + ", <br/> Customer Wants to book a service on this aera .Can you take this service ? " + "<br> Thank you";




                    SendEmail(EmailMessage.Email, body, subject);
                }
            }
            return Ok(Json("true"));




            
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




    }
}
