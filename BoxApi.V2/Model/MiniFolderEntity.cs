namespace BoxApi.V2.Model
{
    /// <summary>
    /// A mini-folder for use in determining paths
    /// </summary>
    public class MiniFolderEntity : Entity
    {
        /// <summary>
        ///     A unique ID for use with the /events endpoint
        /// </summary>
        public string SequenceId { get; set; }

        /// <summary>
        ///     A unique string identifying the version of this file.
        /// </summary>
        public string Etag { get; set; }
    }
}