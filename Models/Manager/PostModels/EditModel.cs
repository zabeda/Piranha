using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using Piranha.Data;

namespace Piranha.Models.Manager.PostModels
{
	/// <summary>
	/// Edit model for a post in the manager area.
	/// </summary>
	public class EditModel
	{
		#region Binder
		public class Binder : DefaultModelBinder
		{
			/// <summary>
			/// Extend the default binder so that html strings can be fetched from the post.
			/// </summary>
			/// <param name="controllerContext">Controller context</param>
			/// <param name="bindingContext">Binding context</param>
			/// <returns>The page edit model</returns>
			public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
				EditModel model = (EditModel)base.BindModel(controllerContext, bindingContext) ;

				bindingContext.ModelState.Remove("Post.Body") ;

				model.Post.Body = 
					new HtmlString(bindingContext.ValueProvider.GetUnvalidatedValue("Post.Body").AttemptedValue) ;

				return model ;
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the post.
		/// </summary>
		public Post Post { get ; set ; }

		/// <summary>
		/// Gets/sets the permalink.
		/// </summary>
		public Permalink Permalink { get ; set ; }

		/// <summary>
		/// Gets/sets the Properties.
		/// </summary>
		public virtual List<Property> Properties { get ; set ; }

		/// <summary>
		/// Gets/sets the post template.
		/// </summary>
		public PostTemplate Template { get ; set ; }

		/// <summary>
		/// Gets/sets the categories associated with the post.
		/// </summary>
		public List<Guid> PostCategories { get ; set ; }
		//public List<Category> PostCategories { get ; set ; }

		/// <summary>
		/// Gets/sets the available categories.
		/// </summary>
		public MultiSelectList Categories { get ; set ; }

		/// <summary>
		/// Gets/sets the attached content.
		/// </summary>
		[ScriptIgnore()]
		public virtual List<Content> AttachedContent { get ; set ; }

		/// <summary>
		/// Gets/sets the available content.
		/// </summary>
		[ScriptIgnore()]
		public List<Content> Content { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public EditModel() {
			Post = new Post() ;
			PostCategories = new List<Guid>() ;
			Properties = new List<Property>() ;
			AttachedContent = new List<Piranha.Models.Content>() ;
			Content = Piranha.Models.Content.Get() ;
		}

		/// <summary>
		/// Creates a new post from the given template and return it 
		/// as a edit model.
		/// </summary>
		/// <param name="templateId">The template id</param>
		/// <returns>The edit model</returns>
		public static EditModel CreateByTemplate(Guid templateId) {
			EditModel m = new EditModel() ;

			m.Post = new Piranha.Models.Post() {
				Id = Guid.NewGuid(),
				TemplateId = templateId 
			} ;
			m.Template = PostTemplate.GetSingle(templateId) ;
			m.Permalink = new Permalink() { 
				ParentId = m.Post.Id, Type = Permalink.PermalinkType.POST } ;
			m.Categories = new MultiSelectList(Category.GetFields("category_id, category_name", 
				new Params() { OrderBy = "category_name" }), "Id", "Name") ;
			m.GetRelated() ;

			return m ;
		}

		/// <summary>
		/// Gets the model for the post
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static EditModel GetById(Guid id) {
			EditModel m = new EditModel() ;
			m.Post = Piranha.Models.Post.GetSingle(id, true) ;
			m.Template = PostTemplate.GetSingle(m.Post.TemplateId) ;
			m.Permalink = Permalink.GetSingle("permalink_parent_id = @0", m.Post.Id) ;
			if (m.Permalink == null)
				m.Permalink = new Permalink() { 
					ParentId = m.Post.Id, Type = Permalink.PermalinkType.POST } ;
			Category.GetByPostId(m.Post.Id).ForEach(c => m.PostCategories.Add(c.Id)) ;
			m.Categories = new MultiSelectList(Category.GetFields("category_id, category_name", 
				new Params() { OrderBy = "category_name" }), "Id", "Name", m.PostCategories) ;
			m.GetRelated() ;

			return m ;
		}

		/// <summary>
		/// Saves the model.
		/// </summary>
		/// <returns>Weather the action was successful</returns>
		public bool SaveAll(bool draft = true) {
			using (IDbTransaction tx = Database.OpenConnection().BeginTransaction()) {
				try {
					if (draft)
						Post.Save(tx) ;
					else Post.SaveAndPublish(tx) ;

					// Properties
					Properties.ForEach(p => { 
						p.IsDraft = true ; 
						p.Save(tx) ;
						if (!draft) {
							if (Property.GetScalar("SELECT COUNT(property_id) FROM property WHERE property_id=@0 AND property_draft=0", p.Id) == 0)
								p.IsNew = true ;
							p.IsDraft = false ; 
							p.Save(tx) ;
						}
					}) ;

					// Permalink
					if (Permalink.IsNew)
						Permalink.Name = Permalink.Generate(Post.Title) ;
					Permalink.Save(tx) ;

					// Update categories
					Relation.DeleteByDataId(Post.Id) ;
					List<Relation> relations = new List<Relation>() ;
					PostCategories.ForEach(pc => relations.Add(new Relation() { 
						DataId = Post.Id, RelatedId = pc, Type = Relation.RelationType.POSTCATEGORY })
						) ;
					relations.ForEach(r => r.Save(tx)) ;
					tx.Commit() ;
				} catch { tx.Rollback() ; throw ; }
			}
			return true ;
		}

		/// <summary>
		/// Deletes the post.
		/// </summary>
		/// <returns></returns>
		public virtual bool DeleteAll() {
			using (IDbTransaction tx = Database.OpenConnection().BeginTransaction()) {
				try {
					Permalink.Delete(tx) ;
					Post.Delete(tx) ;
					tx.Commit() ;
				} catch { tx.Rollback() ; return false ; }
			}
			return true ;
		}

		/// <summary>
		/// Refreshes the current model.
		/// </summary>
		public void Refresh() {
			if (Post != null) {
				if (!Post.IsNew) {
					Post = Piranha.Models.Post.GetSingle(Post.Id, true) ;
					Permalink = Permalink.GetSingle("permalink_parent_id = @0", Post.Id) ;
					Category.GetByPostId(Post.Id).ForEach(c => PostCategories.Add(c.Id)) ;
					Categories = new MultiSelectList(Category.GetFields("category_id, category_name", 
						new Params() { OrderBy = "category_name" }), "Id", "Name", PostCategories) ;
				}
				Template = PostTemplate.GetSingle(Post.TemplateId) ;
				GetRelated() ;
			}
		}

		#region Private methods
		private void GetRelated() {
			// Clear related
			Properties.Clear() ;

			if (Template != null) {
				// Get Properties
				foreach (string name in Template.Properties) {
					Property prp = Property.GetSingle("property_name = @0 AND property_parent_id = @1 AND property_draft = @2", 
						name, Post.Id, Post.IsDraft) ;
					if (prp != null)
						Properties.Add(prp) ;
					else Properties.Add(new Property() { Name = name, ParentId = Post.Id, IsDraft = Post.IsDraft }) ;
				}
			}

			// Get attached content
			if (Post.Attachments.Count > 0) {
				// Content meta data is actually memcached, so this won't result in multiple queries
				Post.Attachments.ForEach(a => {
					Models.Content c = Models.Content.GetSingle(a) ;
					if (c != null)
						AttachedContent.Add(c) ;
				}) ;
			}
		}
		#endregion
	}
}
