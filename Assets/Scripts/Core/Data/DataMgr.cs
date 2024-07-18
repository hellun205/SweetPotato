using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace Core.Data
{
  public static class DataMgr
  {
    public static T[] FromCsv<T>(string path)
    {
      using var reader = new StreamReader(path);
      using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
      return csv.GetRecords<T>().ToArray();
    }
  }
}
