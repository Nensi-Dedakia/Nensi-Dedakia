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
    public class ServiceProviderController : Controller
    {
        private readonly HelperlandContext _helperlandContext;

        public ServiceProviderController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;

        }
        public IActionResult ServiceProviderProfile()
        {
            var Customer = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            spProfileViewModel mymodel = new spProfileViewModel();
            var ID = Customer.UserId;
            var Name = Customer.FirstName;
            ViewBag.ID = ID;
            ViewBag.Name = Name;
            mymodel.FirstName = Customer.FirstName;
            mymodel.LastName = Customer.LastName;
            mymodel.Email = Customer.Email;
            mymodel.Mobile = Customer.Mobile;
            if (Customer.Gender != null)
            {
                mymodel.Gender = Customer.Gender;
            }
            if (Customer.UserProfilePicture == null)
            {
                mymodel.UserProfilePicture = "avatar-hat.png";
            }
            else
            {
                mymodel.UserProfilePicture = Customer.UserProfilePicture;
            }
            ViewBag.Imageprofile = Customer.UserProfilePicture;
            if (Customer.DateOfBirth != null)
            {

                string datetime = Customer.DateOfBirth.ToString();
                string[] DateTime = datetime.Split(' ');
                string Date = DateTime[0];


                string[] dob = Date.Split('-');
                mymodel.BirthYear = Int32.Parse(dob[2]);
                mymodel.BirthMonth = dob[1];
                mymodel.BirthDate = Int32.Parse(dob[0]);
            }
            if (Customer.NationalityId != null)
            {
                mymodel.NationalityId = Customer.NationalityId;
            }

            var spaddress = _helperlandContext.UserAddresses.Where(b => b.UserId.Equals(ID)).FirstOrDefault();
            if (spaddress != null)
            {
                mymodel.AddressLine1 = spaddress.AddressLine1;
                mymodel.AddressLine2 = spaddress.AddressLine2;
                mymodel.City = spaddress.City;
                mymodel.PostalCode = spaddress.PostalCode;
            }


            return View(mymodel);

        }
        [HttpPost]
        public IActionResult ServiceProviderProfileDetail(spProfileViewModel spmodel)
        {
            User use = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();

            use.FirstName = spmodel.FirstName;
            use.LastName = spmodel.LastName;
            use.Email = spmodel.Email;
            use.Mobile = spmodel.Mobile;
            use.Gender = spmodel.Gender;
            if (spmodel.UserProfilePicture == null)
            {
                if (use.UserProfilePicture == null)
                {
                    use.UserProfilePicture = "avatar-hat.png";
                }
                else
                {
                    use.UserProfilePicture = use.UserProfilePicture;
                }

            }
            else
            {
                use.UserProfilePicture = spmodel.UserProfilePicture;
            }
            use.ZipCode = spmodel.PostalCode;
            var BirthDate = spmodel.BirthDate + "-" + spmodel.BirthMonth + "-" + spmodel.BirthYear;
            DateTime birth = Convert.ToDateTime(BirthDate);
            use.DateOfBirth = birth;

            if (spmodel.NationalityId == 1)
            {
                use.NationalityId = (int)NationalityEnum.India;
            }
            else if (spmodel.NationalityId == 2)
            {
                use.NationalityId = (int)NationalityEnum.Germany;
            }
            else if (spmodel.NationalityId == 3)
            {
                use.NationalityId = (int)NationalityEnum.USA;
            }
            _helperlandContext.Users.Update(use);
            _helperlandContext.SaveChanges();
            var ID = use.UserId;

            UserAddress spaddress = _helperlandContext.UserAddresses.Where(b => b.UserId.Equals(ID)).FirstOrDefault();
            if (spaddress == null)
            {
                UserAddress address = new UserAddress();
                address.UserId = ID;
                address.AddressLine1 = spmodel.AddressLine1;
                address.AddressLine2 = spmodel.AddressLine2;
                address.City = spmodel.City;
                address.PostalCode = spmodel.PostalCode;
                _helperlandContext.UserAddresses.Add(address);
                _helperlandContext.SaveChanges();
            }
            else
            {
                spaddress.AddressLine1 = spmodel.AddressLine1;
                spaddress.AddressLine2 = spmodel.AddressLine2;
                spaddress.City = spmodel.City;
                spaddress.PostalCode = spmodel.PostalCode;
                _helperlandContext.UserAddresses.Update(spaddress);
                _helperlandContext.SaveChanges();

            }
           

            return View("ServiceProviderProfile");
        }

        public IActionResult spPassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SPUpdatePassword(spProfileViewModel sp)
        {
            User use = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            var oldpwd = use.Password;
            var userid = use.UserId;

            if (string.Compare(Crypto.Hash(sp.OldPassword), oldpwd) == 0)
            {
                if (sp.Password == sp.ConfirmPassword)
                {
                    sp.Password = Crypto.Hash(sp.Password);
                    use.Password = sp.Password;
                    use.ModifiedDate = DateTime.Now;

                    _helperlandContext.Users.Update(use);
                    _helperlandContext.SaveChanges();
                   
                    TempData["Message"] = "Password Update Successfully";
                }
                else
                {
                    TempData["Message"] = "Does not match confirm password and New Password!";
                   
                }
            }
            else
            {
                
                TempData["Message"] = "Old Password is invalid";
                
            }

            return View("ServiceProviderProfile");
        }








        public IActionResult ServiceProviderPage()
        {


            



            using (HelperlandContext db = new HelperlandContext())
            {
                List<User> user = new List<User>();
                List<ServiceRequestAddress> servicerequestaddress = new List<ServiceRequestAddress>();


                var ServiceProvider = db.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
                var ID = ServiceProvider.UserId;
                var SPName = ServiceProvider.FirstName;
                ViewBag.SPName = SPName;
                var request = db.ServiceRequests.Where(b => b.ServiceProviderId.Equals(ID)).ToList();
                foreach (var item in request)
                {
                    user.AddRange(db.Users.Where(b => b.UserId.Equals(item.UserId)).ToList());

                    servicerequestaddress.AddRange(db.ServiceRequestAddresses
                   .Where(b => b.ServiceRequestId.Equals(item.ServiceRequestId)).ToList());
                }



                List<ServiceRequest> servicerequest = db.ServiceRequests.Where(b => b.ServiceProviderId.Equals(ID)).ToList();




                var UpcomingRecord = from sr in servicerequest
                                     join sra in servicerequestaddress on sr.ServiceRequestId equals sra.ServiceRequestId into table1
                                     from sra in table1.ToList()
                                     join u in user on sr.UserId equals u.UserId into table2
                                     from u in table2.Distinct().ToList()
                                     select new UpcomingViewModel
                                     {
                                         servicerequest = sr,
                                         servicerequestaddress = sra,
                                         user = u
                                     };
                return View(UpcomingRecord);
            }
        }



        public IActionResult spUpcomingService()
        {
            return View();
        }



        [HttpPost]
        public JsonResult UpcomingServiceDetail(int Id)
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

            // var Extra = details.ExtraHours;
            var NetAmount = details.SubTotal;

            var name = _helperlandContext.Users.Where(b => b.UserId.Equals(details.UserId)).FirstOrDefault();
            var Name = name.FirstName + " " + name.LastName;


            var AddressDetails = _helperlandContext.ServiceRequestAddresses.Where(b => b.ServiceRequestId.Equals(Id)).FirstOrDefault();
            var ServiceAddress = AddressDetails.AddressLine1 + AddressDetails.AddressLine2 + AddressDetails.City + AddressDetails.PostalCode;
           
            return Json(new
            {
                id = SrID,

                netamount = NetAmount,
                serviceaddress = ServiceAddress,


                servicedate = ServiceDate,
                servicetime = ServiceTime,
                duration = Duration,
                fname = Name
            });

        }


        public JsonResult CancleUpcomingService(int Id)
        {
            var details = _helperlandContext.ServiceRequests.Where(b => b.ServiceRequestId.Equals(Id)).FirstOrDefault();
            details.Status = (int)ServiceStatus.Cancelled;
            details.ModifiedDate = DateTime.Now;
            _helperlandContext.ServiceRequests.Update(details);

            _helperlandContext.SaveChanges();
            return Json(true);

        }

        public JsonResult CompleteUpcomingService(int Id)
        {
            var details = _helperlandContext.ServiceRequests.Where(b => b.ServiceRequestId.Equals(Id)).FirstOrDefault();





            DateTime dt = details.ServiceStartDate;               //Gets the current date
            string datetime = dt.ToString();          //converts the datetime value to string
            string[] DateTim = datetime.Split(' ');  //splitting the date from time with the help of space delimeter
            string Date = DateTim[0];                //saving the date value from the string array
            string Time = DateTim[1];
            string[] time = Time.Split(':');
            string clocktime = time[0] + ":" + time[1];

            var Extrahour = Convert.ToDouble(details.ExtraHours);
            var Extratime = details.ServiceHours + Extrahour;
           
            DateTime end = Convert.ToDateTime(clocktime);
            DateTime watch = end.AddHours(Extratime);

            string Endtime = watch.ToString();

            string[] d = Endtime.Split(' ');
            string endt = d[1];
          

            DateTime totaltime = Convert.ToDateTime(Date + " " + endt);





            DateTime Current_Date = DateTime.Now;


            if (totaltime < Current_Date)
            {

                details.Status = (int)ServiceStatus.Completed;
                details.ModifiedDate = DateTime.Now;
                _helperlandContext.ServiceRequests.Update(details);
                _helperlandContext.SaveChanges();
                
                return Json(true);
            }
            else
            {
                
                return Json(false);
            }

        }





        [HttpGet]
        public IActionResult spNewServiceRequest()
        {
            var ServiceProvider = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            var ID = ServiceProvider.UserId;
            var zip = ServiceProvider.ZipCode;


            IEnumerable<int> BlockCustomerList = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == ID && x.IsBlocked == true).Select(x => x.TargetUserId).Distinct().ToList();
            //IEnumerable<int> blockedByCustomerList = _testContext.FavoriteAndBlockeds.Where(x => x.TargetUserId == ID && x.IsBlocked == true).Select(x => x.UserId).Distinct().ToList();
            IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequests.Include(x => x.User).Include(x => x.ServiceRequestAddresses).Where(x => x.Status != 1 && x.Status != 3 && !BlockCustomerList.Any(a => a == x.UserId)).ToList();

            return View(serviceRequests);

        }

        public JsonResult SetRecordVersion(int ID)
        {
            var FetchRecord = _helperlandContext.ServiceRequests.Where(b => b.ServiceRequestId.Equals(ID)).FirstOrDefault();
            HttpContext.Session.SetString("ServiceRecordVersion", FetchRecord.RecordVersion.Value.ToString());

            return Json(true);
        }


        public JsonResult AcceptNewServiceRequest(int Id)
        {
            bool conflictService = false;
            var accept = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            string currentrecord = HttpContext.Session.GetString("ServiceRecordVersion");

            var spid = accept.UserId;

            var details = _helperlandContext.ServiceRequests.Where(b => b.ServiceRequestId.Equals(Id)).FirstOrDefault();

            //var Extrahour = Convert.ToDouble(details.ExtraHours);

            var newrequestdate = details.ServiceStartDate;   //start date
            var extra = Convert.ToDouble(details.ExtraHours);
            var newendtime = newrequestdate.AddHours(details.ServiceHours + extra); // end time

            TimeSpan newstime = newrequestdate.TimeOfDay;
            TimeSpan newetime = newendtime.TimeOfDay;


            if (details.RecordVersion.Value.ToString() == currentrecord)
            {
                IEnumerable<ServiceRequest> acceptedservice = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == spid && x.Status == 2 && x.ServiceRequestId != Id).ToList();

                foreach (var request in acceptedservice)
                {
                    var oldStartTime = request.ServiceStartDate; // other request start time
                    var oldextra = Convert.ToDouble(request.ExtraHours);
                    var oldEndTime = oldStartTime.AddHours(request.ServiceHours + oldextra);

                    TimeSpan oldstime = oldStartTime.TimeOfDay;
                    TimeSpan oldetime = oldEndTime.TimeOfDay;

                    conflictService = false;

                    //check time conflicts
                    if ((request.ServiceStartDate == newrequestdate) || (((newrequestdate > oldStartTime) && (newrequestdate < oldEndTime)) || ((newendtime > oldStartTime) && (newendtime < oldEndTime))))
                    {
                        conflictService = true;
                        break;
                    }

                    if (newstime > oldetime)
                    {
                        var difference = newstime.Subtract(oldetime).TotalHours;
                        if (difference < 1)
                        {
                            conflictService = true;
                            break;
                        }
                    }

                    if (newetime < oldstime)
                    {
                        var difference = oldstime.Subtract(newetime).TotalHours;
                        if (difference < 1)
                        {
                            conflictService = true;
                            break;
                        }
                    }
                }


                if (conflictService)
                {

                    return Json(false);
                }
                else
                {
                    details.ServiceProviderId = spid;
                    details.Status = (int)ServiceStatus.Accepted;
                    details.ModifiedDate = DateTime.Now;
                    details.SpacceptedDate = DateTime.Now;
                    details.RecordVersion = Guid.NewGuid();
                    _helperlandContext.ServiceRequests.Update(details);

                    _helperlandContext.SaveChanges();

                    //var subject = "";
                    //var body = "";

                    //if (serviceRequest.ServiceProviderId != null)
                    //{
                    //    subject = "Detail Update";
                    //    body = "Hi " + "<b>" + serviceRequest.ServiceProvider.FirstName + "</b>" + ", <br/>" +
                    //        " Service Request Id :" + Id + "This Service is Rescheduled by customer";

                    //    SendEmail(serviceRequest.ServiceProvider.Email, body, subject);
                    //}
                    return Json(true);

                }
            }
            else
            {
                return Json(false);
            }

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


        public IActionResult spServiceHistory()
        {
            return View();
        }




        public IActionResult BlockCustomer()
        {
            //using (HelperlandContext db = new HelperlandContext())
            //{
            //    List<User> user = new List<User>();
            //    var ServiceProvider = db.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            //    var spid = ServiceProvider.UserId;

            //    var req = db.ServiceRequests.Where(b => b.ServiceProviderId.Equals(spid) && b.Status == 1).Distinct().ToList();

            //    foreach (var item in req)
            //    {
            //        user.AddRange(db.Users.Where(b => b.UserId.Equals(item.UserId)).ToList());
            //    }

            //    List<ServiceRequest> servicerequest = db.ServiceRequests.Where(b => b.ServiceProviderId.Equals(spid)).Distinct().ToList();


            //    var CustomerRecord = from sr in servicerequest
            //                         join u in user on sr.UserId equals u.UserId into table1
            //                         from u in table1.ToList()


            //                         select new UpcomingViewModel
            //                         {
            //                             servicerequest = sr,
            //                             user = u,

            //                         };
            //    return View(CustomerRecord);

            //} 
            var ServiceProvider = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
              var spid = ServiceProvider.UserId;
            IEnumerable<User> sr = _helperlandContext.ServiceRequests.Include(x=>x.User).Where(x => x.ServiceProviderId == spid && x.Status == 1).Select(x=>x.User).Distinct().ToList();

            return View(sr);
        }


        public IActionResult CheckBlock(int id)
        {
            var blocker = _helperlandContext.FavoriteAndBlockeds.Where(a => a.TargetUserId.Equals(id)).FirstOrDefault();
            if (blocker != null && blocker.IsBlocked == true)
            {
                return Json(true);
            }

            else
            {
                return Json(false);
            }
        }


        [HttpPost]
        public JsonResult BlockUser(int id)
        {

            var ServiceProvider = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            var spid = ServiceProvider.UserId;
            var condition = _helperlandContext.FavoriteAndBlockeds.Where(a => a.TargetUserId.Equals(id) & a.UserId.Equals(spid)).FirstOrDefault();
            FavoriteAndBlocked fb = new FavoriteAndBlocked();
            if (condition != null && condition.IsBlocked == true)
            {
                condition.IsBlocked = false;
                _helperlandContext.FavoriteAndBlockeds.Update(condition);
                _helperlandContext.SaveChanges();
                return Json(true);

            }

            else if (condition != null && condition.IsBlocked == false)
            {
                condition.IsBlocked = true;
                _helperlandContext.FavoriteAndBlockeds.Update(condition);
                _helperlandContext.SaveChanges();
                return Json(true);
            }

            else
            {
                fb.UserId = spid;
                fb.TargetUserId = id;
                fb.IsBlocked = true;

                _helperlandContext.FavoriteAndBlockeds.Add(fb);
                _helperlandContext.SaveChanges();
                return Json(true);
            }

        }


        public IActionResult spMyRating() 
        {
            using (HelperlandContext db = new HelperlandContext())
            {
                List<User> user = new List<User>();

                var ServiceProvider = db.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
                var ID = ServiceProvider.UserId;

                var request = db.ServiceRequests.Where(b => b.ServiceProviderId.Equals(ID)).ToList();
                foreach (var item in request)
                {
                    user.AddRange(db.Users.Where(b => b.UserId.Equals(item.UserId)).ToList());


                }
                List<Rating> rating = db.Ratings.Where(b => b.RatingTo.Equals(ID)).Distinct().ToList();

                List<ServiceRequest> servicerequest = db.ServiceRequests.Where(b => b.ServiceProviderId.Equals(ID)).Distinct().ToList();




                var Upcom = from sr in servicerequest
                            join u in user on sr.UserId equals u.UserId into table1
                            from u in table1.Distinct().ToList()
                            join r in rating on sr.ServiceRequestId equals r.ServiceRequestId into table2
                            from r in table2.Distinct().ToList()
                            select new UpcomingViewModel
                            {
                                servicerequest = sr,
                                rating = r,
                                user = u
                            };
                return View(Upcom);
            }
        }




        public FileResult DownloadExcel()
        {
            List<User> user = new List<User>();
            List<ServiceRequestAddress> servicerequestaddress = new List<ServiceRequestAddress>();


            var ServiceProvider = _helperlandContext.Users.Where(b => b.Email.Equals(HttpContext.Session.GetString("Email"))).FirstOrDefault();
            var ID = ServiceProvider.UserId;

            var request = _helperlandContext.ServiceRequests.Where(b => b.ServiceProviderId.Equals(ID)).ToList();
            foreach (var item in request)
            {
                user.AddRange(_helperlandContext.Users.Where(b => b.UserId.Equals(item.UserId)).ToList());

                servicerequestaddress.AddRange(_helperlandContext.ServiceRequestAddresses
               .Where(b => b.ServiceRequestId.Equals(item.ServiceRequestId)).ToList());
            }



            List<ServiceRequest> servicerequest = _helperlandContext.ServiceRequests.Where(b => b.ServiceProviderId.Equals(ID) && b.Status == 1).ToList();




            var UpcomingRecord = from sr in servicerequest
                                 join sra in servicerequestaddress on sr.ServiceRequestId equals sra.ServiceRequestId into table1
                                 from sra in table1.ToList().Distinct()
                                 join u in user on sr.UserId equals u.UserId into table2
                                 from u in table2.ToList().Distinct()
                                 select new UpcomingViewModel
                                 {
                                     servicerequest = sr,
                                     servicerequestaddress = sra,
                                     user = u
                                 };

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "Service Id";
            Sheet.Cells["B1"].Value = "Service Date";
            Sheet.Cells["C1"].Value = "Customer Details";
            int row = 2;
            foreach (var item in UpcomingRecord)
            {
                Sheet.Cells[String.Format("A{0}", row)].Value = item.servicerequest.ServiceRequestId;
                Sheet.Cells[String.Format("B{0}", row)].Value = item.servicerequest.ServiceStartDate.ToShortDateString() + "\n" + item.servicerequest.ServiceStartDate.ToShortTimeString() + " - " + item.servicerequest.ServiceStartDate.AddHours(Convert.ToDouble(item.servicerequest.SubTotal)).ToShortTimeString();
                Sheet.Cells[String.Format("C{0}", row)].Value = item.user.FirstName + " " + item.user.LastName + "\n"
                    + item.servicerequestaddress.AddressLine1 + " " + item.servicerequestaddress.AddressLine2 + "\n" + item.servicerequestaddress.PostalCode + " " + item.servicerequestaddress.City;
                row++;
            }

            Sheet.Cells["A:AZ"].AutoFitColumns();
            using (MemoryStream stream = new MemoryStream())
            {
                Ep.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ServiceHistory.xlsx");
            }
          


        }
    }
}
