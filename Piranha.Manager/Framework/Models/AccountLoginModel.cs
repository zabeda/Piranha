using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

using Piranha.Web;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the login page.
	/// </summary>B
	public sealed class AccountLoginModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the current version.
		/// </summary>
		[ModelProperty(OnLoad="LoadVersion")]
		public string Version { get ; set ; }
		#endregion

		/// <summary>
		/// Loads the current version.
		/// </summary>
		public void LoadVersion() {
			Version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion ;
		}
	}
}