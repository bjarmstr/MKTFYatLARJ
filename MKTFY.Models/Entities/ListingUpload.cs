using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.Entities
{
    public class ListingUpload
    {
        public Guid ListingId{ get; set; }
        public Guid  UploadId{ get; set; }
        
        //navigation properties
        public Upload Upload { get; set; }
        public Listing Listing { get; set; }


    }
}
