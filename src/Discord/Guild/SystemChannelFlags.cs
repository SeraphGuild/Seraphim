using System.Text.Json.Serialization;

namespace Discord;

[JsonConverter(typeof(SystemChannelFlagsJsonConverter))]
public class SystemChannelFlags
{
    public const int SUPPRESS_JOIN_NOTIFICATIONS = 1 << 0;

    public const int SUPPRESS_PREMIUM_SUBSCRIPTIONS = 1 << 1;

    public const int SUPPRESS_GUILD_REMINDER_NOTIFICATIONS = 1 << 2;

    public const int SUPPRESS_JOIN_NOTIFICATION_REPLIES = 1 << 3;

    public SystemChannelFlags(int value)
    {
        this.Value = value;
    }

    public int Value { get; }

    [JsonIgnore]
    public bool JoinNotificationsSuppressed => (this.Value & SUPPRESS_JOIN_NOTIFICATIONS) == SUPPRESS_JOIN_NOTIFICATIONS;

    [JsonIgnore]
    public bool PremiumSubscriptionsSuppressed => (this.Value & SUPPRESS_PREMIUM_SUBSCRIPTIONS) == SUPPRESS_PREMIUM_SUBSCRIPTIONS;

    [JsonIgnore]
    public bool GuildReminderNotificationsSuppressed => (this.Value & SUPPRESS_GUILD_REMINDER_NOTIFICATIONS) == SUPPRESS_GUILD_REMINDER_NOTIFICATIONS;

    [JsonIgnore]
    public bool JoinNotificationRepliesSuppressed => (this.Value & SUPPRESS_JOIN_NOTIFICATION_REPLIES) == SUPPRESS_JOIN_NOTIFICATION_REPLIES;
}