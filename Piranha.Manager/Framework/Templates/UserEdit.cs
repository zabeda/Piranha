using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;

using Piranha.Manager.Models;

namespace Piranha.Manager.Templates
{
	/// <summary>
	/// Page template for the user edit view.
	/// </summary>
	public abstract class UserEdit : Piranha.WebPages.ContentPage<UserEditModel>
	{
		/// <summary>
		/// Executes the page.
		/// </summary>
		protected override void ExecutePage() {
			if (!IsPost) {
				if (UrlData.Count > 0) {
					Page.Title = Piranha.Resources.Settings.EditTitleExistingUser ;
					Model = UserEditModel.GetById(new Guid(UrlData[0])) ;
				} else {
					Page.Title = Piranha.Resources.Settings.EditTitleNewUser ;
					Model = new UserEditModel() ;
				}
				CreateModelState(Model) ;
			}
		}

		/// <summary>
		/// Saves the given user model.
		/// </summary>
		/// <param name="m">The model</param>
		public void Save(UserEditModel m) {
			Model = m ;
			Page.Title = Piranha.Resources.Settings.EditTitleExistingUser ;

			if (ModelState.IsValid) {
				if (m.Save())
					this.SuccessMessage(Piranha.Resources.Settings.MessageUserSaved) ;
				else this.ErrorMessage(Piranha.Resources.Settings.MessageUserNotSaved) ;
			}
			CreateModelState(m) ;
		}

		/// <summary>
		/// Binds the model state. THIS SHOULD BE MOVED TO Piranha.WebPages.ModelBinder
		/// </summary>
		/// <param name="m">The model</param>
		private void CreateModelState(UserEditModel m) {
			ModelState.Add("m.User.Login", new ModelState() { Value = m.User.Login }) ;
			ModelState.Add("m.User.Firstname", new ModelState() { Value = m.User.Firstname }) ;
			ModelState.Add("m.User.Surname", new ModelState() { Value = m.User.Surname }) ;
			ModelState.Add("m.User.Email", new ModelState() { Value = m.User.Email }) ;
			ModelState.Add("m.User.GroupId", new ModelState() { Value = m.User.GroupId }) ;
			ModelState.Add("m.User.IsLocked", new ModelState() { Value = m.User.IsLocked }) ;
			ModelState.Add("m.User.LockedUntil", new ModelState() { Value = m.User.LockedUntil }) ;
		}
	}
}