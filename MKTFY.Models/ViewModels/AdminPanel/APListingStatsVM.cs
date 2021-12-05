using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.AdminPanel
{
    public class APListingStatsVM
    {
        public APListingStatsVM(Entities.User src)
        {
            UserId = src.Id;
            UserFullName = src.FullName;
            UserEmail = src.Email;
            UserLocation = src.Location;
     
        }

        public string UserId { get; set; }

        public string UserFullName { get; set; }

        public string UserEmail { get; set; }

        
        public string UserLocation { get; set; }

        public int UserListingCount { get; set; }

        public int UserPurchaseCount { get; set; }

    }
}
