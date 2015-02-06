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

namespace Piranha.Models
{
	public sealed class User : IModel, IModified
	{
		public Guid Id { get; set; }
		public Guid? GroupId { get; set; }
		public Guid? ApiKey { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }
		public string Culture { get; set; }
		public bool IsLocked { get; set; }
		public DateTime? LockedUntil { get; set; }
		public DateTime? LatestLogin { get; set; }
		public DateTime? PreviousLogin { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Guid? CreatedById { get; set; }
		public Guid? UpdatedById { get; set; }
	}

		//public Guid Id { get; set; }
		//public DateTime Created { get; set; }
		//public DateTime Updated { get; set; }
		//public Guid CreatedById { get; set; }
		//public Guid UpdatedById { get; set; }

		//public User CreatedBy { get; set; }
		//public User UpdatedBy { get; set; }

}
