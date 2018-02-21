using System;
using System.Collections.Generic;

namespace Cloud17.IO.Parsing.Iso8583.Configuration
{
	/// <summary>
	///   Data elements configuration
	/// </summary>
	public class DataElementConfig : Dictionary<int, DataElementInfo>
	{
		#region Constructors

		/// <summary>
		///   Creates instance of data element configuration
		/// </summary>
		/// <param name="config">Dictionary which contains configuration of all data elements which has to be processed</param>
		public DataElementConfig(IDictionary<int, DataElementInfo> config) : base(config)
		{
		}

		#endregion

		/// <summary>
		///   Loads primary bit map and determine which data elements are in the message
		/// </summary>
		/// <param name="primaryBitmap"></param>
		public void LoadPrimaryBitmap(bool[] primaryBitmap)
		{
			if (primaryBitmap == null) throw new ArgumentNullException(nameof(primaryBitmap));
			if (primaryBitmap.Length > 128) throw new IndexOutOfRangeException($"Primary bitmap lenght exceed 128.");

			for (var i = 0; i < primaryBitmap.Length; i++)
			{
				var key = i + 1;
				if (ContainsKey(key))
				{
					this[key].Exists = primaryBitmap[i];
				}
			}
		}
	}
}