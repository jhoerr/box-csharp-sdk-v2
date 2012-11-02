using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxApi.V2.Model
{
    public class BoxAuthToken
    {
        /// <summary>
        /// Status of the auth token request
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The auth token, if a valid one was returned 
        /// </summary>
        public string AuthToken { get; set; }

        /// <summary>
        /// The user associated with the auth token 
        /// </summary>
        public User User { get; set; }
    }
}
