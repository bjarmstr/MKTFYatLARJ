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
        Task<ListingVM> Create(ListingCreateVM src);  //Create a new listing
        Task<ListingVM> Get(Guid id);

        Task<List<ListingVM>> GetAll(); // Read all Listings

        Task<ListingVM> Update(ListingUpdateVM src); //Update an existing Listing

        Task Delete(Guid id);  //Delete a Listing

    }
}
