using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

using AutoMapper;
using Piranha.Entities;
using Piranha.Manager.Models;

namespace Piranha.Manager
{
	/// <summary>
	/// The mapper class handles all of the AutoMapper configurations.
	/// </summary>
	public static class Mappings
	{
		/// <summary>
		/// Initializes and validates the AutoMapper mappings.
		/// </summary>
		public static void Init() {
			#region SelectList mappings
			Mapper.CreateMap<Entities.Group, SelectListItem>()
				.ForMember(i => i.Value, o => o.MapFrom(g => g.Id.ToString()))
				.ForMember(i => i.Text, o => o.MapFrom(g => g.Name))
				.ForMember(i => i.Selected, o => o.Ignore()) ;
			Mapper.CreateMap<Entities.Namespace, SelectListItem>()
				.ForMember(i => i.Value, o => o.MapFrom(n => n.Id.ToString()))
				.ForMember(i => i.Text, o => o.MapFrom(n => n.Name))
				.ForMember(i => i.Selected, o => o.Ignore());
			#endregion

			#region Entity mappings
			Mapper.CreateMap<Entities.User, Entities.User>()
				.ForMember(u => u.Id, o => o.Ignore())
				.ForMember(u => u.Created, o => o.Ignore())
				.ForMember(u => u.CreatedById, o => o.Ignore())
				.ForMember(u => u.Updated, o => o.Ignore())
				.ForMember(u => u.UpdatedById, o => o.Ignore())
				.ForMember(u => u.Password, o => o.Ignore()) ;
			Mapper.CreateMap<Entities.Permission, Entities.Permission>()
				.ForMember(p => p.Id, o => o.Ignore())
				.ForMember(p => p.IsLocked, o => o.Ignore())
				.ForMember(p => p.Created, o => o.Ignore())
				.ForMember(p => p.CreatedById, o => o.Ignore())
				.ForMember(p => p.Updated, o => o.Ignore())
				.ForMember(p => p.UpdatedById, o => o.Ignore()) ;
			Mapper.CreateMap<Entities.Param, Entities.Param>()
				.ForMember(p => p.Id, o => o.Ignore())
				.ForMember(p => p.IsLocked, o => o.Ignore())
				.ForMember(p => p.Created, o => o.Ignore())
				.ForMember(p => p.CreatedById, o => o.Ignore())
				.ForMember(p => p.Updated, o => o.Ignore())
				.ForMember(p => p.UpdatedBy, o => o.Ignore()) ;
			Mapper.CreateMap<Entities.Category, Entities.Category>()
				.ForMember(c => c.Id, o => o.Ignore())
				.ForMember(c => c.Created, o => o.Ignore())
				.ForMember(c => c.CreatedById, o => o.Ignore())
				.ForMember(c => c.Updated, o => o.Ignore())
				.ForMember(c => c.UpdatedById, o => o.Ignore()) ;
			Mapper.CreateMap<Entities.Permalink, Entities.Permalink>()
				.ForMember(p => p.Id, o => o.Ignore())
				.ForMember(p => p.Type, o => o.Ignore())
				.ForMember(p => p.Created, o => o.Ignore())
				.ForMember(p => p.CreatedById, o => o.Ignore())
				.ForMember(p => p.Updated, o => o.Ignore())
				.ForMember(p => p.UpdatedById, o => o.Ignore());
			Mapper.CreateMap<Entities.Comment, Entities.Comment>()
				.ForMember(c => c.Id, o => o.Ignore())
				.ForMember(c => c.ParentId, o => o.Ignore())
				.ForMember(c => c.Created, o => o.Ignore())
				.ForMember(c => c.CreatedById, o => o.Ignore())
				.ForMember(c => c.Updated, o => o.Ignore())
				.ForMember(c => c.UpdatedById, o => o.Ignore()) ;
			#endregion

			#region Model mappings
			Mapper.CreateMap<Entities.Comment, Manager.Models.CommentListModel.CommentModel>()
				.ForMember(c => c.Title, o => o.ResolveUsing(c => !String.IsNullOrEmpty(c.Title) ? c.Title : c.Body.Substring(0, 32) + "..."))
				.ForMember(c => c.AuthorName, o => o.ResolveUsing(c => c.CreatedBy != null ? c.CreatedBy.Firstname + " " + c.CreatedBy.Surname : c.AuthorName))
				.ForMember(c => c.AuthorEmail, o => o.ResolveUsing(c => c.CreatedBy != null ? c.CreatedBy.Email : c.AuthorEmail))
				.ForMember(c => c.Status, o => o.ResolveUsing(c => (Entities.Comment.CommentStatus)c.InternalStatus)) ;
			Mapper.CreateMap<Entities.Post, Manager.Models.PostListModel.PostModel>()
				.ForMember(p => p.NavigationTitle, o => o.Ignore())
				.ForMember(p => p.TemplateName, o => o.ResolveUsing(p => p.Template.Name))
				.ForMember(p => p.Status, o => o.ResolveUsing(p => 
					(!p.Published.HasValue ? PostListModel.PostStatus.UNPUBLISHED : 
					(p.Updated > p.LastPublished.Value ? PostListModel.PostStatus.DRAFT : PostListModel.PostStatus.PUBLISHED))))
				.ForMember(p => p.NewComments, o => o.ResolveUsing(p => p.Comments.Count));
			#endregion

			// Validate mappings
			Mapper.AssertConfigurationIsValid() ;
		}
	}
}