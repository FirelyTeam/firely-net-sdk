/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Terminology;
using Hl7.FhirPath;
using System;
using System.Collections.Generic;

#nullable enable

namespace Hl7.Fhir.FhirPath
{
    /// <summary>
    /// A factory that creates a new model object for the FHIRPath
    /// <see href="https://build.fhir.org/ig/HL7/FHIRPath/en/#instance-selector">instance selector / object creation</see>
    /// feature, e.g. <c>Coding { system: 'http://example.org', code: 'c1' }</c>.
    /// </summary>
    /// <param name="typeName">
    /// The (optionally namespaced) name of the type to create, e.g. <c>Coding</c> or <c>FHIR.Identifier</c>.
    /// </param>
    /// <param name="elements">
    /// The elements to set on the created object, in source order. Each entry pairs an element name with the
    /// (non-empty) values that its value expression evaluated to. Elements whose value evaluated to an empty
    /// collection are already excluded by the evaluator.
    /// </param>
    /// <returns>
    /// The created object as an <see cref="ITypedElement"/>, or <c>null</c> when the type is unknown or cannot
    /// be created (in which case the instance selector yields an empty collection).
    /// </returns>
    public delegate ITypedElement? ModelObjectFactory(string typeName, IReadOnlyCollection<KeyValuePair<string, IEnumerable<ITypedElement>>> elements);

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
        
        public ITerminologyService? TerminologyService { get; set; }

        private static ITypedElement toNearestResource(ScopedNode node)
        {
            var scan = node;

            while (scan.AtResource == false && scan.ParentResource is not null)
            {
                scan = scan.ParentResource;
            }

            return scan;
        }

        private Func<string, ITypedElement?>? _elementResolver;

        /// <summary>
        /// A function that is invoked when resolve() is called in the fhirpath expression.
        /// Should return the ITypedElement for the given Id. Example: Patient/1234
        /// Should returns null if the resource cannot be found.
        /// </summary>
        public Func<string, ITypedElement?>? ElementResolver
        {
            get { return _elementResolver; }
            set { _elementResolver = value; }
        }

        /// <summary>
        /// A factory used by the FHIRPath instance selector / object creation feature
        /// (e.g. <c>Coding { system: 'http://example.org', code: 'c1' }</c>) to create the resulting object.
        /// When not set, evaluating an instance selector expression will signal an error to the calling environment.
        /// </summary>
        /// <remarks>
        /// For POCO-based FHIRPath evaluation this is defaulted to a factory that creates FHIR POCO objects.
        /// </remarks>
        public ModelObjectFactory? ObjectFactory { get; set; }
    }
}

#nullable restore
