using Cloud17.IO.Parsing.Interfaces;
using Cloud17.IO.Parsing.Iso8583.Configuration;
using System;
using System.Collections.Generic;

namespace Cloud17.IO.Parsing.Iso8583.Entities
{
	/// <summary>
	/// Class holds Integrated Produc Message data
	/// </summary>
	public class Message : IReportDocument
	{
		#region Public Properties

		/// <summary>
		/// List of data elements in message
		/// </summary>
		public List<DataElement> DataElements { get; set; }

		/// <inhe
		public IDataProcessor DataProcessor { get; set; }

		/// <summary>
		/// Time when has been message processed
		/// </summary>
		public DateTime DateProcessed { get; set; }

		/// <summary>
		/// Message parser configuration
		/// </summary>
		public ImpConfiguration ImpParserConfig { get; set; }

		/// <summary>
		///
		/// </summary>
		public bool IsNoData { get; set; }

		public bool IsWrongFormat { get; set; }

		public string OriginalDocument { get; set; }

		/// <summary>
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// Message Type Identifier
		/// </summary>
		public int TypeIdentifier { get; set; }

		#endregion Public Properties

		#region Public Constructors

		/// <summary>
		/// Creates object instance
		/// </summary>
		public Message()
		{
			DataElements = new List<DataElement>();
		}

		#endregion Public Constructors

		#region Public Methods

		public bool IsValid()
		{
			return true;
		}

		public bool ShouldSerializeDataElements()
		{
			return DataElements?.Count > 0;
		}

		#endregion Public Methods
	}
}