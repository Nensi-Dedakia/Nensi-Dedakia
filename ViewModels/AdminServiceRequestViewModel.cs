using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class AdminServiceRequestViewModel
    {
        public int serviceRequestId { get; set; }

        public string serviceDate { get; set; }

        public string serviceTime { get; set; }

       
        public string customerName { get; set; }

        public string customerAddress { get; set; }

        public string? spName { get; set; }

        public string? spAvtar { get; set; }

        public decimal? spRating { get; set; }

        public string totalAmount { get; set; }

        public string status { get; set; }
    }
}
