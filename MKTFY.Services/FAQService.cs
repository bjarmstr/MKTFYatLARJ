using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels.FAQ;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MKTFY.Repositories.Repositories.Interfaces;

namespace MKTFY.Services
{
    public class FAQService : IFAQService
    {
        private readonly IFAQRepository _faqRepository;

        public FAQService(IFAQRepository FAQRepository)
        {
            _faqRepository = FAQRepository;
        }
        public async Task<FAQVM> Create(FAQCreateVM src)
        {
            var newEntity = new FAQ(src);
            //check category
            newEntity.DateCreated = DateTime.UtcNow;
            //newEntity.Status = "active";
            var result = await _faqRepository.Create(newEntity);
            var model = new FAQVM(result);
            return model;
        }

        public async Task<FAQVM> Get(Guid id)
        {
            var result = await _faqRepository.Get(id);
            var model = new FAQVM(result);
            return model;
        }

        public async Task<List<FAQVM>> GetAll()
        {
            var results = await _faqRepository.GetAll();
            var models = results.Select(FAQ => new FAQVM(FAQ)).ToList();
            return models;
        }

        public async Task<FAQVM> Update(FAQUpdateVM src)
        {
            var updateData = new FAQ(src);
            var result = await _faqRepository.Update(updateData);
            var model = new FAQVM(result);
            return model;


        }
    }
}
