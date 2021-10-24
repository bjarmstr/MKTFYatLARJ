using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels.Listing
{
    public class ListingUpdateVM
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Product { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        //make sure this is a real category@@jma
        public int CategoryId { get; set; }

        public string Condition { get; set; }

        public string Region { get; set; }

       
    }
}
