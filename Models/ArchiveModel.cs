using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Piranha.Data;

namespace Piranha.Models
{
	/// <summary>
	/// Archive model for the cms application.
	/// </summary>
	public class ArchiveModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the posts in the current archive.
		/// </summary>
		public List<Post> Archive { get ; set ; }

		/// <summary>
		/// Gets/sets the content in the current archive.
		/// </summary>
		public List<Content> Content { get ; set ; }

		/// <summary>
		/// Gets/sets the archive category.
		/// </summary>
		public Category Category { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new archive model.
		/// </summary>
		public ArchiveModel() {
			Archive = new List<Post>() ;
			Content = new List<Content>() ;
		}

		/// <summary>
		/// Gets the archive model for the given permalink.
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <returns>The model</returns>
		public static ArchiveModel GetByPermalink(string permalink) {
			ArchiveModel m = new ArchiveModel() ;

			m.Category = Category.GetByPermalink(permalink) ;
			m.GetRelated() ;
			return m ;
		}

		/// <summary>
		/// Gets the archive model for the given permalink.
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <typeparam name="T">The model type</typeparam>
		/// <returns>The model</returns>
		public static T GetByPermalink<T>(string permalink) where T : ArchiveModel {
			T m = Activator.CreateInstance<T>() ;

			m.Category = Category.GetByPermalink(permalink) ;
			m.GetRelated() ;
			return m ;
		}

		/// <summary>
		/// Gets the related information for the archive.
		/// </summary>
		private void GetRelated() {
			// Get posts
			Archive = Models.Post.GetByCategoryId(Category.Id) ;

			// Get content
			Content = Models.Content.GetByCategoryId(Category.Id) ;
		}
	}
}
