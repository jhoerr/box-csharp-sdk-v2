using System;

using BoxApi.V2.ServiceReference;
using BoxApi.V2.Statuses;

namespace BoxApi.V2.Samples
{
	public class BoxAuthLayer
	{
		internal readonly BoxManager _manager;
		private string _ticket;

		/// <summary>
		/// Initializes BoxProvider type instance
		/// </summary>
		/// <param name="applicationApiKey"></param>
		public BoxAuthLayer(string applicationApiKey, string authToken = null)
		{
            _manager = new BoxManager(applicationApiKey, null, authToken);
		}

		/// <summary>
		/// Asynchronously gets authorization ticket 
		/// and opens web browser to logging on Box.NET portal
		/// </summary>
		public void StartAuthentication()
		{
			_manager.GetTicket(GetTicketCompleted);
		}

		/// <summary>
		/// Finishes authorization process after user has 
		/// successfully finished logging process on Box.com
		/// </summary>
		/// <param name="printUserInfoCallback">Callback method which will be invoked after operation completes</param>
		public void FinishAuthentication(Action<User> printUserInfoCallback)
		{
			_manager.GetAuthenticationToken(_ticket, GetAuthenticationTokenCompleted, printUserInfoCallback);
		}       

		private void GetAuthenticationTokenCompleted(GetAuthenticationTokenResponse response)
		{
			Action<User> printUserInfoCallback = (Action<User>)response.UserState;

			printUserInfoCallback(response.AuthenticatedUser);
		}

		private void GetTicketCompleted(GetTicketResponse response)
		{
			if (response.Status == GetTicketStatus.Successful)
			{
				_ticket = response.Ticket;

				string url = string.Format("www.box.net/api/1.0/auth/{0}", response.Ticket);

				BrowserLauncher.OpenUrl(url);
			}
			else
			{
				Exception error = response.Error ??
								  new ApplicationException(
									string.Format("Can't get an authorization ticket. Operation status is {0}",
												  response.Status));

				throw error;
			}
		}

	}
}
