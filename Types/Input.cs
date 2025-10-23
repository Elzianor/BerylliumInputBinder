namespace BerylliumInputBinder;

public enum DeviceType
{
    Keyboard,
    Mouse,
    Gamepad,
    Midi
}

public enum ModifierType
{
    None,
    Ctrl,
    Alt,
    Shift
}

public abstract class Input
{
    public DeviceType DeviceType { get; init; }
    public int Code { get; init; }
    public ModifierType Modifier { get; init; }
    public object Representation { get; set; }

    public override bool Equals(object obj)
    {
        return obj is Input other &&
               GetType() == other.GetType() &&
               DeviceType == other.DeviceType &&
               Code == other.Code &&
               Modifier == other.Modifier;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), DeviceType, Code, Modifier);
    }
}
