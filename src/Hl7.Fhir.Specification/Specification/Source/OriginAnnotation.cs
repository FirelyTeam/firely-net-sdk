/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.ComponentModel;

namespace Hl7.Fhir.Specification.Source
{
    [Obsolete("OriginInformation has been renamed to OriginAnnotation")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class OriginInformation
    {
        // Replaced by OriginAnnotation
    }

    /// <summary>Annotation for the location from which a resource was originally resolved.</summary>
    public class OriginAnnotation
    {
        /// <summary>Creates a new <see cref="OriginAnnotation"/> instance for the specified resource location.</summary>
        /// <param name="origin">The location from where a resource was originally resolved.</param>
        public OriginAnnotation(string origin) { Origin = origin; }

        /// <summary>Returns the location from which the resource was originally resolved.</summary>
        public string Origin { get; }
    }

    /// <summary>Helper methods to get/set <see cref="OriginAnnotation"/>s on resource instances.</summary>
    public static class OriginAnnotationExtensions
    {
        /// <summary>Annotate the resource with the location from which the resource was originally resolved.</summary>
        /// <param name="resource">A resource instance.</param>
        /// <param name="origin">The location from which the resource was originally resolved.</param>
        /// <remarks>Uses the <see cref="OriginAnnotation"/> annotation class.</remarks>
        public static void SetOrigin(this Resource resource, string origin)
            => resource?.SetAnnotation(origin != null ? new OriginAnnotation(origin) : null);

        /// <summary>Get the annotated original resource location, if it exists.</summary>
        /// <param name="resource">A resource instance.</param>
        /// <returns>The original resource location, if annotated, or <c>null</c> otherwise.</returns>
        /// <remarks>Uses the <see cref="OriginAnnotation"/> annotation class.</remarks>
        public static string GetOrigin(this Resource resource)
            => resource?.Annotation<OriginAnnotation>()?.Origin;

        // <summary>Indicates if the resource has an <see cref="OriginAnnotation"/> with the original location.</summary>
        // <param name="resource">A resource instance.</param>
        // <returns><c>true</c> if the resource has been annotated with the original location, or <c>false</c> otherwise.</returns>
        // public static bool HasOrigin(this Resource resource) => resource?.GetOrigin() != null;
    }
}
