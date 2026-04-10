namespace BerylliumInputBinder.Types.Actions;

public abstract class BaseAction : IEquatable<BaseAction>
{
    private readonly int _hashCode;

    internal InputTypes Type { get; }

    public string Name { get; }
    public string Description { get; init; }
    public object Representation { get; init; }

    protected BaseAction(InputTypes type, string name)
    {
        _hashCode = name.GetHashCode();

        Type = type;
        Name = name;
    }

    internal abstract void Invoke(BaseInput input);

    #region Equality
    public bool Equals(BaseAction other)
    {
        if (other is null) return false;

        return Name == other.Name;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as BaseAction);
    }

    public static bool operator ==(BaseAction left, BaseAction right)
    {
        if (ReferenceEquals(left, right)) return true;

        if (left is null ||
            right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(BaseAction left, BaseAction right)
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
