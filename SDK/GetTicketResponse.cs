using BoxApi.V2.Statuses;

namespace BoxApi.V2
{
	/// <summary>
	/// Represents response from 'GetTicket' method
	/// </summary>
	public sealed class GetTicketResponse : ResponseBase<GetTicketStatus>
	{
		/// <summary>
		/// The ticket used to generate an authentication page and login a user
		/// </summary>
		public string Ticket
		{
			get; 
			internal set;
		}
	}
}
