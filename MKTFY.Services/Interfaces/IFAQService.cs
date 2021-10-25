using MKTFY.Models.ViewModels.FAQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    public interface IFAQService
    {
        Task<FAQVM> Create(FAQCreateVM src);
        Task<FAQVM> Get(Guid id);

        Task<List<FAQVM>> GetAll(bool isDeleted); 

        Task<FAQVM> Update(FAQUpdateVM src);

        Task SoftDelete(Guid id);

    }
}
