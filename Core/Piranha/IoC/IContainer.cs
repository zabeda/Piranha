using System;

namespace Piranha.IoC
{
	/// <summary>
	/// Defines the basic methods that an IoC container should provide.
	/// </summary>
	public interface IContainer
	{
		/// <summary>
		/// Registers the abstract type to be resolved by the concrete type.
		/// </summary>
		/// <typeparam name="TAbstract">The abstract type</typeparam>
		/// <typeparam name="TConcrete">The concrete type</typeparam>
		void RegisterMultiInstance<TAbstract, TConcrete>()
			where TAbstract : class
			where TConcrete : class, TAbstract;

		/// <summary>
		/// Registers the abstract type to be resolved by the concrete type
		/// as a singleton.
		/// </summary>
		/// <typeparam name="TAbstract">The abstract type</typeparam>
		/// <typeparam name="TConcrete">The concrete type</typeparam>
		void RegisterSingleton<TAbstract, TConcrete>()
			where TAbstract : class
			where TConcrete : class, TAbstract;

		/// <summary>
		/// Registers the abstract type to be resolved by the given instance.
		/// </summary>
		/// <typeparam name="TAbstract">The abstract type</typeparam>
		/// <param name="obj">The instance</param>
		void RegisterSingleton<TAbstract>(TAbstract obj)
			where TAbstract : class;

		/// <summary>
		/// Resolves the given type
		/// </summary>
		/// <typeparam name="T">The type</typeparam>
		/// <returns>The resolved object</returns>
		T Resolve<T>() where T : class;
	}
}
