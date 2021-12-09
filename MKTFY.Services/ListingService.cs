using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using MKTFY.Models.ViewModels.AdminPanel;
using MKTFY.Models.ViewModels.Listing;
using MKTFY.Models.ViewModels.Upload;
using MKTFY.Repositories.Repositories.Interfaces;
using MKTFY.Services.Interfaces;
using MKTFY.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly ISearchRepository _searchRepository;
        private readonly IUploadRepository _uploadRepository;

        public ListingService(
            IListingRepository listingRepository,
            ISearchRepository searchRepository,
            IUploadRepository uploadRepository)
        {
            _listingRepository = listingRepository;
            _searchRepository = searchRepository;
            _uploadRepository = uploadRepository;
        }
        public async Task<ListingVM> Create(ListingCreateVM src, string userId)
        {
            //check string field for acceptable values
            src.Condition = ValidateCondition(src.Condition);

            var newEntity = new Listing(src, userId);
            newEntity.DateCreated = DateTime.UtcNow;
            newEntity.TransactionStatus = "listed";

            var result = await _listingRepository.Create(newEntity);
            var resultIncludesUpload = await _listingRepository.Get(result.Id);
            
            var model = new ListingVM(resultIncludesUpload);
            return model;
        }


        public async Task<ListingVM> Get(Guid id)
        {
            var result = await _listingRepository.Get(id);
            var model = new ListingVM(result);
            return model;
        }


        public async Task<ListingWithSellerVM> GetListingWithSeller(Guid id)
        {
            var result = await _listingRepository.GetListingWithSeller(id);
            var sellerListings = await _listingRepository.GetMyListings(result.UserId, "listed");
            int sellerListingCount = sellerListings.Count;
            var model = new ListingWithSellerVM(result, sellerListingCount);
            return model;
        }

        public async Task<List<ListingVM>> GetAll()
        {
            var results = await _listingRepository.GetAll();
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }

        public async Task<ListingVM> Update(ListingUpdateVM src)
        {
            //condition either New or Used
            src.Condition = ValidateCondition(src.Condition);

            var updateData = new Listing(src);
            var result = await _listingRepository.Update(updateData);
            var resultIncludesUpload = await _listingRepository.Get(result.Id);
            var model = new ListingVM(resultIncludesUpload);
            return model;
        }


        public async Task Delete(Guid id)
        {
            await _listingRepository.Delete(id);

        }

        public async Task<List<ListingVM>> GetByCategory(int categoryId, string region, string userId)
        {
            var results = await _listingRepository.GetByCategory(categoryId, region, userId);
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }

        public async Task<List<ListingVM>> GetDeals(string userId, string region)
        {
            //retrieve user's last 3 search terms
            var searchHistory = await _searchRepository.GetLatestSearches(userId);

            //find listings matching search terms
            var dealListings = new List<Listing>();
            foreach (SearchHistory search in searchHistory)
            {
                var dealResults = await _listingRepository.GetBySearchTerm(search.SearchTerm, region, userId);
                dealListings.AddRange(dealResults);
            }

            /// minimum number of listings returned
            int minNumDealsReturned = 14;

            
            if (dealListings.Count < minNumDealsReturned)
            {
                //if not the min # listings based on previous searches return the newest listings 
                
                dealListings.AddRange(await _listingRepository.GetMostRecent(region, userId, minNumDealsReturned));
            }

            //multiple searches may return the same listing more than once --- keep only distinct
            

            var distinctListings = dealListings.Distinct().Take(minNumDealsReturned);
 

            var models = distinctListings.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }

        public async Task<List<ListingVM>> GetBySearchTerm(SearchCreateVM src, string region)
        {
            //save search term to SearchHistory Table
            var newSearchEntity = new SearchHistory(src);
            newSearchEntity.DateCreated = DateTime.UtcNow;
            await _searchRepository.Save(newSearchEntity);

            //get search
            var results = await _listingRepository.GetBySearchTerm(src.SearchTerm, region, src.UserId);
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }

        public async Task<ListingSellerVM> GetPickupInfo(Guid id)
        {

            var result = await _listingRepository.GetPickupInfo(id);

            var model = new ListingSellerVM(result);

            return model;

        }

        public async Task ChangeTransactionStatus(Guid id, string status, string buyerId)
        {

            if (status == "cancelled")
            {
                buyerId = null;
                status = "listed";
            }

            await _listingRepository.ChangeTransactionStatus(id, status, buyerId);

        }

        public async Task<List<ListingPurchaseVM>> GetMyPurchases(string userId)
        {
            var results = await _listingRepository.GetMyPurchases(userId);
            var models = results.Select(Listing => new ListingPurchaseVM(Listing)).ToList();
            return models;
        }

        public async Task<List<ListingSummaryVM>> GetMyListings(string userId, string status)
        {

            var results = await _listingRepository.GetMyListings(userId, status);
            var models = results.Select(Listing => new ListingSummaryVM(Listing)).ToList();
            return models;
        }

        public async Task<List<ListingSummaryVM>> GetAllMyListings(string userId)
        {
            var results = await _listingRepository.GetAllMyListings(userId);
            var models = results.Select(Listing => new ListingSummaryVM(Listing)).ToList();
            return models;
        }



        private async Task<ListingVM> AddUploadDetails(Listing result)
        {
            var model = new ListingVM(result);
            //get the Upload Id
            var uploadIds = result.ListingUploads.Select(i => i.UploadId).ToList();
            //get the Upload Url
            foreach (Guid uploadId in uploadIds)
            {
                var upload = await _uploadRepository.Get(uploadId);
                //model.UploadUrls.Add(upload.Url);
            }
            //model.UploadIds = uploadIds;

            return model;
        }

        private string ValidateCondition(string condition)
        {
            //create consistent capitalization and check for correct terms
            condition.ToLower();
            condition = condition[0].ToString().ToUpper() + condition.Substring(1);
            if (condition != "New" && condition != "Used")
            {
                //TODO create global exception -- input error
                throw new NotFoundException("Incorrect Condition value");
            }
            return condition;
        }





    }
}
