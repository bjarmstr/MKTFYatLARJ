using MKTFY.Models.ViewModels;
using MKTFY.Models.ViewModels.Listing;
using MKTFY.Models.ViewModels.Upload;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MKTFY.Models.Entities
{
    /// <summary>
    /// entity for a Listing
    /// </summary>
    public class Listing
    {
        /// <summary>
        /// 
        /// </summary>
        public Listing()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="userId"></param>
        public Listing(ListingCreateVM src, string userId)
        {
            Product = src.Product;
            Details = src.Details;
            Price = src.Price;
            CategoryId = src.CategoryId;
            Condition = src.Condition;
            Address = src.Address;
            Region = src.Region;
            UserId = userId;
            ListingUploads = src.UploadIds.Select(id => new ListingUpload { UploadId = id }).ToList();
    
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        public Listing(ListingUpdateVM src)
        {
            Id = src.Id;
            Product = src.Product;
            Details = src.Details;
            Price = src.Price;
            CategoryId = src.CategoryId;
            Condition = src.Condition;
            Address = src.Address;
            Region = src.Region;
            ListingUploads = src.UploadIds.Select(id => new ListingUpload { UploadId = id }).ToList();
        }
        /// <summary>
        /// Key - unique generated Guid identifier for Listing
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the product being listing
        /// </summary>
        [Required]
        public string Product { get; set; }

        /// <summary>
        /// Detailed description of the product
        /// </summary>
        [Required]
        public string Details { get; set; }

        /// <summary>
        /// Asking Price
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        
        /// <summary>
        /// CategoryId - unique identifier for the category
        /// </summary>
        [Required]
        public int CategoryId { get; set; }
        /// <summary>
        /// Category contains an id and category name
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// generated value for date listing was created
        /// </summary>
        [Required]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// transaction status can be deleted, listed, pending or sold
        /// </summary>
        [Required]
        public string TransactionStatus { get; set; }
        
        /// <summary>
        /// condition is either new or used **not a validated field** 
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// pickup address for listing
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// address should be in the region ***not a validated field**
        /// </summary>
        [Required]
        public string Region { get; set; }

        /// <summary>
        /// date listing is sold -only for listings in sold status
        /// </summary>
        //DateTime? can be be null
        public DateTime? DateSold { get; set; }

        /// <summary>
        /// buyer if listing in pending or sold status
        /// </summary>
        public string? BuyerId { get; set; }


        /// <summary>
        /// navigation property to image URL
        /// </summary>
        //navigation property 
        public ICollection<ListingUpload> ListingUploads { get; set; }



        /// <summary>
        /// User who created the listing
        /// </summary>
        [Required]
        public string UserId { get; set; }


        /// <summary>
        /// User is a navigation property, which allows access to User details
        /// </summary>
        public User User { get; set; }

    }
}
