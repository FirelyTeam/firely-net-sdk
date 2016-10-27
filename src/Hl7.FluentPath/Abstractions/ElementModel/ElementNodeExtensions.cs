namespace Hl7.ElementModel
{

    public static class ElementNodeExtensions
    {
        public static IElementNavigator ToNavigator(this ElementNode node)
        {
            return new ElementNodeNavigator(node);
        }

        public static string PathString(this ElementNode node)
        {
            return string.Join(".", node.Path());
        }
    }
}