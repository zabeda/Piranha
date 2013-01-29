using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

using AutoMapper;
using Piranha.Entities;
using Piranha.Web;

namespace Piranha.Manager.Models
{
	public class UserEditModel
	{
		#region Properties
		public Entities.User User { get ; set ; }
		public IList<SelectListItem> Groups { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public UserEditModel() {
			using (var db = new DataContext()) {
				Groups = Mapper.Map<List<SelectListItem>>(db.Groups.OrderBy(g => g.Name).ToList()) ;
			}
		}

		public static UserEditModel GetById(Guid id) {
			var m = new UserEditModel() ;

			using (var db = new DataContext()) {
				m.User = db.Users.Where(u => u.Id == id).Single() ;
			}
			return m ;
		}

		public bool Save() {
			using (var db = new DataContext()) {
				var user = db.Users.Where(u => u.Id == User.Id).SingleOrDefault() ;
				if (user == null) {
					user = new Entities.User() ;
					user.Attach(db, EntityState.Added) ;
				}
				Mapper.Map<Entities.User, Entities.User>(User, user) ;

				if (!String.IsNullOrEmpty(User.Password))
					user.Password = Piranha.Models.SysUser.Encrypt(User.Password) ;

				return db.SaveChanges() > 0 ;
			}
		}
	}
}