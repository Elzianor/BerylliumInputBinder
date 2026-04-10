namespace BerylliumInputBinder.Types;

public enum InputBinderResults
{
    Success,
    Failure,
    InputNull,
    ActionNull,
    IndexOutsideOfRange,
    TypesMismatch,
    InputBoundToOtherAction
}

public enum InputSources
{
    Keyboard,
    Mouse,
    GamePad,
    Midi
}

public enum InputTypes
{
    Button,
    OneAxis,
    TwoAxes
}

public enum ButtonStates
{
    Pressed,
    Down,
    Up
}

public enum ButtonModifiers
{
    None,
    Shift,
    Ctrl,
    Alt
}
