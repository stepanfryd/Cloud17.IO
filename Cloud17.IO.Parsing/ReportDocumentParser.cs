using Cloud17.IO.Parsing.Configuration;
using Cloud17.IO.Parsing.Interfaces;

using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Cloud17.IO.Parsing
{
	/// <summary>
	///   Main class responsible for document parsing.
	/// </summary>
	public class ReportDocumentParser
	{
		#region Private fields

		private readonly DocumentSettings _configuration;
		private readonly IReportDocument _entity;
		private readonly Type _entityType;
		private readonly ILogger _logger;

		#endregion Private fields

		#region Constructors

		private ReportDocumentParser(ILogger<ReportDocumentParser> logger, string textBlock, DocumentSettings configuration)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			if (string.IsNullOrEmpty(textBlock)) throw new ArgumentNullException(nameof(textBlock));
			TextBlock = textBlock;
			_entityType = _configuration.DataEntity;
			_entity = (IReportDocument)Activator.CreateInstance(_entityType);

			if (_configuration.DataProcessor != null)
			{
				_entity.DataProcessor = (IDataProcessor)Activator.CreateInstance(configuration.DataProcessor);
			}
			_entity.OriginalDocument = textBlock;
		}

		#endregion Constructors

		#region Public

		/// <summary>
		///   Text block which contains raw text data
		/// </summary>
		protected string TextBlock { get; }

		#endregion Public

		/// <summary>
		///   Metod provides raw document parsing using documetn configuration
		/// </summary>
		/// <returns></returns>
		public IReportDocument ParseDocument()
		{
			foreach (var mapping in _configuration.Mappings)
			{
				if (mapping.Pattern == null && mapping.ValueParser == null) continue;

				var prop = _entityType.GetProperty(mapping.Property);
				if (prop == null) continue;

				if (mapping.Pattern != null)
				{
					if (prop.PropertyType.IsArray)
					{
						GetMatches(prop.PropertyType.GetElementType(), mapping.Pattern, mapping.Pattern.Matches(TextBlock), prop, _entity);
					}
					else if (prop.PropertyType.Module.ScopeName != "CommonLanguageRuntimeLibrary")
					{
						var propInst = Activator.CreateInstance(prop.PropertyType);
						prop.SetValue(_entity, propInst);
						MatchComplexType(propInst, prop.Name, '_', 0, 5, mapping.Pattern.Match(TextBlock));
					}
					else
					{
						GetMatch(mapping.Pattern.Match(TextBlock), prop, mapping, _entity);
					}
				}

				if (mapping.ValueParser == null) continue;

				var textToParse = TextBlock;
				if (mapping.Pattern != null)
				{
					var match = mapping.Pattern.Match(TextBlock);
					if (match.Success)
					{
						textToParse = match.Value.Trim();
					}
				}

				if (!string.IsNullOrEmpty(textToParse))
				{
					GetValueParser(textToParse, prop, mapping, _entity);
				}
			}

			return _entity;
		}

		private void GetValueParser(string textToParse, PropertyInfo prop, ItemMapping mapping, IReportDocument reportDocumentEntity)
		{
			if (string.IsNullOrEmpty(textToParse)) return;
			try
			{
				prop.SetValue(reportDocumentEntity, mapping.ValueParser.Parser.Parse(textToParse));
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"There is an error in custom value parser for type ${reportDocumentEntity.GetType()} and property ${prop.Name}");
			}
		}

		private void GetMatches(Type type, Regex regex, MatchCollection matches, PropertyInfo baseProperty,
			object entity)
		{
			var groupNames = regex.GetGroupNames();
			var props =
				type.GetProperties(BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance)
					.Where(prop => prop.CanWrite && groupNames.Contains(prop.Name))
					.ToList();

			if (groupNames.Length == 0) return;

			var arr = Array.CreateInstance(type, matches.Count);

			for (var i = 0; i < matches.Count; i++)
			{
				var match = matches[i];
				var item = Activator.CreateInstance(type);

				foreach (var itmProp in props)
				{
					GetMatch(match, itmProp, null, item);
				}
				arr.SetValue(item, i);
			}

			baseProperty.SetValue(entity, arr);
		}

		private void GetMatch(Match match, PropertyInfo prop, ItemMapping mapping, object entity)
		{
			var pName = prop.Name;
			var preVal = match.Value;
			var group = match.Groups[pName];

			if (group.Success)
			{
				preVal = group.Value.Trim();
			}
			else
			{
				if (mapping?.MatchIndex != null)
				{
					group = match.Groups[mapping.MatchIndex.Value];
					if (group.Success)
					{
						preVal = group.Value;
					}
				}
			}

			// Empty value is ignored
			if (string.IsNullOrEmpty(preVal)) return;

			object objVal = null;

			if (!string.IsNullOrEmpty(mapping?.ValuePrepocess))
			{
				objVal = Eval.Invoke(string.Format(mapping.ValuePrepocess, preVal));
			}
			else
			{
				if (prop.PropertyType == typeof(bool))
				{
					objVal = match.Success;
				}
				else if (prop.PropertyType == typeof(int) && int.TryParse(preVal, System.Globalization.NumberStyles.Number, _configuration.DocumentCulture, out int parVal))
				{
					objVal = parVal; // Convert.ChangeType(preVal, typeof(int), _configuration.DocumentCulture);
				}
				else
				{
					if (TryGetVal(preVal, prop.PropertyType, out object tryVal))
					{
						objVal = tryVal;
					}
				}
			}

			if (objVal != null)
			{
				prop.SetValue(entity, objVal);
			}
		}

		private void MatchComplexType(object parentObject, string prePath, char pathSeparator, int depth, int maxDepth,
			Match match)
		{
			if (parentObject == null) return;

			if (depth < maxDepth)
			{
				var type = parentObject.GetType();
				foreach (var prop in type.GetProperties())
				{
					var path = prop.Name;
					if (prePath != null)
					{
						path = (prePath + pathSeparator + prop.Name).TrimStart(pathSeparator);
					}
					if (prop.PropertyType.Module.ScopeName != "CommonLanguageRuntimeLibrary")
					{
						var propVal = prop.GetValue(parentObject);
						if (propVal == null)
						{
							propVal = Activator.CreateInstance(prop.PropertyType);
							prop.SetValue(parentObject, propVal);
						}

						MatchComplexType(propVal, path, pathSeparator, depth++, maxDepth, match);
					}
					else
					{
						if (match.Groups[path].Success)
						{
							var matchValue = match.Groups[path].Value;
							try
							{
								if (prop.PropertyType.IsGenericType)
								{
									var genArgs = prop.PropertyType.GetGenericArguments();
									if (genArgs.Length == 1 && genArgs[0].Module.ScopeName == "CommonLanguageRuntimeLibrary")
									{
										var baseType = genArgs[0];
										var baseValue = Convert.ChangeType(matchValue, baseType, _configuration.DocumentCulture);
										var ctor = prop.PropertyType.GetConstructor(new[] { baseType });
										if (ctor != null)
										{
											prop.SetValue(parentObject, ctor.Invoke(new[] { baseValue }));
										}
									}
								}
								else
								{
									prop.SetValue(parentObject, Convert.ChangeType(matchValue, prop.PropertyType, _configuration.DocumentCulture));
								}
							}
							catch (Exception e)
							{
								_logger.LogError(e, $"Complex type parsing error. PropretyPath: {path}, MatchValue ({matchValue.GetType()}=>{prop.PropertyType}): {matchValue}");
							}
						}
					}
				}
			}
		}

		/// <summary>
		///   Parse input string into document
		/// </summary>
		/// <param name="textBlock">String block to be parsed</param>
		/// <param name="configuration">Instance of document settings configuration</param>
		/// <returns></returns>
		public static IReportDocument Parse(string textBlock, DocumentSettings configuration)
		{
			var parser = new ReportDocumentParser(new Logger<ReportDocumentParser>(new LoggerFactory()), textBlock, configuration);
			return parser.ParseDocument();
		}

		private bool TryGetVal(string value, Type destinationType, out object theValue)
		{
			theValue = null;

			if (string.IsNullOrEmpty(value))
			{
				return false;
			}

			value = value.Trim();

			if (destinationType == typeof(string))
			{
				theValue = value;
				return true;
			}

			try
			{
				theValue = Convert.ChangeType(value, destinationType, _configuration.DocumentCulture);
				return true;
			}
			catch (Exception e)
			{
				_logger.LogWarning(e, $"Problem converting to type {destinationType.FullName} value '{value}'");
				return false;
			}
		}
	}
}