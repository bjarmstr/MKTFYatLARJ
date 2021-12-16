using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels.Upload;
using System;
using System.Collections.Generic;
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
        /// Category Id CarsVehicles 1, Furniture 2, Electronics 3, Real Estate 4
        /// </summary>
        [Required]
        public int CategoryId{ get; set; }

        /// <summary>
        /// Condition either new or used
        /// </summary>
        public string Condition { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public List<Guid> UploadIds { get; set; }


    }
}
