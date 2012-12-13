using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Piranha.Entities
{
	/// <summary>
	/// Entity for a comment. A comment can be attached to any entity.
	/// </summary>
	public class Comment : BaseEntity
	{
		#region Properties
		/// <summary>
		/// Gets/sets the comment id.
		/// </summary>
		public Guid Id { get ; set ; }

		/// <summary>
		/// The id of the entity this comment is attached to.
		/// </summary>
		public Guid ParentId { get ; set ; }

		/// <summary>
		/// Gets/sets weather the comment is attached to a draft.
		/// </summary>
		public bool ParentIsDraft { get ; set ; }

		/// <summary>
		/// Gets/sets weather this comment is approved or not.
		/// </summary>
		public bool Approved { get ; set ; }

		/// <summary>
		/// Gets/sets the title.
		/// </summary>
		public string Title { get ; set ; }

		/// <summary>
		/// Gets/sets the body of the content.
		/// </summary>
		public string Body { get ; set ; }

		/// <summary>
		/// Gets/sets the optional author name if this is an anonymous comment.
		/// </summary>
		public string AuthorName { get ; set ; }

		/// <summary>
		/// Gets/sets the optional author email if this is an anonymous comment.
		/// </summary>
		public string AuthorEmail { get ; set ; }

		/// <summary>
		/// Gets/sets the date the entity was first created.
		/// </summary>
		public DateTime Created { get ; set ; }

		/// <summary>
		/// Gets/sets the date the entity was last changed.
		/// </summary>
		public DateTime Updated { get ; set ; }

		/// <summary>
		/// Gets/sets the optional id of the user who initially created the entity.
		/// </summary>
		public Guid? CreatedById { get ; set ; }

		/// <summary>
		/// Gets/sets the optional id of the user who last changed the entity.
		/// </summary>
		public Guid? UpdatedById { get ; set ; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the optional user who initially created the entity.
		/// </summary>
		public User CreatedBy { get ; set ; }

		/// <summary>
		/// Gets/sets the optional user who last changed the entity.
		/// </summary>
		public User UpdatedBy { get ; set ; }
		#endregion

		/// <summary>
		/// Attaches the entity to the given context.
		/// </summary>
		/// <param name="db">The db context</param>
		public void Attach(Piranha.DataContext db) {
			if (this.Id == Guid.Empty || db.Set<Comment>().Count(t => t.Id == this.Id) == 0)
				db.Entry(this).State = EntityState.Added ;
			else db.Entry(this).State = EntityState.Modified ;
		}

		/// <summary>
		/// Saves the current entity.
		/// </summary>
		/// <param name="state">The current entity state</param>
		public override void OnSave(System.Data.EntityState state) {
			var user = HttpContext.Current.User;

			// We never connect comments to drafts.
			ParentIsDraft = false ;

			if (state == EntityState.Added) {
				if (Id == Guid.Empty)
					Id = Guid.NewGuid() ;
				Created = Updated = DateTime.Now ;
				if (user.Identity.IsAuthenticated || DataContext.Identity != Guid.Empty)
					CreatedById = UpdatedById = DataContext.Identity != Guid.Empty ? DataContext.Identity : new Guid(user.Identity.Name) ;
			} else if (state == EntityState.Modified) {
				Updated = DateTime.Now ;
				if (user.Identity.IsAuthenticated || DataContext.Identity != Guid.Empty)
					UpdatedById = DataContext.Identity != Guid.Empty ? DataContext.Identity : new Guid(user.Identity.Name) ;
			}
		}
	}
}
