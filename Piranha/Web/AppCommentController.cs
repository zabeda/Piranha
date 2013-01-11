using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Entities;

namespace Piranha.Web
{
	/// <summary>
	/// Controller for handling comments from the web application.
	/// </summary>
    public class AppCommentController : Controller
	{
		#region Inner classes
		public class CommentSaveModel
		{
			#region Properties
			public Guid Id { get ; set ; }
			public string Title { get ; set ; }
			public string Body { get ; set ; }
			public string SessionId { get ; set ; }
			public string AuthorName { get ; set ; }
			public string AuthorEmail { get ; set ; }
			#endregion
		}
		#endregion

		/// <summary>
		/// Saves the given model.
		/// </summary>
		/// <param name="m">The comment</param>
		[HttpPost(), ValidateInput(false)]
		public ActionResult Save(CommentSaveModel m) {
			if (ModelState.IsValid && m.SessionId == Session.SessionID) {
				using (var db = new DataContext()) {
					Comment comment = null ;

					// Try to load the comment if this is an update
					if (m.Id != null)
						comment = db.Comments.Where(c => c.Id == m.Id).SingleOrDefault() ;

					// If no existing comment was found, create a new
					if (comment == null) {
						comment = new Comment() ;
						comment.Attach(db, EntityState.Added) ;
					}

					// If the user isn't authenticated, add user info
					if (!User.Identity.IsAuthenticated) {
						comment.AuthorName = m.AuthorName ;
						comment.AuthorEmail = m.AuthorEmail ;
					}

					// Update standard properties
					comment.Title = m.Title ;
					comment.Body = m.Body ;

					db.SaveChanges() ;
				}
			}
			return new RedirectResult("~/") ;
		}
	}
}
