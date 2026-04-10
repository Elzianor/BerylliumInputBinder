namespace BerylliumInputBinder.Types.Actions;

public class ButtonAction : BaseAction
{
    private readonly Action<ButtonStates, int> _action;

    public ButtonAction(string name, Action<ButtonStates, int> action)
        : base(InputTypes.Button, name)
    {
        _action = action;
    }

    internal override void Invoke(BaseInput input)
    {
        if (input is ButtonInput buttonInput) _action?.Invoke(buttonInput.State, buttonInput.PlayerNumber);
    }
}
