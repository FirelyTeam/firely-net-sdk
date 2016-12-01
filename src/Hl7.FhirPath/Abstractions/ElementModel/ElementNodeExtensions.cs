using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.ElementModel
{

    public static class ElementNodeExtensions
    {
        public static IElementNavigator ToNavigator(this ElementNode node)
        {
            return new ElementNodeNavigator(node);
        }

        public static IEnumerable<ElementNode> ChildrenWithName(this ElementNode node, string name)
        {
            return node.Children.Where(c => c.Name == name);
        }

        public static string PathString(this ElementNode node)
        {
            // This is now a ad-hoc aggregation of path. So with repeated use, it's slow.

            var segments = new List<string>();

            foreach(var n in node.Path())
            {
                int index = n.Parent?.ChildrenWithName(n.Name).ToList().IndexOf(n) ?? -1;
                if (index >= 0)
                {
                    segments.Add($"{n.Name}[{index}]");
                }
                else
                {
                    segments.Add(n.Name);
                }
            }
            return string.Join(".", segments);
        }

    }
}