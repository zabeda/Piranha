using System;
using System.IO;
using System.Web;

namespace Piranha
{
	/// <summary>
	/// Class for logging error messages.
	/// </summary>
	internal class Log
	{
		#region Member
		/// <summary>
		/// The physical path to the log folder
		/// </summary>
		private readonly string path = HttpContext.Current.Server.MapPath("~/App_Data/Logs") ;

		/// <summary>
		/// The physical path to the log file
		/// </summary>
		private readonly string file = HttpContext.Current.Server.MapPath("~/App_Data/Logs/Log.txt") ;

		/// <summary>
		/// Mutex for keeping it thread safe.
		/// </summary>
		private readonly object mutex = new object() ;

		/// <summary>
		/// The current singleton instance
		/// </summary>
		public static readonly Log Current = new Log();
		#endregion

		/// <summary>
		/// Private constructor.
		/// </summary>
		private Log() {
			// Create the log directory if it doesn't exist
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path) ;
		}

		/// <summary>
		/// Logs an error to the log file.
		/// </summary>
		/// <param name="origin">The origin of the error</param>
		/// <param name="message">The message</param>
		/// <param name="details">Optional error details</param>
		public void Error(string origin, string message, Exception details = null) {
			lock (mutex) {
				using (var writer = new StreamWriter(file)) {
					writer.WriteLine(String.Format(
						"ERROR [{0}] Origin [{1}] Message [{2}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), origin, message)) ;
					if (details != null)
						writer.WriteLine(details) ;
				}
			}
		}
	}
}