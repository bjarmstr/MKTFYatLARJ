using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.Listing
{
    public class ListingSummaryVM
    {
        public ListingSummaryVM() { }

        public ListingSummaryVM(Entities.Listing src)
        {
            Id = src.Id;
            Product = src.Product;
            Price = src.Price;
            ImageUrl = src.ListingUploads.Select(id => id.Upload.Url).First();
            TransactionStatus = src.TransactionStatus;

        }


        public Guid Id { get; set; }

        public string Product { get; set; }

        public string ImageUrl { get; set; } 

        public decimal Price { get; set; }

        public string TransactionStatus { get; set; }

    }
}
