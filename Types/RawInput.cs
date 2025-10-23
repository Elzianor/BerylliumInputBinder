namespace BerylliumInputBinder;

public abstract class RawInput
{
    public DeviceType DeviceType { get; init; }
    public int Code { get; init; }

    public override bool Equals(object obj)
    {
        return obj is RawInput other &&
               GetType() == other.GetType() &&
               DeviceType == other.DeviceType &&
               Code == other.Code;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), DeviceType, Code);
    }
}
