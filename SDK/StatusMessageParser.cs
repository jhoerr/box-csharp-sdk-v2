using BoxApi.V2.Statuses;

namespace BoxApi.V2
{
	/// <summary>
	/// Defines methods for parsing values of status fields
	/// </summary>
	internal static class StatusMessageParser
	{
		internal static AuthenticationStatus ParseAuthorizeStatus(string status)
		{
			switch (status)
			{
				default:
					return AuthenticationStatus.Unknown;
			}
		}

		internal static GetTicketStatus ParseGetTicketStatus(string status)
		{
			switch (status.ToLower())
			{
				case "application_restricted":
					return GetTicketStatus.ApplicationRestricted;
				case "get_ticket_ok":
					return GetTicketStatus.Successful;
				default:
					return GetTicketStatus.Unknown;
			}
		}

		internal static GetAuthenticationTokenStatus ParseGetAuthenticationTokenStatus(string status)
		{
			switch (status.ToLower())
			{
				case "application_restricted":
					return GetAuthenticationTokenStatus.ApplicationRestricted;
				case "not_logged_in":
					return GetAuthenticationTokenStatus.NotLoggedID;
				case "get_auth_token_error":
					return GetAuthenticationTokenStatus.Failed;
				case "get_auth_token_ok":
					return GetAuthenticationTokenStatus.Successful;
				default:
					return GetAuthenticationTokenStatus.Unknown;
			}
		}
	}
}
