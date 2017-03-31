using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.ElementModel
{
    public static class ElementNodeExtensions
    {
        public static IEnumerable<IElementNode> Children(this IEnumerable<IElementNode> nodes)
        {
            return nodes.SelectMany(n => n.Children);
        }

        public static IEnumerable<IElementNode> Children(this IElementNode node, string name)
        {
            return node.Children.Where(c => c.Name == name);
        }

        public static IEnumerable<IElementNode> Children(this IEnumerable<IElementNode> nodes, string name)
        {
            return nodes.SelectMany(n => n.Children(name));
        }

        public static bool HasChildren(this IElementNode node) => node.Children.Any();

        public static bool HasChildren(this IEnumerable<IElementNode> nodes) => nodes.Children().Any();

        public static IEnumerable<IElementNode> Descendants(this IElementNode element)
        {
            foreach (var child in element.Children)
            {
                yield return child;

                foreach (var grandchild in child.Descendants())
                {
                    yield return grandchild;
                }
            }
        }

        public static IEnumerable<IElementNode> Descendants(this IEnumerable<IElementNode> elements)
        {
            return elements.SelectMany(e => e.Descendants());
        }


        public static IEnumerable<IElementNode> DescendantsAndSelf(this IElementNode element)
        {
            return (new[] { element }).Concat(element.Descendants());
        }

        public static IEnumerable<IElementNode> DescendantsAndSelf(this IEnumerable<IElementNode> elements)
        {
            return elements.SelectMany(e => e.DescendantsAndSelf());
        }


        public static IEnumerable<IElementNode> Ancestors(this IElementNode node)
        {
            var scan = node.Parent;

            while(scan != null)
            {
                yield return scan;
                scan = scan.Parent;
            }
        }

        public static IEnumerable<IElementNode> Ancestors(this IEnumerable<IElementNode> nodes)
        {
            // all the parents of the given lists of nodes. Does not include the nodes, except if one is a parent of another.
            return nodes.SelectMany(e => e.Ancestors()).Distinct();
        }

        public static void Visit(this IElementNode node, Action<IElementNode> visitor)
        {
            visitor(node);
            foreach (var child in node.Children)
            {
                Visit(child, visitor);
            }
        }

        private static void visit(this IElementNode node, Action<int, IElementNode> visitor, int depth = 0)
        {
            visitor(depth, node);
            foreach (var child in node.Children)
            {
                visit(child, visitor, depth + 1);
            }
        }

        public static void Visit(this IElementNode node, Action<int, IElementNode> visitor) => node.visit(visitor, 0);
    }
}
