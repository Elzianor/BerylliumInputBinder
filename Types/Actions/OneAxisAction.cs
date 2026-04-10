namespace BerylliumInputBinder.Types.Actions;

public class OneAxisAction : BaseAction
{
    private readonly Action<float, int> _action;

    public OneAxisAction(string name, Action<float, int> action)
        : base(InputTypes.OneAxis, name)
    {
        _action = action;
    }

    internal override void Invoke(BaseInput input)
    {
        if (input is OneAxisInput oneAxisInput) _action?.Invoke(oneAxisInput.Value, oneAxisInput.PlayerNumber);
    }
}
