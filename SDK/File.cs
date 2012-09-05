using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace BoxApi.V2
{
	/// <summary>
	/// Represents the Box.NET file entity
	/// </summary>
	[DebuggerDisplay("ID = {ID}, Name = {Name}, Size = {Size}, Tags = {Tags.Count} IsShared = {IsShared}")]
	public class File
	{
		/// <summary>
		/// Gets or sets file ID
		/// </summary>
		public long ID
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets file name
		/// </summary>
		public string Name
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets SHA1 hash
		/// </summary>
		public string SHA1Hash
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets ID of the file's owner
		/// </summary>
		public long? OwnerID
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets file description
		/// </summary>
		public string Description
		{
			get; 
			set;
		}

		/// <summary>
		/// Indicates if file is shared
		/// </summary>
		public bool? IsShared
		{
			get; 
			set;
		}

		/// <summary>
		/// Link to shared file
		/// </summary>
		public string SharedLink
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets public name of the file (if file is shared)
		/// </summary>
		public string PublicName
		{
			get; 
			set;
		}

		/// <summary>
		/// Date when file was created
		/// </summary>
		public DateTime Created
		{
			get; 
			set;
		}

		/// <summary>
		/// Date when file was updated
		/// </summary>
		public DateTime? Updated
		{
			get; 
			set;
		}

		/// <summary>
		/// File size
		/// </summary>
		public long Size
		{
			get; 
			set;
		}

		/// <summary>
		/// User permissions for file
		/// </summary>
		public UserPermissionFlags? PermissionFlags
		{
			get; 
			set;
		}

		private List<TagPrimitive> _tags = new List<TagPrimitive>();

		/// <summary>
		/// List of tags associated with file
		/// </summary>
		public List<TagPrimitive> Tags
		{
			get
			{
				return _tags;
			}
			set
			{
				_tags = value;
			}
		}
	}
}
