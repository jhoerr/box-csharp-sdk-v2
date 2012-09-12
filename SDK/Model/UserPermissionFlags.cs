using System;

namespace BoxApi.V2.SDK.Model
{
	/// <summary>
	/// Specifies type of user access for specific object
	/// </summary>
	[Flags]
	public enum UserPermissionFlags
	{
		/// <summary>
		/// No permissions
		/// </summary>
		None = 1,

		/// <summary>
		/// Permission to download the object
		/// </summary>
		Download = 2,

		/// <summary>
		/// Permission to delete the object
		/// </summary>
		Delete = 4,

		/// <summary>
		/// Permission to rename the object
		/// </summary>
		Rename = 8,

		/// <summary>
		/// Permission to share the object
		/// </summary>
		Share = 16,

		/// <summary>
		/// Permission to upload the object
		/// </summary>
		Upload = 32,

		/// <summary>
		/// Permission to view the object
		/// </summary>
		View = 64
	}
}
