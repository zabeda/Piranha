using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Piranha.Entities
{
	/// <summary>
	/// The core user entity.
	/// </summary>
	public class User : BaseEntity
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique user id.
		/// </summary>
		public Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets the id of the group to which the user belong.
		/// </summary>
		public Guid? GroupId { get ; set ; }

		/// <summary>
		/// Gets/sets the unique login name.
		/// </summary>
		public string Login { get ; set ; }

		/// <summary>
		/// Gets/sets the SHA2 encrypted password.
		/// </summary>
		public string Password { get ; set ; }

		/// <summary>
		/// Gets/sets the firstname of the user.
		/// </summary>
		public string Firstname { get ; set ; }

		/// <summary>
		/// Gets/sets the surname of the user.
		/// </summary>
		public string Surname { get ; set ; }

		/// <summary>
		/// Gets/sets the surname of the user.
		/// </summary>
		public string Email { get ; set ; }

		/// <summary>
		/// Gets/sets the users prefered culture.
		/// </summary>
		public string Culture { get ; set ; }

		/// <summary>
		/// Gets/sets the date the user was initially created.
		/// </summary>
		public DateTime Created { get ; set ; }

		/// <summary>
		/// Gets/sets the date the user was last updated.
		/// </summary>
		public DateTime Updated { get ; set ; }

		/// <summary>
		/// Gets/sets the id of the user who initially created the user.
		/// </summary>
		public Guid? CreatedById { get ; set ; }

		/// <summary>
		/// Gets/sets the id of the user who last updated the user.
		/// </summary>
		public Guid? UpdatedById { get ; set ; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the group to which the user belong.
		/// </summary>
		public Group Group { get ; set ; }

		/// <summary>
		/// Gets/sets the currently available extensions.
		/// </summary>
		public IList<Extension> Extensions { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public User() {
			Extensions = new List<Extension>() ;
		}

		/// <summary>
		/// Saves the current user.
		/// </summary>
		/// <param name="state">The entity state</param>
		public override void OnSave(System.Data.EntityState state) {
			var user = HttpContext.Current != null ? HttpContext.Current.User : null ;

			if (DataContext.Identity != Guid.Empty || user.Identity.IsAuthenticated) {
				if (state == EntityState.Added) {
					if (Id == Guid.Empty)
						Id = Guid.NewGuid() ;
					Created = Updated = DateTime.Now ;
					CreatedById = UpdatedById = DataContext.Identity != Guid.Empty ? DataContext.Identity : new Guid(user.Identity.Name) ;
				} else if (state == EntityState.Modified) {
					Updated = DateTime.Now ;
					UpdatedById = DataContext.Identity != Guid.Empty ? DataContext.Identity : new Guid(user.Identity.Name) ;
				}
			} else throw new UnauthorizedAccessException("User must be logged in to save entity") ;
		}
	}
}
