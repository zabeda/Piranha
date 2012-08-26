using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.Extend
{
	/// <summary>
	/// Interface for defining a property type.
	/// </summary>
	/// <typeparam name="T">The type of the value</typeparam>
	public interface IProperty<T>
	{
		/// <summary>
		/// Gets/sets the value of the property.
		/// </summary>
		T Value { get ; set ; }
	}
}
