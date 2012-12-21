using Newtonsoft.Json;

namespace BoxApi.V2.Model
{
    /// <summary>
    /// Represents an email alias for a user
    /// </summary>
    public class EmailAlias : IdentifiedEntity
    {
        /// <summary>
        /// The alias
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Whether the user has confirmed this alias
        /// </summary>
        [JsonProperty(PropertyName = "is_confirmed")]
        public bool IsConfirmed { get; set; }
    }

    /// <summary>
    /// A collection of user email aliases
    /// </summary>
    public class EmailAliasCollection : Collection<EmailAlias>
    {
    }
}