﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Dom_zdravlja.Utilities
{
    public class DateConverter : JsonConverter<DateTime>
    {
        private readonly string _dateFormat = "dd'/'MM'/'yyyy";

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(_dateFormat));
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var dateString = (string)reader.Value;

            if (DateTime.TryParseExact(dateString, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            throw new JsonSerializationException($"Unexpected date format: {dateString}. Expected format: {_dateFormat}.");
        }
    }
}