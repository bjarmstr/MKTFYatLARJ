using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels.Listing;
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

        public ListingService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }
        public async Task<ListingVM> Create(ListingCreateVM src)
        {
            var newEntity = new Listing(src);
            //check category
            newEntity.DateCreated = DateTime.UtcNow;
            newEntity.TransactionStatus = "listed";
            var result = await _listingRepository.Create(newEntity);
            var model = new ListingVM(result);
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

        public async Task<List<ListingVM>> GetByCategory(int categoryId)
        {
            var results = await _listingRepository.GetByCategory(categoryId);
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }
    }
}
