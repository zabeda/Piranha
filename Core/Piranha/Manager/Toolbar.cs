using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Piranha.Manager
{
	/// <summary>
	/// Toolbar helpers for the manager interface.
	/// </summary>
	public static class Toolbar
	{
		/// <summary>
		/// Renders the toolbar.
		/// </summary>
		/// <param name="url">The url</param>
		/// <param name="model">The current model</param>
		/// <returns>The rendered html</returns>
		public static HtmlString Render(UrlHelper url, object model) {
			StringBuilder str = new StringBuilder() ;

			if (model is Models.Manager.PageModels.ListModel && Piranha.Hooks.Manager.Page.Toolbar.ListToolbarRender != null)
				Piranha.Hooks.Manager.Page.Toolbar.ListToolbarRender(url, str, (Models.Manager.PageModels.ListModel)model) ;
			else if (model is Models.Manager.PageModels.EditModel && Piranha.Hooks.Manager.Page.Toolbar.EditToolbarRender != null)
				Piranha.Hooks.Manager.Page.Toolbar.EditToolbarRender(url, str, (Models.Manager.PageModels.EditModel)model) ;
			else if (model is Areas.Manager.Models.PostListModel && Piranha.Hooks.Manager.Post.Toolbar.ListToolbarRender != null)
				Piranha.Hooks.Manager.Post.Toolbar.ListToolbarRender(url, str, (Areas.Manager.Models.PostListModel)model) ;
			else if (model is Models.Manager.PostModels.EditModel && Piranha.Hooks.Manager.Post.Toolbar.EditToolbarRender != null)
				Piranha.Hooks.Manager.Post.Toolbar.EditToolbarRender(url, str, (Models.Manager.PostModels.EditModel)model) ;
			else if (model is Models.Manager.ContentModels.ListModel && Piranha.Hooks.Manager.Media.Toolbar.ListToolbarRender != null)
				Piranha.Hooks.Manager.Media.Toolbar.ListToolbarRender(url, str, (Models.Manager.ContentModels.ListModel)model) ;
			else if (model is Models.Manager.ContentModels.EditModel && Piranha.Hooks.Manager.Media.Toolbar.EditToolbarRender != null)
				Piranha.Hooks.Manager.Media.Toolbar.EditToolbarRender(url, str, (Models.Manager.ContentModels.EditModel)model) ;
			else if (model is Areas.Manager.Models.SiteTreeListModel && Piranha.Hooks.Manager.SiteTree.Toolbar.ListToolbarRender != null)
				Piranha.Hooks.Manager.SiteTree.Toolbar.ListToolbarRender(url, str, (Areas.Manager.Models.SiteTreeListModel)model) ;
			else if (model is Areas.Manager.Models.SiteTreeEditModel && Piranha.Hooks.Manager.SiteTree.Toolbar.EditToolbarRender != null)
				Piranha.Hooks.Manager.SiteTree.Toolbar.EditToolbarRender(url, str, (Areas.Manager.Models.SiteTreeEditModel)model) ;

			return new HtmlString(str.ToString()) ;
		}
	}
}