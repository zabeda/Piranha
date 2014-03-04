using System;

namespace Piranha.IO
{
	/// <summary>
	/// Mock media provider for unit tests.
	/// </summary>
	public sealed class MockMediaProvider : IMediaProvider
	{
		/// <summary>
		/// Gets the data for the media entity with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="type">The media type</param>
		/// <returns>The media data, null if the data wasn't found</returns>
		public byte[] Get(Guid id, MediaType type = MediaType.Media) {
			return null;
		}

		/// <summary>
		/// Gets the draft data for the media entity with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="type">The media type</param>
		/// <returns>The draft media data, null if the data wasn't found</returns>
		public byte[] GetDraft(Guid id, MediaType type = MediaType.Media) {
			return null;
		}

		/// <summary>
		/// Stores the given data for the media entity with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="data">The media data</param>
		/// <param name="type">The media type</param>
		public void Put(Guid id, byte[] data, MediaType type = MediaType.Media) { }

		/// <summary>
		/// Stores the given data as a draft for the media entity with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="data">The media data</param>
		/// <param name="type">The media type</param>
		public void PutDraft(Guid id, byte[] data, MediaType type = MediaType.Media) { }

		/// <summary>
		/// Deletes the data for the media entity with given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="type">The media type</param>
		public void Delete(Guid id, MediaType type = MediaType.Media) { }

		/// <summary>
		/// Deletes the draft for the media entity with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="type">The media type</param>
		public void DeleteDraft(Guid id, MediaType type = MediaType.Media) { }

		/// <summary>
		/// Publishes the current draft for the media entity with the given id. This means
		/// making the draft data the published data
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="type">The media type</param>
		public void Publish(Guid id, MediaType type = MediaType.Media) { }

		/// <summary>
		/// Unpublishes the current draft for the media entity with the given id. This means
		/// making the published data the draft data
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="type">The media type</param>
		public void Unpublish(Guid id, MediaType type = MediaType.Media) { }
	}
}