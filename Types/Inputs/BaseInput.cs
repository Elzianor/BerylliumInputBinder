namespace BerylliumInputBinder.Types.Inputs;

public abstract class BaseInput
{
    internal readonly InputSources Source;
    internal readonly InputTypes Type;

    public object Representation { get; init; }
    public int PlayerNumber { get; init; }

    protected BaseInput(InputSources source, InputTypes type)
    {
        Source = source;
        Type = type;
    }

    public abstract override bool Equals(object obj);
    public abstract override int GetHashCode();

    protected int CalculateHashCode(int id, int modifier)
    {
        return HashCode.Combine(Source, Type, id, modifier);
    }
}
