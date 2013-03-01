using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Manager.Models;
using Piranha.WebPages;

namespace Piranha.Manager.Templates
{
	/// <summary>
	/// Page template for the post list view.
	/// </summary>
	[Access(Function="ADMIN_POST", RedirectUrl="~/manager/account")]
	public abstract class PostList : ContentPage<PostListModel>
	{
		/// <summary>
		/// Executes the page.
		/// </summary>
		protected override void ExecutePage() {
			Model = PostListModel.Get() ;
			if (Hooks.PostListModelLoaded != null)
				Hooks.PostListModelLoaded(this, Menu.GetActiveMenuItem(), Model) ;
		}
	}
}