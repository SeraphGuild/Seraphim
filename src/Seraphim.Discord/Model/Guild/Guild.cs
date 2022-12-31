using Discord.API;
using System.Text.Json.Serialization;

namespace Discord.Core;

public class Guild
{
    [JsonConstructor]
    public Guild(
        Snowflake id,
        string name,
        string? icon,
        string? iconHash,
        string? splash,
        string? discovertySplash,
        bool? owner,
        Snowflake ownerId,
        string? permissions,
        string? region,
        Snowflake? AFKChannelId,
        int? aFKTimeout,
        bool? widgetEnabled,
        Snowflake? widgetChannelId,
        VerificationLevel? verificationLevel,
        DefaultMessageNotificationLevel? defaultMessageNotifications,
        ExplicitContentFilterLevel? explicitContentFilter,
        Role[] roles,
        Emojis[] emojis,
        GuildFeature[] features,
        MFALevel? MFALevel,
        Snowflake? applicationId,
        Snowflake? systemChannelId,
        SystemChannelFlags? systemChannelFlags,
        Snowflake? rulesChannelId,
        int? maxPresences,
        int? maxMembers,
        string? vanityUrlCode,
        string? description,
        string? banner,
        PremiumTier? premiumTier,
        int? premiumSubscriptionCount,
        string? preferredLocale,
        Snowflake? publicUpdatesChannelId,
        int? maxVideoChannelUsers,
        int? approximateMemberCount,
        int? approximatePresenceCount,
        WelcomeScreen? welcomeScreen,
        bool? NSFW,
        GuildNSFWLevel? NSFWLevel,
        Sticker[] stickers,
        bool? premiumProgressBarEnabled)
    {
        Id = id;
        Name = name;
        Icon = icon;
        IconHash = iconHash;
        Splash = splash;
        DiscovertySplash = discovertySplash;
        Owner = owner;
        OwnerId = ownerId;
        Permissions = permissions;
        Region = region;
        this.AFKChannelId = AFKChannelId;
        AFKTimeout = aFKTimeout;
        WidgetEnabled = widgetEnabled;
        WidgetChannelId = widgetChannelId;
        VerificationLevel = verificationLevel;
        DefaultMessageNotifications = defaultMessageNotifications;
        ExplicitContentFilter = explicitContentFilter;
        Roles = roles;
        Emojis = emojis;
        Features = features;
        this.MFALevel = MFALevel;
        ApplicationId = applicationId;
        SystemChannelId = systemChannelId;
        SystemChannelFlags = systemChannelFlags;
        RulesChannelId = rulesChannelId;
        MaxPresences = maxPresences;
        MaxMembers = maxMembers;
        VanityUrlCode = vanityUrlCode;
        Description = description;
        Banner = banner;
        PremiumTier = premiumTier;
        PremiumSubscriptionCount = premiumSubscriptionCount;
        PreferredLocale = preferredLocale;
        PublicUpdatesChannelId = publicUpdatesChannelId;
        MaxVideoChannelUsers = maxVideoChannelUsers;
        ApproximateMemberCount = approximateMemberCount;
        ApproximatePresenceCount = approximatePresenceCount;
        WelcomeScreen = welcomeScreen;
        NSFW = NSFW;
        this.NSFWLevel = NSFWLevel;
        Stickers = stickers;
        PremiumProgressBarEnabled = premiumProgressBarEnabled;
    }

    public Snowflake Id { get; }

    public string Name { get; }

    public string? Icon { get; }

    public string? IconHash { get; }

    public string? Splash { get; }

    public string? DiscovertySplash { get; }

    public bool? Owner { get; }

    public Snowflake OwnerId { get; }

    public string? Permissions { get; }

    [Obsolete]
    public string? Region { get; }

    public Snowflake? AFKChannelId { get; }

    public int? AFKTimeout { get; }

    public bool? WidgetEnabled { get; }

    public Snowflake? WidgetChannelId { get; }

    public VerificationLevel? VerificationLevel { get; }

    public DefaultMessageNotificationLevel? DefaultMessageNotifications { get; }

    public ExplicitContentFilterLevel? ExplicitContentFilter { get; }

    public Role[] Roles { get; }

    public Emojis[] Emojis { get; }

    public GuildFeature[] Features { get; }

    public MFALevel? MFALevel { get; }

    public Snowflake? ApplicationId { get; }

    public Snowflake? SystemChannelId { get; }

    public SystemChannelFlags? SystemChannelFlags { get; }

    public Snowflake? RulesChannelId { get; }

    public int? MaxPresences { get; }

    public int? MaxMembers { get; }

    public string? VanityUrlCode { get; }

    public string? Description { get; }

    public string? Banner { get; }

    public PremiumTier? PremiumTier { get; }

    public int? PremiumSubscriptionCount { get; }

    public string? PreferredLocale { get; }

    public Snowflake? PublicUpdatesChannelId { get; }

    public int? MaxVideoChannelUsers { get; }

    public int? ApproximateMemberCount { get; }

    public int? ApproximatePresenceCount { get; }

    public WelcomeScreen? WelcomeScreen { get; }

    public bool? NSFW { get; }

    public GuildNSFWLevel? NSFWLevel { get; }

    public Sticker[] Stickers { get; }

    public bool? PremiumProgressBarEnabled { get; }

    public Task<Channel[]> GetGuildChannels()
    {
        throw new NotImplementedException();
    }

    public Task<Channel> CreateGuildChannel(Channel channel)
    {
        throw new NotImplementedException();
    }

    public Task<object> ListActiveGuildThreads()
    {
        throw new NotImplementedException();
    }

    public Task<object> GetGuildMember(Snowflake userId)
    {
        throw new NotImplementedException();
    }

    public Task<object> ListGuildMembers()
    {
        throw new NotImplementedException();
    }

    public Task<object> SearchGuildMembers()
    {
        throw new NotImplementedException();
    }

    public Task<object> AddGuildMember(Snowflake userId)
    {
        throw new NotImplementedException();
    }

    public Task<object> ModifyGuildMember(Snowflake userId)
    {
        throw new NotImplementedException();
    }

    public Task<object> AddGuildMemberRole(Snowflake userId, Snowflake roleId)
    {
        throw new NotImplementedException();
    }

    public Task<object> RemoveGuildMemberRole(Snowflake userId, Snowflake roleId)
    {
        throw new NotImplementedException();
    }

    public Task<object[]> GetGuildRoles()
    {
        throw new NotImplementedException();
    }
}