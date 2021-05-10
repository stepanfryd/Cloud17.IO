using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Cloud17.IO.Parsing.Configuration
{
	/// <summary>
	///   Item mapping configuration
	/// </summary>
	public class ItemMapping
	{
		#region Public

		/// <summary>
		///   Property name
		/// </summary>
		[JsonPropertyName("property")]
		public string Property { get; set; }

		/// <summary>
		///   Regular expressin pattern string for value recognition
		/// </summary>
		[JsonPropertyName("pattern")]
		public string PatternString { get; set; }

		/// <summary>
		///   Regex pattern
		/// </summary>
		[JsonIgnore]
		public Regex Pattern
		{
			get
			{
				if (!string.IsNullOrEmpty(PatternString))
				{
					return new Regex(PatternString, RegexOptions.Multiline);
				}
				return null;
			}
		}

		/// <summary>
		///   If pattern contains unnamed match groups then is used its index
		/// </summary>
		[JsonPropertyName("matchIndex")]
		public int? MatchIndex { get; set; }

		/// <summary>
		///   Value processor script CSharp. Simple script sctring prepare string value for processor. Script is executed in
		///   runtime and must have return value.
		///   If not then parser thrown exception
		///   <example>"System.DateTime.ParseExact(\"{0}\", \"ddMMMyy\", CultureInfo.InvariantCulture);"</example>
		/// </summary>
		[JsonPropertyName("valuePreprocess")]
		public string ValuePrepocess { get; set; }

		/// <summary>
		///   Value parser configuration object <see cref="ValueParserConfiguration" />
		/// </summary>
		[JsonPropertyName("valueParser")]
		public ValueParserConfiguration ValueParser { get; set; }

		#endregion
	}
}