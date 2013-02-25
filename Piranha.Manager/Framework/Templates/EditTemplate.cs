using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.WebPages;

namespace Piranha.Manager.Templates
{
	/// <summary>
	/// Base class for edit page templates.
	/// </summary>
	/// <typeparam name="T">The edit model</typeparam>
	public abstract class EditTemplate<T> : ContentPage<T> where T : Models.IEditModel
	{
		#region Abstract members
		// Status messages
		protected abstract string MessageSaved { get ; }
		protected abstract string MessageNotSaved { get ; }
		protected abstract string MessageSaveError { get ; }
		protected abstract string MessageDeleted { get ; }
		protected abstract string MessageNotDeleted { get ; }
		protected abstract string MessageDeleteError { get ; }

		// Page titles
		protected abstract string TitleNew { get ; }
		protected abstract string TitleExisting { get ; }

		// List URL
		protected abstract string ListUrl { get ; }
		#endregion

		/// <summary>
		/// Saves the given model.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		public virtual void Save(T m) {
			Model = m ;
			Page.Title = TitleExisting ;

			try {
				if (ModelState.IsValid) {
					if (m.Save())
						this.SuccessMessage(MessageSaved) ;
					else this.InformationMessage(MessageNotSaved) ;
				}
			} catch {
				this.ErrorMessage(MessageSaveError) ;
			}
		}

		/// <summary>
		/// Deletes the model with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="fromList">Weather the call came from the list</param>
		public void Delete(string id, bool fromList = false) {
			Model = Activator.CreateInstance<T>() ;
			Model.LoadById(new Guid(id)) ;
			Page.Title = TitleExisting ;
			
			try {
				if (Model.Delete()) {
					Response.Redirect(ListUrl + "?msg=deleted") ;
				} else { 
					if (fromList)
						Response.Redirect(ListUrl + "?msg=notdeleted") ;
					else this.ErrorMessage(MessageNotDeleted) ;
				}
			} catch {
				if (fromList)
					Response.Redirect(ListUrl + "?msg=deleteerror") ;
				else this.ErrorMessage(MessageDeleteError) ;
			}
		}
	}
}