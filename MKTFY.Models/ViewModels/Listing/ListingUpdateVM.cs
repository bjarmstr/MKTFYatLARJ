using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels.Upload;
using System;
using System.Collections.Generic;
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

        [Required]
        public string Condition { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public List<Guid> UploadIds { get; set; }


    }
}
