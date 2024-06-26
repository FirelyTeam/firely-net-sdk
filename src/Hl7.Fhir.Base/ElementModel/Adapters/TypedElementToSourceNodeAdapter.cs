/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel.Adapters
{
    internal class TypedElementToSourceNodeAdapter : ISourceNode, IAnnotated, IExceptionSource, IResourceTypeSupplier
    {
        public readonly ITypedElement Current;

        public TypedElementToSourceNodeAdapter(ITypedElement element)
        {
            this.Current = element ?? throw Error.ArgumentNull(nameof(element));
            this.Location = element.Location;

            if (element is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private TypedElementToSourceNodeAdapter(TypedElementToSourceNodeAdapter parent, ITypedElement child)
        {
            Current = child;
            ExceptionHandler = parent.ExceptionHandler;
            // if the typed element was derived from a source node, we can use the location of the source node
            Location = Current.Annotation<ISourceNode>()?.Location ?? 
                (child.Definition?.IsChoiceElement is true 
                    // if the child is a choice element, we need to insert the type name into the location
                    ? child.Location.Insert(child.Location.LastIndexOf("[0]", StringComparison.Ordinal), child.InstanceType.Capitalize()) 
                    // otherwise we can just use the location of the child (NOT the shortpath, since the shortpath does not contain the index of non-collection elements, which should be preserved on source nodes)
                    : child.Location
                );
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name
        {
            get
            {
                return Current.Definition?.IsChoiceElement == true ?
                    Current.Name + Current.InstanceType.Capitalize() : Current.Name;
            }
        }

        public string Text => Current.Value == null ? null :
            PrimitiveTypeConverter.ConvertTo<string>(Current.Value);

        public string Location { get; }

        public string ResourceType
        {
            get
            {
                return Current.Definition?.IsResource == true ? Current.InstanceType : null;
            }
        }

        public IEnumerable<ISourceNode> Children(string name = null) =>
            Current.Children()
                .Select(c => new TypedElementToSourceNodeAdapter(this, c))
                .Where(c => c.Name.MatchesPrefix(name));

        IEnumerable<object> IAnnotated.Annotations(Type type)
        {
            return type == typeof(TypedElementToSourceNodeAdapter) || type == typeof(ISourceNode) || type == typeof(IResourceTypeSupplier)
                ? (new[] { this })
                : Current.Annotations(type);
        }
    }
}
