using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

using Piranha.Models;

namespace Piranha.WebPages.RequestHandlers
{
	/// <summary>
	/// Request handler for content.
	/// </summary>
	public class ContentHandler : IRequestHandler
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
		/// <param name="draft">Whether to get the draft or not</param>
		/// <param name="args">Optional url arguments passed to the handler</param>
		protected void HandleRequest(HttpContext context, bool draft, params string[] args) {
			if (args != null && args.Length > 0) {
				Content content = Content.GetSingle(new Guid(args[0]), draft) ;

				// Since we don't handle viewing image drafts right now, don't execute the overhead
				//if (content.IsDraft && !Extend.ExtensionManager.Current.MediaProvider.ExistsDraft(content.Id))
				//	content.IsDraft = false ;

				if (content != null) {
					int? width = null ;
					int? height = null ;

					if (args.Length > 1)
						try {
							width = Convert.ToInt32(args[1]) ;
						} catch {}
					if (args.Length > 2)
						try {
							height = Convert.ToInt32(args[2]) ;
						} catch {}

					if (height.HasValue)
						content.GetMedia(context, width, height) ;
					content.GetMedia(context, width) ;
				} else context.Response.StatusCode = 404 ;
			} else context.Response.StatusCode = 500 ;
		}
	}
}
