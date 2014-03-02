using System;

namespace Piranha.IoC
{
	/// <summary>
	/// IoC container implemented with TinyIoC.
	/// </summary>
	public class TinyIoCContainer : IContainer
	{
		#region Members
		/// <summary>
		/// The internal container
		/// </summary>
		private readonly TinyIoC.TinyIoCContainer container;
		#endregion

		/// <summary>
		/// Creates a new TinyUiC container.
		/// </summary>
		/// <param name="current">The optional existing container</param>
		public TinyIoCContainer(TinyIoC.TinyIoCContainer current = null) {
			container = current != null ? current : new TinyIoC.TinyIoCContainer();
		}

		/// <summary>
		/// Registers the abstract type to be resolved by the concrete type.
		/// </summary>
		/// <typeparam name="TAbstract">The abstract type</typeparam>
		/// <typeparam name="TConcrete">The concrete type</typeparam>
		public virtual void RegisterMultiInstance<TAbstract, TConcrete>()
			where TAbstract : class
			where TConcrete : class, TAbstract 
		{
			container.Register<TAbstract, TConcrete>().AsMultiInstance();
		}

		/// <summary>
		/// Registers the abstract type to be resolved by the concrete type
		/// as a singleton.
		/// </summary>
		/// <typeparam name="TAbstract">The abstract type</typeparam>
		/// <typeparam name="TConcrete">The concrete type</typeparam>
		public virtual void RegisterSingleton<TAbstract, TConcrete>()
			where TAbstract : class
			where TConcrete : class, TAbstract 
		{
			container.Register<TAbstract, TConcrete>().AsSingleton();
		}

		/// <summary>
		/// Registers the abstract type to be resolved by the given instance.
		/// </summary>
		/// <typeparam name="TAbstract">The abstract type</typeparam>
		/// <param name="obj">The instance</param>
		public void RegisterSingleton<TAbstract>(TAbstract obj)
			where TAbstract : class 
		{
			container.Register<TAbstract>(obj);
		}


		/// <summary>
		/// Resolves the given type
		/// </summary>
		/// <typeparam name="T">The type</typeparam>
		/// <returns>The resolved object</returns>
		public virtual T Resolve<T>() where T : class {
			return container.Resolve<T>();
		}
	}
}
