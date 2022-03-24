using System.Text.Json;
using System.Text.Json.Serialization;

namespace Seraphim.Discord;

/// <summary>
///     Serializes and deseralizes instances of the <see cref="Snowflake"/> class.
/// </summary>
internal class SnowflakeJsonConverter : JsonConverter<Snowflake>
{
    /// <summary>
    ///     Reads the seralized <see cref="Snowflake"/> class.
    /// </summary>
    /// <param name="reader">the json reader</param>
    /// <param name="typeToConvert">the type that is being converted to</param>
    /// <param name="options">the json serialization option</param>
    /// <returns>the deserialized <see cref="Snowflake"/></returns>
    public override Snowflake? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new Snowflake(reader.GetString() ?? string.Empty);
    }

    /// <summary>
    ///     Serializes an instance of the <see cref="Snowflake"/> class 
    /// </summary>
    /// <param name="writer">the json writer</param>
    /// <param name="value">the value that is being serialized</param>
    /// <param name="options">the json serialization options</param>
    public override void Write(Utf8JsonWriter writer, Snowflake value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
