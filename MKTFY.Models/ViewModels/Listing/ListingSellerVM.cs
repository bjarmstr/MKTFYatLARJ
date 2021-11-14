using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.Listing
{
   public class ListingSellerVM
    {
        public ListingSellerVM(Entities.Listing src)
        {
            Id = src.Id;
            Product = src.Product;
            SellerName = src.User.FullName;
            Address = src.User.Address;
        }

        public Guid Id { get; set; }
        public string Product { get; set; }

        public string SellerName { get; set; }

        public string Address { get; set; }


    }
}
