namespace BerylliumInputBinder;

public static class InputBinder
{
    private static int _maxInputsForAction;
    private static readonly Dictionary<Input, GameAction> InputToActionMap;
    private static readonly Dictionary<RawInput, GameAction[]> RawInputToActionsMap;

    public static readonly Dictionary<GameAction, Input[]> ActionToInputsMap;

    static InputBinder()
    {
        _maxInputsForAction = 2;
        InputToActionMap = [];
        ActionToInputsMap = [];
        RawInputToActionsMap = [];
    }

    public static void Initialize(int maxInputsForAction)
    {
        _maxInputsForAction = maxInputsForAction;
        InputToActionMap.Clear();
        ActionToInputsMap.Clear();
        RawInputToActionsMap.Clear();
    }

    public static bool TryRegisterAction(GameAction action)
    {
        if (ActionToInputsMap.ContainsKey(action)) return false;

        ActionToInputsMap.Add(action, new Input[_maxInputsForAction]);

        return true;
    }

    public static bool IsInputAlreadyBound(Input input, out GameAction boundAction)
    {
        boundAction = null;

        if (!InputToActionMap.TryGetValue(input, out var action)) return false;

        boundAction = action;

        return true;
    }

    public static bool TryBindInput(Input input, GameAction action, int index)
    {
        if (index < 0 ||
            index >= _maxInputsForAction)
            return false;

        if (!ActionToInputsMap.ContainsKey(action) &&
            !TryRegisterAction(action))
            return false;

        // search if input is bound elsewhere and try to remove it
        RemoveInput(input);

        AddInput(input, action, index);

        return true;
    }

    public static void ClearInputAt(GameAction action, int index)
    {
        if (index < 0 ||
            index >= _maxInputsForAction)
            return;
        if (!ActionToInputsMap.TryGetValue(action, out var inputs)) return;

        var input = inputs[index];
        var rawInput = GetRawInput(input);

        if (!RawInputToActionsMap.TryGetValue(rawInput, out var actions)) return;

        inputs[index] = null;

        InputToActionMap.Remove(input);

        RemoveFromRawMap(actions, input.Modifier, rawInput);
    }

    public static void Update(List<RawInput> rawInputs,
        bool isWithCtrl = false,
        bool isWithAlt = false,
        bool isWithShift = false)
    {
        foreach (var rawInput in rawInputs)
        {
            if (!RawInputToActionsMap.TryGetValue(rawInput, out var actions)) continue;

            var modifier = GetModifier(isWithCtrl, isWithAlt, isWithShift);
            var rawIndex = GetRawActionIndexByModifier(modifier);
            var action = actions[rawIndex];

            if (action == null) continue;

            if (IsApplicableToAction(rawInput, action.Type)) action.Action?.Invoke();
        }
    }

    #region Helpers
    private static void AddInput(Input input, GameAction action, int index)
    {
        // check if we will overwrite previous input in new place
        RemoveInput(ActionToInputsMap[action][index]);

        ActionToInputsMap[action][index] = input;

        InputToActionMap.Add(input, action);

        var rawInput = GetRawInput(input);

        if (!RawInputToActionsMap.TryGetValue(rawInput, out var actions))
        {
            actions = new GameAction[4];
            RawInputToActionsMap.Add(rawInput, actions);
        }

        actions[GetRawActionIndexByModifier(input.Modifier)] = action;
    }

    private static void RemoveInput(Input input)
    {
        if (input == null) return;
        if (!InputToActionMap.TryGetValue(input, out var action)) return;
        if (!ActionToInputsMap.TryGetValue(action, out var inputs)) return;

        var rawInput = GetRawInput(input);

        if (!RawInputToActionsMap.TryGetValue(rawInput, out var actions)) return;

        for (var i = 0; i < inputs.Length; i++)
        {
            if (!inputs[i].Equals(input)) continue;

            inputs[i] = null;

            break;
        }

        InputToActionMap.Remove(input);

        RemoveFromRawMap(actions, input.Modifier, rawInput);
    }

    private static void RemoveFromRawMap(GameAction[] actions, ModifierType inputModifier, RawInput rawInput)
    {
        actions[GetRawActionIndexByModifier(inputModifier)] = null;

        if (actions.All(a => a == null)) RawInputToActionsMap.Remove(rawInput);
    }

    private static ModifierType GetModifier(bool isWithCtrl, bool isWithAlt, bool isWithShift)
    {
        return isWithCtrl ?
            ModifierType.Ctrl :
            isWithAlt ?
                ModifierType.Alt :
                isWithShift ?
                    ModifierType.Shift :
                    ModifierType.None;
    }

    private static int GetRawActionIndexByModifier(ModifierType modifier)
    {
        return modifier switch
        {
            ModifierType.Ctrl => 1,
            ModifierType.Alt => 2,
            ModifierType.Shift => 3,
            _ => 0
        };
    }

    private static RawInput GetRawInput(Input input)
    {
        if (input == null) return null;

        return input is AxisInput axisInput ?
            new RawAxisInput
            {
                DeviceType = axisInput.DeviceType,
                Code = axisInput.Code,
                IsPositive = axisInput.IsPositive
            } :
            new RawButtonInput
            {
                DeviceType = input.DeviceType,
                Code = input.Code
            };
    }

    private static bool IsApplicableToAction(RawInput rawInput, GameActionType actionType)
    {
        if (rawInput is RawAxisInput) return true;
        if (actionType == GameActionType.Сontinuous) return true;

        return rawInput is RawButtonInput { State: ButtonState.Pressed };
    }
    #endregion
}
