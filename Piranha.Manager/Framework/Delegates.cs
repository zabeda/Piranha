using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.WebPages;

namespace Piranha.Manager
{
	public static class Delegates
	{
		public delegate void ModelLoadedHook<T>(BasePage page, MenuItem menu, T model) ;
	}
}