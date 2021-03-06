namespace BoxApi.V2.Model
{
    public class Version : ModifiableEntity
    {
        /// <summary>
        ///     The item size in bytes
        /// </summary>
        public double Size { get; set; }
    }

    public class VersionCollection : Collection<Version>
    {
    }
}
