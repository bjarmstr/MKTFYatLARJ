using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using MKTFY.Models.ViewModels.Listing;
using MKTFY.Models.ViewModels.Upload;
using MKTFY.Repositories.Repositories.Interfaces;
using MKTFY.Services.Interfaces;
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

        public ListingService(IListingRepository listingRepository, ISearchRepository searchRepository)
        {
            _listingRepository = listingRepository;
            _searchRepository = searchRepository;
        }
        public async Task<ListingVM> Create(ListingCreateVM src, string userId)
        {
            var newEntity = new Listing(src, userId);
            newEntity.DateCreated = DateTime.UtcNow;
            newEntity.TransactionStatus = "listed";
            var result = await _listingRepository.Create(newEntity);
            var model = new ListingVM(result);
            //get url collection
            return model;
        }


        public async Task<ListingVM> Get(Guid id)
        {
            var result = await _listingRepository.Get(id);
            var model = new ListingVM(result);
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
            var updateData = new Listing(src);
            var result = await _listingRepository.Update(updateData);
            var model = new ListingVM(result);
            return model;
        }


        public async Task Delete(Guid id)
        {
            await _listingRepository.Delete(id);
        }

        public async Task<List<ListingVM>> GetByCategory(int categoryId, string region)
        {
            var results = await _listingRepository.GetByCategory(categoryId, region);
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
                var dealResults = await _listingRepository.GetBySearchTerm(search.SearchTerm, region);
                dealListings.AddRange(dealResults);
            }
            var distinctListings = dealListings.Distinct();
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
            var results = await _listingRepository.GetBySearchTerm(src.SearchTerm, region);
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }

    }
}
