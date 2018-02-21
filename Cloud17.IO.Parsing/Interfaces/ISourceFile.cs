using System;
using System.Collections.Generic;

namespace Cloud17.IO.Parsing.Interfaces
{
	public interface ISourceFile
	{
		#region Public

		/// <summary>
		///   Report file name
		/// </summary>
		string FileName { get; set; }

		/// <summary>
		///   Time when file has been processed
		/// </summary>
		DateTime ProcessedTime { get; set; }

		/// <summary>
		///   Processing status
		/// </summary>
		FileProcessingState ProcessingState { get; set; }

		/// <summary>
		///   Report file location
		/// </summary>
		string BaseLocation { get; set; }

		/// <summary>
		///   Context is used during file processing
		/// </summary>
		IDictionary<string, object> FileProcessingContext { get; set; }

		/// <summary>
		///   Source data from original file
		/// </summary>
		byte[] OriginalSource { get; set; }

		#endregion Public
	}
}