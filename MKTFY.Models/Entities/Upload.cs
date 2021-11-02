using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.Entities
{
    public class Upload
    {
        [Key]
        public Guid Id { get; set; }

        //not saving file name, just its location
        [Required]
        public string Url { get; set; }

        public ICollection<ListingUpload> ListingUploads { get; set; }


    }
}
