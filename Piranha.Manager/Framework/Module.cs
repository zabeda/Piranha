using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.WebPages;
using System.Web.WebPages.Html;

using AutoMapper;
using Piranha.Manager.Models;

namespace Piranha.Manager
{
	/// <summary>
	/// The site manager class.
	/// </summary>
	public static class Module
	{
		#region Members
		/// <summary>
		/// Weather the manager has been initialized.
		/// </summary>
		private static bool Initialized = false ;
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the virtual path of the manager interface.
		/// </summary>
		public static string VirtualPath { 
			get { return "~/manager/" ; } 
		}
		#endregion

		/// <summary>
		/// Registers the manager module.
		/// </summary>
		internal static void RegisterModule() {
			ApplicationPart.Register(new ApplicationPart(Assembly.GetExecutingAssembly(), VirtualPath)) ;
		}

		/// <summary>
		/// Initializes the manager.
		/// </summary>
		public static void Init() {
			if (!Initialized) {
				// Register module
				RegisterModule() ;

				// Initialize AutoMapper
				Mappings.Init() ;

				// Set application part to initialized
				Initialized = true ;
			}
		}
	}
}