using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.Entities
{
    public class SearchHistory
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Product { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public string UserId { get; set; }
        //User User is a navigation property, which allows access to User details
        public User User { get; set; }
    }
}
