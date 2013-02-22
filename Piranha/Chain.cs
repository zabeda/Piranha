using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha
{
	internal class Chain
	{
		private const string STACK_ID = "PIRANHA_MANAGER_CHAIN_STACKS" ;

		private Dictionary<Guid, Stack<string>> Stacks {
			get {
				if (HttpContext.Current.Session[STACK_ID] == null)
					HttpContext.Current.Session[STACK_ID] = new Dictionary<Guid, Stack<string>>() ;
				return (Dictionary<Guid, Stack<string>>)HttpContext.Current.Session[STACK_ID] ;
			}
		}

		public void Push(Guid id, string url) {
			EnsureStack(id).Push(url) ;
		}

		public string Pop(Guid id) {
			return EnsureStack(id).Pop() ;
		}

		public bool IsEmpty(Guid id) {
			return EnsureStack(id).Count == 0 ;
		}

		private Stack<string> EnsureStack(Guid id) {
			if (!Stacks.ContainsKey(id))
				Stacks[id] = new Stack<string>() ;
			return Stacks[id] ;
		}
	}
}