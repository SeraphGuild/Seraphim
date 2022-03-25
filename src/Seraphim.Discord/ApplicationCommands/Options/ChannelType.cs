namespace Seraphim.Discord;

public enum ChannelType
{
    /// <summary>
    ///     A standard text channel in a server
    /// </summary>
    GUILD_TEXT = 0,

    DM = 1,

    GUILD_VOICE = 2,

    GROUP_DM = 3,

    /// <summary>
    ///     An organizational structure for channels in a srver
    /// </summary>
    GUILD_CATEGORY = 4,

    /// <summary>
    ///     A channel users can follow and crosspost into their own server
    /// </summary>
    GUILD_NEWS = 5,

    /// <summary>
    ///     A channel that game developers can sell their game in
    /// </summary>
    GUILD_STORE = 6,

    /// <summary>
    ///     A temporary sub-channel within a <see cref="GUILD_NEWS"/> channel
    /// </summary>
    GUILD_NEWS_THREAD = 10,

    /// <summary>
    ///     A temporary sub-channel within a <see cref="GUILD_TEXT"/> channel
    /// </summary>
    GUILD_PUBLIC_THREAD = 11,

    /// <summary>
    ///     A temporary sub-channel within a <see cref="GUILD_TEXT"/> channel
    ///     that is only viewable by those invited and those with the MANAGE_THREADS
    ///     permission
    /// </summary>
    GUILD_PRIVATE_THREAD = 12,

    /// <summary>
    ///     A voice channel for hosting events with an auidence
    /// </summary>
    GUILD_STAGE_VOICE = 13
}