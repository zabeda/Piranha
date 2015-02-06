using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Piranha.Data.Tests
{
	[TestClass]
	public class DbTests
	{
		[TestMethod]
		public void Create() {
			using (var db = new Db()) {
				db.Users.Count();
			}
		}
	}
}
