namespace BoxApi.V2
{
	/// <summary>
	/// Represents the callback method to invoke when authrization status is updated
	/// </summary>
	/// <param name="status">Status message</param>
	public delegate void UpdateStatus(string status);

	/// <summary>
	/// Represents the callback method to invoke when authentication process is finished
	/// </summary>
	/// <param name="isSuccessful">Indicates if authentication process finished successfully</param>
	public delegate void AuthenticationProcessFinished(bool isSuccessful);

	/// <summary>
	/// Represents the callback method to invoke when operation is finished
	/// </summary>
	/// <typeparam name="TResponseType">Type of operation status</typeparam>
	/// <param name="response">Response information</param>
	public delegate void OperationFinished<TResponseType>(TResponseType response);

	/// <summary>
	/// Represents the callback method to invoke when operation is finished
	/// </summary>
	/// <typeparam name="TStatusType">Type of operation status</typeparam>
	/// <typeparam name="TResultType">Type of object which is returned by the operation as a execution result</typeparam>
	/// <param name="status">Operation status</param>
	/// <param name="result">Execution result</param>
	internal delegate void OperationFinished<TStatusType, TResultType>(TStatusType status, TResultType result);

	/// <summary>
	/// Defines helper methods to work with delegates of UpdateStatus type
	/// </summary>
	internal static class EventHandlerExtensions
	{
		internal static void SafeInvoke(this UpdateStatus handler, string status)
		{
			if (handler != null)
			{
				handler(status);
			}
		}
	}
}
