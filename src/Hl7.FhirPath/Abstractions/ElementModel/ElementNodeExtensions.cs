using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.ElementModel
{
    public static class ElementNodeExtensions
    {
        public static void AddChild(this IElementNode node, IElementNode child)
        {
            child.Parent = node;

            if (!node.HasChildNodes()) node.Children = new List<IElementNode>();
            
            node.Children.Add(child);
        }


        public static IElementNavigator ToNavigator(this IElementNode node)
        {
            return new ElementNodeNavigator(node);
        }

        public static IEnumerable<IElementNode> ChildrenWithName(this IElementNode node, string name)
        {
            return node.Children.Where(c => c.Name == name);
        }

        public static string BuildPath(this IElementNode node)
        {
            // This is now a ad-hoc aggregation of path. So with repeated use, it's slow.

            var segments = new List<string>();

            foreach (var n in node.Ancestors())
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



        public static IEnumerable<IElementNode> All(this IEnumerable<IElementNode> elements)
        {
            return elements.SelectMany(e => e.All());
        }

        public static IEnumerable<IElementNode> All(this IElementNode element)
        {
            yield return element;
            foreach (var child in element.Children)
            {
                yield return child;
            }

        }
   
        public static IEnumerable<IElementNode> All(this IElementNode element, Predicate<IElementNode> predicate)
        {
            if (predicate(element)) yield return element;

            foreach (var child in element.Children)
            {
                foreach (var grandchild in child.All())
                {
                    yield return grandchild;
                }
            }
        }

        public static IEnumerable<IElementNode> Except(this IEnumerable<IElementNode> list, IElementNode item)
        {
            return list.Except(new IElementNode[] { item });
        }

        public static IEnumerable<IElementNode> Ascendants(this IElementNode node)
        {
            return node.Ancestors().Except(node);
        }

        public static IEnumerable<IElementNode> Ascendants(this IEnumerable<IElementNode> nodes)
        {
            // all the parents of the given lists of nodes. Does not include the nodes, except if one is a parent of another.
            return nodes.SelectMany(e => e.Ascendants()).Distinct();
        }

        public static IEnumerable<IElementNode> DescendantNodes(this IElementNode element)
        {
            foreach (var child in element.Children)
            {
                yield return child;

                foreach (var grandchild in child.DescendantNodes())
                {
                    yield return grandchild;
                }
            }
        }

        public static IEnumerable<IElementNode> DescendantNodes(this IElementNode element, Predicate<IElementNode> predicate)
        {
            return element.DescendantNodes().Where(e=>predicate(e));
        }

        public static void Visit(this IElementNode node, Action<IElementNode> visitor)
        {
            visitor(node);
            foreach (var child in node.Children)
            {
                Visit(child, visitor);
            }
        }

        public static void DepthVisit(this IElementNode node, Action<int, IElementNode> visitor, int depth = 0)
        {
            visitor(depth, node);
            foreach (var child in node.Children)
            {
                DepthVisit(child, visitor, depth + 1);
            }
        }

        //public static void RemoveChild<T>(this T node, T child) where T : INode<T>
        //{
        //    if (node.FirstChild == null) return;

        //    if (node.FirstChild.Equals(child))
        //    {
        //        node.FirstChild = node.FirstChild.Next;
        //    }
        //    else
        //    {

        //        var n = node.FirstChild;
        //        do
        //        {
        //            if (!n.Next.Equals(child))
        //            {
        //                n = n.Next;
        //            }
        //            else
        //            {
        //                n.Next = child.Next; // skip one
        //                return;
        //            }
        //        }
        //        while (n != null);
        //    }
        //}

        /*
        public static void RemoveChildren<T>(this T node) where T : INode<T>
        {
            node.FirstChild = default(T); // is this sufficient
        }

        public static void RemoveChildren<T>(this IEnumerable<T> nodes) where T : INode<T>
        {
            foreach (var node in nodes)
            {
                node.RemoveChildren();
            }
        }

        public static bool Replace<T>(this List<T> list, T toRemove, T toAdd) where T : INode<T>
        {
            bool found = list.Remove(toRemove);
            if (found)
            {
                list.Add(toAdd);
            }
            return found;
        }

        public static bool ReplaceChild<T>(this T node, T toRemove, T toAdd) where T : INode<T>
        {
            if (node.FirstChild == null) return false;

            toAdd.Next = toRemove.Next;
            toAdd.Parent = toRemove.Parent;

            if (node.FirstChild.Equals(toRemove))
            {
                node.FirstChild = toAdd;
                return true;
            }
            else
            {
                var n = node.FirstChild;
                do
                {
                    if (n.Next.Equals(toRemove))
                    {
                        n.Next = toAdd;
                        return true;
                    }
                }
                while (n != null);

                return false;
            }
            //return node.Children.Replace(toRemove, toAdd);
        }

        public static T Clone<T>(this T original) where T : INode<T>, ICopyNode<T>, new()
        {
            T clone = new T();
            original.CopyTo(clone);
            return clone;
        }

        public static T CloneTree<T>(this T source) where T : INode<T>, ICopyNode<T>, new()
        {
            T target = new T();
            source.CopyTreeTo(target);
            return target;
        }

        public static void CopyTreeTo<T>(this T source, T target) where T : INode<T>, ICopyNode<T>, new()
        {
            source.CopyTo(target);
            foreach (var child in source.Children())
            {
                var childclone = child.CloneTree();
                target.AddChild(childclone);
            }

        }

        //public static void CopyTo<T>(this T source, T target) where T : Node<T>, ICopyNode<T>, new()
        //{
        //    source.CopyTo(target);
        //}

        public static T LastChild<T>(this T node) where T : INode<T>
        {
            if (node.FirstChild == null) return default(T);

            var n = node.FirstChild;
            while (n.Next != null) n = n.Next;
            return n;
        }

        public static void AddChild<T>(this T node, T child) where T : INode<T>
        {
            if (!node.HasChildren())
            {
                child.Parent = node;
                node.FirstChild = child;
            }
            else
            {
                var n = node.LastChild();
                if (n != null)
                {
                    child.Parent = node;
                    n.Next = child;
                }
            }
        }

        public static void AddChildren<T>(this T node, IEnumerable<T> nodes) where T : INode<T>
        {
            // node.EnsureChildren();
            foreach (T item in nodes)
            {
                node.AddChild(item);
            }
        }*/

        public static bool HasChildNodes(this IElementNode node)
        {
            return node.Children != null && node.Children.Any();
        }

        public static int CountChildNodes(this IElementNode node)
        {
            if (node.HasChildNodes())
                return node.Children.Count;
            else
                return 0;
        }

        //public static void EnsureChildren<T>(this T node) where T : Node<T>
        //{
        //    if (node. == null)
        //    {
        //        node.Children = new List<T>();
        //    }
        //}

        //public static T RemoveFromParent<T>(this T node) where T : INode<T>
        //{
        //    node.Parent.RemoveChild(node);
        //    return node;
        //}

        public static IEnumerable<IElementNode> Ancestors(this IElementNode node)
        {
            var current = node;
            do
            {
                yield return current;
                current = current.Parent;
            }
            while (current != null);
        }
    }
}
