﻿using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class ListingCreateVM
    {


        [Required]
        public string Product { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}