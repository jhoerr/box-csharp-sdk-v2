namespace BoxApi.V2.Statuses
{
	/// <summary>
	/// Specifies statuses of 'get_auth_token' web method
	/// </summary>
	public enum GetAuthenticationTokenStatus : byte
	{
		/// <summary>
		/// Unknown status string
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Represents 'get_auth_token_ok' status string
		/// </summary>
		Successful = 1,

		/// <summary>
		/// Represents 'get_auth_token_error' status string
		/// </summary>
		Failed = 2,

		/// <summary>
		/// The user did not successfully authenticate.
		/// Represents 'not_logged_id' status string.
		/// </summary>
		NotLoggedID = 3,

		/// <summary>
		/// An invalid API key was provided, or the API key is restricted from calling this function.
		/// Represents 'application_restricted' status string.
		/// </summary>
		ApplicationRestricted = 4
	}
}
