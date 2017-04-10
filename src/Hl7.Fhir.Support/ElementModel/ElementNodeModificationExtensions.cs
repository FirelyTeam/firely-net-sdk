using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.ElementModel
{
    public static class ElementNodeModificationExtensions
    {
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
    }
}
