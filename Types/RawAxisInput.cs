namespace BerylliumInputBinder;

public class RawAxisInput : RawInput
{
    public bool IsPositive { get; init; }

    public override bool Equals(object obj)
    {
        return obj is RawAxisInput other &&
               base.Equals(obj) &&
               IsPositive == other.IsPositive;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), IsPositive);
    }
}
