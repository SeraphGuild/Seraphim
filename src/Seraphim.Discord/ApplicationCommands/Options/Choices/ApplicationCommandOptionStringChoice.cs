using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Seraphim.Discord;

public class ApplicationCommandOptionStringChoice
{
    public ApplicationCommandOptionStringChoice(string value)
    {
        this.Value = value;
    }

    [MaxLength(100)]
    public string Value { get; private set; }
}