using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

using Piranha.Data;

namespace Piranha.Models
{
	/// <summary>
	/// Active record for a post template.
	/// 
	/// Changes made to records of this type are logged.
	/// </summary>
	[PrimaryKey(Column="posttemplate_id")]
	[Serializable]
	public class PostTemplate : PiranhaRecord<PostTemplate>
	{
		#region Fields
		/// <summary>
		/// Gets/sets the id.
		/// </summary>
		[Column(Name="posttemplate_id")]
		public override Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets the permalink id.
		/// </summary>
		[Column(Name="posttemplate_permalink_id")]
		public Guid PermalinkId { get ; set ; }

		/// <summary>
		/// Gets/sets the name.
		/// </summary>
		[Column(Name="posttemplate_name")]
		[Display(ResourceType=typeof(Piranha.Resources.Global), Name="Name")]
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Global), ErrorMessageResourceName="NameRequired")]
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the description.
		/// </summary>
		[Column(Name="posttemplate_description")]
		[Display(ResourceType=typeof(Piranha.Resources.Post), Name="Description")]
		public string Description { get ; set ; }

		/// <summary>
		/// Gets/sets the description.
		/// </summary>
		[Column(Name="posttemplate_preview")]
		[Display(ResourceType=typeof(Piranha.Resources.Post), Name="HtmlPreview")]
		public HtmlString Preview { get ; set ; }

		/// <summary>
		/// Gets/sets the associated properties.
		/// </summary>
		[Column(Name="posttemplate_properties", Json=true)]
		public List<string> Properties { get ; set ; }

		/// <summary>
		/// Gets/sets the optional controller for the template.
		/// </summary>
		[Column(Name="posttemplate_controller")]
		[Display(ResourceType=typeof(Piranha.Resources.Post), Name="Template")]
		public string Controller { get ; set ; }

		/// <summary>
		/// Gets/sets wether the controller can be overridden by the implementing post.
		/// </summary>
		[Column(Name="posttemplate_controller_show")]
		public bool ShowController { get ; set ; }

		/// <summary>
		/// Gets/sets the optional view for the template.
		/// </summary>
		[Column(Name="posttemplate_view")]
		[Display(ResourceType=typeof(Piranha.Resources.Post), Name="View")]
		public string View { get ; set ; }

		/// <summary>
		/// Gets/sets whether the view can be overridden by the implementing page.
		/// </summary>
		[Column(Name="posttemplate_view_show")]
		public bool ShowView { get ; set ; }

		/// <summary>
		/// Gets/sets the optional controller for the template.
		/// </summary>
		[Column(Name="posttemplate_archive_controller")]
		[Display(ResourceType=typeof(Piranha.Resources.Post), Name="ArchiveTemplate")]
		public string ArchiveController { get ; set ; }

		/// <summary>
		/// Gets/sets wether the controller can be overridden by the implementing post.
		/// </summary>
		[Column(Name="posttemplate_archive_controller_show")]
		public bool ShowArchiveController { get ; set ; }

		/// <summary>
		/// Gets/sets if posts of this type should be exported as rss.
		/// </summary>
		[Column(Name="posttemplate_rss")]
		[Display(ResourceType=typeof(Piranha.Resources.Template), Name="AllowRss")]
		public bool AllowRss { get ; set ; }

		/// <summary>
		/// Gets/sets the type that created this template if it was create by code.
		/// </summary>
		[Column(Name="posttemplate_type")]
		public string Type { get ; set ; }

		/// <summary>
		/// Gets/sets the created date.
		/// </summary>
		[Column(Name="posttemplate_created")]
		public override DateTime Created { get ; set ; }

		/// <summary>
		/// Gets/sets the updated date.
		/// </summary>
		[Column(Name="posttemplate_updated")]
		public override DateTime Updated { get ; set ; }
		#endregion

		#region Properties
		/// <summary>
		/// Gets if the template is locked from editing in the manager interface.
		/// </summary>
		public bool IsLocked {
			get { 
				if (!String.IsNullOrEmpty(Type))
					return App.Instance.ExtensionManager.PostTypes.Where(pt => pt.GetType().FullName == Type).SingleOrDefault() != null ;
				return false ;
			} 
		}
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PostTemplate() : base() {
			Properties = new List<string>() ;
			LogChanges = true ;
		}
	}
}
