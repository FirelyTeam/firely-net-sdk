
namespace Hl7.Fhir.Serialization;

/// <summary>
/// These extensions provide a convenient way to manipulate the path stack in the PocoDeserializerState,
/// and are compatible with the old PathStack class - this means an easier migration for us, and no need
/// to rewrite the tests.
/// </summary>
internal static class PathStackExtensions
{
    public static string GetInstancePath(this PocoDeserializerState state) => state.Path.GetInstancePath();

    public static void EnterResource(this PocoDeserializerState state, string name)
    {
        state.Path = state.Path.EnterResource(name);
    }

    public static void ExitResource(this PocoDeserializerState state)
    {
        state.Path = state.Path.ExitResource();
    }

    public static void EnterElement(this PocoDeserializerState state, string name, bool isArray = false)
    {
        state.Path = state.Path.EnterElement(name);

        if(isArray)
            state.Path = state.Path.SetIndex(0);
    }

    public static void ExitElement(this PocoDeserializerState state)
    {
        state.Path = state.Path.ExitElement();
    }

    public static void SetIndex(this PocoDeserializerState state, int index)
    {
        state.Path = state.Path.SetIndex(index);
    }
}