using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha;
using Piranha.Data;
using Piranha.Models;
using Piranha.Models.Manager.SettingModels;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Settings controller for the manager area.
	/// </summary>
    public class SettingsController : ManagerController
    {
		/// <summary>
		/// List action.
		/// </summary>
        public ActionResult Index() {
            return View("Index", ListModel.Get());
        }

		#region User actions
		/// <summary>
		/// Edits or creates a new user.
		/// </summary>
		/// <param name="id">The user id</param>
		[Access(Function="ADMIN_USER")]
		public new ActionResult User(string id) {
			if (!String.IsNullOrEmpty(id)) {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingUser ;
				return View("User", UserEditModel.GetById(new Guid(id))) ;
			} else {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewUser ;
				return View("User", new UserEditModel()) ;
			}
		}

		/// <summary>
		/// Searches the users by the given filter.
		/// </summary>
		/// <param name="filter">The search filter.</param>
		public ActionResult SearchUser(string filter) {
			string[] strings = filter.Split(new char[] { ' ' }) ;
			string where = "" ;
			List<object> args = new List<object>() ;

			ViewBag.SelectedTab = "users" ;

			foreach (string str in strings) {
				where += (where != "" ? " OR " : "") +
					"(sysuser_login LIKE @0 OR " +
					"sysuser_firstname LIKE @0 OR " +
					"sysuser_surname LIKE @0 OR " +
					"sysuser_email LIKE @0 OR " +
					"sysuser_created LIKE @0 OR " +
					"sysuser_updated LIKE @0)" ;
				args.Add("%" + str + "%") ;
			}
			args.Add(new Params() { OrderBy = "sysuser_login ASC" }) ;
			return View("Index", ListModel.GetByUserFilter(where, args.ToArray())) ;
		}

		/// <summary>
		/// Saves the model
		/// </summary>
		/// <param name="em">The model</param>
		[HttpPost()]
		[Access(Function="ADMIN_USER")]
		public new ActionResult User(UserEditModel um) {
			if (um.User.IsNew)
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewUser ;
			else ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingUser ;

			if (ModelState.IsValid) {
				try {
					if (um.SaveAll()) {
						ModelState.Clear() ;
						ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingUser ;
						ViewBag.Message = Piranha.Resources.Settings.MessageUserSaved ;
					} else ViewBag.Message = Piranha.Resources.Settings.MessageUserNotSaved ;
				} catch (Exception e) {
					ViewBag.Message = e.ToString() ;
				}
			}
			return View("User", um) ;
		}

		/// <summary>
		/// Deletes the specified user
		/// </summary>
		/// <param name="id">The user id</param>
		[Access(Function="ADMIN_USER")]
		public ActionResult DeleteUser(string id) {
			UserEditModel um = UserEditModel.GetById(new Guid(id)) ;
			
			ViewBag.SelectedTab = "users" ;
			if (um.DeleteAll())
				ViewBag.Message = Piranha.Resources.Settings.MessageUserDeleted ;
			else ViewBag.Message = Piranha.Resources.Settings.MessageUserNotDeleted ;
			
			return Index() ;
		}

		/// <summary>
		/// Generates a new random password for the given user.
		/// </summary>
		/// <param name="id">The user id</param>
		[Access(Function="ADMIN_USER")]
		public ActionResult GeneratePassword(string id) {
			SysUserPassword password = SysUserPassword.GetSingle(new Guid(id)) ;
			string newpwd = SysUserPassword.GeneratePassword() ;

			password.Password = password.PasswordConfirm = newpwd ;
			password.Save() ;
			ViewBag.Message = Piranha.Resources.Settings.MessageNewPassword + newpwd ;

			return User(id) ;
		}
		#endregion

		#region Group actions
		/// <summary>
		/// Edits or creates a new group
		/// </summary>
		/// <param name="id">The group id</param>
		[Access(Function="ADMIN_GROUP")]
		public ActionResult Group(string id) {
			if (!String.IsNullOrEmpty(id)) {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingGroup ;
				return View("Group", GroupEditModel.GetById(new Guid(id))) ;
			} else {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewGroup ;
				return View("Group", new GroupEditModel()) ;
			}
		}

		/// <summary>
		/// Searches the groups for the given search filter
		/// </summary>
		/// <param name="filter">The search string</param>
		public ActionResult SearchGroup(string filter) {
			string[] strings = filter.Split(new char[] { ' ' }) ;
			string where = "" ;
			List<object> args = new List<object>() ;

			ViewBag.SelectedTab = "groups" ;

			foreach (string str in strings) {
				where += (where != "" ? " OR " : "") +
					"(sysgroup_name LIKE @0 OR " +
					"sysgroup_description LIKE @0 OR " +
					"sysgroup_created LIKE @0 OR " +
					"sysgroup_updated LIKE @0)" ;
				args.Add("%" + str + "%") ;
			}
			args.Add(new Params() { OrderBy = "sysgroup_name ASC" }) ;
			return View("Index", ListModel.GetByGroupFilter(where, args.ToArray())) ;
		}

		/// <summary>
		/// Saves the group
		/// </summary>
		/// <param name="gd">The model</param>
		[HttpPost()]
		[Access(Function="ADMIN_GROUP")]
		public ActionResult Group(GroupEditModel gm) {
			if (gm.Group.IsNew)
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewGroup ;
			else ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingGroup ;

			if (ModelState.IsValid) {
				try {
					if (gm.SaveAll()) {
						ModelState.Clear() ;
						ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingGroup ;
						ViewBag.Message = Piranha.Resources.Settings.MessageGroupSaved ;
					} else ViewBag.Message =Piranha.Resources.Settings.MessageGroupNotSaved ;
				} catch (Exception e) {
					ViewBag.Message = e.ToString() ;
				}
			}
			gm.Refresh() ;
			return View("Group", gm) ;
		}

		/// <summary>
		/// Deletes the specified group
		/// </summary>
		/// <param name="id">The group id</param>
		[Access(Function="ADMIN_GROUP")]
		public ActionResult DeleteGroup(string id) {
			GroupEditModel gm = GroupEditModel.GetById(new Guid(id)) ;
			
			ViewBag.SelectedTab = "groups" ;
			if (gm.DeleteAll())
				ViewBag.Message = Piranha.Resources.Settings.MessageGroupDeleted ;
			else ViewBag.Message = Piranha.Resources.Settings.MessageGroupNotDeleted ;
			
			return Index() ;
		}
		#endregion

		#region Access actions
		/// <summary>
		/// Edits or creates a new group
		/// </summary>
		/// <param name="id">The group id</param>
		[Access(Function="ADMIN_ACCESS")]
		public ActionResult Access(string id) {
			if (!String.IsNullOrEmpty(id)) {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingAccess ;
				return View("Access", AccessEditModel.GetById(new Guid(id))) ;
			} else {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewAccess ;
				return View("Access", new AccessEditModel()) ;
			}
		}

		/// <summary>
		/// Searches the access roles by the given filter.
		/// </summary>
		/// <param name="filter">The search filter.</param>
		public ActionResult SearchAccess(string filter) {
			string[] strings = filter.Split(new char[] { ' ' }) ;
			string where = "" ;
			List<object> args = new List<object>() ;

			ViewBag.SelectedTab = "access" ;

			foreach (string str in strings) {
				where += (where != "" ? " OR " : "") +
					"(sysaccess_function LIKE @0 OR " +
					"sysaccess_description LIKE @0 OR " +
					"sysgroup_name LIKE @0 OR " +
					"sysaccess_created LIKE @0 OR " +
					"sysaccess_updated LIKE @0)" ;
				args.Add("%" + str + "%") ;
			}
			args.Add(new Params() { OrderBy = "sysaccess_function ASC" }) ;
			return View("Index", ListModel.GetByAccessFilter(where, args.ToArray())) ;
		}

		/// <summary>
		/// Saves the access
		/// </summary>
		/// <param name="gd">The model</param>
		[HttpPost()]
		[Access(Function="ADMIN_ACCESS")]
		public ActionResult Access(AccessEditModel am) {
			if (am.Access.IsNew)
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewAccess ;
			else ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingAccess ;

			if (ModelState.IsValid) {
				try {
					if (am.SaveAll()) {
						ModelState.Clear() ;
						ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingAccess ;
						ViewBag.Message = Piranha.Resources.Settings.MessageAccessSaved ;
					} else ViewBag.Message = Piranha.Resources.Settings.MessageAccessNotSaved ;
				} catch (Exception e) {
					ViewBag.Message = e.ToString() ;
				}
			}
			return View("Access", am) ;
		}

		/// <summary>
		/// Deletes the specified group
		/// </summary>
		/// <param name="id">The access id</param>
		[Access(Function="ADMIN_ACCESS")]
		public ActionResult DeleteAccess(string id) {
			AccessEditModel am = AccessEditModel.GetById(new Guid(id)) ;
			
			ViewBag.SelectedTab = "access" ;
			if (am.DeleteAll())
				ViewBag.Message = Piranha.Resources.Settings.MessageAccessDeleted ;
			else ViewBag.Message = Piranha.Resources.Settings.MessageAccessNotDeleted ;

			return Index() ;
		}
		#endregion

		#region Param actions
		/// <summary>
		/// Edits or creates a new parameter
		/// </summary>
		/// <param name="id">Parameter id</param>
		[Access(Function="ADMIN_PARAM")]
		public ActionResult Param(string id) {
			if (!String.IsNullOrEmpty(id)) {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingParam ;
				return View("Param", ParamEditModel.GetById(new Guid(id))) ;
			} else {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewParam ;
				return View("Param", new ParamEditModel()) ;
			}
		}

		/// <summary>
		/// Searches the params by the given filter.
		/// </summary>
		/// <param name="filter">The search filter.</param>
		public ActionResult SearchParam(string filter) {
			string[] strings = filter.Split(new char[] { ' ' }) ;
			string where = "" ;
			List<object> args = new List<object>() ;

			ViewBag.SelectedTab = "params" ;

			foreach (string str in strings) {
				where += (where != "" ? " OR " : "") +
					"(sysparam_name LIKE @0 OR " +
					"sysparam_description LIKE @0 OR " +
					"sysparam_value LIKE @0 OR " +
					"sysparam_created LIKE @0 OR " +
					"sysparam_updated LIKE @0)" ;
				args.Add("%" + str + "%") ;
			}
			args.Add(new Params() { OrderBy = "sysparam_name ASC" }) ;
			return View("Index", ListModel.GetByParamFilter(where, args.ToArray())) ;
		}

		/// <summary>
		/// Edits or creates a new parameter
		/// </summary>
		/// <param name="id">Parameter id</param>
		[HttpPost()]
		[Access(Function="ADMIN_PARAM")]
		public ActionResult Param(ParamEditModel pm) {
			if (pm.Param.IsNew)
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewParam ;
			else ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingParam ;

			if (ModelState.IsValid) {
				try {
					if (pm.SaveAll()) {
						ModelState.Clear() ;
						ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingParam ;
						ViewBag.Message = Piranha.Resources.Settings.MessageParamSaved ;
					} else ViewBag.Message = Piranha.Resources.Settings.MessageParamNotSaved ;
				} catch (Exception e) {
					ViewBag.Message = e.ToString() ;
				}
			}
			return View("Param", pm) ;
		}


		/// <summary>
		/// Deletes the specified param
		/// </summary>
		/// <param name="id">The param</param>
		public ActionResult DeleteParam(string id) {
			ParamEditModel pm = ParamEditModel.GetById(new Guid(id)) ;
			
			ViewBag.SelectedTab = "params" ;
			if (pm.DeleteAll())
				ViewBag.Message = Piranha.Resources.Settings.MessageParamDeleted ;
			else ViewBag.Message = Piranha.Resources.Settings.MessageParamNotDeleted ;

			return Index() ;
		}
		#endregion
	}
}
