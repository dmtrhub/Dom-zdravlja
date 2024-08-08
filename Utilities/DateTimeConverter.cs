using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Dom_zdravlja.Utilities
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _dateTimeFormat = "dd'/'MM'/'yyyy HH:mm";

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(_dateTimeFormat));
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var dateTimeString = (string)reader.Value;

            if (DateTime.TryParseExact(dateTimeString, _dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
            {
                return dateTime;
            }

            throw new JsonSerializationException($"Unexpected date/time format: {dateTimeString}. Expected format: {_dateTimeFormat}.");
        }
    }
}