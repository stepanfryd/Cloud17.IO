using System.Collections.Generic;

namespace Cloud17.IO.Parsing.Utility
{
	public interface IArchiveHelper
	{
		List<ArchiveFileInfo> GetContent(byte[] originalContent, IEnumerable<string> contentPaths);
	}
}
