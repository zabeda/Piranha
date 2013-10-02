using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Piranha.Rest.DataContracts
{
	[DataContract]
	public class MediaFolder
	{
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		[DataMember]
		public Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets the folder name.
		/// </summary>
		[DataMember]
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the availble child folders.
		/// </summary>
		[DataMember]
		public IList<MediaFolder> Folders { get ; set ; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public MediaFolder() {
			Folders = new List<MediaFolder>() ;
		}
	}
}