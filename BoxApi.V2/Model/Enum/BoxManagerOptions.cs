using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxApi.V2.Model.Enum
{
    /// <summary>
    /// Options for customizing the behavior of the BoxManager.
    /// </summary>
    [Flags]
    public enum BoxManagerOptions
    {
        /// <summary>
        /// No options requested
        /// </summary>
        None = 0,
        /// <summary>
        /// Any request that receives an HTTP 500 (Internal Server Error) response should be retried once.
        /// </summary>
        RetryRequestOnceWhenHttp500Received,
    }
}
