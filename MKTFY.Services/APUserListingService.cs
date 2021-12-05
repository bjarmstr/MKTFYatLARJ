using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels.AdminPanel;
using MKTFY.Repositories.Repositories.Interfaces;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Services
{
    public class APUserListingService : IAPUserListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly IUserRepository _userRepository;
        public APUserListingService(IListingRepository listingRepository, IUserRepository userRepository)
        {
            {
                _listingRepository = listingRepository;
                _userRepository = userRepository;
            }

        }

        public async Task<List<APListingStatsVM>> APListingStats(int pageIndex, int pageSize)
        {

            var users = await _userRepository.GetAllActiveUsers(pageIndex, pageSize);
            var model = users.Select(user => new APListingStatsVM(user)).ToList();
            foreach (APListingStatsVM user in model)
            {
                user.UserListingCount = await _listingRepository.GetMyListingsCount(user.UserId, "listed");
            }

            return model;
        }



        //public async Task<List<APListingStatsVM>> APListingStats()
        //{
        //    var results = await _listingRepository.APListingStats(userId);
        //    var models = results.Select(Listing => new ListingSummaryVM(Listing)).ToList();
        //    return models;
        //}



    }
}
