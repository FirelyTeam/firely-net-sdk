/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;
using System.Collections.Generic;

namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// This interface is implemented by FHIR parsers to represent the resource type indicator
    /// found in the parsed data.
    /// </summary>
    public interface IResourceTypeSupplier
    {
        /// <summary>
        /// Gets the resource type found at the location of the node in the source data (if any).
        /// </summary>
        /// <value>The value of resource type indicator (e.g. <c>resourceType</c> in json, or contained node in XML) or
        /// <c>null</c> if such an indicator was not found.</value>
        string ResourceType { get; }
    }
}