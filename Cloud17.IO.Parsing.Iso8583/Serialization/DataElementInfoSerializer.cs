using Cloud17.IO.Parsing.Iso8583.Configuration;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cloud17.IO.Parsing.Iso8583.Serialization
{
	public class DataElementInfoSerializer : JsonConverter<DataElementInfo>
	{
		private const string Variable = "var";
		private const string Fixed = "fix";
		private const string Elements = "elements";
		private const string Fields = "fields";

		public override DataElementInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
			{
				throw new JsonException();
			}

			var retVal = new DataElementInfo();

			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
				{
					return retVal;
				}

				switch (reader.TokenType)
				{
					case JsonTokenType.PropertyName:
						string propertyName = reader.GetString();

						switch (propertyName)
						{
							case Variable:
							case Fixed:
								retVal.DataLength = GetDataLength(propertyName, ref reader);
								break;
							case Fields:
								retVal.Fields = GetFields(ref reader);
								break;
							case Elements:
								retVal.Elements = GetElements(ref reader);
								break;
						}
						break;

				}

					/*
					// For performance, parse with ignoreCase:false first.
					if (!Enum.TryParse(propertyName, ignoreCase: false, out TKey key) &&
							!Enum.TryParse(propertyName, ignoreCase: true, out key))
					{
						throw new JsonException(
								$"Unable to convert \"{propertyName}\" to Enum \"{_keyType}\".");
					}

					// Get the value.
					TValue value;
					if (_valueConverter != null)
					{
						reader.Read();
						value = _valueConverter.Read(ref reader, _valueType, options);
					}
					else
					{
						value = JsonSerializer.Deserialize<TValue>(ref reader, options);
					}
					*/
				
			}

			return retVal;
		}

		private List<SubFieldInfo> GetFields(ref Utf8JsonReader reader)
		{
			reader.Read();
			return JsonSerializer.Deserialize<List<SubFieldInfo>>(ref reader);
		}

		private SubElementInfo GetElements(ref Utf8JsonReader reader)
		{
			reader.Read();
			return JsonSerializer.Deserialize<SubElementInfo>(ref reader);
		}

		/*
		 
		// There is no value (as distinct from System.Text.Json.JsonTokenType.Null).
		None,
		// The token type is the start of a JSON object.
		StartObject,
		// The token type is the end of a JSON object.
		EndObject,
		// The token type is the start of a JSON array.
		StartArray,
		// The token type is the end of a JSON array.
		EndArray,
		// The token type is a JSON property name.
		PropertyName,
		// The token type is a comment string.
		Comment,
		// The token type is a JSON string.
		String,
		// The token type is a JSON number.
		Number,
		// The token type is the JSON literal true.
		True,
		// The token type is the JSON literal false.
		False,
		// The token type is the JSON literal null.
		Null


		 */

		public override void Write(Utf8JsonWriter writer, DataElementInfo value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		private DataLength GetDataLength(string type, ref Utf8JsonReader reader)
		{
			var retVal = new DataLength
			{
				IsVariable = type == Variable
			};

			reader.Read();
			if(reader.TokenType==JsonTokenType.Number)
			{
				retVal.Length = reader.GetInt32();
			}

			return retVal;
		}
	}
}
