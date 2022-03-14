using Helperland.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class UpcomingViewModel
    {
        public ServiceRequest servicerequest { get; set; }
        public ServiceRequestAddress servicerequestaddress { get; set; }
        public User user { get; set; }

        public FavoriteAndBlocked favoriteandblocked { get; set; }

        public Rating rating { get; set; }
    }
}
