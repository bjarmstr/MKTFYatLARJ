using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.User
{
    /// <summary>
    /// View All User Profile Data
    /// </summary>
    public class UserUpdateVM
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [MinLength (10, ErrorMessage = "The property {0} should have a minimum of {1} digits")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "The property {0} should only contain numbers")]
        public string Phone { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
