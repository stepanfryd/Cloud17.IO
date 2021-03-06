﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cloud17.IO.Parsing.Configuration
{
	/// <summary>
	///   Value parser configuration settings
	/// </summary>
	public class ValueParserConfiguration
	{
		#region Fields

		private IValueParser<IValueParserResult> _parser;

		#endregion

		#region Public

		/// <summary>
		///   Parser type
		/// </summary>
		[JsonPropertyName("type")]
		public Type ParserType { get; set; }

		/// <summary>
		///   Parser instance
		/// </summary>
		[JsonIgnore]
		public IValueParser<IValueParserResult> Parser
		{
			get
			{
				if (ParserType != null && _parser == null)
				{
					_parser = (IValueParser<IValueParserResult>)Activator.CreateInstance(ParserType, Settings);
				}

				return _parser;
			}
		}

		/// <summary>
		///   Parser configuration settings
		/// </summary>
		[JsonPropertyName("settings")]
		public Dictionary<string, object> Settings { get; set; }

		#endregion
	}
}