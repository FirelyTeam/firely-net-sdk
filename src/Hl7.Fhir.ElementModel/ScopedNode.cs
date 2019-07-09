/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public class ScopedNode : BaseScopedNode
    {
        public ScopedNode(ITypedElement wrapped)
        {
            //if (wrapped.GetElementDefinitionSummary() == null)
            //    throw Error.Argument("ScopedNavigator can only be used on a navigator chain that supplies type information (e.g. TypedNavigator, PocoNavigator)", nameof(wrapped));

            Current = wrapped;
            if (Current is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        public ScopedNode(BaseScopedNode parent, BaseScopedNode parentResource, ITypedElement wrapped)
        {
            Current = wrapped;
            ExceptionHandler = parent.ExceptionHandler;
            ParentResource = parent.AtResource ? parent : parentResource;
        }
        
        public string NearestResourceType => ParentResource == null ? Location : ParentResource.InstanceType;
        
        public override IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(ScopedNode) || type == typeof(BaseScopedNode))
                return new[] { this };
            else
                return Current.Annotations(type);
        }

        public override IEnumerable<ITypedElement> Children(string name = null) =>
            Current.Children(name).Select(c => new ScopedNode(this as ScopedNode, this.ParentResource, c));
    }


    public interface IBundledResource
    {
        string FullUrl { get; set; }
        BaseScopedNode Resource { get; set; }
    }
}
