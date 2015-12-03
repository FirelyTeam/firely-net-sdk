/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Navigation
{
    /// <summary>Provides static methods for creating a <see cref="FhirNavigationTree"/> structure.</summary>
    public static class LinkedTreeFactory
    {
        /// <summary>
        /// Create a <see cref="FhirNavigationTree"/> structure from the specified (anonymous) object.
        /// Recursively reflect on the object properties to generate tree nodes.
        /// Convert complex property values to internal nodes. Convert simple property values to node values.
        /// </summary>
        /// <param name="obj">The (anonymous) object instance to convert to a tree.</param>
        /// <param name="name">The name of the root node.</param>
        /// <returns>A <see cref="FhirNavigationTree"/> instance that represents the root of the generated tree.</returns>
        public static FhirNavigationTree CreateFromObject(object obj, string name)
        {
            var root = FhirNavigationTree.Create(name);
            AddFromObject(root, obj);
            return root;
        }

        // Private static helper class for caching the generic MethodInfo
        private static class LinkedTreeBuilderHelper<TNode>
            where TNode : ILinkedTree<TNode>, ILinkedTreeBuilder<TNode>
        {
            private const string memberName = "AddLastChild"; // nameof(ILinkedTreeBuilderWithValues<TNode>.AddLastChild)

            public static MethodInfo MiAddLastChild =
                
                // Awkward way of retrieving the desired methodinfo
                // http://stackoverflow.com/questions/588149/referencing-desired-overloaded-generic-method

                (from m in typeof(ILinkedTreeBuilderWithValues<TNode>).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                 where m.Name == memberName && m.GetGenericArguments().Length == 1
                 && m.GetParameters().Length == 2
                 && m.ReturnType == typeof(TNode)
                 select m).Single();
        }

        // Create a leaf node of type IValue<T> for the specified value, where T is the runtime value type
        private static TNode AddLastChild<TNode>(TNode root, string name, object value)
            where TNode : ILinkedTree<TNode>, ILinkedTreeBuilder<TNode>
        {
            // Dynamically create and invoke a concrete methodinfo for the specified runtime value type
            var miAddLastChild = LinkedTreeBuilderHelper<TNode>.MiAddLastChild;
            var mi = miAddLastChild.MakeGenericMethod(value.GetType());
            var result = mi.Invoke(root, new object[] { name, value });
            return (TNode)result;
        }

        /// <summary>
        /// Create a generic tree structure from a given (anonymous) object hierarchy.
        /// Recursively maps object properties to tree nodes using reflection.
        /// </summary>
        /// <typeparam name="TNode">The type of the generated root node.</typeparam>
        /// <param name="root">The tree root node.</param>
        /// <param name="obj">The (anonymous) object instance to convert to a tree.</param>
        /// <param name="predicate">An optional predicate to filter the enumerated properties. Return <c>false</c> to exclude the specified property.</param>
        /// <returns>A node instance of type <typeparamref name="TNode"/> that represents the root of the generated tree.</returns>
        private static void AddFromObject<TNode>(
            TNode root,
            object obj,
            Func<PropertyInfo, bool> predicate = null)
            where TNode : ILinkedTree<TNode>, ILinkedTreeBuilder<TNode>
        {
            // if (obj == null) { return default(TNode); }

            if (obj == null) { throw new ArgumentNullException("obj"); } // nameof(obj)
            if (root == null) { throw new ArgumentNullException("root"); } // nameof(root)

            // var node = createInternalNode(name);
            var props = obj.GetType().GetProperties() as IEnumerable<PropertyInfo>;
            // Optionally apply the specified filter
            if (predicate != null)
            {
                props = props.Where(predicate);
            }
            foreach (var prop in props)
            {
                if (IsSimpleType(prop.PropertyType))
                {
                    // Leaf node
                    var childNode = AddLastChild(root, prop.Name, prop.GetValue(obj));
                }
                else
                {
                    var childObj = prop.GetValue(obj);
                    var childNode = root.AddLastChild(prop.Name);
                    AddFromObject(childNode, childObj, predicate);
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
    }

}
