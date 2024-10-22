using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;

#nullable enable

namespace Hl7.FhirPath;

public class EvaluationContext
{
    [Obsolete("This method does not initialize any members and will be removed in a future version. Use the empty constructor instead.")]
    public static EvaluationContext CreateDefault() => new();

    
    public EvaluationContext()
    {
        // no defaults yet
    }

    /// <summary>
    /// Create an EvaluationContext with the given value for <c>%resource</c>.
    /// </summary>
    /// <param name="resource">The data that will be represented by %resource</param>
    [Obsolete("%resource and %rootResource are inferred from scoped nodes by the evaluator. If you do not have access to a scoped node, or if you wish to explicitly override this behaviour, use the EvaluationContext.WithResourceOverrides() method.")]
    public EvaluationContext(ITypedElement? resource) : this(resource, null) { }

    /// <summary>
    /// Create an EvaluationContext with the given value for <c>%resource</c> and <c>%rootResource</c>.
    /// </summary>
    /// <param name="resource">The data that will be represented by <c>%resource</c>.</param>
    /// <param name="rootResource">The data that will be represented by <c>%rootResource</c>.</param>
    [Obsolete("%resource and %rootResource are inferred from scoped nodes by the evaluator. If you do not have access to a scoped node, or if you wish to explicitly override this behaviour, use the EvaluationContext.WithResourceOverrides() method.")]
    public EvaluationContext(ITypedElement? resource, ITypedElement? rootResource)
    {
        Resource = resource;
        RootResource = rootResource ?? resource;
    }
        
    [Obsolete("%resource and %rootResource are inferred from scoped nodes by the evaluator. If you do not have access to a scoped node, or if you wish to explicitly override this behaviour, use the EvaluationContext.WithResourceOverrides() method. Environment can be set explicitly after construction of the base context")]
    public EvaluationContext(ITypedElement? resource, ITypedElement? rootResource, IDictionary<string, IEnumerable<ITypedElement>> environment) : this(resource, rootResource)
    {
        Environment = environment;
    }
    
    /// <summary>
    /// The data represented by <c>%rootResource</c>.
    /// </summary>
    public ITypedElement? RootResource { get; set; }

    /// <summary>
    /// The data represented by <c>%resource</c>.
    /// </summary>
    public ITypedElement? Resource { get; set; }

    /// <summary>
    /// The environment variables that are available to the FHIRPath expressions.
    /// </summary>
    public IDictionary<string, IEnumerable<ITypedElement>> Environment { get; set; } = new Dictionary<string, IEnumerable<ITypedElement>>();

    /// <summary>
    /// A delegate that handles the output for the <c>trace()</c> function.
    /// </summary>
    public Action<string, IEnumerable<ITypedElement>>? Tracer { get; set; }
}

public static class EvaluationContextExtensions
{
    public static T WithResourceOverrides<T>(this T context, ITypedElement? resource, ITypedElement? rootResource = null) where T : EvaluationContext
    {
        context.Resource = resource;
        context.RootResource = rootResource ?? resource;
        return context;
    }
}