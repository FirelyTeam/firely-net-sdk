using Hl7.ElementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Serialization 
{
    public static class ElementNodeWriter
    {
        public static ElementNode CreateElementModel(this IElementNavigator navigator)
        {
            var children = CreateChildren(navigator.Clone());
            
            // this is trickery to deal with the limited constructor
            var root = ElementNode.Valued(navigator.Name, navigator.Value, navigator.TypeName, children.ToArray());
            return root;
        }
     
        private static IList<ElementNode> CreateChildren(this IElementNavigator navigator)
        {
            var nodes = new List<ElementNode>();

            if (navigator.MoveToFirstChild())
            {
                do
                {
                    var children = CreateChildren(navigator.Clone());

                    // todo: this is trickery to comply to the limited constructor
                    ElementNode node = ElementNode.Valued(navigator.Name, navigator.Value, navigator.TypeName, children.ToArray());
                    nodes.Add(node);
                }
                while (navigator.MoveToNext());
            }

            return nodes;
        }

        public static ElementNode CreateElementNode(this IElementNavigator navigator)
        {
            return ElementNode.Valued(navigator.Name, navigator.Value, navigator.TypeName);
        }
    }
}
