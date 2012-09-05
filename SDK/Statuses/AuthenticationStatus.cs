namespace BoxApi.V2.Statuses
{
	/// <summary>
	/// Specifies authentication process statuses
	/// </summary>
	public enum AuthenticationStatus : byte
	{
		/// <summary>
		/// Unknown status
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Authentication completed successfully
		/// </summary>
		Successful = 1,

		/// <summary>
		/// Authentication failed
		/// </summary>
		Failed = 2
	}
}
