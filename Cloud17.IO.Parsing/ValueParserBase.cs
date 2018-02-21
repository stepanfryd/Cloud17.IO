using System.Collections.Generic;

namespace Cloud17.IO.Parsing
{
	/// <summary>
	///   Base value parser with common functions
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class ValueParserBase<T> : IValueParser<T>
	{
		#region Constructors

		/// <summary>
		///   Creates instance of value parser
		/// </summary>
		/// <param name="settings">Value parser configuration settings</param>
		protected ValueParserBase(IDictionary<string, object> settings)
		{
			Settings = settings;
		}

		#endregion

		#region Public

		/// <summary>
		///   Value parser configuration settings
		/// </summary>
		protected IDictionary<string, object> Settings { get; private set; }

		#endregion

		#region Other

		/// <summary>
		///   Parse input value and return document
		/// </summary>
		/// <param name="inputValue"></param>
		/// <returns></returns>
		public T Parse(string inputValue)
		{
			return ParseDocument(inputValue);
		}

		#endregion

		/// <summary>
		///   Method implements documnet parsing functionality
		/// </summary>
		/// <param name="inputValue"></param>
		/// <returns></returns>
		protected abstract T ParseDocument(string inputValue);
	}
}