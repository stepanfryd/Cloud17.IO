using Newtonsoft.Json;

namespace Cloud17.IO.Parsing.Configuration
{
	/// <summary>
	/// Main parser configuration
	/// </summary>
	public class ParserConfiguration : IParserConfiguration
	{
		#region Public Properties

		/// <summary>
		/// Single document configuration settings
		/// </summary>
		[JsonProperty("documents")]
		public DocumentSettings[] DocumentsSettings { get; set; }

		#endregion Public Properties
	}
}