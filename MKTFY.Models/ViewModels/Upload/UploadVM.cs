using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.Upload
{
    public class UploadVM
    {

        public UploadVM() { }

        public UploadVM(Entities.Upload src) {
            Id = src.Id;
            Url = src.Url;    
        }
        public Guid Id { get; set; }          
        public string Url { get; set; }
    }
}
