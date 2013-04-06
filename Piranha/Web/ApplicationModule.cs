using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Piranha.Web.ApplicationModule), "Init")]

namespace Piranha.Web
{
	public static class ApplicationModule
	{
		public static void Init() {

		}
	}
}