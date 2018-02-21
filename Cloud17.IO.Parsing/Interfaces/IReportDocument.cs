namespace Cloud17.IO.Parsing.Interfaces
{
	/// <summary>
	///   Interface describe document common functionality and properties
	/// </summary>
	public interface IReportDocument
	{
		#region Private and protected

		/// <summary>
		///   Method says if document is valid
		/// </summary>
		/// <returns></returns>
		bool IsValid();

		#endregion

		#region Public

		/// <summary>
		///   Says if document contains any data. If true, document is empty
		/// </summary>
		bool IsNoData { get; set; }

		/// <summary>
		///   Says if document in wrong format
		/// </summary>
		bool IsWrongFormat { get; set; }

		/// <summary>
		///   Holds original document in plain text
		/// </summary>
		string OriginalDocument { get; set; }

		/// <summary>
		/// Instance of class responsible for data processing
		/// </summary>
		IDataProcessor DataProcessor { get; set; }

		#endregion
	}
}