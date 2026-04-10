namespace BerylliumInputBinder.Types.Actions;

public class TwoAxesAction : BaseAction
{
    private readonly Action<Vector2, int> _action;

    public TwoAxesAction(string name, Action<Vector2, int> action)
        : base(InputTypes.TwoAxes, name)
    {
        _action = action;
    }

    internal override void Invoke(BaseInput input)
    {
        if (input is TwoAxesInput twoAxesInput) _action?.Invoke(twoAxesInput.Value, twoAxesInput.PlayerNumber);
    }
}
