using MKTFY.Models.ViewModels.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.Listing
{
    public class ListingWithSellerVM
    {
        public ListingWithSellerVM() { }

        // Constructor for populating a new ListingVM from a Listing entity
        public ListingWithSellerVM(Entities.Listing src, int sellerListingCount)
        {
            Id = src.Id;
            Product = src.Product;
            Details = src.Details;
            Price = src.Price;
            CategoryId = src.CategoryId;
            SellerId = src.UserId;
            Condition = src.Condition;
            Region = src.Region;
            SellerFullName = src.User.FullName;

            //The following code was refactored into the one line below
            //Images = new List<UploadVM>();
            //foreach (ListingUpload listingUpload in src.ListingUploads)
            //{
            //    var imageId = listingUpload.Upload.Id;
            //    var imageUrl = listingUpload.Upload.Url;
            //    var image = new UploadVM { Id = imageId, Url = imageUrl };
            //    Images.Add(image);
            //}
            Images = src.ListingUploads.Select(id => new UploadVM { Id = id.Upload.Id, Url = id.Upload.Url }).ToList();

            //CategoryName only needed in admin panel
            //CategoryName = src.Category?.Name;

            SellerTotalNumListings = sellerListingCount;
        }


        public Guid Id { get; set; }


        public string Product { get; set; }


        public string Details { get; set; }


        public decimal Price { get; set; }


        public int CategoryId { get; set; }

        public string Condition { get; set; }

        public string Region { get; set; }

        public List<UploadVM> Images { get; set; }

        public string SellerId { get; set; }

        public string SellerFullName { get; set; }

        public int SellerTotalNumListings { get; set; }

    }
}
