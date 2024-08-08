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
    public class JsonFileService<T> : IJsonFileService<T>
    {
        private readonly string _filePath;
        private readonly JsonSerializerSettings _jsonSettings;

        public JsonFileService(string filePath)
        {
            _filePath = HttpContext.Current.Server.MapPath(filePath);
            _jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter> { new DateConverter(), new DateTimeConverter() }
            };
        }

        public List<T> Read()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<T>>(json, _jsonSettings);
        }

        public void Write(List<T> items)
        {
            var json = JsonConvert.SerializeObject(items, _jsonSettings);
            File.WriteAllText(_filePath, json);
        }

        public void Update(T item, Predicate<T> predicate)
        {
            var items = Read();
            var index = items.FindIndex(predicate);
            if (index != -1)
            {
                items[index] = item;
                Write(items);
            }
        }
    }
}