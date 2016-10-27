using Hl7.ElementModel;

namespace Hl7.FluentPath.Functions
{

    // todo: maybe you can find a better name --mh.

    public static class NodeIdentificationExtensions
    {

        public static bool IsTypeProvider(this IElementNavigator n)
        {
            return !string.IsNullOrEmpty(n.TypeName);
        }

        public static bool IsNamedNode(this IElementNavigator n)
        {
            return (!string.IsNullOrEmpty(n.Name));
        }

        public static bool IsValueProvider(this IElementNavigator n)
        {
            return (n.Value != null);
        }

    }
}