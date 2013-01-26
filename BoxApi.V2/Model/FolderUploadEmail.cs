using BoxApi.V2.Model.Enum;

namespace BoxApi.V2.Model
{
    /// <summary>
    /// The upload email address for this folder
    /// </summary>
    public class FolderUploadEmail
    {
        /// <summary>
        /// The upload accessibility of this folder
        /// </summary>
        public Access Access { get; set; }

        /// <summary>
        /// The email address to which content can be sent
        /// </summary>
        public string Email { get; set; }
    }
}