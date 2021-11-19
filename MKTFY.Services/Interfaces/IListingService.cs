using MKTFY.Models.ViewModels;
using MKTFY.Models.ViewModels.Listing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    public interface IListingService
    {
        Task<ListingVM> Create(ListingCreateVM src, string userId);  //Create a new listing
        Task<ListingVM> Get(Guid id);

        Task<List<ListingVM>> GetAll(); // Read all Listings

        Task<ListingVM> Update(ListingUpdateVM src); //Update an existing Listing

        Task Delete(Guid id);  //Delete a Listing

        Task<List<ListingVM>> GetByCategory(int categoryId, string region);

        Task<List<ListingVM>> GetDeals(string userId, string region);

        Task<List<ListingVM>> GetBySearchTerm(SearchCreateVM src, string userId);

        Task<ListingSellerVM> GetPickupInfo(Guid id);

        Task ChangeTransactionStatus(Guid id, string status, string buyerId);

        Task<List<ListingPurchaseVM>> GetMyPurchases(string userId);

        Task<List<ListingSummaryVM>> GetMyListings(string userId, string status);

    }
}
