using System.Text;
using BoxApi.V2.Model.Enum;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     The base type for objects returned from Box.
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        ///     The type of item being returned.
        /// </summary>
        public ResourceType Type { get; set; }
    }

    public class IdentifiedEntity : EntityBase
    {
        /// <summary>
        ///     The item's ID
        /// </summary>
        public string Id { get; set; }
    }

    public class Entity : IdentifiedEntity
    {
        /// <summary>
        ///     The name of the item
        /// </summary>
        public string Name { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("type: {0} id: {1} name: {2}", Type, Id, Name);
            return sb.ToString();
        }
    }
}