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

        public IActionResult ZipCodeValue(BookNowViewModel model)
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

            return View();
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



      
      //  [HttpGet]

        //public JsonResult GetUserAddresses(BookNowViewModel getaddress)
        //{
        //    var CustomerId = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
        //    int ID = CustomerId.UserId;

        //    getaddress.Address = _helperlandContext.UserAddresses.Where(c => c.UserId.Equals(ID)).ToList();
        //    var json = JsonConvert.SerializeObject(getaddress.Address);

        //    return Json(json, JsonRequestBehavior.AllowGet);

        //}



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
                //  Email = address.email,
            };


            _helperlandContext.Add(add);

            _helperlandContext.SaveChanges();
            return Ok(Json("true"));
          //  return new JsonResult()
        }





        [HttpPost]

        public ActionResult CompleteBooking(BookNowViewModel booking)
        {
            var CustomerId = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            int ID = CustomerId.UserId;

            var ZipValue = HttpContext.Session.GetString("ZipCode");

            ServiceRequest service = new ServiceRequest
            {
                ZipCode = ZipValue,
                UserId = ID,
                ServiceStartDate = DateTime.Now,
                ServiceHourlyRate = booking.ServiceHourlyRate,
                ExtraHours = booking.ExtraHours,
                SubTotal = booking.SubTotal,
                ServiceHours = booking.ServiceHours,

                Discount = booking.Discount,
                TotalCost = booking.TotalCost,
                Comments = booking.Comments,
                HasPets = booking.HasPets,
            };







            _helperlandContext.Add(service);

            _helperlandContext.SaveChanges();



            return Ok(Json("true"));




            // return View();
        }




    }
}
