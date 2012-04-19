using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

using Piranha.Data;

namespace Piranha.Models
{
	/// <summary>
	/// Active record for a page.
	/// </summary>
	[PrimaryKey(Column="post_id,post_draft")]
	[Join(TableName="posttemplate", ForeignKey="post_template_id", PrimaryKey="posttemplate_id")]
	[Join(TableName="permalink", ForeignKey="post_id", PrimaryKey="permalink_parent_id")]
	public class Post : DraftRecord<Post>, IPost
	{
		#region Fields
		/// <summary>
		/// Gets/sets the id.
		/// </summary>
		[Column(Name="post_id")]
		public override Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets weather this is a draft.
		/// </summary>
		[Column(Name="post_draft")]
		public override bool IsDraft { get ; set ; }

		/// <summary>
		/// Gets/sets the template id.
		/// </summary>
		[Column(Name="post_template_id")]
		public Guid TemplateId { get ; set ; }

		/// <summary>
		/// Gets/sets the title.
		/// </summary>
		[Column(Name="post_title")]
		[Display(ResourceType=typeof(Piranha.Resources.Post), Name="Title")]
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Post), ErrorMessageResourceName="TitleRequired")]
		[StringLength(128, ErrorMessageResourceType=typeof(Piranha.Resources.Post), ErrorMessageResourceName="TitleLength")]
		public string Title { get ; set ; }

		/// <summary>
		/// Gets/sets the permalink.
		/// </summary>
		[Column(Name="permalink_name", ReadOnly=true, Table="permalink")]
		[Display(ResourceType=typeof(Piranha.Resources.Post), Name="Permalink")]
		public string Permalink { get ; set ; }

		/// <summary>
		/// Gets/sets the excerpt.
		/// </summary>
		[Column(Name="post_excerpt")]
		[Display(ResourceType=typeof(Piranha.Resources.Post), Name="Excerpt")]
		[StringLength(255, ErrorMessageResourceType=typeof(Piranha.Resources.Post), ErrorMessageResourceName="ExcerptLength")]
		public string Excerpt { get ; set ; }

		/// <summary>
		/// Gets/sets the body.
		/// </summary>
		[Column(Name="post_body")]
		public HtmlString Body { get ; set ; }

		/// <summary>
		/// Gets/sets the template name.
		/// </summary>
		[Column(Name="posttemplate_name", Table="posttemplate", Json=true)]
		public ComplexName TemplateName { get ; private set ; }

		/// <summary>
		/// Gets/sets the attachments.
		/// </summary>
		[Column(Name="post_attachments", Json = true)]
		public List<Guid> Attachments { get ; set ; }

		/// <summary>
		/// Gets/sets the created date.
		/// </summary>
		[Column(Name="post_created")]
		public override DateTime Created { get ; set ; }

		/// <summary>
		/// Gets/sets the updated date.
		/// </summary>
		[Column(Name="post_updated")]
		public override DateTime Updated { get ; set ; }

		/// <summary>
		/// Gets/sets the published date.
		/// </summary>
		[Column(Name="post_published")]
		public override DateTime Published { get ; set ; }

		/// <summary>
		/// Gets/sets the published date.
		/// </summary>
		[Column(Name="post_last_published")]
		public override DateTime LastPublished { get ; set ; }

		/// <summary>
		/// Gets/sets the user id that created the record.
		/// </summary>
		[Column(Name="post_created_by")]
		public override Guid CreatedBy { get ; set ; }

		/// <summary>
		/// Gets/sets the user id that created the record.
		/// </summary>
		[Column(Name="post_updated_by")]
		public override Guid UpdatedBy { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new post.
		/// </summary>
		public Post() {
			IsDraft = true ;
			Attachments = new List<Guid>() ;
		}

		#region Static accessors
		/// <summary>
		/// Gets a single post from the database.
		/// </summary>
		/// <param name="id">The post id</param>
		/// <param name="draft">Weather to get the draft</param>
		/// <returns>The post</returns>
		public static Post GetSingle(Guid id, bool draft) {
			if (!draft)
				return GetSingle(id) ;
			return GetSingle("post_id = @0 AND post_draft = @1", id, draft) ;
		}

		/// <summary>
		/// Gets the post specified by the given permalink.
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <returns>The post</returns>
		public static Post GetByPermalink(string permalink, bool draft = false) {
			return Post.GetSingle("permalink_name = @0 AND post_draft = @1", permalink, draft) ;
		}

		/// <summary>
		/// Gets the posts for the given category id.
		/// </summary>
		/// <param name="id">The category id</param>
		/// <returns>A list of posts</returns>
		public static List<Post> GetByCategoryId(Guid id) {
			return GetFieldsByCategoryId("*", id) ;
		}

		/// <summary>
		/// Gets the posts for the given category id.
		/// </summary>
		/// <param name="fields">The fields</param>
		/// <param name="id">The category id</param>
		/// <returns>A list of posts</returns>
		public static List<Post> GetFieldsByCategoryId(string fields, Guid id) {
			return Post.GetFields(fields, "post_draft = 0 AND post_id IN (" +
				"SELECT relation_data_id FROM relation WHERE relation_type = @0 AND relation_related_id = @1)",
				Relation.RelationType.POSTCATEGORY, id, new Params() { OrderBy = "post_published DESC" }) ;
		}

		/// <summary>
		/// Gets the posts for the given category name.
		/// </summary>
		/// <param name="name">The category name</param>
		/// <returns>A list of posts</returns>
		public static List<Post> GetByCategoryName(string name) {
			return Post.Get("post_draft = 0 AND post_id IN (" +
				"SELECT relation_data_id FROM relation WHERE relation_type = @0 AND relation_related_id = (" +
				"SELECT category_id FROM category WHERE category_name = @1))", Relation.RelationType.POSTCATEGORY, name) ;
		}

		public static List<Post> GetByTemplateId(Guid id) {
			return Post.Get("post_draft = 0 AND post_template_id = @0", id) ;
		}
		#endregion

		#region Handlers
		/// <summary>
		/// Create an empty attachment list if it is null in the database.
		/// </summary>
		/// <param name="lst">The attachments</param>
		/// <returns>The attachments, or a default list</returns>
		protected List<Guid> OnAttachmentsLoad(List<Guid> lst) {
			if (lst != null)
				return lst ;
			return new List<Guid>() ;
		}
		#endregion
	}
}
