using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Manager
{
	/// <summary>
	/// Defines the managers menu.
	/// </summary>
	public static class Menu
	{
		public static List<MenuGroup> Items = new List<MenuGroup>() {
			new MenuGroup() { InternalId = "Content", Name = @Resources.Menu.Content, IconUrl = "~/manager/content/img/ico-menu-content.png", 
				Items = new List<MenuItem>() {
					new MenuItem() { InternalId = "Pages", Name = @Resources.Menu.ContentPages, Href = "~/manager/page",
						Permission = "ADMIN_PAGE" },
					new MenuItem() { InternalId = "Posts", Name = @Resources.Menu.ContentPosts, Href = "~/manager/post",
						Permission = "ADMIN_POST" },
					new MenuItem() { InternalId = "Media", Name = @Resources.Menu.ContentMedia, Href = "~/manager/media",
						Permission = "ADMIN_CONTENT" },
					new MenuItem() { InternalId = "Comments", Name = @Resources.Menu.ContentComments, Href = "~/manager/comment",
						Permission = "ADMIN_COMMENT" },
				}
			},
			new MenuGroup() { InternalId = "Settings", Name = @Resources.Menu.Settings, IconUrl = "~/manager/content/img/ico-menu-settings.png",
				Items = new List<MenuItem>() {
					new MenuItem() { InternalId = "PageTypes", Name = @Resources.Menu.SettingsPageTypes, Href = "~/manager/pagetype",
						Permission = "ADMIN_PAGE_TEMPLATE" },
					new MenuItem() { InternalId = "PostTypes", Name = @Resources.Menu.SettingsPostTypes, Href = "~/manager/posttype",
						Permission = "ADMIN_POST_TEMPLATE" },
					new MenuItem() { InternalId = "Categories", Name = @Resources.Menu.SettingsCategories, Href = "~/manager/category",
						Permission = "ADMIN_CATEGORY" },
					new MenuItem() { InternalId = "Comments", Name = @Resources.Menu.SettingsComments, Href = "~/manager/commentsettings",
						Permission = "ADMIN_COMMENT" },
				}
			},
			new MenuGroup() { InternalId = "System", Name = @Resources.Menu.System, IconUrl = "~/manager/content/img/ico-menu-system.png",
				Items = new List<MenuItem>() {
					new MenuItem() { InternalId = "Users", Name = @Resources.Menu.SystemUsers, Href = "~/manager/user",
						Permission = "ADMIN_USER" },
					new MenuItem() { InternalId = "Groups", Name = @Resources.Menu.SystemGroups, Href = "~/manager/group",
						Permission = "ADMIN_GROUP" },
					new MenuItem() { InternalId = "Permissions", Name = @Resources.Menu.SystemPermissions, Href = "~/manager/permission",
						Permission = "ADMIN_ACCESS" },
					new MenuItem() { InternalId = "Parameters", Name = @Resources.Menu.SystemParams, Href = "~/manager/param",
						Permission = "ADMIN_PARAM" }
				}
			}
		} ;
	}

	/// <summary>
	/// A menu group on the top level in the manager interface.
	/// </summary>
	public class MenuGroup
	{
		#region Properties
		/// <summary>
		/// The internal, non translatable textual id.
		/// </summary>
		public string InternalId { get ; set ; }

		/// <summary>
		/// The name of the group.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the virtual path to the icon.
		/// </summary>
		public string IconUrl { get ; set ; }

		/// <summary>
		/// The menu items this group contains.
		/// </summary>
		public IList<MenuItem> Items { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public MenuGroup() {
			Items = new List<MenuItem>() ;
		}

		/// <summary>
		/// Checks if the current user has access to the group.
		/// </summary>
		/// <returns>Weather the current user has access.</returns>
		public bool HasAccess() {
			return ItemsForUser().Count > 0 ;
		}

		/// <summary>
		/// Checks if the current group is active.
		/// </summary>
		/// <returns>Weather the group is active</returns>
		public bool IsActive() {
			foreach (var item in ItemsForUser()) {
				if (item.IsActive())
					return true ;
			}
			return false ;
		}

		/// <summary>
		/// Gets the items available for the current user.
		/// </summary>
		/// <returns>The menu items</returns>
		public IList<MenuItem> ItemsForUser() {
			var ret = new List<MenuItem>() ;

			foreach (var item in Items)
				if (!String.IsNullOrEmpty(item.Permission)) {
					if (HttpContext.Current.User.HasAccess(item.Permission))
						ret.Add(item) ;
				} else ret.Add(item) ;
			return ret ;
		}
	}

	/// <summary>
	/// A menu item in the manager interface.
	/// </summary>
	public class MenuItem
	{
		#region Properties
		/// <summary>
		/// The internal, non translatable textual id.
		/// </summary>
		public string InternalId { get ; set ; }

		/// <summary>
		/// Gets/sets the item name.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the item href.
		/// </summary>
		public string Href { get ; set ; }

		/// <summary>
		/// Gets/sets the permission needed to access the item.
		/// </summary>
		public string Permission { get ; set ; }
		#endregion

		/// <summary>
		/// Gets weather this menu item is currently active.
		/// </summary>
		/// <returns>Weather the item is active</returns>
		public bool IsActive() {
			var current = ("~" + HttpContext.Current.Request.Url.PathAndQuery.ToLower()).Split(new char[] { '/' }) ;
			var href = Href.ToLower().Split(new char[] { '/' }) ;
			
			for (int n = 0; n < href.Length; n++) {
				if (current.Length < n || href[n] != current[n])
					return false ;
			}
			return true ;
		}
	}
}