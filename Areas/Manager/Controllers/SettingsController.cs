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
		#region User actions
		/// <summary>
		/// Gets the list of all users.
		/// </summary>
		/// <returns></returns>
		[Access(Function="ADMIN_USER")]
		public ActionResult UserList() {
            return View("UserList", UserListModel.Get());
		}

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
			
			return UserList() ;
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
		/// Gets the group list.
		/// </summary>
		[Access(Function="ADMIN_GROUP")]
        public ActionResult GroupList() {
            return View("GroupList", GroupListModel.Get());
        }

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
			
			return GroupList() ;
		}
		#endregion

		#region Access actions
		/// <summary>
		/// Gets the access list.
		/// </summary>
		[Access(Function="ADMIN_ACCESS")]
        public ActionResult AccessList() {
            return View("AccessList", AccessListModel.Get());
        }

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

			return AccessList() ;
		}
		#endregion

		#region Param actions
		/// <summary>
		/// Gets the param list.
		/// </summary>
		[Access(Function="ADMIN_PARAM")]
        public ActionResult ParamList() {
            return View("ParamList", ParamListModel.Get());
        }

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

			return ParamList() ;
		}
		#endregion
	}
}
