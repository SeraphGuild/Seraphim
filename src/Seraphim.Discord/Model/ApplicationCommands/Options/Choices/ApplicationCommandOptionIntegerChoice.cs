namespace Discord.API;

public class ApplicationCommandOptionIntegerChoice
{
    public ApplicationCommandOptionIntegerChoice(int value)
    {
        this.Value = value;
    }

    public int Value { get; private set; }
}