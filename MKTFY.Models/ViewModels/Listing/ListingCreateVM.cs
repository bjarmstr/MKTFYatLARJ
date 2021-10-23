using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels.Listing
{
    public class ListingCreateVM
    {


        [Required]
        public string Product { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Category Id , Deals - Category 1, Real Estate - Category 5
        /// </summary>
        [Required]
        public int CategoryId{ get; set; }

        public string Condition;

    }
}
