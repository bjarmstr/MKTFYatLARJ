using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.User
{
    /// <summary>
    /// View All User Profile Data
    /// </summary>
    public class UserVM
    {
        public UserVM()
        {

        }
        public UserVM(Entities.User src)
        {
            Id = src.Id;
            Email = src.Email;
            FirstName = src.FirstName;
            LastName = src.LastName;
            Phone = src.Phone;
            StreetAddress = src.StreetAddress;
            City = src.City;
            Province = src.Province;
            Country = src.Country;
        }

        public string Id { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }

       
        public string LastName { get; set; }

     
        public string Phone { get; set; }

    
        public string StreetAddress { get; set; }

      
        public string City { get; set; }

      
        public string Province { get; set; }

   
        public string Country { get; set; }
    }
}
