namespace BerylliumInputBinder.Types.Inputs;

public class ButtonInput : BaseInput, IEquatable<ButtonInput>
{
    private int _hashCode;
    private readonly int _button;

    internal readonly ButtonStates State;

    internal ButtonModifiers Modifier
    {
        get;
        set
        {
            field = value;
            SetHashCode();
        }
    }

    public ButtonInput(InputSources source,
        int button,
        ButtonStates state,
        ButtonModifiers modifier = ButtonModifiers.None,
        int playerNumber = 0)
        : base(source, InputTypes.Button, playerNumber)
    {
        _button = button;
        State = state;

        Modifier = source == InputSources.Keyboard ? modifier : ButtonModifiers.None;
    }

    #region Equality
    public bool Equals(ButtonInput other)
    {
        if (other is null) return false;

        return Source == other.Source &&
               Type == other.Type &&
               _button == other._button &&
               Modifier == other.Modifier;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as ButtonInput);
    }

    public static bool operator ==(ButtonInput left, ButtonInput right)
    {
        if (ReferenceEquals(left, right)) return true;

        if (left is null ||
            right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(ButtonInput left, ButtonInput right)
    {
        return !(left == right);
    }
    #endregion

    #region Hash code
    private void SetHashCode()
    {
        _hashCode = CalculateHashCode(_button, (int)Modifier);
    }

    public override int GetHashCode()
    {
        return _hashCode;
    }
    #endregion
}
