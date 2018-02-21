using System.IO;
using System.Collections.Generic;
using System.Linq;
using	SevenZibSharp =	SevenZip;
using Cloud17.IO.Parsing.Utility;

namespace Cloud17.IO.Archive.SevenZip
{
	/// <summary>
	/// Helper for Zip archives
	/// </summary>
	public class SevenZipArchiveHelper : BaseArchiveHelper
	{
		/// <summary>
		/// Get content of compressed archive
		/// </summary>
		/// <returns></returns>
		protected override List<ArchiveFileInfo> GetDecompressedContent()
		{
			var retVal = new List<ArchiveFileInfo>();

			using (var orMs = new MemoryStream(this.OriginalContent))
			{
				orMs.Seek(0, SeekOrigin.Begin);

				using (var archiveFile = new SevenZibSharp.SevenZipExtractor(orMs))
				{
					if (archiveFile.ArchiveFileData.Count == 1 || ContentPaths == null || ContentPaths.Count() == 0)
					{
						retVal.Add(GetArchiveFileInfo(archiveFile.ArchiveFileData[0], archiveFile));
					}
					else if (archiveFile.ArchiveFileData.Count > 1 && ContentPaths?.Count() > 0)
					{
						foreach (var path in this.ContentPaths)
						{
							var fent = archiveFile.ArchiveFileData.SingleOrDefault(e => e.FileName.ToLower() == path.ToLower());
							if (fent != null)
							{
								retVal.Add(GetArchiveFileInfo(fent, archiveFile));
							}
						}
					}
				}
				orMs.Close();
				return retVal;
			}
		}

		private ArchiveFileInfo GetArchiveFileInfo(SevenZibSharp.ArchiveFileInfo entry, SevenZibSharp.SevenZipExtractor archiveFile)
		{
			using (var entryStream = new MemoryStream())
			{
				archiveFile.ExtractFile(entry.FileName, entryStream);
				var retVal = new ArchiveFileInfo
				{
					FileName = entry.FileName,
					Content = entryStream.ToArray()
				};
				entryStream.Close();
				return retVal;
			}
		}
	}
}
