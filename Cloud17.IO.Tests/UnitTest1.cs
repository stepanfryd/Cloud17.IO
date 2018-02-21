using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Cloud17.IO.Archive.SevenZip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Cloud17.IO.Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
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

			var serConf = JsonConvert.SerializeObject(archiverConfig);
			var deser = JsonConvert.DeserializeObject<Dictionary<Type, List<byte[]>>>(serConf);

			Assert.IsNotNull(deser);
			Assert.IsTrue(JsonConvert.SerializeObject(archiverConfig) == JsonConvert.SerializeObject(deser));
			Assert.IsTrue(deser.ContainsKey(testType));
			Assert.IsTrue(deser[testType].Count == archiverConfig[testType].Count);

			for (var i = 0; i < deser[testType].Count; i++)
			{
				Assert.IsTrue(deser[testType][i].SequenceEqual(archiverConfig[testType][i]));
			}
		}
	}
}
