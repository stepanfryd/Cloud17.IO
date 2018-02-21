namespace Cloud17.IO.Parsing

{
	/// <summary>
	/// Interface for value parser types
	/// </summary>
	/// <typeparam name="T">What is the document type</typeparam>
	public interface IValueParser<out T>
	{
		/// <summary>
		/// Parse input value and return document
		/// </summary>
		/// <param name="inputValue"></param>
		/// <returns></returns>
		T Parse(string inputValue);
	}
}