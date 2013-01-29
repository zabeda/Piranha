using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using Piranha.Entities;
using Piranha.Web;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the user list.
	/// </summary>
	public class UserListModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available users.
		/// </summary>
		[ModelProperty(OnLoad="LoadUsers")]
		public IList<Entities.User> Users { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public UserListModel() {
			Users = new List<Entities.User>() ;
		}

		/// <summary>
		/// Loads the user list.
		/// </summary>
		public void LoadUsers() {
			using (var db = new DataContext()) {
				Users = db.Users.Include(u => u.Group).OrderBy(u => u.Login).ToList() ;
			}
		}
	}
}