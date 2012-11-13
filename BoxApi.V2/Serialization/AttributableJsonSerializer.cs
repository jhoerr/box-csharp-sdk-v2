using System.IO;
using Newtonsoft.Json;
using RestSharp.Serializers;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace BoxApi.V2.Serialization
{
    /// <summary>
    ///     Default JSON serializer for request bodies
    ///     Doesn't currently use the SerializeAs attribute, defers to Newtonsoft's attributes
    /// </summary>
    public class AttributableJsonSerializer : ISerializer
    {
        private readonly JsonSerializer _serializer;

        /// <summary>
        ///     Default serializer
        /// </summary>
        public AttributableJsonSerializer()
        {
            ContentType = "application/json";
            _serializer = new JsonSerializer
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Include,
                    DefaultValueHandling = DefaultValueHandling.Include
                };
        }

        /// <summary>
        ///     Default serializer with overload for allowing custom Json.NET settings
        /// </summary>
        public AttributableJsonSerializer(JsonSerializer serializer)
        {
            ContentType = "application/json";
            _serializer = serializer;
        }

        /// <summary>
        ///     Serialize the object as JSON
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>JSON as String</returns>
        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    jsonTextWriter.QuoteChar = '"';

                    _serializer.Serialize(jsonTextWriter, obj);

                    var result = stringWriter.ToString();
                    return result;
                }
            }
        }

        /// <summary>
        ///     Unused for JSON Serialization
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        ///     Unused for JSON Serialization
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        ///     Unused for JSON Serialization
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        ///     Content type for serialized content
        /// </summary>
        public string ContentType { get; set; }
    }
}