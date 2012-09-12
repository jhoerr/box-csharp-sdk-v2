using System;
using System.Linq.Expressions;
using BoxApi.V2.ServiceReference;

namespace BoxApi.V2.SDK.Model
{
	/// <summary>
	/// Represents Box.NET user entity
	/// </summary>
	public sealed class User
	{
		private Expression<Func<int, SOAPUser>> _materialize;
		private bool isMaterialized;
		private int _id;
		private int _accessID;
		private string _email;
		private string _login;
		private long _maxUploadSize;
		private long _spaceAmount;
		private long _spaceUsed;

		/// <summary>
		/// Initializes object
		/// </summary>
		public User()
		{
		}

		internal User(SOAPUser user)
		{
			Initialize(user);
		}

		/// <summary>
		/// Initializes object
		/// </summary>
		/// <param name="id">The unique ID of the user</param>
		/// <param name="materialize">Callback method for 'lazy' load object's data</param>
		public User(int id, Expression<Func<int, SOAPUser>> materialize)
		{
			_id = id;
			_materialize = materialize;
		}

		private void Materialize()
		{
			SOAPUser user = _materialize.Compile()(_id);

			Initialize(user);
		}

		private void Initialize(SOAPUser user)
		{
			isMaterialized = true;

			_id = user.user_id;
			_accessID = user.access_id;
			_email = user.email;
			_login = user.login;
			_maxUploadSize = user.max_upload_size;
			_spaceAmount = user.space_amount;
			_spaceUsed = user.space_used;
		}

		/// <summary>
		/// The unique ID of the user
		/// </summary>
		public int ID
		{
			get
			{
				return _id;
			}
		}

		/// <summary>
		/// The user's email address
		/// </summary>
		public string Email
		{
			get
			{
				if (!isMaterialized)
				{
					Materialize();
				}

				return _email;
			}
		}

		/// <summary>
		/// The user's login name
		/// </summary>
		public string Login
		{
			get
			{
				if (!isMaterialized)
				{
					Materialize();
				}

				return _login;
			}
		}

		/// <summary>
		/// The maximum size of the file that user can upload
		/// </summary>
		public long MaxUploadSize
		{
			get
			{
				return _maxUploadSize;
			}
		}

		/// <summary>
		/// The total amount of space allocated to that account
		/// </summary>
		public long SpaceAmount
		{
			get
			{
				return _spaceAmount;
			}
		}

		/// <summary>
		/// The amount of space currently utilized by the user
		/// </summary>
		public long SpaceUsed
		{
			get
			{
				return _spaceUsed;
			}
		}

		/// <summary>
		/// If the user is a guest, the AccessID will be the ID of the guest's parent.
		/// If this is a full user, the AccessID will be the same as the ID property
		/// </summary>
		public int AccessID
		{
			get
			{
				return _accessID;
			}
		}
	}
}
