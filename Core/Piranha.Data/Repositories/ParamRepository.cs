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
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Repositories
{
	/// <summary>
	/// Repository for handling parameters.
	/// </summary>
	public sealed class ParamRepository : Repository<Data.Param>
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="db">The current context</param>
		public ParamRepository(Db db) : base(db) { }

		/// <summary>
		/// Gets the param identified by the given name.
		/// </summary>
		/// <param name="name">The unique name</param>
		/// <returns>The matching model</returns>
		public Data.Param GetByName(string name) {
			return Query().Where(p => p.Name == name).SingleOrDefault();
		}

		/// <summary>
		/// Gets the param identitifed by the given name.
		/// </summary>
		/// <param name="name">The unique name</param>
		/// <returns>The matching model</returns>
		public Task<Data.Param> GetByNameAsync(string name) {
			return Query().Where(p => p.Name == name).SingleOrDefaultAsync();
		}
	}
}
