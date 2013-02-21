using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

using Piranha.Data;

namespace Piranha.Models
{
	[PrimaryKey(Column="permalink_id")]
	public class Permalink : PiranhaRecord<Permalink>, ICacheRecord<Permalink>
	{
		#region Inner classes
		/// <summary>
		/// Inner class for mapping permalink names in namespaces to permalink id.
		/// </summary>
		private class NamespaceDictionary
		{
			#region Members
			private Dictionary<Guid, Dictionary<string, Guid>> InnerCache = new Dictionary<Guid,Dictionary<string,Guid>>() ;
			#endregion

			public Dictionary<string, Guid> this[Guid namespaceId] {
				get {
					if (!InnerCache.ContainsKey(namespaceId))
						InnerCache.Add(namespaceId, new Dictionary<string,Guid>()) ;
					return InnerCache[namespaceId] ;
				}
			}
		}
		#endregion

		#region Members
		public static Guid DefaultNamespace = new Guid("8FF4A4B4-9B6C-4176-AAA2-DB031D75AC03") ;
		#endregion

		#region Inner classes
		public enum PermalinkType {
			PAGE, POST, CATEGORY
		}
		#endregion

		#region Fields
		[Column(Name="permalink_id")]
		[Required()]
		public override Guid Id { get ; set ; }

		[Column(Name="permalink_namespace_id")]
		public Guid NamespaceId { get ; set ; }

		[Column(Name="permalink_type")]
		public PermalinkType Type { get ; set ; }

		[Column(Name="permalink_name")]
		public string Name { get ; set ; }

		[Column(Name="permalink_created")]
		public override DateTime Created { get ; set ; }

		[Column(Name="permalink_updated")]
		public override DateTime Updated { get ; set ; }

		[Column(Name="permalink_created_by")]
		public override Guid CreatedBy { get ; set ; }

		[Column(Name="permalink_updated_by")]
		public override Guid UpdatedBy { get ; set ; }
		#endregion

		#region Cache
		private static NamespaceDictionary NamespaceCache = new NamespaceDictionary() ;
		#endregion

		#region Static accessors
		/// <summary>
		/// Gets the permalink with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>The permalink</returns>
		public static Permalink GetSingle(Guid id) {
			if (id != Guid.Empty) {
				if (!Cache.Current.Contains(id.ToString())) {
					var perm = GetSingle((object)id) ;
					if (perm != null)
						AddToCache(perm) ;
					return perm ;
				}
				return (Permalink)Cache.Current[id.ToString()] ;
			}
			return null ;
		}

		/// <summary>
		/// Gets the permalink with the given name from the default namespace.
		/// </summary>
		/// <param name="name">The permalink name</param>
		/// <returns>The permalink</returns>
		public static Permalink GetByName(string name) {
			return GetByName(DefaultNamespace, name) ;
		}

		/// <summary>
		/// Gets the permalink with the given name.
		/// </summary>
		/// <param name="namespaceid">The namespace id</param>
		/// <param name="name">The permalink name</param>
		/// <returns>The permalink</returns>
		public static Permalink GetByName(Guid namespaceid, string name) {
			if (!NamespaceCache[namespaceid].ContainsKey(name)) {
				var perm = GetSingle("permalink_name = @0 AND permalink_namespace_id = @1", name, namespaceid) ;

				if (perm != null) {
					AddToCache(perm) ;
					return perm ;
				}
				return null ;
			}
			return (Permalink)Cache.Current[NamespaceCache[namespaceid][name].ToString()] ;
		}
		#endregion

		/// <summary>
		/// Saves the current entity.
		/// </summary>
		/// <param name="tx">Optional transaction</param>
		/// <returns>Weather the operation succeeded</returns>
		public override bool Save(System.Data.IDbTransaction tx = null) {
			// Check for duplicates 
			if (Permalink.GetSingle("permalink_name = @0 AND permalink_namespace_id = @2" + (!IsNew ? " AND permalink_id != @1" : ""), Name, Id, NamespaceId) != null)
 				throw new DuplicatePermalinkException() ;
			return base.Save(tx);
		}

		/// <summary>
		/// Converts the given string to a web safe permalink.
		/// </summary>
		/// <param name="str">The string</param>
		/// <returns>A permalink</returns>
		public static string Generate(string str) {
			return Regex.Replace(str.ToLower().Replace(" ", "-").Replace("å", "a").Replace("ä", "a").Replace("ö", "o"),
				@"[^a-z0-9-]", "") ;
		}

		/// <summary>
		/// Invalidates the current record from the cache.
		/// </summary>
		/// <param name="record">The record</param>
		public void InvalidateRecord(Permalink record) {
			Cache.Current.Remove(record.Id.ToString()) ;
			if (NamespaceCache[record.NamespaceId].ContainsKey(record.Name))
				NamespaceCache[record.NamespaceId].Remove(record.Name) ;
		}

		/// <summary>
		/// Adds the given permalink to the cache.
		/// </summary>
		/// <param name="perm">The permalink</param>
		private static void AddToCache(Permalink perm) {
			Cache.Current[perm.Id.ToString()] = perm ;
			NamespaceCache[perm.NamespaceId][perm.Name] = perm.Id ;
		}
	}
}
