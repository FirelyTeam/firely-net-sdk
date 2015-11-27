using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Navigation
{
    /// <summary>
    /// Extension methods on <see cref="IEnumerable{T}"/> of <see cref="INavTreeNode{T}"/>.
    /// Implemented by lifting existing extension methods on <see cref="INavTreeNode{T}"/>.
    /// </summary>
    public class NavTreeNodeSetExtensions
    {
        public static IEnumerable<T> Children<T>(IEnumerable<T> nodeSet) where T : INavTreeNode<T>
        {
            return nodeSet.SelectMany(node => node.Children());
        }

        public static IEnumerable<T> Descendants<T>(IEnumerable<T> nodeSet) where T : INavTreeNode<T>
        {
            return nodeSet.SelectMany(node => node.Descendants());
        }

        public static IEnumerable<T> Ancestors<T>(IEnumerable<T> nodeSet) where T : INavTreeNode<T>
        {
            return nodeSet.SelectMany(node => node.Ancestors());
        }

        public static IEnumerable<T> PrecedingSiblings<T>(IEnumerable<T> nodeSet) where T : INavTreeLeafNode<T>
        {
            return nodeSet.SelectMany(node => node.PrecedingSiblings());
        }

        public static IEnumerable<T> FollowingSiblings<T>(IEnumerable<T> nodeSet) where T : INavTreeLeafNode<T>
        {
            return nodeSet.SelectMany(node => node.PrecedingSiblings());
        }
    }
}
