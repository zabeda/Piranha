using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using AutoMapper;
using Piranha.Entities;
using Piranha.Web;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the comment list view.
	/// </summary>
	public class CommentListModel
	{
		#region Inner classes
		/// <summary>
		/// Internal comment view model.
		/// </summary>
		public class CommentModel {
			/// <summary>
			/// Gets/sets the id.
			/// </summary>
			public Guid Id { get ; set ; }

			/// <summary>
			/// Gets/sets the comment title.
			/// </summary>
			public string Title { get ; set ; }

			/// <summary>
			/// Gets/sets the current comment status.
			/// </summary>
			public Entities.Comment.CommentStatus Status { get ; set ; }

			/// <summary>
			/// Gets the status name.
			/// </summary>
			public string StatusName {
				get { return Entities.Comment.GetStatusName(Status) ; }
			}

			/// <summary>
			/// Gets/sets the author name.
			/// </summary>
			public string AuthorName { get ; set ; }

			/// <summary>
			/// Gets/sets the autor email.
			/// </summary>
			public string AuthorEmail { get ; set ; }

			/// <summary>
			/// Gets/sets the created date.
			/// </summary>
			public DateTime Created { get ; set ; }
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the available comments.
		/// </summary>
		[ModelProperty(OnLoad="LoadComments")]
		public List<CommentModel> Comments { get ; set ; }
		#endregion

		/// <summary>
		/// Loads the available comments.
		/// </summary>
		public void LoadComments() {
			using (var db = new DataContext()) {
				Comments = Mapper.Map<List<CommentModel>>(db.Comments.Include(c => c.CreatedBy).OrderByDescending(c => c.Created).ToList()) ;
			}
		}

		/// <summary>
		/// Gets the comments for the given parent id.
		/// </summary>
		/// <param name="parentid">The parent id.</param>
		/// <returns>The comments</returns>
		public static List<Entities.Comment> GetCommentList(Guid parentid) {
			using (var db = new DataContext()) {
				return db.Comments.
					Include(c => c.CreatedBy).
					Where(c => c.ParentId == parentid && c.ParentIsDraft == false).
					OrderByDescending(c => c.Created).ToList() ;
			}
		}
	}
}