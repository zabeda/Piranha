/*
 * Copyright (c) 2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Repositories
{
	/// <summary>
	/// Base repository for models with a unique id.
	/// </summary>
	/// <typeparam name="T">The model type</typeparam>
	public abstract class Repository<T> where T : class, Data.IModel
	{
		#region Members
		/// <summary>
		/// The current db context.
		/// </summary>
		protected readonly Db db;
		#endregion

		/// <summary>
		/// Default protected constructor.
		/// </summary>
		/// <param name="db">The current context</param>
		protected Repository(Db db) {
			this.db = db;
		}

		/// <summary>
		/// Gets the model identified by the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The matching model</returns>
		public virtual T GetById(Guid id) {
			return Query().Where(m => m.Id == id).SingleOrDefault();
		}

		/// <summary>
		/// Gets the model identified by the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The matching model</returns>
		public virtual Task<T> GetByIdAsync(Guid id) {
			return Query().Where(m => m.Id == id).SingleOrDefaultAsync();
		}

		/// <summary>
		/// Gets all of the available models.
		/// </summary>
		/// <returns>The models</returns>
		public virtual List<T> GetAll() {
			return Query().ToList();
		}

		/// <summary>
		/// Gets all of the available models.
		/// </summary>
		/// <returns>The models</returns>
		public virtual Task<List<T>> GetAllAsync() {
			return Query().ToListAsync();
		}

		/// <summary>
		/// Adds a new model to the repository.
		/// </summary>
		/// <param name="model">The model</param>
		public virtual void Add(T model) {
			db.Set<T>().Add(model);
		}

		/// <summary>
		/// Removes a model from the repository.
		/// </summary>
		/// <param name="model">The model</param>
		public virtual void Remove(T model) {
			db.Set<T>().Remove(model);
		}

		/// <summary>
		/// Gets the base query for the model.
		/// </summary>
		/// <returns>The base query</returns>
		protected virtual IQueryable<T> Query() {
			return db.Set<T>();
		}
	}
}
