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
    public class AdminController : Controller
    {
        private readonly HelperlandContext  _helperlandContext;


        public AdminController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;

        }
        public IActionResult AdminRequest()
        {
            return View();
        }


        [HttpPost]
        public JsonResult FilterData()
        {
            try
            {
                //retrieve filter parameter from request
                IFormCollection form = HttpContext.Request.ReadFormAsync().Result;
                var set = form.ToHashSet();
                var draw = form["draw"].FirstOrDefault();
                var searchItems = form["search[value]"].FirstOrDefault().Length;
                var start = form["start"].FirstOrDefault(); 
                var length = form["length"].FirstOrDefault();
                var sortColumn = form["columns[" + form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDir = form["order[0][dir]"].FirstOrDefault();
                //var city = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt16(start) : 0;
                int recordsTotal = 0;

                //var data = _helperlandContext.ServiceRequests.Include(x => x.User).ThenInclude(x => x.UserAddresses).Include(x => x.ServiceProvider).Include(x => x.ServiceProvider).ThenInclude(x =>x.RatingRatingToNavigations).ToList();

                var data = _helperlandContext.ServiceRequests.Include(x => x.ServiceRequestAddresses).Include(x => x.User).Include(x => x.ServiceProvider).ThenInclude(x => x.RatingRatingToNavigations).ToList();
                //apply filters
                if (searchItems > 0)
                {
                    var searchArray = form["search[value]"].FirstOrDefault().Split(",");
                    var serviceId = searchArray[0];
                    var postalCode = searchArray[1];
                    var email = searchArray[2];
                    var customerName = searchArray[3];
                    var spName = searchArray[4];
                    var status = searchArray[5];
                    var hasIssue = searchArray[6];
                    var fromDate = searchArray[7];
                    var toDate = searchArray[8];

                    if (!string.IsNullOrEmpty(serviceId))
                    {
                        data = data.Where(x => x.ServiceRequestId == Int16.Parse(serviceId)).ToList();
                    }

                    if (!string.IsNullOrEmpty(postalCode))
                    {
                        data = data.Where(x => x.ZipCode.Contains(postalCode)).ToList();
                    }
                    if (!string.IsNullOrEmpty(email))
                    {
                        data = data.Where(x => x.User.Email.Contains(email, StringComparison.OrdinalIgnoreCase) || (!string.IsNullOrEmpty(x.ServiceProvider?.Email) ? x.ServiceProvider.Email.Contains(email, StringComparison.OrdinalIgnoreCase) : false)).ToList();
                    }

                    if (!string.IsNullOrEmpty(customerName))
                    {
                        //var name = customerName.Split(" ");
                        data = data.Where(x => x.User.FirstName.Contains(customerName, StringComparison.OrdinalIgnoreCase) || x.User.LastName.Contains(customerName, StringComparison.OrdinalIgnoreCase)).ToList();

                    }

                    if (!string.IsNullOrEmpty(spName))
                    {



                        data = data.Where(x => (!string.IsNullOrEmpty(x.ServiceProvider?.FirstName) ? x.ServiceProvider.FirstName.Contains(spName, StringComparison.OrdinalIgnoreCase) : false) || (!string.IsNullOrEmpty(x.ServiceProvider?.LastName) ? x.ServiceProvider.LastName.Contains(spName, StringComparison.OrdinalIgnoreCase) : false)).ToList();

                    }
                    if (!string.IsNullOrEmpty(status))
                    {
                        data = data.Where(x => x.Status == Int16.Parse(status)).ToList();
                    }

                    //data = data.Where(x => x.HasIssue == bool.Parse(hasIssue)).ToList();

                    if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                    {
                        data = data.Where(x => x.ServiceStartDate >= DateTime.Parse(fromDate) && x.ServiceStartDate <= DateTime.Parse(toDate)).ToList();
                    }
                }
                //apply sorting on different columns
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    if (sortColumnDir.Equals("asc"))
                    {
                        switch (sortColumn)
                        {
                            case "ServiceRequestId":
                                data = data.OrderBy(x => x.ServiceRequestId).ToList();
                                break;
                            case "ServiceStartDate":
                                data = data.OrderBy(x => x.ServiceStartDate).ToList();
                                break;
                            case "CustomerName":
                                data = data.OrderBy(x => x.User.FirstName).ToList();
                                break;
                            case "SPName":
                                data = data.OrderBy(x => x.ServiceProvider?.FirstName).ToList();
                                break;
                        }
                    }
                    else
                    {
                        switch (sortColumn)
                        {
                            case "ServiceRequestId":
                                data = data.OrderByDescending(x => x.ServiceRequestId).ToList();
                                break;
                            case "ServiceStartDate":
                                data = data.OrderByDescending(x => x.ServiceStartDate).ToList();
                                break;
                            case "CustomerName":
                                data = data.OrderByDescending(x => x.User.FirstName).ToList();
                                break;
                            case "SPName":
                                data = data.OrderByDescending(x => x.ServiceProvider?.FirstName).ToList();
                                break;
                        }
                    }
                }
                //return data according to page size
                recordsTotal = data.Count();
                data = data.Skip(skip).Take(pageSize).ToList();
                List<AdminServiceRequestViewModel> adminServiceRequests = new List<AdminServiceRequestViewModel>();
                foreach (var request in data)
                {
                    var serviceRequest = new AdminServiceRequestViewModel();
                    serviceRequest.serviceRequestId = request.ServiceRequestId;
                    serviceRequest.serviceDate = request.ServiceStartDate.ToString("dd/MM/yyyy");
                    serviceRequest.serviceTime = request.ServiceStartDate.ToString("HH:mm") + "-" + request.ServiceStartDate.AddHours(request.ServiceHours).ToString("HH:mm");
                    serviceRequest.customerName = request.User.FirstName + " " + request.User.LastName;

                    serviceRequest.customerAddress = request.ServiceRequestAddresses.ElementAt(0).AddressLine1+" " +request.ServiceRequestAddresses.ElementAt(0).AddressLine2+"</br> "+
                        request.ServiceRequestAddresses.ElementAt(0).City+" "+ request.ServiceRequestAddresses.ElementAt(0).PostalCode;

                    serviceRequest.spName = request.ServiceProvider?.FirstName + " " + request.ServiceProvider?.LastName;
                    serviceRequest.spAvtar = request.ServiceProvider?.UserProfilePicture;

                    serviceRequest.spRating = request.ServiceProvider?.RatingRatingToNavigations.Average(x => x.Ratings);
                    //var rs = Convert.ToInt32(serviceRequest.spRating);
                    //serviceRequest.spRating =Decimal.Round(rs , 2);
                    serviceRequest.totalAmount = request.TotalCost.ToString();
                    serviceRequest.status = request.Status.ToString();
                    adminServiceRequests.Add(serviceRequest);
                }
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = adminServiceRequests });
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Json("ok");
            }
        }


        public IActionResult EditModal()
        {
            return View();
        } 
        public JsonResult EditServiceRequest(int id)
        {
            var reqdetails = _helperlandContext.ServiceRequests.Where(a => a.ServiceRequestId.Equals(id)).FirstOrDefault();

            DateTime DatenTime = reqdetails.ServiceStartDate;
            string datetime = DatenTime.ToString();
            string[] DateTime = datetime.Split(' ');

            string Date = DateTime[0];

            string Time = DateTime[1];
            string[] time = Time.Split(':');


            string starttime = time[0] + ":" + time[1];


            var addressdetails = _helperlandContext.ServiceRequestAddresses.Where(a => a.ServiceRequestId.Equals(id)).FirstOrDefault();

            var line1 = addressdetails.AddressLine1;
            var line2 = addressdetails.AddressLine2;
            var postal = addressdetails.PostalCode;
            var City = addressdetails.City;
            var ID = id;






            return Json(new
            {
                date = Date,
                time = starttime,
                line = line1,
                linetwo = line2,
                postcode = postal,
                city = City,
                ids = ID
            });
        }


        [HttpPost]
        public JsonResult UpdateDetails(int id, AdminViewModel avm)
        {
            ServiceRequest sr = _helperlandContext.ServiceRequests.Where(a => a.ServiceRequestId.Equals(id)).FirstOrDefault();
            var CustomerId = sr.UserId;
            var spid = sr.ServiceProviderId;

            User s = _helperlandContext.Users.Where(a => a.UserId.Equals(spid)).FirstOrDefault();
            var sname = s.FirstName;

            User u = _helperlandContext.Users.Where(a => a.UserId.Equals(CustomerId)).FirstOrDefault();
            var CustName = u.FirstName;

            var day = avm.Date.ToString("dd-MM-yyyy");
            var time = avm.Time.ToString("hh:mm:ss");
            var actual = day + " " + time;
            DateTime dt = DateTime.Parse(actual);

            sr.ServiceStartDate = dt;
            _helperlandContext.ServiceRequests.Update(sr);
            _helperlandContext.SaveChanges();

            ServiceRequestAddress sra = _helperlandContext.ServiceRequestAddresses.Where(a => a.ServiceRequestId.Equals(id)).FirstOrDefault();
            sra.AddressLine1 = avm.AddressLine1;
            sra.AddressLine2 = avm.AddressLine2;
            sra.City = avm.City;
            sra.PostalCode = avm.PostalCode;

            _helperlandContext.ServiceRequestAddresses.Update(sra);
            _helperlandContext.SaveChanges();

            var subject = "";
            var body = "";

            subject = "Detail Update";
            body = "Hi " + "<b>" + CustName + "</b>" + ", <br/>" +
                  " Your Details of " + "<b>" + id + "</b> " + "Are Updated As Below By Admin:" +
                  "<br> Service Start Date:" + sr.ServiceStartDate +
                  "<br> Service Address:" + sra.AddressLine1 + " " + sra.AddressLine2 + " " + sra.City + "-" + " " + sra.PostalCode +
                  "<br> Thank you";
            SendEmail(u.Email, body, subject);

            if (sr.ServiceProviderId != null)
            {
                subject = "Detail Update";
                body = "Hi " + "<b>" + sname + "</b>" + ", <br/>" +
                    " Service Request of " + "<b>" + id + "</b> " + "</br>" + "Details Are Updated As Below By Admin:" +
                    "<br> Service Start Date:" + sr.ServiceStartDate +
                    "<br> Service Address:" + " " + sra.AddressLine1 + " " + sra.AddressLine2 + " " + sra.City + "-" + " " + sra.PostalCode +
                    "<br> Thank you";
                SendEmail(s.Email, body, subject);
            }

            return Json("true");
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


        public IActionResult AdminManagment()
        {
            return View();
        }


        [HttpPost]
        public JsonResult UserManagementData()
        {
            try
            {
                //retrieve filter parameter from request
                IFormCollection form = HttpContext.Request.ReadFormAsync().Result;
                var set = form.ToHashSet();
                var draw = form["draw"].FirstOrDefault();
                var searchItems = form["search[value]"].FirstOrDefault().Length;
                var start = form["start"].FirstOrDefault(); ;
                var length = form["length"].FirstOrDefault();
                var sortColumn = form["columns[" + form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDir = form["order[0][dir]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt16(start) : 0;
                int recordsTotal = 0;

                var data = _helperlandContext.Users.Where(x => x.IsDeleted != true).ToList();
                //apply filters
                if (searchItems > 0)
                {
                    var searchArray = form["search[value]"].FirstOrDefault().Split(",");
                    var userName = searchArray[0];
                    var userType = searchArray[1];
                    var phoneNumber = searchArray[2];
                    var postalCode = searchArray[3];
                    var email = searchArray[4];
                    var fromDate = searchArray[5];
                    var toDate = searchArray[6];


                    if (!string.IsNullOrEmpty(userName))
                    {
                        //var fullName = userName.Split(" ");
                        //data = data.Where(x => x.FirstName == fullName[0] && x.LastName == fullName[1]).ToList();
                        data = data.Where(x => x.FirstName.Contains(userName, StringComparison.OrdinalIgnoreCase) || x.LastName.Contains(userName, StringComparison.OrdinalIgnoreCase)).ToList();
                    }

                    if (!string.IsNullOrEmpty(userType))
                    {
                        data = data.Where(x => x.UserTypeId == Int16.Parse(userType)).ToList();
                    }

                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        data = data.Where(x => x.Mobile == phoneNumber).ToList();
                    }

                    if (!string.IsNullOrEmpty(postalCode))
                    {
                        data = data.Where(x => x.ZipCode == postalCode).ToList();
                      
                    }

                    if (!string.IsNullOrEmpty(email))
                    {
                        //data = data.Where(x => x.Email == email).ToList();
                        data = data.Where(x => x.Email.Contains(email, StringComparison.OrdinalIgnoreCase)).ToList();
                    }

                    if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                    {
                        data = data.Where(x => x.CreatedDate >= DateTime.Parse(fromDate) && x.CreatedDate <= DateTime.Parse(toDate)).ToList();
                    }
                }
                //apply sorting on different columns
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    if (sortColumnDir.Equals("asc"))
                    {
                        switch (sortColumn)
                        {
                            case "User Name":
                                data = data.OrderBy(x => x.FirstName).ToList();
                                break;
                            case "Registration Date":
                                data = data.OrderBy(x => x.CreatedDate).ToList();
                                break;
                            case "Phone":
                                data = data.OrderBy(x => x.Mobile).ToList();
                                break;
                        }
                    }
                    else
                    {
                        switch (sortColumn)
                        {
                            case "User Name":
                                data = data.OrderByDescending(x => x.FirstName).ToList();
                                break;
                            case "Registration Date":
                                data = data.OrderByDescending(x => x.CreatedDate).ToList();
                                break;
                            case "Phone":
                                data = data.OrderByDescending(x => x.Mobile).ToList();
                                break;
                        }
                    }
                }
               
                    
                //return data according to page sige 
                recordsTotal = data.Count();
                data = data.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data});
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Json("ok");
            }



        }
            public FileResult DownloadExcel()
            {

                List<User> user = _helperlandContext.Users.ToList();

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
                Sheet.Cells["A1"].Value = "UserName";
                Sheet.Cells["B1"].Value = "Date Of Registration";
                Sheet.Cells["C1"].Value = "User Type";
                Sheet.Cells["D1"].Value = "Phone";
                Sheet.Cells["E1"].Value = "PostalCode";
                Sheet.Cells["F1"].Value = "Status";
                //Sheet.Cells["G1"].Value = "Action";

                int row = 2;
                foreach (var item in user)
                {

                    Sheet.Cells[string.Format("A{0}", row)].Value = item.FirstName + " " + item.LastName;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.CreatedDate.ToShortDateString();
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.UserTypeId;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.Mobile;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.ZipCode;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.Status;


                    row++;
                }

                Sheet.Cells["A:AZ"].AutoFitColumns();
                using (MemoryStream stream = new MemoryStream())
                {
                    Ep.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "UserManagement.xlsx");
                }
            }

       
        public JsonResult CancleService(int Id)
        {
            //var subject = "";
            //var body = "";
            var details = _helperlandContext.ServiceRequests.Where(b => b.ServiceRequestId.Equals(Id)).FirstOrDefault();
            User s = _helperlandContext.Users.Where(a => a.UserId.Equals(details.ServiceProviderId)).FirstOrDefault();
            var sname = s.FirstName;
            details.Status = (int)ServiceStatus.Cancelled;
            details.ModifiedDate = DateTime.Now;
            details.ModifiedBy = (int)UserTypeEnum.Admin;
            _helperlandContext.ServiceRequests.Update(details);
            //_helperlandContext.ServiceRequestAddresses.Remove(detail);
            _helperlandContext.SaveChanges();
            return Json(true);

            //if (details.ServiceProviderId != null)
            //{
            //    subject = "Detail Update";
            //    body = "Hi " + "<b>" + sname + "</b>" + ", <br/>" +
            //        " Service Request Id :" + Id + "This Service is cancelled by customer";

            //    SendEmail(s.Email, body, subject);
            //}


        }


        public IActionResult ActivateUser(int Id)
        {  
           
            User use = _helperlandContext.Users.Where(b => b.UserId.Equals(Id)).FirstOrDefault();
            if (use.UserTypeId == 2)
            {
                if (use.IsApproved == true)
                {
                    use.IsActive = true;
                    use.ModifiedDate = DateTime.Now;
                    _helperlandContext.Users.Update(use);
                    _helperlandContext.SaveChanges();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                use.IsActive = true;
                use.ModifiedDate = DateTime.Now;
                _helperlandContext.Users.Update(use);
                _helperlandContext.SaveChanges();
                return Json(true);
            }
           
        }

        public IActionResult DeActivateUser(int Id)
        {
            User use = _helperlandContext.Users.Where(b => b.UserId.Equals(Id)).FirstOrDefault();
            use.IsActive = false;
            if(use.UserTypeId==2)
            {
                use.IsApproved = false;
            }
            use.ModifiedDate = DateTime.Now;
            _helperlandContext.Users.Update(use);
            _helperlandContext.SaveChanges();
            return Json(true);
        }

        public IActionResult ApproveSP(int Id)
        {

            User use = _helperlandContext.Users.Where(b => b.UserId.Equals(Id)).FirstOrDefault();
            use.IsApproved = true;
            use.ModifiedDate = DateTime.Now;
            _helperlandContext.Users.Update(use);
            _helperlandContext.SaveChanges();
           
            return Json(true);
        }

    }
}
