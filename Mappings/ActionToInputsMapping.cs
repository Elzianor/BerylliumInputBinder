namespace BerylliumInputBinder.Mappings;

internal class ActionToInputsMapping
{
    public readonly int InputBindingsPerAction;
    public readonly Dictionary<BaseAction, BaseInput[]> ActionToInputsMap = [];

    public ActionToInputsMapping(int inputBindingsPerAction = 2)
    {
        InputBindingsPerAction = inputBindingsPerAction < 1 ? 1 : inputBindingsPerAction;
    }

    public InputBinderResults TryRegisterAction(BaseAction action)
    {
        return ActionToInputsMap.TryAdd(action, new BaseInput[InputBindingsPerAction]) ?
            InputBinderResults.Success :
            InputBinderResults.Failure;
    }

    public InputBinderResults TryUnregisterAction(BaseAction action)
    {
        return ActionToInputsMap.Remove(action) ?
            InputBinderResults.Success :
            InputBinderResults.Failure;
    }

    public BaseAction GetActionByName(string actionName)
    {
        return ActionToInputsMap.Keys.FirstOrDefault(a => a.Name == actionName);
    }

    public BaseInput[] GetInputs(BaseAction action)
    {
        return ActionToInputsMap.GetValueOrDefault(action);
    }

    public BaseInput GetInputAt(BaseAction action, int inputIndex)
    {
        return ActionToInputsMap.TryGetValue(action, out var inputs) ?
            inputs[inputIndex] :
            null;
    }

    public bool SetInputAt(BaseAction action, int inputIndex, BaseInput input)
    {
        if (!ActionToInputsMap.TryGetValue(action, out var inputs)) return false;

        inputs[inputIndex] = input;

        return true;
    }

    public int GetInputIndex(BaseAction action, BaseInput input)
    {
        if (!ActionToInputsMap.TryGetValue(action, out var inputs)) return -1;

        return inputs.IndexOf(input);
    }
}
