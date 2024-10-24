/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Terminology;
using Hl7.FhirPath;
using System;
using System.Collections.Generic;

#nullable enable

namespace Hl7.Fhir.FhirPath
{
    public class FhirEvaluationContext : EvaluationContext
    {
        /// <summary>Creates a new <see cref="FhirEvaluationContext"/> instance with default property values.</summary>
        [Obsolete("This method does not initialize any members and will be removed in a future version. Use the empty constructor instead.")]
        public static new FhirEvaluationContext CreateDefault() => new();

        /// <summary>Default constructor. Creates a new <see cref="FhirEvaluationContext"/> instance with default property values.</summary>
        public FhirEvaluationContext()
        {
        }

        /// <inheritdoc cref="EvaluationContext(ITypedElement)"/>
        [Obsolete("%resource and %rootResource are inferred from scoped nodes by the evaluator. This behaviour is triggered when using the parameterless FhirEvaluationContext() constructor. " +
                  "If you do not have access to a scoped node, or if you wish to explicitly override this behaviour, use the FhirEvaluationContext.WithResourceOverrides() method.")]
        public FhirEvaluationContext(ITypedElement resource) : base(resource)
        {
        }

        /// <inheritdoc cref="EvaluationContext(ITypedElement, ITypedElement)"/>
        [Obsolete("%resource and %rootResource are inferred from scoped nodes by the evaluator. This behaviour is triggered when using the parameterless FhirEvaluationContext() constructor. " +
                  "If you do not have access to a scoped node, or if you wish to explicitly override this behaviour, use the FhirEvaluationContext.WithResourceOverrides() method.")]
        public FhirEvaluationContext(ITypedElement? resource, ITypedElement? rootResource) : base(resource, rootResource)
        {
        }

        /// <summary>
        /// Create a FhirEvaluationContext with a resource and an environment.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="environment"></param>
        [Obsolete("%resource and %rootResource are inferred from scoped nodes by the evaluator. This behaviour is triggered when using the parameterless FhirEvaluationContext() constructor. " +
                  "If you do not have access to a scoped node, or if you wish to explicitly override this behaviour, use the FhirEvaluationContext.WithResourceOverrides() method.")]
        public FhirEvaluationContext(ITypedElement resource, IDictionary<string, IEnumerable<ITypedElement>> environment) : base(resource, null, environment)
        {
        }

        /// <summary>
        /// Create a FhirEvaluationContext and also set the variables <c>%resource</c> and <c>%rootResource</c> to their correct values.
        /// </summary>
        /// <param name="node">input for determining the variables <c>%resource</c> and <c>%rootResource</c></param>
        [Obsolete("%resource and %rootResource are inferred from scoped nodes by the evaluator. This behaviour is triggered when using the parameterless FhirEvaluationContext() constructor. " +
                  "If you do not have access to a scoped node, or if you wish to explicitly override this behaviour, use the FhirEvaluationContext.WithResourceOverrides() method.")]
        public FhirEvaluationContext(ScopedNode node)
            : this(toNearestResource(node))
        {
            RootResource = Resource is ScopedNode sn ? sn.ResourceContext : node;
        }

        public ICodeValidationTerminologyService? TerminologyService { get; set; }

        private static ITypedElement toNearestResource(ScopedNode node)
        {
            var scan = node;

            while (scan.AtResource == false && scan.ParentResource is not null)
            {
                scan = scan.ParentResource;
            }

            return scan;
        }

        private Func<string, IScopedNode>? _elementResolver;

        public Func<string, IScopedNode>? ElementResolver
        {
            get { return _elementResolver; }
            set { _elementResolver = value; }
        }
    }
}

#nullable restore