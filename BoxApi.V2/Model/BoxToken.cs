using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxApi.V2.Model
{
    /// <summary>
    /// Tokens provide an easy and instant way to authenticate a user to Box, even if they don’t have a Box account. When the token is created, a special folder for only your app is created in the user’s account. The token can only be used to access this specific folder and any items contained within it.
    /// </summary>
    public class BoxToken : EntityBase
    {
        /// <summary>
        /// The auth token that can be used to access the user’s account
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The folder that has been created in the user’s account for your app
        /// </summary>
        public Entity Item { get; set; }
    }
}
