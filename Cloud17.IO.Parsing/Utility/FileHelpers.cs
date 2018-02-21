using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloud17.IO.Parsing.Utility
{
	public static class FileHelpers
	{
		/// <summary>
		/// list of known ISO MTI codes
		/// </summary>
		private static readonly List<string> KnowIso8583MtiCodes = new List<string>
			{
				"1240",
				"1242",
				"1244",
				"1440",
				"1442",
				"1444",
				"1640",
				"1642",
				"1644",
				"1740",
				"1742",
				"1744"
			};

		#region Private and protected

		/// <summary>
		///   Determine if the source file is IMP bitmap binary file in ISO 8583 format
		/// </summary>
		/// <param name="fileHeader">First 8 bytes of the file</param>
		/// <param name="sourcEncoding"></param>
		/// <param name="targetEncoding"></param>
		/// <returns></returns>
		public static bool IsIso8583File(byte[] fileHeader, Encoding sourcEncoding = null, Encoding targetEncoding = null)
		{
			if (fileHeader == null) throw new ArgumentNullException(nameof(fileHeader));
			if (fileHeader.Length > 8)
				throw new ArgumentOutOfRangeException(nameof(fileHeader), "Header size is too big. Expecting 8 bytes");
			if (sourcEncoding == null) sourcEncoding = Encoding.GetEncoding("IBM037");
			if (targetEncoding == null) targetEncoding = Encoding.UTF8;

			var header = new byte[] { 0, 0, 0, 74, 0, 0, 0, 0 };

			if (KnowIso8583MtiCodes != null)
			{
				foreach (var code in KnowIso8583MtiCodes)
				{
					var encoded = Encoding.Convert(targetEncoding, sourcEncoding, targetEncoding.GetBytes(code));
					Array.Copy(encoded, 0, header, 4, 4);
					if (header.SequenceEqual(fileHeader))
					{
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		///   Determine if the source file is IMP bitmap binary file in ISO 8583 format
		/// </summary>
		/// <param name="fileHeader">First 8 bytes of the file</param>
		/// <returns></returns>
		public static IArchiveHelper GetArchiveHelper(IDictionary<IArchiveHelper, List<byte[]>> archiverConfig, byte[] fileHeader)
		{
			if (fileHeader == null) return null;
			if (archiverConfig == null) return null;

			foreach (var entry in archiverConfig)
			{
				if (entry.Value == null) continue;
				foreach (var seq in entry.Value)
				{
					if (fileHeader.Take(seq.Length).SequenceEqual(seq)) return entry.Key;
				}
			}
			return null;
		}

		#endregion Private and protected
	}
}