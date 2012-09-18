namespace BoxApi.V2
{
    /// <summary>
	/// Represents the callback method to invoke when operation is finished
	/// </summary>
	/// <typeparam name="TResponseType">Type of operation status</typeparam>
	/// <param name="response">Response information</param>
	public delegate void OperationFinished<TResponseType>(TResponseType response);
}
