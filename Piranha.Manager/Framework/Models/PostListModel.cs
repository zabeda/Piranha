using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using AutoMapper;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the post list.
	/// </summary>
	public class PostListModel
	{
		#region Inner classes
		/// <summary>
		/// The different statuses a post can have.
		/// </summary>
		public enum PostStatus {
			UNPUBLISHED, DRAFT, PUBLISHED
		}

		/// <summary>
		/// Post list model.
		/// </summary>
		public class PostModel
		{
			#region Properties
			/// <summary>
			/// Gets/sets the id.
			/// </summary>
			public Guid Id { get ; set ; }

			/// <summary>
			/// Gets/sets the title.
			/// </summary>
			public string Title { get ; set ; }

			/// <summary>
			/// Gets/sets the navigation title.
			/// </summary>
			public string NavigationTitle { get ; set ; }

			/// <summary>
			/// Gets/sets the title to display.
			/// </summary>
			public string DisplayTitle { get { return !String.IsNullOrEmpty(NavigationTitle) ? NavigationTitle : Title ; } }

			/// <summary>
			/// Gets/sets the template name.
			/// </summary>
			public string TemplateName { get ; set ; }

			/// <summary>
			/// Gets/sets the created date.
			/// </summary>
			public DateTime Created { get ; set ; }

			/// <summary>
			/// Gets/sets the updated date.
			/// </summary>
			public DateTime Updated { get ; set ; }

			/// <summary>
			/// Gets/sets the status of the post.
			/// </summary>
			public PostStatus Status { get ; set ; }

			/// <summary>
			/// Gets/sets the number of new comments.
			/// </summary>
			public int NewComments { get ; set ; }
			#endregion
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the available posts.
		/// </summary>
		public IList<PostModel> Posts { get ; set ; }

		/// <summary>
		/// Gets/sets the available templates.
		/// </summary>
		public IList<Entities.PostTemplate> Templates { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PostListModel() {
			using (var db = new DataContext()) {
				Templates = db.PostTemplates.OrderBy(t => t.Name).ToList() ;
			}
		}

		/// <summary>
		/// Gets the model with the currently available posts.
		/// </summary>
		/// <returns>The model</returns>
		public static PostListModel Get() {
			var m = new PostListModel() ;

			using (var db = new DataContext()) {
				m.Posts = Mapper.Map<List<PostModel>>(
					db.PostDrafts.Include(p => p.Comments).Include(p => p.Template).OrderByDescending(p => p.Updated).ToList()) ;
			}
			return m ;
		}
	}
}