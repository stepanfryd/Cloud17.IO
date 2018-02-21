namespace Cloud17.IO.Parsing
{
	public enum FileProcessingState
	{
		/// <summary>
		///   Unknown state
		/// </summary>
		Unknown = 0,

		/// <summary>
		///  File has been saved and ready for processing
		/// </summary>
		Pending = 5,

		/// <summary>
		///   File processing in progress
		/// </summary>
		InProgress = 10,

		/// <summary>
		///   File processing finished
		/// </summary>
		Finished = 20
	}
}