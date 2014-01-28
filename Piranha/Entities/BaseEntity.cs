using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace Piranha.Entities
{
	/// <summary>
	/// Entity base class exposing some events.
	/// </summary>
	[Serializable]
	public class BaseEntity
	{
		/// <summary>
		/// Attaches the entity to the given context with the specified state.
		/// </summary>
		/// <param name="db">The db context</param>
		/// <param name="state">The entity state</param>
		public void Attach(DataContext db, EntityState state) {
			db.Entry(this).State = state ;
		}

		/// <summary>
		/// Called when the entity has been loaded.
		/// </summary>
		/// <param name="db">The db context</param>
		public virtual void OnLoad(DataContext db) {}

		/// <summary>
		/// Called when the entity is about to get saved.
		/// </summary>
		/// <param name="db">The db context</param>
		/// <param name="state">The current entity state</param>
		public virtual void OnSave(DataContext db, EntityState state) {}

		/// <summary>
		/// Called when the entity is about to get deleted.
		/// </summary>
		/// <param name="db">The db context</param>
		public virtual void OnDelete(DataContext db) {}

		/// <summary>
		/// Validates an entity
		/// </summary>
		/// <param name="entity">The entity to validate</param>
		/// <returns>The validation result, null if no errors occurred</returns>
		public virtual DbEntityValidationResult OnValidate(DbEntityEntry entity) { 
			return null ;
		}
	}
}
