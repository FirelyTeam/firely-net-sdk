using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;

namespace Hl7.FhirPath
{
    public class EvaluationContext
    {
        public static EvaluationContext CreateDefault() => new();

        public EvaluationContext()
        {
            // no defaults yet
        }

        /// <summary>
        /// Create an EvaluationContext with the given value for <c>%resource</c>.
        /// </summary>
        /// <param name="resource">The data that will be represented by %resource</param>
        public EvaluationContext(ITypedElement resource) : this(resource, null) { }

        /// <summary>
        /// Create an EvaluationContext with the given value for <c>%resource</c> and <c>%rootResource</c>.
        /// </summary>
        /// <param name="resource">The data that will be represented by <c>%resource</c>.</param>
        /// <param name="rootResource">The data that will be represented by <c>%rootResource</c>.</param>
        public EvaluationContext(ITypedElement resource, ITypedElement rootResource)
        {
            Resource = resource;
            RootResource = rootResource ?? resource;
        }

        public EvaluationContext(IScopedNode resource) : this(resource.AsTypedElement(), resource.GetRootResource().AsTypedElement())
        {
        }

        /// <summary>
        /// The data represented by <c>%rootResource</c>.
        /// </summary>
        public ITypedElement RootResource { get; set; }

        /// <summary>
        /// The data represented by <c>%resource</c>.
        /// </summary>
        public ITypedElement Resource { get; set; }

        /// <summary>
        /// A delegate that handles the output for the <c>trace()</c> function.
        /// </summary>
        public Action<string, IEnumerable<ITypedElement>> Tracer { get; set; }
    }
}