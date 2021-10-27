using MKTFY.Models.ViewModels.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MKTFY.Services.Interfaces
{
    public interface IUploadService
    {
        Task <List<UploadResultVM>> UploadFiles (List<IFormFile> files);
    }
}
