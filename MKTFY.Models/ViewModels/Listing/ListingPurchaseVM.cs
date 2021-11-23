using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.Listing
{
    public class ListingPurchaseVM
    {
        public ListingPurchaseVM() { }

        public ListingPurchaseVM(Entities.Listing src)
        {
            Id = src.Id;
            Product = src.Product;
            Price = src.Price;
            TransactionStatus = src.TransactionStatus;
            DateSold = src.DateSold;
                   
        }


        public Guid Id { get; set; }

        public string Product { get; set; }

        public decimal Price { get; set; }

        public string TransactionStatus { get; set; }

        //TODO remove ?
        public DateTime? DateSold { get; set; }

    }
}
