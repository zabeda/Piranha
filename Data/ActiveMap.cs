using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Piranha.Data
{
	public abstract class ActiveMap<T>
	{
		#region Members
		/// <summary>
		/// Gets/sets the entity type for the map.
		/// </summary>
		internal Type Type = typeof(T) ;

		/// <summary>
		/// Gets/set the table name.
		/// </summary>
		internal string TableName = typeof(T).Name ;

		/// <summary>
		/// Gets/sets the primary keys
		/// </summary>
		internal List<string> PrimaryKeys = new List<string>() { "Id" } ;

		/// <summary>
		/// Gets/sets the columns.
		/// </summary>
		internal Dictionary<string, PropertyInfo> Columns = new Dictionary<string,PropertyInfo>() ;
		#endregion

		/// <summary>
		/// Sets the table name for the mapping.
		/// </summary>
		/// <param name="tablename">The table name</param>
		protected void HasTable(string tablename) {
			TableName = tablename ;
		}

		/// <summary>
		/// Sets the primary keys for the mapping.
		/// </summary>
		/// <typeparam name="TKey">The key type</typeparam>
		/// <param name="expr">The key expression</param>
		/// <param name="name">Optional column name</param>
		protected void HasKey<TKey>(Expression<Func<T, TKey>> expr, string name = "") {
			if (expr.Body.NodeType == ExpressionType.MemberAccess) {
				var p = ((MemberExpression)expr.Body).Member ;

				PrimaryKeys.Clear() ;
				PrimaryKeys.Add(name != "" ? name : p.Name) ;
				Columns.Add(name != "" ? name : p.Name, Type.GetProperty(((MemberExpression)expr.Body).Member.Name)) ;
			}
		}

		/// <summary>
		/// Adds a column for the mapping.
		/// </summary>
		/// <typeparam name="TKey">The column type</typeparam>
		/// <param name="expr">The column expression</param>
		/// <param name="name">Optional column name</param>
		protected void HasColumn<TKey>(Expression<Func<T, TKey>> expr, string name = "") {
			if (expr.Body.NodeType == ExpressionType.MemberAccess) {
				var p = ((MemberExpression)expr.Body).Member ;

				Columns.Add(name != "" ? name : p.Name, Type.GetProperty(((MemberExpression)expr.Body).Member.Name)) ;
			}
		}
	}

	public class PageMap : ActiveMap<Models.Page> {
		public PageMap() {
			HasKey(p => p.Id) ;

			HasColumn(p => p.Title) ;
		}
	}
}
