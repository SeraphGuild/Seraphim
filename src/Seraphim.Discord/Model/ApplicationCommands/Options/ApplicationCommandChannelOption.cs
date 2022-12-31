using System.Text.Json.Serialization;

namespace Discord.API;

public class ApplicationCommandChannelOption : ApplicationCommandOption
{
    public ApplicationCommandChannelOption(
        ApplicationCommandOptionType type,
        string name,
        string description,
        IDictionary<string, string>? nameLocalizations = null,
        IDictionary<string, string>? descriptionLocalizations = null,
        bool? required = null,
        IList<ChannelType>? channelTypes = null) : base(
            type, 
            name,
            description,
            nameLocalizations,
            descriptionLocalizations,
            required)
    {
        ChannelTypes = channelTypes;
    }

    /// <summary>
    ///     The list of <see cref="ChannelType"/>s the option will be restricted to.
    /// </summary>
    [JsonPropertyName("channel_types")]
    public IList<ChannelType>? ChannelTypes { get; private set; }
}