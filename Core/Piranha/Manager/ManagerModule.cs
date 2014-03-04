using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace Piranha.Manager
{
	/// <summary>
	/// Static class responsible for starting the manager module.
	/// </summary>
	public static class ManagerModule
	{
		/// <summary>
		/// Create the view engine.
		/// </summary>
		public static void Init() {
			if (!Config.DisableManager) {
				// Register the manager area
				var manager = new ManagerRegistration();
				var context = new AreaRegistrationContext(manager.AreaName, RouteTable.Routes);
				manager.RegisterArea(context);

				// Register custom model binders
				RegisterBinders();

				// Register json deserialization for post data
				ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
			}
		}

		#region Private methods
		/// <summary>
		/// Registers all custom model binders for the manager.
		/// </summary>
		private static void RegisterBinders() {
			if (ModelBinders.Binders[typeof(Piranha.Models.Manager.PageModels.EditModel)] == null)
				ModelBinders.Binders.Add(typeof(Piranha.Models.Manager.PageModels.EditModel),
					new Piranha.Models.Manager.PageModels.EditModel.Binder());
			if (ModelBinders.Binders[typeof(Piranha.Models.Manager.PostModels.EditModel)] == null)
				ModelBinders.Binders.Add(typeof(Piranha.Models.Manager.PostModels.EditModel),
					new Piranha.Models.Manager.PostModels.EditModel.Binder());
			if (ModelBinders.Binders[typeof(Piranha.Models.Manager.TemplateModels.PageEditModel)] == null)
				ModelBinders.Binders.Add(typeof(Piranha.Models.Manager.TemplateModels.PageEditModel),
					new Piranha.Models.Manager.TemplateModels.PageEditModel.Binder());
			if (ModelBinders.Binders[typeof(Piranha.Models.Manager.TemplateModels.PostEditModel)] == null)
				ModelBinders.Binders.Add(typeof(Piranha.Models.Manager.TemplateModels.PostEditModel),
					new Piranha.Models.Manager.TemplateModels.PostEditModel.Binder());
			if (ModelBinders.Binders[typeof(Piranha.Models.Manager.CategoryModels.EditModel)] == null)
				ModelBinders.Binders.Add(typeof(Piranha.Models.Manager.CategoryModels.EditModel),
					new Piranha.Models.Manager.CategoryModels.EditModel.Binder());
			if (ModelBinders.Binders[typeof(Piranha.Models.Manager.SettingModels.UserEditModel)] == null)
				ModelBinders.Binders.Add(typeof(Piranha.Models.Manager.SettingModels.UserEditModel),
					new Piranha.Models.Manager.SettingModels.UserEditModel.Binder());
			if (ModelBinders.Binders[typeof(Piranha.Models.Manager.ContentModels.EditModel)] == null)
				ModelBinders.Binders.Add(typeof(Piranha.Models.Manager.ContentModels.EditModel),
					new Piranha.Models.Manager.ContentModels.EditModel.Binder());
			if (ModelBinders.Binders[typeof(Piranha.Extend.IExtension)] == null)
				ModelBinders.Binders.Add(typeof(Piranha.Extend.IExtension),
					new Binders.IExtensionBinder());
		}
		#endregion
	}
}