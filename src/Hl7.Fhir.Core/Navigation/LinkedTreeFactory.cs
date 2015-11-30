using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Navigation
{
    /// <summary>Provides static methods for creating a <see cref="DoublyLinkedTree"/> structure.</summary>
    public static class LinkedTreeFactory
    {
        /// <summary>
        /// Create a structure of <see cref="DoublyLinkedTree"/> instances from the specified (anonymous) object.
        /// Recursively reflect on the object properties to generate tree nodes and leaf items.
        /// Convert complex properties to internal nodes. Convert simple properties to leaf items.
        /// </summary>
        /// <param name="obj">The (anonymous) object instance to convert to a tree.</param>
        /// <param name="name">The name of the root node.</param>
        /// <returns>A <see cref="DoublyLinkedTree"/> instance that represents the root of the generated tree.</returns>
        public static DoublyLinkedTree CreateFromObject(object obj, string name)
        {
            var root = DoublyLinkedTree.Create(name);
            AddFromObject<DoublyLinkedTree>(root, obj, DoublyLinkedTreeNode.Create, CreateNode);
            return root;
        }

        // Helper function to create a tree node (without value) or leaf item (with value)
        private static DoublyLinkedTree CreateNode(string name, object value)
        {
            if (value == null) { return DoublyLinkedTreeNode.Create(name); }

            // Create a DoublyLinkedTreeLeaf<T> instance for the specified value of type T
            var valueNodeType = typeof(DoublyLinkedTreeLeaf<>).MakeGenericType(value.GetType());
            var instance = Activator.CreateInstance(valueNodeType, name, value);

            return instance as DoublyLinkedTree;
        }

        /// <summary>
        /// Create a generic tree structure from a given (anonymous) object hierarchy.
        /// Recursively maps object properties to tree nodes using reflection.
        /// </summary>
        /// <typeparam name="TNode">The type of the generated root node.</typeparam>
        /// <param name="root">The tree root node.</param>
        /// <param name="obj">The (anonymous) object instance to convert to a tree.</param>
        /// <param name="createInternalNode">Factory function that should create a new internal node from the specified name.</param>
        /// <param name="createLeafNode">Factory function that should create a new leaf node from the specified name and value.</param>
        /// <param name="predicate">An optional predicate to filter the enumerated properties. Return <c>false</c> to exclude the specified property.</param>
        /// <returns>A node instance of type <typeparamref name="TNode"/> that represents the root of the generated tree.</returns>
        private static void AddFromObject<TNode>(
            TNode root,
            object obj,
            Func<string, TNode> createInternalNode,
            Func<string, object, TNode> createLeafNode,
            Func<PropertyInfo, bool> predicate = null)
            where TNode : ILinkedTree<TNode>, ILinkedTreeBuilder<TNode>
        {
            // if (obj == null) { return default(TNode); }

            if (obj == null) { throw new ArgumentNullException("obj"); } // nameof(obj)
            if (root == null) { throw new ArgumentNullException("root"); } // nameof(root)
            if (createInternalNode == null) { throw new ArgumentNullException("createInternalNode"); } // nameof(createInternalNode)
            if (createLeafNode == null) { throw new ArgumentNullException("createLeafNode"); } // nameof(createLeafNode)

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
                    var childNode = root.AddChild(prop.Name, prop.GetValue(obj));
                }
                else
                {
                    var childObj = prop.GetValue(obj);
                    var childNode = root.AddChild(prop.Name);
                    AddFromObject(childNode, childObj, createInternalNode, createLeafNode, predicate);
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
