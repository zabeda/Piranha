using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

using AutoMapper;
using Piranha.Entities;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// Edit model for the param view.
	/// </summary>
	public sealed class ParamEditModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the current param.
		/// </summary>
		public Entities.Param Param { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ParamEditModel() {
			Param = new Entities.Param() ;
		}

		/// <summary>
		/// Gets the edit model for the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>The model</returns>
		public static ParamEditModel GetById(Guid id) {
			var m = new ParamEditModel() ;

			using (var db = new DataContext()) {
				m.Param = db.Params.Where(p => p.Id == id).Single() ;
			}
			return m ;
		}

		/// <summary>
		/// Saves the current edit model.
		/// </summary>
		/// <returns>Weather the database was updated</returns>
		public bool Save() {
			using (var db = new DataContext()) {
				var param = db.Params.Where(p => p.Id == Param.Id).SingleOrDefault() ;
				if (param == null) {
					param = new Entities.Param() ;
					param.Attach(db, EntityState.Added) ;
				}
				Mapper.Map<Entities.Param, Entities.Param>(Param, param) ;

				var ret = db.SaveChanges() > 0 ;
				Param.Id = param.Id ;

				return ret ;
			}
		}

		/// <summary>
		/// Deletes the current edit model.
		/// </summary>
		/// <returns>Weather the database was updated</returns>
		public bool Delete() {
			using (var db = new DataContext()) {
				Param.Attach(db, EntityState.Deleted) ;
				return db.SaveChanges() > 0 ;
			}
		}

		/// <summary>
		/// Validates the current model and stores the result in the model state.
		/// </summary>
		/// <param name="state">The model state</param>
		public void Validate(ModelStateDictionary state) {
			// Name
			if (String.IsNullOrEmpty(Param.Name))
				state.AddError("m.Param.Name", Piranha.Resources.Settings.ParamNameRequired) ;
			else if (Param.Name.Length > 64)
				state.AddError("m.Param.Name", Piranha.Resources.Settings.ParamNameLength) ;
			// Value
			if (Param.Value.Length > 128)
				state.AddError("m.Param.Value", Piranha.Resources.Settings.ParamValueLength) ;
			// Description
			if (Param.Description.Length > 255)
				state.AddError("m.Param.Description", Piranha.Resources.Settings.ParamDescriptionLength) ;
		}
	}
}