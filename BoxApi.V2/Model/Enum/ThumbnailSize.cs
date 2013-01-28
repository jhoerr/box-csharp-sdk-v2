using System.ComponentModel;

namespace BoxApi.V2.Model.Enum
{
    public enum ThumbnailSize
    {
        /// <summary>
        ///     A 32x32 pixel thumbnail
        /// </summary>
        [Description("32")] Small = 1,

        /// <summary>
        ///     A 64x64 pixel thumbnail
        /// </summary>
        [Description("64")] Medium = 2,

        /// <summary>
        ///     A 128x128 thumbnail
        /// </summary>
        [Description("128")] Large = 3,

        /// <summary>
        ///     A 256x256 thumbnail
        /// </summary>
        [Description("256")] Jumbo = 4,
    }
}