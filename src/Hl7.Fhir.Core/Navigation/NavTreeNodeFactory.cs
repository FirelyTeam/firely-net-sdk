using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Navigation
{
    public class NavTreeNodeFactory
    {
        /// <summary>
        /// Create a structure of <see cref="NavTreeNode"/> instances from the specified (anonymous) object.
        /// </summary>
        /// <param name="obj">The (anonymous) object instance to convert to a tree.</param>
        /// <param name="name">The name of the root node.</param>
        /// <returns>A <see cref="NavTreeNode"/> instance that represents the root of the generated tree.</returns>
        public static NavTreeNode CreateFromObject(object obj, string name)
        {
            return CreateFromObject<NavTreeNode>(obj, name, CreateNode, CreateNode);
        }

        // Helper function to create a NavTreeNode (without value) or NavTreeNode<T> (with value)
        private static NavTreeNode CreateNode(string name, object value)
        {
            if (value == null) { return new NavTreeNode(name); }

            // Create a NavTreeNode<T> instance for the specified value of type T
            var valueNodeType = typeof(NavTreeLeafNode<>).MakeGenericType(value.GetType());
            var instance = Activator.CreateInstance(valueNodeType, name, value);

            return instance as NavTreeNode;
        }

        private static NavTreeNode CreateNode(string name) { return new NavTreeNode(name); }

        /// <summary>
        /// Create a generic tree structure from a given (anonymous) object hierarchy.
        /// Recursively maps object properties to tree nodes using reflection.
        /// </summary>
        /// <typeparam name="TNode">The type of the generated root node.</typeparam>
        /// <param name="obj">The (anonymous) object instance to convert to a tree.</param>
        /// <param name="name">The name of the root node.</param>
        /// <param name="createInternalNode">Factory function that should create a new internal node from the specified name.</param>
        /// <param name="createLeafNode">Factory function that should create a new leaf node from the specified name and value.</param>
        /// <param name="predicate">An optional predicate to filter the enumerated properties. Return <c>false</c> to exclude the specified property.</param>
        /// <returns>A node instance of type <typeparamref name="TNode"/> that represents the root of the generated tree.</returns>
        public static TNode CreateFromObject<TNode>(object obj, string name,
            Func<string, TNode> createInternalNode,
            Func<string, object, TNode> createLeafNode,
            Func<PropertyInfo, bool> predicate = null)
            where TNode : INavTreeNode<TNode>, INavTreeBuilder<TNode>
        {
            if (createLeafNode == null) { throw new ArgumentNullException("createLeafNode"); } // nameof(createLeafNode)
            if (obj == null) { return default(TNode); }

            var node = createInternalNode(name);
            var props = obj.GetType().GetProperties() as IEnumerable<PropertyInfo>;
            // Optionally apply the specified filter
            if (predicate != null)
            {
                props = props.Where(predicate);
            }
            foreach (var prop in props)
            {
                TNode childNode;
                var propType = prop.PropertyType;
                if (IsSimpleType(propType))
                {
                    // Leaf node
                    childNode = createLeafNode(prop.Name, prop.GetValue(obj));
                }
                else
                {
                    var propVal = prop.GetValue(obj);
                    childNode = CreateFromObject<TNode>(propVal, prop.Name, createInternalNode, createLeafNode, predicate);
                }
                node.AppendChild(childNode);
            }
            return node;
        }

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
