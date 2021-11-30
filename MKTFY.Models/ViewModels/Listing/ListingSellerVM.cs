using MKTFY.Models.ViewModels.Upload;
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
            Price = src.Price;
            SellerName = src.User.FullName;
            Address = src.Address;
            Phone = src.User.Phone;
            Region = src.Region;
            ImageUrl = src.ListingUploads.Select(id =>id.Upload.Url).First();
        }

        public Guid Id { get; set; }
        public string Product { get; set; }

        public string  ImageUrl { get; set;}

        public decimal Price { get; set; }

        public string SellerName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Region { get; set; }


    }
}
