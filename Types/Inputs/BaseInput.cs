namespace BerylliumInputBinder.Types.Inputs;

public abstract class BaseInput
{
    internal readonly InputSources Source;
    internal readonly InputTypes Type;
    internal readonly int PlayerNumber;

    public object Representation { get; init; }

    protected BaseInput(InputSources source, InputTypes type, int playerNumber)
    {
        Source = source;
        Type = type;
        PlayerNumber = playerNumber;
    }

    public abstract override bool Equals(object obj);
    public abstract override int GetHashCode();

    protected int CalculateHashCode(int id, int modifier)
    {
        return HashCode.Combine(Source, Type, id, modifier);
    }
}
