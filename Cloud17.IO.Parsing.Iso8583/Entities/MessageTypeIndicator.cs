using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Cloud17.IO.Parsing.Iso8583.Entities
{
	/// <summary>
	/// Message type indicator
	/// </summary>
	[DebuggerDisplay("MIT: {MessageType}")]
	public struct MessageTypeIndicator
	{
		/// <summary>
		/// ISO 8583 version
		/// </summary>
		public ISO8583Version Version { get; set; }

		/// <summary>
		/// Message class
		/// </summary>
		public MessageClass Class { get; set; }

		/// <summary>
		/// Message function
		/// </summary>
		public MessageFunction Function { get; set; }

		/// <summary>
		/// Message origin
		/// </summary>
		public MessageOrigin Origin { get; set; }

		public string MessageType
		{
			get
			{
				return $"{(byte)Version}{(byte)Class}{(byte)Function}{(byte)Origin}";
			}
		}

		public MessageTypeIndicator(byte version, byte @class, byte function, byte origin)
			: this((ISO8583Version)version, (MessageClass)@class, (MessageFunction)function, (MessageOrigin)origin)
		{ }

		public MessageTypeIndicator(ISO8583Version version, MessageClass @class, MessageFunction function, MessageOrigin origin)
		{
			Version = version;
			Class = @class;
			Function = function;
			Origin = origin;
		}

		public override string ToString()
		{
			return MessageType;
		}

		public static MessageTypeIndicator Parse(string value)
		{
			if (string.IsNullOrEmpty(value))
				throw new ArgumentNullException(nameof(value));

			if (!Regex.Match(value, @"\d{4}").Success)
				throw new ArgumentOutOfRangeException(nameof(value), "Expected value is exactly 4 digits");

			var v = Regex.Match(value, @"(?<version>\d)(?<class>\d)(?<function>\d)(?<origin>\d)");

			return new MessageTypeIndicator(
				byte.Parse(v.Groups["version"].Value),
				byte.Parse(v.Groups["class"].Value),
				byte.Parse(v.Groups["function"].Value),
				byte.Parse(v.Groups["origin"].Value)
				);
		}
	}
}
