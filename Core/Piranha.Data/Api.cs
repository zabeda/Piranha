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
using System.Threading.Tasks;

namespace Piranha
{
	/// <summary>
	/// The main application api.
	/// </summary>
	public sealed class Api : IDisposable
	{
		#region Members
		/// <summary>
		/// The private db context.
		/// </summary>
		private readonly Db db;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the param repository.
		/// </summary>
		public Repositories.ParamRepository Params { get; private set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Api() {
			db = new Db();

			Params = new Repositories.ParamRepository(db);
		}

		/// <summary>
		/// Saves the changes made to the api.
		/// </summary>
		/// <returns>The number of saved changes</returns>
		public int SaveChanges() {
			return db.SaveChanges();
		}

		/// <summary>
		/// Saves the changes made to the api.
		/// </summary>
		/// <returns>The number of saved changes</returns>
		public Task<int> SaveChangesAsync() {
			return db.SaveChangesAsync();
		}

		/// <summary>
		/// Disposes the current api.
		/// </summary>
		public void Dispose() {
			db.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
