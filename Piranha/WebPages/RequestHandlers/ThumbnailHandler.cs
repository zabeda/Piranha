using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

using Piranha.Models;

namespace Piranha.WebPages.RequestHandlers
{
	public class ThumbnailHandler : IRequestHandler
	{
		/// <summary>
		/// Handles the current request.
		/// </summary>
		/// <param name="context">The current context</param>
		/// <param name="args">Optional url arguments passed to the handler</param>
		public virtual void HandleRequest(HttpContext context, params string[] args) {
			HandleRequest(context, false, args) ;
		}

			/// <summary>
		/// Handles the current request.
		/// </summary>
		/// <param name="context">The current context</param>
		/// <param name="draft">Whether to get the draft thumbnail or not</param>
		/// <param name="args">Optional url arguments passed to the handler</param>
		protected void HandleRequest(HttpContext context, bool draft, params string[] args) {
			if (args != null && args.Length > 1) {
				if (!GetThumbnail(context, draft, args, new Guid(args[0]))) {
					var page = Page.GetSingle(new Guid(args[0])) ;
					if (page != null && page.Attachments.Count > 0) {
						GetThumbnail(context, draft, args, page.Attachments[0]) ;
					} else {
						var post = Post.GetSingle(new Guid(args[0])) ;
						if (post != null && post.Attachments.Count > 0)
							GetThumbnail(context, draft, args, post.Attachments[0]) ;
					}
				}
			}
		}

		/// <summary>
		/// Gets the thumbnail for the content with the given id.
		/// </summary>
		/// <param name="context">The current http context.</param>
		/// <param name="args">The args</param>
		/// <param name="id">The content id</param>
		/// <returns>Whether a content record was found with the given id</returns>
		private bool GetThumbnail(HttpContext context, bool draft, string[] args, Guid id) {
			Content content = Content.GetSingle(id, draft) ;

			if (content != null) {
				if (content.IsDraft && !File.Exists(content.PhysicalPath))
					content.IsDraft = false ;
				if (args.Length == 1)
					content.GetThumbnail(context, 60, draft) ;
				else content.GetThumbnail(context, Convert.ToInt32(args[1]), draft) ;
				
				return true ;
			} else {
				if (args.Length == 1)
					Content.GetResourceThumbnail(context, id) ;
				else Content.GetResourceThumbnail(context, id, Convert.ToInt32(args[1])) ;
			}
			return false ;
		}
	}
}
