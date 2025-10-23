namespace BerylliumInputBinder;

public class AxisInput : Input
{
    public bool IsPositive { get; init; }

    public override bool Equals(object obj)
    {
        return obj is AxisInput other &&
               base.Equals(obj) &&
               IsPositive == other.IsPositive;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), IsPositive);
    }
}
