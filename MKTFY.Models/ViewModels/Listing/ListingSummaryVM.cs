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
                   
        }


        public Guid Id { get; set; }

        public string Product { get; set; }

        public decimal Price { get; set; }

    }
}
