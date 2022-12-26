using System.Text.Json;
using System.Text.Json.Serialization;

namespace Discord;

internal class SnowflakeJsonConverter : JsonConverter<Snowflake>
{
    public override Snowflake? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new Snowflake(reader.GetString() ?? string.Empty);
    }

    public override void Write(Utf8JsonWriter writer, Snowflake value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
