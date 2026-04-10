namespace BerylliumInputBinder.Types.Inputs;

public class OneAxisInput : BaseInput, IEquatable<OneAxisInput>
{
    private readonly int _hashCode;
    private readonly int _axisId;

    internal readonly float Value;

    public OneAxisInput(InputSources source, int axisId, float value, int playerNumber = 0)
        : base(source, InputTypes.OneAxis, playerNumber)
    {
        _hashCode = CalculateHashCode(axisId, -1);
        _axisId = axisId;
        Value = value;
    }

    #region Equality
    public bool Equals(OneAxisInput other)
    {
        if (other is null) return false;

        return Source == other.Source &&
               Type == other.Type &&
               _axisId == other._axisId;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as OneAxisInput);
    }

    public static bool operator ==(OneAxisInput left, OneAxisInput right)
    {
        if (ReferenceEquals(left, right)) return true;

        if (left is null ||
            right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(OneAxisInput left, OneAxisInput right)
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
