using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

using Piranha.Extend;
using Piranha.Entities;

namespace Piranha.Extensions.Regions
{
	/// <summary>
	/// Simple region for attaching posts to a page.
	/// </summary>
	[Extension(
		InternalId="Container", 
		Name="CRName", 
		ResourceType=typeof(Piranha.Extensions.Resources),
		Type=ExtensionType.Region)]
	public class ContainerRegion : IExtension
	{
		#region Properties
		/// <summary>
		/// Gets/sets the id's of the attached posts.
		/// </summary>
		public List<Guid> Posts { get ; set ; }

		/// <summary>
		/// Gets the currently attached posts.
		/// </summary>
		[ScriptIgnore()]
		public List<Post> Current { get ; private set ; }

		/// <summary>
		/// Gets the currenty available posts.
		/// </summary>
		[ScriptIgnore()]
		public List<Post> Available { get ; private set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ContainerRegion() {
			Posts = new List<Guid>() ;
			Current = new List<Post>() ;
		}

		/// <summary>
		/// Initializes the region.
		/// </summary>
		/// <param name="model">The current page model.</param>
		public virtual void Init(object model) {
			using (var db = new DataContext()) {
				// Get the currently attached posts
				var posts = db.Posts.Include(p => p.Template).Include(p => p.Categories).Where(p => Posts.Contains(p.Id)).ToList() ;
				foreach (var id in Posts) {
					var post = posts.Where(p => p.Id == id).SingleOrDefault() ;
					if (posts != null)
						Current.Add(post) ;
				}
				// Get all of the available posts.
				Available = db.Posts.Include(p => p.Template).OrderBy(p => p.Template.Name).ThenBy(p => p.Title).ToList() ;
			}
		}
	}
}