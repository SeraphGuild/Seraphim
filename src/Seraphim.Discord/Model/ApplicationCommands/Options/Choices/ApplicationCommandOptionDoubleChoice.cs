namespace Discord.API;

public class ApplicationCommandOptionDoubleChoice
{
    public ApplicationCommandOptionDoubleChoice(double value)
    {
        this.Value = value;
    }

    public double Value { get; private set; }
}