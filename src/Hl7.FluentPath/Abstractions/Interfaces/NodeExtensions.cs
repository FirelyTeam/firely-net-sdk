using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.ElementModel
{
    public static class NodeExtensions
    {

        public static IEnumerable<T> All<T>(this IEnumerable<T> elements) where T : INode<T>
        {
            return elements.SelectMany(e => e.All());
        }

        public static IEnumerable<T> All<T>(this T element) where T : INode<T>
        {
            yield return element;
            foreach (var child in element.Children)
            {
                yield return child;
            }

        }

    
        public static IEnumerable<T> All<T>(this T element, Predicate<T> predicate) where T : INode<T>
        {
            if (predicate(element)) yield return element;

            foreach (T child in element.Children)
            {
                foreach (T grandchild in child.All())
                {
                    yield return grandchild;
                }
            }
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> list, T item)
        {
            return list.Except(new T[] { item });
        }

        public static IEnumerable<T> Ascendants<T>(this T node) where T : INode<T>
        {
            return node.Path().Except(node);
        }

        public static IEnumerable<T> Ascendants<T>(this IEnumerable<T> nodes) where T : INode<T>
        {
            // all the parents of the given lists of nodes. Does not include the nodes, except if one is a parent of another.
            return nodes.SelectMany(e => e.Ascendants()).Distinct();
        }

        public static IEnumerable<T> DescendantNodes<T>(this T element) where T : INode<T>
        {
            foreach (T child in element.Children)
            {
                yield return child;

                foreach (T grandchild in child.DescendantNodes())
                {
                    yield return grandchild;
                }
            }
        }

        public static IEnumerable<T> DescendantNodes<T>(this T element, Func<T, bool> predicate) where T : INode<T>
        {
            return element.DescendantNodes().Where(predicate);
        }

        public static void Visit<T>(this T node, Action<T> visitor) where T : INode<T>
        {
            visitor(node);
            foreach (T child in node.Children)
            {
                Visit(child, visitor);
            }
        }

        public static void DepthVisit<T>(this T node, Action<int, T> visitor, int depth = 0) where T : INode<T>
        {
            visitor(depth, node);
            foreach (T child in node.Children)
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

        public static bool HasChildNodes<T>(this T node) where T : INode<T>
        {
            return node.Children != null && node.Children.Any();
        }

        public static int CountChildNodes<T>(this T node) where T : INode<T>
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

        public static List<T> Path<T>(this T node) where T : INode<T>
        {
            var path = new List<T>();
            T current = node;
            do
            {
                path.Insert(0, current);
                current = current.Parent;
            }
            while (current != null);

            return path;
        }
    }
}
