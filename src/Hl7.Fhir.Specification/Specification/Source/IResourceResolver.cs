﻿/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Interface for resolving FHIR artifacts by (canonical) uri.</summary>
    public interface IResourceResolver // open ended domain
    {
        /// <summary>Find a resource based on its relative or absolute uri.</summary>
        /// <param name="uri">A resource uri.</param>
        Resource ResolveByUri(string uri);


        /// <summary>Find a (conformance) resource based on its canonical uri.</summary>
        /// <param name="uri">The canonical url of a (conformance) resource.</param>
        Resource ResolveByCanonicalUri(string uri); // IConformanceResource
    }

}
