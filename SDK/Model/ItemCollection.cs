using System.Collections.Generic;

namespace BoxApi.V2.Model
{
    public class Collection<T> where T: class, new()
    {
        /// <summary>
        /// Count of Entries
        /// </summary>
        public string TotalCount { get; set; }

        /// <summary>
        /// A collection of T items
        /// </summary>
        public List<T> Entries { get; set; } 
    }

    /// <summary>
    /// Detailed information about a folder's contents
    /// </summary>
    public class ItemCollection : Collection<Folder>
    {
    }

    /// <summary>
    /// A list of errors returned during an operation
    /// </summary>
    public class ErrorCollection : Collection<Error>
    {
    }

    /// <summary>
    /// A list of comments attached to a file or discussion
    /// </summary>
    public class CommentCollection : Collection<Comment>
    {
    }

}