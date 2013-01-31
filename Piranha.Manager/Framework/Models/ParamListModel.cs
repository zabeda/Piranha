using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Entities;
using Piranha.Web;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// View model for the param list.
	/// </summary>
	public class ParamListModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available params.
		/// </summary>
		[ModelProperty(OnLoad="LoadParams")]
		public IList<Entities.Param> Params { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ParamListModel() {
			Params = new List<Entities.Param>() ;
		}

		/// <summary>
		/// Loads the param list.
		/// </summary>
		public void LoadParams() {
			using (var db = new DataContext()) {
				Params = db.Params.OrderBy(p => p.Name).ToList() ;
			}
		}
	}
}