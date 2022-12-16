/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using System;

namespace Hl7.Fhir.FhirPath
{
    public class FhirEvaluationContext : EvaluationContext
    {
        /// <summary>Creates a new <see cref="FhirEvaluationContext"/> instance with default property values.</summary>
        public static new FhirEvaluationContext CreateDefault() => new FhirEvaluationContext();

        /// <summary>Default constructor. Creates a new <see cref="FhirEvaluationContext"/> instance with default property values.</summary>
        public FhirEvaluationContext() : base()
        {
        }

        /// <inheritdoc cref="EvaluationContext.EvaluationContext(ITypedElement)"/>
        public FhirEvaluationContext(ITypedElement resource) : base(resource)
        {
        }

        /// <inheritdoc cref="EvaluationContext.EvaluationContext(ITypedElement, ITypedElement)"/>
        public FhirEvaluationContext(ITypedElement resource, ITypedElement rootResource) : base(resource, rootResource)
        {
        }

        /// <summary>
        /// Create a FhirEvaluationContext and also set the variables <c>%resource</c> and <c>%rootResource</c> to their correct values.
        /// </summary>
        /// <param name="node">input for determining the variables <c>%resource</c> and <c>%rootResource</c></param>
        public FhirEvaluationContext(ScopedNode node)
            : this(toNearestResource(node))
        {
            RootResource = Resource is ScopedNode sn ? sn.ResourceContext : node;
        }

        private static ITypedElement toNearestResource(ScopedNode node)
        {
            var scan = node;

            while (scan.AtResource == false && scan.ParentResource is not null)
            {
                scan = scan.ParentResource;
            }

            return scan;
        }

        private Func<string, ITypedElement> _elementResolver;

        public Func<string, ITypedElement> ElementResolver
        {
            get { return _elementResolver; }
            set { _elementResolver = value; }
        }
    }
}
