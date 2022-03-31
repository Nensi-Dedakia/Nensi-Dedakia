using Helperland.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class AdminViewModel
    {
        public ServiceRequest servicerequest { get; set; }
        public ServiceRequestAddress servicerequestaddress { get; set; }
        public User user { get; set; }

        public User serviceprovider { get; set; }
        public Rating rating { get; set; }

        public int? ServiceProviderid { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public string InvoiceAddressLine1 { get; set; }
        public string InvoiceAddressLine2 { get; set; }
        public string InvoiceCity { get; set; }
        public string InvoicePostalCode { get; set; }
    }
}
