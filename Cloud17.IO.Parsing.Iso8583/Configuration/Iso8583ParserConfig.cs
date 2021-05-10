using Cloud17.IO.Parsing.Configuration;

using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace Cloud17.IO.Parsing.Iso8583.Configuration
{
	/// <summary>
	/// ISO8583 file data configuration
	/// </summary>
	[XmlRoot("parserConfig")]
	public class Iso8583ParserConfig : IParserConfiguration
	{
		#region Private Fields

		private Encoding _sourceEncoding;

		private Encoding _targetEncoding;

		#endregion Private Fields

		#region Public Properties

		/// <summary>
		/// Padding between blocks
		/// </summary>
		[XmlAttribute("blockPadding")]
		[DefaultValue(2)]
		public int BlockPadding { get; set; } = 2;

		/// <summary>
		/// Size of data block in Blocked File Layout
		/// </summary>
		[XmlAttribute("blockSizeInBytes")]
		[DefaultValue(1012)]
		public int BlockSizeInBytes { get; set; } = 1012;

		/// <summary>
		/// Path to JSON file with Data Element configuration
		/// </summary>
		[XmlAttribute("dataElementConfigPath")]
		public string DataElementConfigPath { get; set; }

		/// <summary>
		/// Source bytes encoding
		/// </summary>
		[XmlIgnore]
		public Encoding SourceEncoding
		{
			get
			{
				if (_sourceEncoding != null) return _sourceEncoding;
				try
				{
					_sourceEncoding = Encoding.GetEncoding(!string.IsNullOrEmpty(SourceEncodingName) ? SourceEncodingName : "ASCII");
				}
				catch
				{
					_sourceEncoding = CodePagesEncodingProvider.Instance.GetEncoding(SourceEncodingName);
				}

				if (_sourceEncoding == null)
					_sourceEncoding = Encoding.ASCII;

				return _sourceEncoding;
			}
		}

		/// <summary>
		/// Source data encoding
		/// </summary>
		[XmlAttribute("sourceEncoding")]
		[DefaultValue("IBM037")]
		public string SourceEncodingName { get; set; }

		/// <summary>
		/// Target bytes encoding
		/// </summary>
		[XmlIgnore]
		public Encoding TargetEncoding
		{
			get
			{
				if (_targetEncoding != null) return _targetEncoding;
				try
				{
					_targetEncoding = !string.IsNullOrEmpty(TargetEncodingName)
						? Encoding.GetEncoding(TargetEncodingName)
						: Encoding.ASCII;
				}
				catch
				{
					_targetEncoding = Encoding.ASCII;
				}

				return _targetEncoding;
			}
		}

		/// <summary>
		/// Target data encoding
		/// </summary>
		[XmlAttribute("targetEncoding")]
		[DefaultValue("ASCII")]
		public string TargetEncodingName { get; set; }

		#endregion Public Properties
	}
}