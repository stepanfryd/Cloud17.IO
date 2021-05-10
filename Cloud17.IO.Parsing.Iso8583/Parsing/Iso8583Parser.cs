using Cloud17.IO.Parsing.Interfaces;
using Cloud17.IO.Parsing.Iso8583.Configuration;
using Cloud17.IO.Parsing.Iso8583.Entities;
using Cloud17.IO.Parsing.Iso8583.Serialization;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Cloud17.IO.Parsing.Iso8583.Parsing
{
	/// <summary>
	/// General parser IPM messages based on the variable-length specifications of the International
	/// Organization for Standardization (ISO) 8583–1993 Financial Transaction Card Originated Messages
	/// </summary>
	public class Iso8583Parser : BaseSchemeParser<Iso8583ParserConfig>
	{
		#region Private Fields

		private const int BITMAP_LENGTH = 16;
		private const int MESSAGE_SIZE_LENGTH = 4;
		private const int MTI_LENGTH = 4;
		private int _currentPosition;
		private ParserConfiguration _impParserConfig;

		private bool disposedValue = false;

		#endregion Private Fields

		#region Public Properties

		public IDataProcessor MessageDataProcessor { get; set; }

		#endregion Public Properties

		#region Private Properties

		/// <summary>
		/// Size of content in bytes
		/// </summary>
		private long TotalLength => FileContent.Length;

		#endregion Private Properties

		#region Public Constructors

		/// <summary>
		/// Creates instance of type
		/// </summary>
		public Iso8583Parser(ILogger<Iso8583Parser> logger, byte[] fileContent, Iso8583ParserConfig configuration) : base(logger, fileContent, configuration)
		{
			LoadElementConfiguration();
		}

		#endregion Public Constructors

		#region Private Destructors

		~Iso8583Parser()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(false);
		}

		#endregion Private Destructors

		#region Public Methods

		// This code added to correctly implement the disposable pattern.
		public override void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above. GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Start processing file.
		/// </summary>
		public override void Parse()
		{
			_currentPosition = 0;
			Documents.Clear();
			LoadFileContents();

			while (_currentPosition < TotalLength)
			{
				// MESSAGE SIZE - not encoded
				var subArray = SubArray(FileContent, _currentPosition, MESSAGE_SIZE_LENGTH);
				Move(MESSAGE_SIZE_LENGTH);

				var hexRDW = GetHexString(subArray);

				var rdw = int.Parse(hexRDW, NumberStyles.HexNumber);

				if (rdw > 0)
				{
					var message = CreateMessage(GetData(GetDataAndMove(MTI_LENGTH)), rdw);

					byte[] bitmap;
					var bits = GetBitmapBits(out bitmap);
					message.DataElements.Add(CreateDataElement(1, GetHexString(bitmap.Skip(8).Take(8).ToArray())));
					_impParserConfig.DataElements.LoadPrimaryBitmap(bits);

					foreach (var field in _impParserConfig.DataElements)
					{
						var fk = field.Key;
						var fv = field.Value;
						if (fv.Exists && fk > 1)
						{
							var dataElement = CreateDataElement(fk);

							byte[] data = { };
							var length = 0;

							if (fv.DataLength.IsVariable)
							{
								var dataGet = GetData(GetDataAndMove(fv.DataLength.Length));
								length = Convert.ToInt32(dataGet);
								data = GetDataAndMove(length);
							}
							else
							{
								length = fv.DataLength.Length;
								data = GetDataAndMove(length);
							}

							if (data != null && data.Length > 0)
							{
								dataElement.Value = GetData(data);

								if (fv.Fields != null)
								{
									GetSubFields(dataElement, fv.Fields, data);
								}

								if (fv.Elements != null)
								{
									GetSubElements(dataElement, fv.Elements, data);
								}

								dataElement.Length = length;
								message.DataElements.Add(dataElement);
							}
						}
					}

					Documents.Add(message);
				}
				else if (rdw == 0)
				{
					_currentPosition = FileContent.Length;
				}
				else if (rdw < 0)
				{
					var msg = $"MSG ({rdw} bytes). Error reading binary stream, message length is negative.";
					var exc = new Exception(msg);
					Log.LogError(exc, msg);

					throw exc;
				}
			}
		}

		#endregion Public Methods

		#region Protected Methods

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				MessageDataProcessor = null;
				Documents.Clear();

				disposedValue = true;
			}
		}

		#endregion Protected Methods

		#region Private Methods

		private DataElement CreateDataElement(int code, string value = null)
		{
			value = value?.Trim();

			return new DataElement
			{
				Code = code,
				Value = string.IsNullOrEmpty(value) ? null : value
			};
		}

		private Message CreateMessage(string typeIdentifier, int size)
		{
			return new Message
			{
				DataProcessor = this.MessageDataProcessor,
				ParserConfiguration = _impParserConfig,
				MIT = MessageTypeIndicator.Parse(typeIdentifier),
				Size = size,
				DateProcessed = DateTime.Now
			};
		}

		private bool[] GetBitmapBits(out byte[] bitmap)
		{
			var retVal = new bool[128];

			bitmap = SubArray(FileContent, _currentPosition, BITMAP_LENGTH);
			for (var i = 0; i < bitmap.Length; i++)
			{
				for (var j = 0; j < 8; j++)
				{
					retVal[i * 8 + j] = (bitmap[i] & (128 / (int)Math.Pow(2, j))) > 0;
				}
			}
			Move(BITMAP_LENGTH);
			return retVal;
		}

		private string GetData(IEnumerable<byte> byteArray)
		{
			return
				Encoding.ASCII.GetString(
					Encoding.Convert(ParserConfiguration.SourceEncoding, ParserConfiguration.TargetEncoding, byteArray.ToArray()));
		}

		private byte[] GetDataAndMove(int len)
		{
			var retVal = SubArray(FileContent, _currentPosition, len);
			Move(len);
			return retVal;
		}

		private string GetHexString(byte[] byteArray)
		{
			return string.Concat(Array.ConvertAll(byteArray, b => b.ToString("X2")));
		}

		private void GetSubElements(DataElement element, SubElementInfo subElementInfo, byte[] data)
		{
			if (subElementInfo == null) return;
			if (data == null) return;

			var pos = 0;
			while (pos < data.Length)
			{
				var tag = GetData(data.Skip(pos).Take(subElementInfo.TagLength).ToArray());
				pos = pos + subElementInfo.TagLength;
				var dataLen = int.Parse(GetData(data.Skip(pos).Take(subElementInfo.DataLength).ToArray()));
				pos = pos + subElementInfo.DataLength;

				var dataValBytes = data.Skip(pos).Take(dataLen).ToArray();
				var dataVal = GetData(dataValBytes);
				pos = pos + dataLen;

				List<SubField> subfields = null;
				if (_impParserConfig.SubDataElementFields.ContainsKey(tag))
				{
					subfields = GetSubFields(_impParserConfig.SubDataElementFields[tag], dataValBytes);
				}

				element.SubElements.Add(new SubElement(int.Parse(tag), dataLen, dataVal, subfields));
			}
		}

		private void GetSubFields(DataElement element, IEnumerable<SubFieldInfo> fieldInfos, byte[] data)
		{
			element.SubFields.AddRange(GetSubFields(fieldInfos, data));
		}

		private List<SubField> GetSubFields(IEnumerable<SubFieldInfo> fieldInfos, byte[] data)
		{
			var retVal = new List<SubField>();

			if (fieldInfos == null || data == null) return retVal;

			var pos = 0;
			foreach (var fi in fieldInfos)
			{
				if (pos < data.Length)
				{
					retVal.Add(new SubField
					{
						Code = fi.Code,
						Name = fi.Name,
						Length = fi.Length,
						Value = GetData(data.Skip(pos).Take(fi.Length))
					});

					pos = pos + fi.Length;
				}
			}

			return retVal;
		}

		private void LoadElementConfiguration()
		{
			if (_impParserConfig == null)
			{
				if (!File.Exists(ParserConfiguration.DataElementConfigPath))
				{
					throw new FileNotFoundException("DataElement JSON configuration file doesn't exists",
						ParserConfiguration.DataElementConfigPath);
				}

				_impParserConfig = JsonSerializer.Deserialize<ParserConfiguration>(
					File.ReadAllText(ParserConfiguration.DataElementConfigPath), new JsonSerializerOptions
					{
						Converters = {
							new DataElementInfoSerializer()
						}
					});
			}
		}

		private void LoadFileContents()
		{
			if (ParserConfiguration.BlockSizeInBytes > 0)
			{
				var temp = new List<byte>();

				var offset = 0;
				var blocks = FileContent.Length / (ParserConfiguration.BlockSizeInBytes + ParserConfiguration.BlockPadding);

				for (var i = 0; i < blocks; i++)
				{
					temp.AddRange(SubArray(FileContent, offset, ParserConfiguration.BlockSizeInBytes));
					offset = (i + 1) * (ParserConfiguration.BlockSizeInBytes + ParserConfiguration.BlockPadding);
				}

				FileContent = temp.ToArray();
			}
		}

		/// <summary>
		/// Move cursor in buffer for specified seek
		/// </summary>
		/// <param name="seek">
		/// </param>
		private void Move(int seek)
		{
			_currentPosition = _currentPosition + seek;
		}

		private T[] SubArray<T>(T[] data, int index, int length)
		{
			var result = new T[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}

		#endregion Private Methods

		// To detect redundant calls
	}
}