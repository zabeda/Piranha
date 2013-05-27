using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Piranha.Security
{
	/// <summary>
	/// User provider that uses the microsoft membership provider system.
	/// </summary>
	internal class MembershipUserProvider : IUserProvider
	{
		#region Members
		/// <summary>
		/// Collection for mapping membership users to Piranha CMS users.
		/// </summary>
		private readonly Dictionary<object, Guid> MemberToUser = new Dictionary<object, Guid>() ;

		/// <summary>
		/// Collection for mapping Piranha CMS users to membership users.
		/// </summary>
		private readonly Dictionary<Guid, object> UserToMember = new Dictionary<Guid,object>() ;

		/// <summary>
		/// Mutex for locking the user map collection.
		/// </summary>
		private readonly object mutex = new object() ;
		#endregion

		#region Properties
		/// <summary>
		/// Gets if the current user is authenticated.
		/// </summary>
		public bool IsAuthenticated {
			get { return HttpContext.Current.User.Identity.IsAuthenticated ; }
		}

		/// <summary>
		/// Gets the user id of the current user.
		/// </summary>
		public Guid UserId {
			get {
				var id = Membership.GetUser(false).ProviderUserKey ;

				if (!MemberToUser.ContainsKey(id)) {
					lock (mutex) {
						if (!MemberToUser.ContainsKey(id)) {
							using (var db = new DataContext()) {
								var user = db.Users.Where(u => u.ExternalId == id.ToString()).SingleOrDefault() ;
								if (user != null) {
									MemberToUser.Add(id, user.Id) ;
									UserToMember.Add(user.Id, id) ;
								} else MemberToUser.Add(id, Guid.Empty) ;
							}
						}
					}
				}
				return MemberToUser[id] ;
			}
		}

		/// <summary>
		/// Get the current user.
		/// </summary>
		public MembershipUser User {
			get { return Membership.GetUser(false) ; }
		}
		#endregion

		/// <summary>
		/// Authenticates the user.
		/// </summary>
		/// <param name="username">The username</param>
		/// <param name="password">The password</param>
		/// <returns>If the username and passwords matches</returns>
		public bool Authenticate(string username, string password) {
			return Membership.ValidateUser(username, password) ;
		}

		/// <summary>
		/// Creates a new user entity with the given credentials.
		/// </summary>
		/// <param name="username">The username</param>
		/// <param name="password">The password</param>
		/// <param name="email">The email address</param>
		/// <returns>The provider user key for the new user</returns>
		public object Create(string username, string password, string email) {
			var user = Membership.CreateUser(username, password, email) ;

			return user.ProviderUserKey;
		}

		/// <summary>
		/// Deletes the user with the given Piranha CMS user id.
		/// </summary>
		/// <param name="id">The Piranha CMS user id</param>
		/// <returns>If the user was deleted successfully</returns>
		public bool Delete(Guid id) {
			if (UserToMember.ContainsKey(id))
				return Delete(UserToMember[id]) ;
			return false ;
		}

		/// <summary>
		/// Deletes the user with the given membership provider key.
		/// </summary>
		/// <param name="id">The membership user id</param>
		/// <returns>If the user was deleted successfully</returns>
		public bool Delete(object id) {
			var user = Membership.GetUser(id, false) ;

			if (user != null)
				return Membership.DeleteUser(user.UserName, true);
			return false ;			
		}

		/// <summary>
		/// Gets the user with the given Piranha CMS user id.
		/// </summary>
		/// <param name="id">The Piranha CMS user id</param>
		/// <returns>The user</returns>
		public MembershipUser GetUser(Guid id) {
			if (UserToMember.ContainsKey(id))
				return Membership.GetUser(UserToMember[id], false) ;
			return null ;
		}
	}
}