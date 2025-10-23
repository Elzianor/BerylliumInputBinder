namespace BerylliumInputBinder;

public enum ButtonState
{
    Pressed,
    Down
}

public class RawButtonInput : RawInput
{
    public ButtonState State { get; init; }
}
