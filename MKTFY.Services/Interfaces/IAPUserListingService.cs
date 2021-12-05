using MKTFY.Models.ViewModels.AdminPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    public interface IAPUserListingService
    {
        Task<List<APListingStatsVM>> APListingStats(int pageIndex, int pageSize);
    }
}
