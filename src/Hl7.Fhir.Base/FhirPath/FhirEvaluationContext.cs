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
        /// <summary>Default constructor. Creates a new <see cref="FhirEvaluationContext"/> instance with default property values.</summary>
        public FhirEvaluationContext()
        {
        }

        public ICodeValidationTerminologyService? TerminologyService { get; set; }

        private static IScopedNode toNearestResource(ScopedNode node)
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