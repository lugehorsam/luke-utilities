namespace Utilities.Scripting
{
    public interface IRuntimeResolvable
    {
        ScriptObject GetResolvedValue(ScriptRuntime runtime);
    }
}
