using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

using Piranha.Data;

namespace Piranha.Models
{
	/// <summary>
	/// Base class for all records that uses a guid as their primary key column.
	/// </summary>
	/// <typeparam name="T">The type of the record</typeparam>
	[Serializable]
	public abstract class GuidRecord<T> : ActiveRecord<T>
	{
		#region Fields
		/// <summary>
		/// Gets/sets the id.
		/// </summary>
		public abstract Guid Id { get ; set ; }
		#endregion

		#region Handlers
		/// <summary>
		/// NEVER load passwords from the database ever.
		/// </summary>
		/// <param name="pwd">The password</param>
		/// <returns>An empty string</returns>
		protected string OnPasswordLoad(string pwd) {
			return "" ;
		}

		/// <summary>
		/// Encrypts the password before saving it to the database.
		/// </summary>
		/// <param name="pwd">The password</param>
		/// <returns>The encrypted password</returns>
		protected string OnPasswordSave(string pwd) {
			return Encrypt(pwd) ;
		}
		#endregion

		/// <summary>
		/// Saves the current record to the database.
		/// </summary>
		/// <param name="tx">Optional transaction</param>
		/// <returns>Wether the operation was successful</returns>
		public override bool Save(System.Data.IDbTransaction tx = null) {
			if (IsNew && Id == Guid.Empty)
				Id = Guid.NewGuid() ;
			return base.Save(tx);			
		}

		/// <summary>
		/// Encrypts the given string with MD5.
		/// </summary>
		/// <param name="str">The encrypted string</param>
		/// <returns></returns>
		public static string Encrypt(string str) {
			UTF8Encoding encoder = new UTF8Encoding() ;
			SHA256CryptoServiceProvider crypto = new SHA256CryptoServiceProvider() ;

			byte[] bytes = crypto.ComputeHash(encoder.GetBytes(str)) ;
			return Convert.ToBase64String(bytes) ;
		}
	}
}
