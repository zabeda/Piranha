using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Piranha.Data;
using Piranha.WebPages;

namespace Piranha.Models
{
	/// <summary>
	/// Record for the post archive.
	/// </summary>
	[Table(Name="post")]
	public class PostArchive : ActiveRecord<PostArchive>
	{
		#region Members
		private static string Select = 
			"SELECT	" +
			"CONVERT(INT, SUBSTRING(CONVERT(NCHAR(10), post_published, 120), 1, 4)) AS [Year]," +
			"CONVERT(INT, SUBSTRING(CONVERT(NCHAR(10), post_published, 120), 6, 2)) AS [Month]," +
			"COUNT(*) AS [Count] " +
			"FROM post " +
			"INNER JOIN relation ON relation_data_id = post_id " +
			"INNER JOIN category ON relation_related_id = category_id " +
			"WHERE post_draft = 0 {0} " +
			"GROUP BY SUBSTRING(CONVERT(NCHAR(10), post_published, 120), 1, 4)," +
			"SUBSTRING(CONVERT(NCHAR(10), post_published, 120), 6, 2)" ;
		#endregion

		#region Fields
		/// <summary>
		/// Gets/sets the year.
		/// </summary>
		[Column(ReadOnly=true)]
		public int Year { get ; set ; }

		/// <summary>
		/// Gets/sets the month.
		/// </summary>
		[Column(ReadOnly=true)]
		public int Month { get ; set ; }

		/// <summary>
		/// Gets/sets the article count.
		/// </summary>
		[Column(ReadOnly=true)]
		public int Count { get ; set ; }
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the date representation of the current archive record.
		/// </summary>
		private DateTime Date { get ; set ; }

		/// <summary>
		/// Gets name of the month.
		/// </summary>
		public string MonthName {
			get { return CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(Date.ToString("MMMM")) ; }
		}
		#endregion

		#region Static accessors
		/// <summary>
		/// Gets the current post archive.
		/// </summary>
		/// <returns>The archive</returns>
		public static List<PostArchive> Get() {
			return Get(Guid.Empty) ;
		}

		/// <summary>
		/// Gets the current post archive for the given category.
		/// </summary>
		/// <param name="categoryid">The category id</param>
		/// <returns>The archive</returns>
		public static List<PostArchive> Get(Guid categoryid) {
			string where = categoryid != Guid.Empty ?
				"AND category_id=@0 " : "" ;
			if (categoryid != Guid.Empty)
				return Query(String.Format(Select, where), categoryid) ;
			List<PostArchive> archive = Query(String.Format(Select, "")) ;
			archive.ForEach(a => a.Date = new DateTime(a.Year, a.Month, 1)) ;

			return archive ;
		}
		#endregion
	}
}
