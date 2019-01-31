/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public static class TypedElementExtensions
    {
        public static ElementNode NewChild(this ITypedElement parent, IStructureDefinitionSummaryProvider provider, string name, object value, string instanceType = null)
        {
            //TODO: probably figure out whether parent is already ElementNode to do performance improvements
            var childDefs = parent.ChildDefinitions(provider ?? throw Error.ArgumentNull(nameof(provider)));

            var childDef = childDefs.Where(cd => cd.ElementName == name).SingleOrDefault();

            // Note may create a child with Definition==null when no definition found
            return new ElementNode(name, value, instanceType, childDef);
        }

        public static ElementNode NewChild(this ITypedElement parent, IStructureDefinitionSummaryProvider provider, string name, string instanceType = null)
             => parent.NewChild(provider, name, null, instanceType);

        // base this in TypedElementOnSourceNode.NewChild("name",....) which creates a new typed child,
        // probably making now-private code that connects the source+element world re-useable.
        //public static ElementNode NewChild(this ITypedElement parent, ISourceNode node, bool recursive = true, IEnumerable<Type> annotationsToCopy = null)
        //        => throw new NotImplementedException();
    }

}
