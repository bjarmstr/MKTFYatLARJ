using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Repositories.Repositories.Interfaces
{
    public interface IListingRepository
    {
        Task<Listing> Create(Listing src); //Create New Listing

        Task<Listing> Get(Guid id); //Read One Listing

        Task<List<Listing>> GetAll(); // Read all Listings

        Task<Listing> Update(Listing src); //Update an existing Listing

        Task<ICollection<ListingUpload>>Delete(Guid id);  //Delete a Listing

        Task<List<Listing>> GetByCategory(int categoryId, string region );

        Task<List<Listing>> GetBySearchTerm(string searchTerm, string region);


    }
}