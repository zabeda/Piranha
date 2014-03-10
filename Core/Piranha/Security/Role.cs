using System;

namespace Piranha.Security
{
	/// <summary>
	/// Roles are used to specify permissions.
	/// </summary>
	public sealed class Role
	{
		/// <summary>
		/// The role id.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// The role name.
		/// </summary>
		public string Name { get; set; }
	}
}