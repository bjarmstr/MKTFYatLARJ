using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Repositories.Repositories.Interfaces
{
    public interface ISearchRepository
    {
        Task Save(SearchHistory search);
        Task<List<SearchHistory>> GetLatestSearches(string userId);
    }
}
