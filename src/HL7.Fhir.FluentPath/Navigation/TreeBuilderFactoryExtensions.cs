/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

#define USE_DYNAMIC
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Navigation
{
    /// <summary>Extension methods for <see cref="ILinkedTree{T}"/> to create (sub)trees from (anonymous) objects.</summary>
    public static class TreeBuilderFactoryExtensions
    {
        /// <summary>
        /// Create a generic tree structure of type <typeparamref name="T"/> from the specified (anonymous) object.
        /// Recursively maps object properties to tree nodes using reflection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root">The root node.</param>
        /// <param name="obj">An (anonymous) object instance to convert to a tree.</param>
        /// <returns>A tree of type <typeparamref name="T"/>.</returns>
        public static T AddChildrenFromObject<T>(this T root, object obj)
            where T : ILinkedTree<T>, IVariantTreeBuilder<T>
        {
            AddChildrenFromObject(root, obj, null);
            return root;
        }

        /// <summary>
        /// Create a generic tree structure of type <typeparamref name="T"/> from the specified (anonymous) object.
        /// Recursively maps object properties to tree nodes using reflection.
        /// </summary>
        /// <typeparam name="T">The type of the generated root node.</typeparam>
        /// <param name="root">The root node.</param>
        /// <param name="obj">An anonymous) object instance to convert to a tree.</param>
        /// <param name="predicate">An optional predicate to filter the enumerated properties. Return <c>false</c> to exclude the specified property.</param>
        public static void AddChildrenFromObject<T>(
            this T root,
            object obj,
            Func<PropertyInfo, bool> predicate)
            where T : ILinkedTree<T>, IVariantTreeBuilder<T>
        {
            if (obj == null) { throw new ArgumentNullException("obj"); } // nameof(obj)
            if (root == null) { throw new ArgumentNullException("root"); } // nameof(root)

            var props = obj.GetType().GetProperties() as IEnumerable<PropertyInfo>;
            props = props.Where(p => p.CanRead);
            // Optionally apply the specified filter
            if (predicate != null)
            {
                props = props.Where(predicate);
            }
            foreach (var prop in props)
            {
                if (IsSimpleType(prop.PropertyType))
                {
                    // Leaf node; emit node and value
#if USE_DYNAMIC
                    // C#4 dynamic keyword allows runtime binding to generic method based on actual property value type
                    // e.g. if the property value represents an int, then call AddLastChild<int>

                    // root.AddLastChild(prop.Name, (dynamic)prop.GetValue(obj));
                    var name = prop.Name;
#if NET40
                    var value = (dynamic)prop.GetValue(obj, null);
#else
                    var value = (dynamic)prop.GetValue(obj);
#endif
                    if (root.IsLeaf)
                    {
                        root.AddFirstChild(name, value);
                    }
                    else
                    {
                        root.LastChild().AddNextSibling(name, value);
                    }
#else
                    AddLastChild(root, prop.Name, prop.GetValue(obj));
#endif
                }
                else
                {
                    // Internal node; emit node and recurse
#if NET40
                    var childObj = prop.GetValue(obj, null);
#else
                    var childObj = prop.GetValue(obj);
#endif
                    var childNode = root.AddLastChild(prop.Name);
                    AddChildrenFromObject(childNode, childObj, predicate);
                }
            }
        }

        // Helper function to determine if a property of the specified type can contain sub-properties
        // Default property Type.IsPrimitive returns true for basic value types, but not for e.g. string
        // https://gist.github.com/jonathanconway/3330614
        static bool IsSimpleType(Type type)
        {
            return type.IsValueType ||
                   type.IsPrimitive ||
                   DerivedSimpleTypes.Contains(type) ||
                   Convert.GetTypeCode(type) != TypeCode.Object;
        }

        static readonly Type[] DerivedSimpleTypes = new Type[] {
            typeof(String),
            typeof(Decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid)
        };

#if !USE_DYNAMIC
        // Create a leaf node of type IValue<T> for the specified value, where T is the runtime value type
        private static TNode AddLastChild<TNode>(TNode root, string name, object value)
            where TNode : ILinkedTree<TNode>, IVariantTreeBuilder<TNode>
        {
            var valueType = value.GetType();

            // Dynamically create and invoke a concrete methodinfo for the specified runtime value type
            var miAddLastChild = LinkedTreeBuilderHelper<TNode>.MiAddLastChild;
            var mi = miAddLastChild.MakeGenericMethod(valueType);
            var result = mi.Invoke(root, new object[] { name, value });
            return (TNode)result;
        }

        // Private static helper class for caching the MethodInfo reflection data of the generic AddLastChild method per node type TNode
        private static class LinkedTreeBuilderHelper<TNode>
            where TNode : ILinkedTree<TNode>, IVariantTreeBuilder<TNode>
        {
            private const string memberName = "AddLastChild"; // nameof(IMixedValueLinkedTreeBuilder<TNode>.AddLastChild)

            public static MethodInfo MiAddLastChild =
                
                // Awkward way of retrieving the desired methodinfo
                // http://stackoverflow.com/questions/588149/referencing-desired-overloaded-generic-method

                (from m in typeof(IVariantTreeBuilder<TNode>).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                 let p = m.GetParameters()
                 where m.Name == memberName && m.GetGenericArguments().Length == 1
                 && p.Length == 2
                 // && p[0].ParameterType == typeof(TNode)
                 && m.ReturnType == typeof(TNode)
                 select m).Single();
        }
#endif

    }

}
