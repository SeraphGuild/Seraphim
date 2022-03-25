using Newtonsoft.Json;

namespace Seraphim.Discord;

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
    ///     If the option is a <see cref="ApplicationCommandOptionType.CHANNEL"/> type, the channels
    ///     shown will be restricted these types.
    /// </summary>
    [JsonProperty("channel_types")]
    public IList<ChannelType>? ChannelTypes { get; private set; }
}