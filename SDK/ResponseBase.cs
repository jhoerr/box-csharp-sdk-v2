using System;

namespace BoxApi.V2
{
	/// <summary>
	/// Base type for all operation response types
	/// </summary>
	/// <typeparam name="TStatusType"></typeparam>
	public abstract class ResponseBase<TStatusType>
	{
		/// <summary>
		/// Gets operation status
		/// </summary>
		public TStatusType Status
		{
			get; 
			internal set;
		}

		/// <summary>
		/// Gets a value indicating which error occurred during an operation execution
		/// </summary>
		public Exception Error
		{
			get; 
			internal set;
		}

		/// <summary>
		/// Gets user state object
		/// </summary>
		public object UserState
		{
			get; 
			internal set;
		}
	}
}
