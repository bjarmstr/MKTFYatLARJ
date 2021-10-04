﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.Listing
{
    public class ListingVM
    {
        public ListingVM() { }

        // Constructor for populating a new ListingVM from a Listing entity
        public ListingVM(Entities.Listing src)
        {
            Id = src.Id;
            Product = src.Product;
            Details = src.Details;
            Price = src.Price;
            Category = src.CategoryName;

        }


        public Guid Id { get; set; }


        public string Product { get; set; }


        public string Details { get; set; }


        public decimal Price { get; set; }


        public string Category { get; set; }

        

    }
}
