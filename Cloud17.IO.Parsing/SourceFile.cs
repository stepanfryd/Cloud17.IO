using Cloud17.IO.Parsing.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cloud17.IO.Parsing
{
	/// <summary>
	///   Source file entity
	/// </summary>
	public class SourceFile<T>: ISourceFile
	{
		#region Constructors

		public SourceFile(T fileId)
		{
			Id = fileId;
			ProcessedTime = DateTime.Now;
			FileProcessingContext = new Dictionary<string, object>();
		}

		/// <summary>
		///   Construct of source file
		/// </summary>
		/// <param name="fileFullPath">Full path to report file</param>
		/// <param name="originalSource">Original file content source</param>
		/// <param name="state">Parser processor state</param>
		public SourceFile(T fileId, string fileFullPath, byte[] originalSource, FileProcessingState state = FileProcessingState.Unknown) : this(fileId)
		{
			var pathParts = fileFullPath.Split(new[] { '\\', '/' });

			if (pathParts == null || pathParts.Length == 0)
			{
				FileName = fileFullPath;
				BaseLocation = "Unknown";
			}
			else
			{
				FileName = pathParts[pathParts.Length - 1];
				BaseLocation = string.Join("\\", pathParts.Take(pathParts.Length - 1));
			}

			OriginalSource = originalSource;
			ProcessingState = state;
		}

		#endregion Constructors

		#region Public

		public T Id { get; set; }

		
		/// <inheritdoc />
		public string FileName { get; set; }

		/// <summary>
		public DateTime ProcessedTime { get; set; }

		/// <summary>
		public FileProcessingState ProcessingState { get; set; }

		/// <summary>
		public string BaseLocation { get; set; }

		/// <summary>
		public IDictionary<string, object> FileProcessingContext { get; set; }

		/// <summary>
		public byte[] OriginalSource { get; set; }

		#endregion Public
	}
}