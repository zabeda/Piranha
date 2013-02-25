using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// Interface for an edit model.
	/// </summary>
	public interface IEditModel
	{
		/// <summary>
		/// Loads the model with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		void LoadById(Guid id) ;

		/// <summary>
		/// Saves the current model to the database.
		/// </summary>
		/// <returns>Weather the database was updated.</returns>
		bool Save() ;

		/// <summary>
		/// Deletes the current model to the database.
		/// </summary>
		/// <returns>Weather the database was updated.</returns>
		bool Delete() ;
	}
}
