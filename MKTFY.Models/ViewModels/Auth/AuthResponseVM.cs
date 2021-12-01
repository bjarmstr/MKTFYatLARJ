using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.Auth
{
    /// <summary>
    /// Access Token and Expiry for Authorization
    /// </summary>
    public class AuthResponseVM
    {
        /// <summary>
        /// Authorization JWT token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Expiry
        /// </summary>
        public int ExpiresIn { get; set; }
    }
}
