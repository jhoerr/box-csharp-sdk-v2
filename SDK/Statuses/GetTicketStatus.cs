namespace BoxApi.V2.Statuses
{
	/// <summary>
	/// Specifies statuses of 'get_ticket' web method
	/// </summary>
	public enum GetTicketStatus
	{
		/// <summary>
		/// Used if status string doen't match to any of enum members
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Represents 'get_ticket_ok' status string
		/// </summary>
		Successful = 1,

		/// <summary>
		/// An error occured during execution.
		/// </summary>
		Failed = 2,

		/// <summary>
		/// An invalid API key was provided, or the API key is restricted from calling this function.
		/// Represents 'application_restricted' status string.
		/// </summary>
		ApplicationRestricted = 4
	}
}