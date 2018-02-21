using System.Collections.Generic;
using System.Linq;

namespace Cloud17.IO.Parsing.Utility
{
	/// <summary>
	/// Helper class for archive type recognition. Select correct archive helper and process file decompressiong
	/// </summary>
	public class CompressedArchiveHelper
	{
		private ArchiveFileInfo _archiveFile;
		private IEnumerable<string> _contentPaths;
		
		public IDictionary<IArchiveHelper, List<byte[]>> ArchiverConfig { get; set; }

		/// <param name="archiveFile">Archive file instance</param>
		/// <param name="contentPaths">Archive content paths to sele tion</param>
		public CompressedArchiveHelper(ArchiveFileInfo archiveFile, IEnumerable<string> contentPaths = null) {
			_archiveFile = archiveFile ?? throw new System.ArgumentNullException(nameof(archiveFile));
			_contentPaths = contentPaths;
		}

		/// <summary>
		/// Method identify archive content and if recodgnize correct archive process its decompression and returns required archive entries
		/// </summary>
		/// <returns>List of identifies</returns>
		public List<ArchiveFileInfo> GetContent() {
			var archiveHelper = FileHelpers.GetArchiveHelper(ArchiverConfig, this._archiveFile.Content);

			return archiveHelper != null
				? archiveHelper.GetContent(_archiveFile.Content, _contentPaths)
					.Select(f => new ArchiveFileInfo { FileName = $"{_archiveFile.FileName}\\{f.FileName}", Content = f.Content })
					.ToList()
				: new List<ArchiveFileInfo> { _archiveFile };
		}
	}
}


