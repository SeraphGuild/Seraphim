using System.Text.Json;
using System.Text.Json.Serialization;

namespace Seraphim.Discord;

internal class ApplicationCommandJsonConverter : JsonConverter<ApplicationCommand>
{
    public override ApplicationCommand? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ApplicationCommand value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
