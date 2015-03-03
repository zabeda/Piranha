/*
 * Copyright (c) 2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

using System;

namespace Piranha.Data
{
	/// <summary>
	/// The default sys user.
	/// </summary>
	public sealed class User : IModel, IModified
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the group id.
		/// </summary>
		public Guid? GroupId { get; set; }

		/// <summary>
		/// Gets/sets the optional api key.
		/// </summary>
		public Guid? ApiKey { get; set; }

		/// <summary>
		/// Gets/set the username.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Gets/sets the password.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Gets/sets the firstname.
		/// </summary>
		public string Firstname { get; set; }

		/// <summary>
		/// Gets/sets the lastname.
		/// </summary>
		public string Lastname { get; set; }

		/// <summary>
		/// Gets/sets the email.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets/sets the optional culture.
		/// </summary>
		public string Culture { get; set; }

		/// <summary>
		/// Gets/sets if the user is locked.
		/// </summary>
		public bool IsLocked { get; set; }

		/// <summary>
		/// Gets/sets how long the user lock is valid.
		/// </summary>
		public DateTime? LockedUntil { get; set; }

		/// <summary>
		/// Gets/sets the last login made by the user.
		/// </summary>
		public DateTime? LatestLogin { get; set; }

		/// <summary>
		/// Gets/sets the previous login made by the user.
		/// </summary>
		public DateTime? PreviousLogin { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially created.
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		public DateTime Updated { get; set; }

		/// <summary>
		/// Gets/sets the optional id of the user who initially 
		/// created the model.
		/// </summary>
		public Guid? CreatedById { get; set; }

		/// <summary>
		/// Gets/sets the optional id of the user who last
		/// updated the model.
		/// </summary>
		public Guid? UpdatedById { get; set; }
		#endregion
	}
}
