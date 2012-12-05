using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Piranha.Entities
{
	/// <summary>
	/// Entity for the page.
	/// </summary>
	public class Page : StandardEntity<Page>
	{
		#region Properties
		/// <summary>
		/// Gets/sets weather this page instance is a draft.
		/// </summary>
		internal bool IsDraft { get ; set ; }

		/// <summary>
		/// Gets/sets the id of the page template.
		/// </summary>
		public Guid TemplateId { get ; set ; }

		/// <summary>
		/// Gets/sets the optional group id.
		/// </summary>
		public Guid? GroupId { get ; set ; }

		/// <summary>
		/// Gets/sets the optional parent id.
		/// </summary>
		public Guid? ParentId { get ; set ; }

		/// <summary>
		/// Gets/sets the id of the permalink.
		/// </summary>
		public Guid PermalinkId { get ; set ; }

		/// <summary>
		/// Gets/sets the sequence number.
		/// </summary>
		public int Seqno { get ; set ; }

		/// <summary>
		/// Gets/sets the page title.
		/// </summary>
		public string Title { get ; set ; }

		/// <summary>
		/// Gets/sets the optional navigation title.
		/// </summary>
		public string NavigationTitle { get ; set ; }

		/// <summary>
		/// Gets/sets weather this page should be hidden in the navigation.
		/// </summary>
		public bool IsHidden { get ; set ; }

		/// <summary>
		/// Gets/sets the meta keywords.
		/// </summary>
		public string Keywords { get ; set ; }

		/// <summary>
		/// Gets/sets the meta description.
		/// </summary>
		public string Description { get ; set ; }
		
		/// <summary>
		/// Gets/sets the id's of content attached to this page.
		/// </summary>
		public IList<Guid> Attachments { get ; set ; }

		/// <summary>
		/// Gets/sets the optional template that should render the view.
		/// </summary>
		public string ViewTemplate { get ; set ; }

		/// <summary>
		/// Gets/sets the optional redirect for the page.
		/// </summary>
		public string ViewRedirect { get ; set ; }

		/// <summary>
		/// Gets/sets the date the post was initially published.
		/// </summary>
		public DateTime ?Published { get ; set ; }

		/// <summary>
		/// Gets/sets the date the post was last updated.
		/// </summary>
		public DateTime ?LastPublished { get ; set ; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the page template.
		/// </summary>
		public PageTemplate Template { get ; set ; }

		/// <summary>
		/// Gets/sets the permalink used to access the page.
		/// </summary>
		public Permalink Permalink { get ; set ; }

		/// <summary>
		/// Gets/sets the regions attached to the page.
		/// </summary>
		public IList<Region> Regions { get ; set ; }

		/// <summary>
		/// Gets/sets the properties attached to the page.
		/// </summary>
		public IList<Property> Properties { get ; set ; }

		/// <summary>
		/// Gets/sets the currently available extensions.
		/// </summary>
		public IList<Extension> Extensions { get ; set ; }
		#endregion

		#region Internal properties
		/// <summary>
		/// Gets/sets the internal json data for the attachments.
		/// </summary>
		internal string AttachmentsJson { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Page() {
			Attachments = new List<Guid>() ;
			Regions = new List<Region>() ;
			Properties = new List<Property>() ;
			Extensions = new List<Extension>() ;
		}

		/// <summary>
		/// Gets the attachments for the current page.
		/// </summary>
		/// <returns>The attachments</returns>
		public IList<Media> GetAttachments() {
			using (var db = new DataContext()) {
				return db.Media.Where(m => Attachments.Contains(m.Id)).ToList() ;
			}
		}

		#region Events
		/// <summary>
		/// Called when the entity has been loaded.
		/// </summary>
		public override void OnLoad() {
			var js = new JavaScriptSerializer() ;

			Attachments = !String.IsNullOrEmpty(AttachmentsJson) ? js.Deserialize<List<Guid>>(AttachmentsJson) : Attachments ;

			base.OnLoad() ;
		}

		/// <summary>
		/// Called when the entity is about to be saved.
		/// </summary>
		/// <param name="state">The current entity state</param>
		public override void OnSave(System.Data.EntityState state) {
			var js = new JavaScriptSerializer() ;

			AttachmentsJson = js.Serialize(Attachments) ;

			base.OnSave(state);
		}
		#endregion
	}
}
