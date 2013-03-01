using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Piranha.Extend
{
	/// <summary>
	/// The extension container handles all extensions loaded by MEF.
	/// </summary>
	public class ExtensionContainer : IPartImportsSatisfiedNotification
	{
		#region Members
		/// <summary>
		/// The private composition container.
		/// </summary>
		private CompositionContainer Container = null ;

		/// <summary>
		/// The currently available extensions
		/// </summary>
		[ImportMany(AllowRecomposition=true)]
		public IEnumerable<Lazy<IExtension, IExtensionMeta>> Extensions;

		/// <summary>
		/// Gets/sets weather the container is ready.
		/// </summary>
		public bool IsReady { get ; private set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new extension container and registers all
		/// exported objects.
		/// </summary>
		public ExtensionContainer() {
			var catalog = new AggregateCatalog() ;
			catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly())) ;
			Container = new CompositionContainer(catalog) ;
			Container.ComposeParts(this) ;
		}

		/// <summary>
		/// Method is called when all imports are finised.
		/// </summary>
		public void OnImportsSatisfied() {
			// Run the ensure method for all extensions.
			foreach (var ext in Extensions) {
				var ensure = ext.Value.GetType().GetMethod("Ensure") ;
				if (ensure != null)
					ensure.Invoke(ext.Value, null) ;
			}
			IsReady = true ;
		}
	}
}