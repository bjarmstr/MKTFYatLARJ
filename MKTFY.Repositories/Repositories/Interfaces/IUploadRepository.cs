using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Repositories.Repositories.Interfaces
{
    public interface IUploadRepository
    {
        Task<Upload> Create(Upload src);  // Create a new upload
        Task<Upload> Get(Guid id);        // Get a single existing upload by Id
        Task Delete(Guid id);             // Delete a upload

    }
}
