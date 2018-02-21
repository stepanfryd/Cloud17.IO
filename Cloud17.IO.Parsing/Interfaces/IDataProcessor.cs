using System.Threading.Tasks;

namespace Cloud17.IO.Parsing.Interfaces
{
	public interface IDataProcessor
	{
		Task ProcessDataAsync(object document, ISourceFile reportFile);
	}
}