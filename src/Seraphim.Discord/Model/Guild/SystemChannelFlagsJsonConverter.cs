using System.Text.Json;
using System.Text.Json.Serialization;

namespace Discord.Core;

internal class SystemChannelFlagsJsonConverter : JsonConverter<SystemChannelFlags>
{
    public override SystemChannelFlags? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new SystemChannelFlags(reader.GetInt32());
    }

    public override void Write(Utf8JsonWriter writer, SystemChannelFlags value, JsonSerializerOptions options)
    {
        writer.WriteNumber("system_channel_flags", value.Value);
    }
}
