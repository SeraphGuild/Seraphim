using System.Text.Json.Serialization;

namespace Discord;

/// <summary>
///     A unique id provided by discord applications and APIs
/// </summary>
[JsonConverter(typeof(SnowflakeJsonConverter))]
public class Snowflake
{
    /// <summary>
    ///     The number of milliseconds that have passed from Jan 1, 1970 to Jan 1, 2015
    /// </summary>
    private const long TimestampOffset = 1420070400000;

    /// <summary>
    ///     Provides access to the sub-components of a discord snowflake
    /// </summary>
    /// <param name="id">the decimal representation of the snowflake as a string</param>
    /// <exception cref="ArgumentException">when the given id cannot be parsed as a long</exception>
    public Snowflake(string id)
    {
        if (!long.TryParse(id, out long parsed_id))
        {
            throw new ArgumentException("Cannot parse given snowflake as a long");
        }

        this.Id = parsed_id;
    }

    /// <summary>
    ///     The snowflake's decimal representation
    /// </summary>
    public long Id { get; }

    /// <summary>
    ///     The timestamp encapsulated by the snowflake
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset Timestamp { 
        get {
            long discordEpochTimestamp = (this.Id >> 22);
            long unixTimestamp = discordEpochTimestamp + TimestampOffset;
            return DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp);
        }
    }

    /// <summary>
    ///     Gets or sets the id of the worker that generated the snowflake
    /// </summary>
    [JsonIgnore]
    public short InternalWorkerId
    {
        get
        {
            return (short)((this.Id & 0x3E0000) >> 17);
        }
    }

    /// <summary>
    ///     Gets the id of the process that generated the snowflake
    /// </summary>
    [JsonIgnore]
    public short InternalProcessId
    {
        get
        {
            return (short)((this.Id & 0x1F000) >> 12);
        }
    }

    /// <summary>
    ///     Gets of sets this snowflake's increment. For every ID that is 
    ///     generated on that process, this number is incremented
    /// </summary>
    [JsonIgnore]
    public int Increment
    {
        get 
        { 
            return (int)(this.Id & 0xFFF);
        }
    }

    /// <summary>
    ///     Returns the decimal representation of the snowflake as a string
    /// </summary>
    /// <returns>the decimal representation of the snowflake as a string</returns>
    public override string ToString()
    {
        return this.Id.ToString();
    }
}
