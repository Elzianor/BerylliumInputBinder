namespace BerylliumInputBinder.Types.Inputs;

public class ButtonInput : BaseInput, IEquatable<ButtonInput>
{
    private int _hashCode;
    private readonly int _button;

    internal ButtonModifiers Modifier
    {
        get;
        set
        {
            field = value;
            SetHashCode();
        }
    }

    public ButtonStates State { get; init; }

    public ButtonInput(InputSources source, int button, ButtonModifiers modifier = ButtonModifiers.None)
        : base(source, InputTypes.Button)
    {
        _button = button;

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
