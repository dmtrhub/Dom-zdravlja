using Dom_zdravlja.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Dom_zdravlja.Services
{
    public class JsonFileService<T>
    {
        private readonly string _filePath;

        public JsonFileService(string filePath)
        {
            _filePath = filePath;
        }

        public List<T> GetAll()
        {
            var json = File.ReadAllText(_filePath);

            var settings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };
            settings.Converters.Add(new CustomDateTimeConverter());

            return JsonConvert.DeserializeObject<List<T>>(json, settings);
        }

        public void Add(T item)
        {
            var items = GetAll();
            items.Add(item);
            var json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}