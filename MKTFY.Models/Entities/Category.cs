using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.Entities
{
    public class Category
    {
        [Key]
        public string Name { get; set; }


        public virtual ICollection<Listing> Listings { get; set; }


    }
}
