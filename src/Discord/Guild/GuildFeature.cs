namespace Discord;

public abstract record GuildFeature(string FeatureName)
{
    public record ANIMATED_BANNER() : GuildFeature(nameof(ANIMATED_BANNER));

    public record ANIMATED_ICON() : GuildFeature(nameof(ANIMATED_ICON));

    public record APPLICATION_COMMAND_PERMISSIONS_V2() : GuildFeature(nameof(APPLICATION_COMMAND_PERMISSIONS_V2));

    public record AUTO_MODERATION() : GuildFeature(nameof(AUTO_MODERATION));

    public record BANNER() : GuildFeature(nameof(BANNER));

    public record DEVELOPER_SUPPORT_SERVER() : GuildFeature(nameof(DEVELOPER_SUPPORT_SERVER));

    public record FEATURABLE() : GuildFeature(nameof(FEATURABLE));

    public record INVITE_SPLASH() : GuildFeature(nameof(INVITE_SPLASH));

    public record MEMBER_VERIFICATION_GATE_ENABLED() : GuildFeature(nameof(MEMBER_VERIFICATION_GATE_ENABLED));

    public record MONETIZATION_ENABLED() : GuildFeature(nameof(MONETIZATION_ENABLED));

    public record MORE_STICKERS() : GuildFeature(nameof(MORE_STICKERS));

    public record NEWS() : GuildFeature(nameof(NEWS));

    public record PARTNERED() : GuildFeature(nameof(PARTNERED));

    public record PREVIEW_ENABLED() : GuildFeature(nameof(PREVIEW_ENABLED));

    public record ROLE_ICONS() : GuildFeature(nameof(ROLE_ICONS));

    public record TICKETED_EVENTS_ENABLED() : GuildFeature(nameof(TICKETED_EVENTS_ENABLED));

    public record VANITY_URL() : GuildFeature(nameof(VANITY_URL));

    public record VERIFIED() : GuildFeature(nameof(VERIFIED));

    public record VIP_REGIONS() : GuildFeature(nameof(VIP_REGIONS));

    public record WELCOME_SCREEN_ENABLED() : GuildFeature(nameof(WELCOME_SCREEN_ENABLED));

    public abstract record MutableGuildFeature(string FeatureName) : GuildFeature(FeatureName);

    public record COMMUNITY() : MutableGuildFeature(nameof(COMMUNITY));

    public record DISCOVERABLE(): MutableGuildFeature(nameof(DISCOVERABLE));

    public record INVITES_DISABLED(): MutableGuildFeature(nameof(INVITES_DISABLED));
}