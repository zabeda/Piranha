using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

using AutoMapper;
using Piranha.Entities;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the comment edit view.
	/// </summary>
	public class CommentEditModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the current comment.
		/// </summary>
		public Entities.Comment Comment { get ; set ; }

		/// <summary>
		/// Gets/sets the available statuses.
		/// </summary>
		public IList<SelectListItem> Statuses { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CommentEditModel() {
			Statuses = (from Enum e in Enum.GetValues(typeof(Entities.Comment.CommentStatus))
						   select new SelectListItem() { Value = e.ToString(), Text = Entities.Comment.GetStatusName((Entities.Comment.CommentStatus)e) }).ToList() ;
		}

		/// <summary>
		/// Gets the edit model for the comment with the given id.
		/// </summary>
		/// <param name="id">The comment id.</param>
		/// <returns></returns>
		public static CommentEditModel GetById(Guid id) {
			var m = new CommentEditModel() ;

			using (var db = new DataContext()) {
				m.Comment = db.Comments.Where(c => c.Id == id).Single() ;
			}
			return m ;
		}

		/// <summary>
		/// Saves the current edit model.
		/// </summary>
		/// <returns>Weather the entity was updated or not</returns>
		public bool Save() {
			using (var db = new DataContext()) {
				var comment = db.Comments.Where(c => c.Id == Comment.Id).SingleOrDefault() ;
				if (comment == null) {
					comment = new Entities.Comment() ;
					db.Comments.Add(comment) ;
				}
				Mapper.Map<Entities.Comment, Entities.Comment>(Comment, comment) ;

				var ret = db.SaveChanges() > 0 ;
				Comment.Id = comment.Id ;

				return ret ;
			}
		}

		/// <summary>
		/// Deletes the current comment.
		/// </summary>
		/// <returns>Weather the entity was removed or not</returns>
		public bool Delete() {
			using (var db = new DataContext()) {
				Comment.Attach(db, EntityState.Deleted) ;
				return db.SaveChanges() > 0 ;
			}
		}
	}
}