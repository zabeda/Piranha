using System;
using System.Collections.Generic;

namespace Piranha.Security
{
	/// <summary>
	/// Mock security provider for unit tests.
	/// </summary>
	public sealed class MockSecurityManager : ISecurityManager
	{
		/// <summary>
		/// Gets if the currently logged in user is an administrator.
		/// </summary>
		public bool IsAdmin { get { return false; } }

		/// <summary>
		/// Gets if the current user is authenticated.
		/// </summary>
		public bool IsAuthenticated { get { return false;  } }

		/// <summary>
		/// Gets the currently available roles.
		/// </summary>
		/// <returns>The roles</returns>
		public IEnumerable<Role> GetRoles() { return new List<Role>(); }

		/// <summary>
		/// Logs in the user with the given username and password.
		/// </summary>
		/// <param name="username">The username</param>
		/// <param name="password">The password</param>
		/// <returns>If the user was successfully logged in</returns>
		public bool Login(string username, string password) {
			return false;
		}

		/// <summary>
		/// Logs our the currently logged in user.
		/// </summary>
		public void Logout() { }
	}
}