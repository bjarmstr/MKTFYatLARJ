using MKTFY.Models.ViewModels;
using MKTFY.Models.ViewModels.Listing;
using MKTFY.Models.ViewModels.Upload;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MKTFY.Models.Entities
{
    public class Listing
    {
        public Listing()
        {

        }

        public Listing(ListingCreateVM src, string userId)
        {
            Product = src.Product;
            Details = src.Details;
            Price = src.Price;
            CategoryId = src.CategoryId;
            Condition = src.Condition;
            Region = src.Region;
            UserId = userId;
            ListingUploads = src.UploadIds.Select(id => new ListingUpload { UploadId = id }).ToList();
    
        }

        public Listing(ListingUpdateVM src)
        {
            Id = src.Id;
            Product = src.Product;
            Details = src.Details;
            Price = src.Price;
            CategoryId = src.CategoryId;
            Condition = src.Condition;
            Region = src.Region;
            //UploadIds = src.UploadIds;
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
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public string TransactionStatus { get; set; }

        public string Condition { get; set; }

        [Required]
        public string Region { get; set; }

        //navigation property 
        public ICollection<ListingUpload> ListingUploads { get; set; }



        /// <summary>
        /// User who created the listing
        /// </summary>
        [Required]
        public string UserId { get; set; }
        //User User is a navigation property, which allows access to User details
        public User User { get; set; }

    }
}
