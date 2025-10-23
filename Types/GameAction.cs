namespace BerylliumInputBinder;

public enum GameActionType
{
    Single,
    Сontinuous
}

public class GameAction
{
    public string Id { get; init; }
    public GameActionType Type { get; init; }
    public string Description { get; set; }
    public Action Action { get; init; }

    public override bool Equals(object obj)
    {
        return obj is GameAction other &&
               Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
