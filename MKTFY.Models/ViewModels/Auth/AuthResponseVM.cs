using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.Auth
{
    public class AuthResponseVM
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
