using System;
using System.Buffers;
using System.Buffers.Text;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Util.Json
{
    public class TextJsonConvert
    {
        public class DateTimeConverter : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    if (DateTime.TryParse(reader.GetString(), out DateTime date))
                        return date;
                }
                return reader.GetDateTime();
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        public class DateTimeNullableConverter : JsonConverter<DateTime?>
        {
            public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return string.IsNullOrEmpty(reader.GetString()) ? default(DateTime?) : DateTime.Parse(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value?.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        /// <summary>
        /// long主键转string
        /// </summary>
        public class LongToStringConverter : System.Text.Json.Serialization.JsonConverter<long>
        {
            public override long Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
                    if (Utf8Parser.TryParse(span, out long number, out int bytesConsumed) && span.Length == bytesConsumed)
                        return number;

                    if (long.TryParse(reader.GetString(), out number))
                        return number;
                }

                return reader.GetInt64();
            }

            public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }

        /// <summary>
        /// string转int32
        /// </summary>
        public class Int32Converter : JsonConverter<int>
        {
            public override int Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
                    if (Utf8Parser.TryParse(span, out int number, out int bytesConsumed) && span.Length == bytesConsumed)
                        return number;

                    if (int.TryParse(reader.GetString(), out number))
                        return number;
                }

                return reader.GetInt32();
            }

            public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
            {
                writer.WriteNumberValue(value);
            }
        }

        /// <summary>
        /// string转decimal
        /// </summary>
        public class DecimalConverter : JsonConverter<Decimal>
        {
            public override decimal Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
                    if (Utf8Parser.TryParse(span, out decimal number, out int bytesConsumed) && span.Length == bytesConsumed)
                        return number;

                    if (decimal.TryParse(reader.GetString(), out number))
                        return number;
                }

                return reader.GetDecimal();
            }

            public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
            {
                writer.WriteNumberValue(value);
            }
        }

        /// <summary>
        /// int转string
        /// </summary>
        public class StringConverter : JsonConverter<string>
        {
            public override string Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Number)
                {
                    if (reader.TryGetInt32(out int number))
                        return number.ToString();
                    if (reader.TryGetInt64(out long longNumber))
                        return longNumber.ToString();
                }

                return reader.GetString();
            }

            public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value);
            }
        }
    }
}
