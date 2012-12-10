using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Extend;

namespace Piranha.Extensions
{
	/// <summary>
	/// Class registering the assembly as a module. This is needed to ensure that the assembly is scanned for
	/// embedded view when the manager interface is rendered.
	/// </summary>
	[Module(Id="19B30D10-7F29-4471-8FA1-EA25967FA12B", Name="Core extensions", InternalId="CoreExtensions")]
	public class ModuleRegistration : Module {}
}