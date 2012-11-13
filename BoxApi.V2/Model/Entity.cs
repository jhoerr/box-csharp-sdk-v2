using System.Text;
using BoxApi.V2.Model.Enum;

namespace BoxApi.V2.Model
{
    public class EntityBase
    {
        /// <summary>
        /// Type of item.  One of: File, Folder, Comment, Discussion, Event, Token
        /// </summary>
        public ResourceType Type { get; set; }
    }

    public class Entity : EntityBase
    {
        /// <summary>
        /// The item's ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A unique ID for use with the /events endpoint
        /// </summary>
        public string SequenceId { get; set; }

        /// <summary>
        /// The name of the item
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