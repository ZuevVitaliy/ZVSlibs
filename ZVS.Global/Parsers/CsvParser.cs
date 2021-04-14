using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZVS.Global.Parsers
{
    public class CsvParser
    {
        public char Delimiter { get; set; } = ',';

        public string ToCsv<T>(IEnumerable<T> source)
        {
            StringBuilder builder = new StringBuilder();
            var propertiesInfos = typeof(T).GetProperties();
            foreach (var propertyName in propertiesInfos.Select(x => x.Name))
            {
                builder.Append(propertyName + Delimiter);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.AppendLine();

            foreach (var item in source)
            {
                foreach (var propertyInfo in propertiesInfos)
                {
                    builder.Append(propertyInfo.GetValue(item).ToString() + Delimiter);
                }
                builder.Remove(builder.Length - 1, 1);
                builder.AppendLine();
            }

            return builder.Remove(builder.Length - 1, 1).ToString();
        }

        public IEnumerable<T> ToObject<T>(string source) where T : new()
        {
            List<T> result = new List<T>();
            source = source.Replace('\r', '\n');
            var lines = source.Split('\n').Where(x => !string.IsNullOrEmpty(x)).ToList();
            var propertiesNames = lines.First().Split(Delimiter);
            lines.RemoveAt(0);
            var propertyInfos = propertiesNames.Select(x => typeof(T).GetProperty(x)).ToList();
            foreach (var line in lines)
            {
                T obj = new T();
                var data = line.Split(Delimiter);
                for (int i = 0; i < propertyInfos.Count; i++)
                {
                    propertyInfos[i].SetValue(obj, data[i]);
                }
                result.Add(obj);
            }

            return result;
        }
    }
}