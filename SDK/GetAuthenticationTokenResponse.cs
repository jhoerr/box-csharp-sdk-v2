using BoxApi.V2.SDK.Model;
using BoxApi.V2.Statuses;

namespace BoxApi.V2
{
	/// <summary>
	/// Represents response from 'GetAuthenticationToken' method
	/// </summary>
	public sealed class GetAuthenticationTokenResponse : ResponseBase<GetAuthenticationTokenStatus>
	{
		/// <summary>
		/// Basic information about a user's account
		/// </summary>
		public User AuthenticatedUser
		{
			get;
			internal set;
		}

		/// <summary>
		/// The authentication token used to access and apply operations to a user's account.
		/// </summary>
		public string AuthenticationToken
		{
			get;
			internal set;
		}
	}
}
