using Cloud17.IO.Parsing.Interfaces;
using System.Threading.Tasks;

namespace Cloud17.IO.Parsing
{
	/// <summary>
	///   Base report document contains common functionality and properties
	/// </summary>
	public abstract class BaseReportDocument : IReportDocument
	{
		#region Public

		/// <summary>
		///   Instance if exists of data processor responsible for storing document data into storage
		/// </summary>
		public IDataProcessor DataProcessor { get; set; }

		/// <summary>
		///   Says if document contains any data. If true, document is empty
		/// </summary>
		public bool IsNoData { get; set; }

		/// <summary>
		///   Says if document in wrong format
		/// </summary>
		public bool IsWrongFormat { get; set; }

		/// <summary>
		///   Holds original document in plain text
		/// </summary>
		public string OriginalDocument { get; set; }

		#endregion Public

		#region Other

		/// <summary>
		///   Method says if document is valid
		/// </summary>
		/// <returns></returns>
		public abstract bool IsValid();

		/// <summary>
		///   Method execute data processing if processor exists
		/// </summary>
		/// <param name="reportFile"></param>
		public async Task ProcessDocumentDataAsync(ISourceFile reportFile)
		{
			var processDataAsync = DataProcessor?.ProcessDataAsync(this, reportFile);
			if (processDataAsync != null)
			{
				await processDataAsync;
			}
		}

		#endregion Other
	}
}