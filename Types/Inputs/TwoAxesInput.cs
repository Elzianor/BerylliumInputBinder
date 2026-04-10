namespace BerylliumInputBinder.Types.Inputs;

public class TwoAxesInput : BaseInput, IEquatable<TwoAxesInput>
{
    private readonly int _hashCode;
    private readonly int _axesId;

    internal readonly Vector2 Value;

    public TwoAxesInput(InputSources source, int axesId, Vector2 value, int playerNumber = 0)
        : base(source, InputTypes.TwoAxes, playerNumber)
    {
        _hashCode = CalculateHashCode(axesId, -1);
        _axesId = axesId;
        Value = value;
    }

    #region Equality
    public bool Equals(TwoAxesInput other)
    {
        if (other is null) return false;

        return Source == other.Source &&
               Type == other.Type &&
               _axesId == other._axesId;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as TwoAxesInput);
    }

    public static bool operator ==(TwoAxesInput left, TwoAxesInput right)
    {
        if (ReferenceEquals(left, right)) return true;

        if (left is null ||
            right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(TwoAxesInput left, TwoAxesInput right)
    {
        return !(left == right);
    }
    #endregion

    #region Hash code
    public override int GetHashCode()
    {
        return _hashCode;
    }
    #endregion
}
