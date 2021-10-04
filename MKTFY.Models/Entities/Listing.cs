using MKTFY.Models.ViewModels;
using MKTFY.Models.ViewModels.Listing;
using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class Listing
    {
        public Listing()
        {

        }

        public Listing(ListingCreateVM src)
        {
            Product = src.Product;
            Details = src.Details;
            Price = src.Price;
            CategoryName = src.CategoryName;
            
           

        }

        public Listing(ListingUpdateVM src)
        {
            Id = src.Id;
            Product = src.Product;
            Details = src.Details;
            Price = src.Price;
            CategoryName = src.CategoryName;
          

        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Product { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string CategoryName { get; set; }


        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public string TransactionStatus { get; set; }

        public virtual Category Category { get; set; }



    }
}
