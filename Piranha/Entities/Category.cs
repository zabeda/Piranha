using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.Entities
{
	/// <summary>
	/// The category entity. Categories can be attached to any entity.
	/// </summary>
	[Serializable]
	public class Category : StandardEntity<Category>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the optional id of the parent category.
		/// </summary>
		public Guid? ParentId { get ; set ; }

		/// <summary>
		/// Gets/sets the id of the permalink.
		/// </summary>
		public Guid PermalinkId { get ; set ; }

		/// <summary>
		/// Gets/sets the name.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the description.
		/// </summary>
		public string Description { get ; set ; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the optional parent category.
		/// </summary>
		public Category Parent { get ; set ; }

		/// <summary>
		/// Gets/sets the permalink used to access the category.
		/// </summary>
		public Permalink Permalink { get ; set ; }

		/// <summary>
		/// Gets/sets the currently available extensions.
		/// </summary>
		public IList<Extension> Extensions { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Category() {
			Extensions = new List<Extension>() ;
		}
	}
}
