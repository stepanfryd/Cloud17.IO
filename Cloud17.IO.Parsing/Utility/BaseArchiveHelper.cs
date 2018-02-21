using System.Collections.Generic;

namespace Cloud17.IO.Parsing.Utility
{
	/// <summary>
	/// Base class for different archive operations
	/// </summary>
	public abstract class BaseArchiveHelper : IArchiveHelper
	{
		/// <summary>
		/// Byte array content of original archive file
		/// </summary>
		protected byte[] OriginalContent { get; private set; }

		/// <summary>
		/// List of entry paths to search for extract in case of multifile archive
		/// </summary>
		protected IEnumerable<string> ContentPaths { get; private set; }

		/// <summary>
		/// Returns content of compressed archive. If archive contains only one entry, this entry is returned. 
		/// If contains multiple entries and any match with any ContentPath, list of entries is returned
		/// </summary>
		/// <param name="originalContent">Original compressed archive</param>
		/// <param name="contentPaths">List of entry paths</param>
		/// <returns></returns>
		public List<ArchiveFileInfo> GetContent(byte[] originalContent, IEnumerable<string> contentPaths) {
			OriginalContent = originalContent;
			ContentPaths = contentPaths;
			return GetDecompressedContent();
		}

		/// <summary>
		/// Decompression implementation
		/// </summary>
		/// <returns></returns>
		protected abstract List<ArchiveFileInfo> GetDecompressedContent();
	}
}
