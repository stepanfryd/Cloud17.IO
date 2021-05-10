using Cloud17.IO.Archive.SevenZip;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

using Xunit;

namespace Cloud17.IO.Archive._7z.Tests
{
	public class UnitTests
	{
		[Fact]
		public void ArchiveTypeIdentification()
		{
			var testType = typeof(SevenZipArchiveHelper);

			var archiverConfig = new Dictionary<Type, List<byte[]>> {
				{
					testType, new List<byte[]> {
						new byte[] { 0x60, 0xEA }, // ARJ
						new byte[] { 0x50, 0x4B, 0x03, 0x04 }, // ZIP
						new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x00 }, // RAR 1.50
						new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x01, 0x00 }, // RAR 5
						new byte[] { 0x37, 0x7A, 0xBC, 0xAF, 0x27, 0x1C } // 7z
					}
				}
			};

			var serConf = JsonSerializer.Serialize(archiverConfig);
			var deser = JsonSerializer.Deserialize<Dictionary<Type, List<byte[]>>>(serConf);

			Assert.NotNull(deser);
			Assert.True(JsonSerializer.Serialize(archiverConfig) == JsonSerializer.Serialize(deser));
			Assert.True(deser.ContainsKey(testType));
			Assert.True(deser[testType].Count == archiverConfig[testType].Count);

			for (var i = 0; i < deser[testType].Count; i++)
			{
				Assert.True(deser[testType][i].SequenceEqual(archiverConfig[testType][i]));
			}
		}
	}
}
