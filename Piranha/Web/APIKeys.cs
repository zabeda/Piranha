using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Piranha.Web
{
	/// <summary>
	/// Class for handling user API-keys.
	/// </summary>
	public static class APIKeys
	{
		#region Members
		/// <summary>
		/// The internal cache object.
		/// </summary>
		internal static Dictionary<Guid, Guid?> Cache {
			get {
				if (HttpContext.Current != null) {
					if (HttpContext.Current.Cache[typeof(APIKeys).Name] == null)
						HttpContext.Current.Cache[typeof(APIKeys).Name] = new Dictionary<Guid, Guid?>() ;
					return (Dictionary<Guid, Guid?>)HttpContext.Current.Cache[typeof(APIKeys).Name] ;
				}
				return new Dictionary<Guid, Guid?>() ;
			}
		}
		#endregion

		/// <summary>
		/// Gets the user key pair for the given API-key
		/// </summary>
		/// <param name="key">The encrypted API-key</param>
		/// <returns>The user key pair, null if not found.</returns>
		public static Guid? GetUserId(string key) {
			var decrypt = Decrypt(key) ;

			if (!String.IsNullOrEmpty(decrypt)) {
				var args = decrypt.Split(new char[] { '|' }) ;

				if (args.Length == 2) {
					var date = Convert.ToDateTime(args[1]) ;

					if (DateTime.Now < date.AddMinutes(30)) {
						var apiKey = new Guid(args[0]) ;

						if (!Cache.ContainsKey(apiKey)) {
							using (var db = new DataContext()) {
								Cache[apiKey] = db.Users.Where(u => u.APIKey == apiKey).Select(u => u.Id).SingleOrDefault() ;
							}
						}
						return Cache[apiKey] ;
					}
				}
			}
			return null ;
		}

		/// <summary>
		/// Checks if the given API-key is valid.
		/// </summary>
		/// <param name="apiKey">The API-key</param>
		/// <returns>Weather the given key is valid</returns>
		public static bool IsValidKey(string apiKey) {
			if (!String.IsNullOrEmpty(apiKey))
				return GetUserId(apiKey) != null ;
			return false ;
		}

		/// <summary>
		/// Generates a new private key.
		/// </summary>
		/// <returns>The key</returns>
		public static string GeneratePrivateKey() {
			return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16) ;
		}

		/// <summary>
		/// Encrypts the given API-key with the site private key
		/// </summary>
		/// <param name="apiKey">The API-key</param>
		/// <returns>The encrypted API-key string</returns>
		public static string EncryptApiKey(Guid apiKey) {
			return Encrypt(apiKey.ToString() + "|" + DateTime.Now.ToString()) ;
		}

		/// <summary>
		/// Encrypts the given string with the site private key.
		/// </summary>
		/// <param name="src">The string</param>
		/// <returns>The encrypted string</returns>
		public static string Encrypt(string src) {
			var key = Piranha.Models.SysParam.GetByName("SITE_PRIVATE_KEY").Value ;
			var input = UTF8Encoding.UTF8.GetBytes(src) ;
			var crypto = new TripleDESCryptoServiceProvider() ;

			crypto.Key = UTF8Encoding.UTF8.GetBytes(key) ;
			crypto.Mode = CipherMode.ECB ;
			crypto.Padding = PaddingMode.PKCS7 ;
			
			var cTransform = crypto.CreateEncryptor() ;
			var result = cTransform.TransformFinalBlock(input, 0, input.Length) ;
			crypto.Clear() ;

			return Convert.ToBase64String(result, 0, result.Length) ;
		}

		/// <summary>
		/// Decrypts the given string with the site private key.
		/// </summary>
		/// <param name="src">The ecrypted string</param>
		/// <returns>The decrypted string</returns>
		public static string Decrypt(string src) {
			var key = Piranha.Models.SysParam.GetByName("SITE_PRIVATE_KEY").Value ;
			var input = Convert.FromBase64String(src) ;
			//var input = UTF8Encoding.UTF8.GetBytes(src) ;
		    var crypto = new TripleDESCryptoServiceProvider() ;

			crypto.Key = UTF8Encoding.UTF8.GetBytes(key) ;
			crypto.Mode = CipherMode.ECB;
			crypto.Padding = PaddingMode.PKCS7;
		  
			var cTransform = crypto.CreateDecryptor() ;
			var result = cTransform.TransformFinalBlock(input, 0, input.Length) ;
			crypto.Clear() ;

			return UTF8Encoding.UTF8.GetString(result) ;
	   }

		/// <summary>
		/// Clears the internal api-key cache.
		/// </summary>
		internal static void ClearCache() {
			Cache.Clear() ;
		}
	}
}