using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Util.Json
{
    public static class JsonUtility
    {
        public static string Serialize<TValue>(TValue value, JsonSerializerOptions options = null)
        {
            if (options == null)
            {
                options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                options.Converters.Add(new TextJsonConvert.DateTimeConverter());
                options.Converters.Add(new TextJsonConvert.DateTimeNullableConverter());
                options.Converters.Add(new TextJsonConvert.LongToStringConverter());
                options.Converters.Add(new TextJsonConvert.Int32Converter());
                options.Converters.Add(new TextJsonConvert.DecimalConverter());
                options.Converters.Add(new TextJsonConvert.Int32Converter());
            }
            return JsonSerializer.Serialize(value, options);
        }
    }
}
