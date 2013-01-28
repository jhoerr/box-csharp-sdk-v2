using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxApi.V2.Model.Enum
{
    public enum ThumbnailSize
    {
        /// <summary>
        /// A 32x32 pixel thumbnail
        /// </summary>
        Small = 1,
        /// <summary>
        /// A 64x64 pixel thumbnail
        /// </summary>
        Medium = 2,
        /// <summary>
        /// A 128x128 thumbnail
        /// </summary>
        Large = 3,
        /// <summary>
        /// A 256x256 thumbnail
        /// </summary>
        Jumbo = 4,
    }
}
