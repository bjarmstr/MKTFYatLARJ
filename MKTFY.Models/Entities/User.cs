using MKTFY.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.Entities
{
    public class User
    {
        public User()
        {

        }

        public User(UserCreateVM src)
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


        [Key]
        public string Id { get; set; }


        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


        [Required]
        public string Phone { get; set; }

        [Required]
        public string StreetAddress{ get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

    }
}
