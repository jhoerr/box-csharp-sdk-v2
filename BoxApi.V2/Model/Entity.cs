using System.Text;

namespace BoxApi.V2.Model
{
    public class Entity
    {
        /// <summary>
        /// Type of item.  One of: File, Folder, Comment, Discussion, Event, Token
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The items's ID
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