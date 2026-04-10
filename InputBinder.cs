namespace BerylliumInputBinder;

public class InputBinder
{
    private readonly ActionToInputsMapping _actionToInputsMap;
    private readonly InputToActionMapping _inputToActionMap;

    public int InputBindingsPerAction => _actionToInputsMap.InputBindingsPerAction;
    public Dictionary<BaseAction, BaseInput[]> ActionToInputsMap => _actionToInputsMap.ActionToInputsMap;

    public InputBinder(int inputBindingsPerAction = 2)
    {
        _actionToInputsMap = new ActionToInputsMapping(inputBindingsPerAction);
        _inputToActionMap = new InputToActionMapping();
    }

    public InputBinderResults TryRegisterAction(BaseAction action)
    {
        return action == null ? InputBinderResults.ActionNull : _actionToInputsMap.TryRegisterAction(action);
    }

    public InputBinderResults TryUnregisterAction(BaseAction action)
    {
        if (action == null) return InputBinderResults.ActionNull;

        foreach (var input in _actionToInputsMap.GetInputs(action)) _inputToActionMap.RemoveInput(input);

        return _actionToInputsMap.TryUnregisterAction(action);
    }

    public InputBinderResults TryMapInputToAction(BaseAction action,
        int inputIndex,
        BaseInput input,
        bool forceMap,
        out BaseAction otherAction)
    {
        otherAction = null;

        if (input == null) return InputBinderResults.InputNull;
        if (action == null) return  InputBinderResults.ActionNull;
        if (inputIndex < 0 ||
            inputIndex >= InputBindingsPerAction)
            return InputBinderResults.IndexOutsideOfRange;
        if (input.Type != action.Type) return InputBinderResults.TypesMismatch;

        var inputs = _actionToInputsMap.GetInputs(action);

        if (inputs == null) return InputBinderResults.Failure;

        var previousAction = _inputToActionMap.GetAction(input);

        if (previousAction != null &&
            previousAction != action &&
            !forceMap)
        {
            otherAction = previousAction;

            return InputBinderResults.InputBoundToOtherAction;
        }

        TryUnmapInputFromAction(previousAction, _actionToInputsMap.GetInputIndex(previousAction, input));

        if (!_actionToInputsMap.SetInputAt(action, inputIndex, input)) return  InputBinderResults.Failure;

        _inputToActionMap.MapActionToInput(input, action);

        return InputBinderResults.Success;
    }

    public InputBinderResults TryUnmapInputFromAction(BaseAction action, int inputIndex)
    {
        if (action == null) return InputBinderResults.ActionNull;
        if (inputIndex < 0 ||
            inputIndex >= InputBindingsPerAction)
            return InputBinderResults.IndexOutsideOfRange;

        var input = _actionToInputsMap.GetInputAt(action, inputIndex);

        if (input == null) return InputBinderResults.Failure;

        _inputToActionMap.RemoveInput(input);
        _actionToInputsMap.SetInputAt(action, inputIndex, null);

        return InputBinderResults.Success;
    }

    #region Updater
    public void Update(IEnumerable<ButtonInput> buttonInputs,
        IEnumerable<OneAxisInput> oneAxisInputs,
        IEnumerable<TwoAxesInput> twoAxesInputs,
        bool withShift,
        bool withCtrl,
        bool withAlt)
    {
        foreach (var buttonInput in buttonInputs)
        {
            InvokeWithModifier(buttonInput, ButtonModifiers.None);

            if (buttonInput.Source != InputSources.Keyboard) continue;

            if (withShift) InvokeWithModifier(buttonInput, ButtonModifiers.Shift);
            if (withCtrl) InvokeWithModifier(buttonInput, ButtonModifiers.Ctrl);
            if (withAlt) InvokeWithModifier(buttonInput, ButtonModifiers.Alt);
        }

        foreach (var oneAxisInput in oneAxisInputs) _inputToActionMap.InvokeAction(oneAxisInput);
        foreach (var twoAxesInput in twoAxesInputs) _inputToActionMap.InvokeAction(twoAxesInput);
    }
    #endregion

    #region Helpers
    private void InvokeWithModifier(ButtonInput input, ButtonModifiers modifier)
    {
        input.Modifier = modifier;
        _inputToActionMap.InvokeAction(input);
    }
    #endregion
}
