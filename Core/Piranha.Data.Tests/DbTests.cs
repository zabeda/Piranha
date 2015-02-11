using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Piranha.Data.Tests
{
	[TestClass]
	public class DbTests
	{
		[TestMethod]
		[TestCategory("Database")]
		public void Create() {
			using (var db = new Db()) {
				db.Users.Count();
			}
		}

		[TestMethod]
		[TestCategory("Database")]
		public void IsCompatible() {
			using (var db = new Db()) {
				Assert.IsTrue(db.IsCompatible);
			}
		}

		[TestMethod]
		[TestCategory("Database")]
		public void Migrate() {
			using (var db = new Db()) {
				if (!db.IsCompatible)
					db.Migrate();
			}
		}
	}
}
