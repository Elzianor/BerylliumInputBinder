namespace BerylliumInputBinder.Mappings;

internal class InputToActionMapping
{
    private readonly Dictionary<BaseInput, BaseAction> _inputToActionMap = [];

    public void MapActionToInput(BaseInput input, BaseAction action)
    {
        if (!_inputToActionMap.TryAdd(input, action)) _inputToActionMap[input] = action;
    }

    public void RemoveInput(BaseInput input)
    {
        _inputToActionMap.Remove(input);
    }

    public void InvokeAction(BaseInput input)
    {
        if (_inputToActionMap.TryGetValue(input, out var action)) action.Invoke(input);
    }

    public BaseAction GetAction(BaseInput input)
    {
        return _inputToActionMap.GetValueOrDefault(input);
    }
}
