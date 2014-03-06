using System;
using System.Collections.Generic;
using System.Reflection;

namespace Piranha.Hooks
{
	/// <summary>
	/// The hooks available for the main application.
	/// </summary>
	public static class App
	{
		/// <summary>
		/// The delegates specific for the main app.
		/// </summary>
		public static class Delegates
		{ 
			/// <summary>
			/// Delegate for changing the default IoC registration.
			/// </summary>
			/// <param name="container">The current IoC container.</param>
			public delegate void IoCRegistrationDelegate(IoC.IContainer container);

			/// <summary>
			/// Delegate for adding an assembly to the precompiled view engine.
			/// </summary>
			/// <param name="assemblies">The collection of assemblies</param>
			public delegate void PrecompiledViewEngingeRegistration(IList<Assembly> assemblies);
		}

		/// <summary>
		/// The hooks available for application init.
		/// </summary>
		public static class Init 
		{
			/// <summary>
			/// Called when the IoC container is created. By implementing this
			/// hook you can replace the default container based on TinyIoC with
			/// another IoC container implementation.
			/// </summary>
			public static Hooks.Delegates.CreateDelegate<IoC.IContainer> CreateContainer;

			/// <summary>
			/// Called when the IoC container wants to register the cache provider. By
			/// implementing this hook the default registration is replaced.
			/// </summary>
			public static Delegates.IoCRegistrationDelegate RegisterCache;

			/// <summary>
			/// Called when the IoC container wants to register the log provider. By
			/// implementing this hook the default registration is replaced.
			/// </summary>
			public static Delegates.IoCRegistrationDelegate RegisterLog;

			/// <summary>
			/// Called when the IoC container wants to register the media provider. By
			/// implementing this hook the default registration is replaced.
			/// </summary>
			public static Delegates.IoCRegistrationDelegate RegisterMedia;

			/// <summary>
			/// Called when the IoC container wants to register the media cache provider. By
			/// implementing this hook the default registration is replaced.
			/// </summary>
			public static Delegates.IoCRegistrationDelegate RegisterMediaCache;

			/// <summary>
			/// Called when the IoC container wants to register the Api. By
			/// implementing this hook the default registration is replaced.
			/// </summary>
			public static Delegates.IoCRegistrationDelegate RegisterApi;

			/// <summary>
			/// Called when the IoC container has finished registering the default
			/// types. This hook can be used to register additional types in the
			/// IoC container.
			/// </summary>
			public static Delegates.IoCRegistrationDelegate Register;

			/// <summary>
			/// Called when the view engines are registered. This hooks can be used
			/// to add an assembly to be registered for precompiled views.
			/// </summary>
			public static Delegates.PrecompiledViewEngingeRegistration RegisterPrecompiledViews;
		}
	}
}