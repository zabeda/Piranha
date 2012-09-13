using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace Piranha.Extend
{
	internal static class ExtensionProvider
	{
		 public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider) {
			 var ret = new List<Type>() ;
			 ExtensionManager.Regions.ForEach(e => ret.Add(e.Type)) ;
			 ExtensionManager.Extensions.ForEach(e => ret.Add(e.Type)) ;
			 return ret ;
		 }
	}
}