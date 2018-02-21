namespace Cloud17.IO.Parsing.Utility
{
	/// <summary>
	/// Basic information about file/entry from compressed archive
	/// </summary>
	public class ArchiveFileInfo
	{
		/// <summary>
		/// Archive file name
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// Binary content
		/// </summary>
		public byte[] Content { get; set; }
	}
}
