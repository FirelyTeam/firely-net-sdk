/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal abstract class PathBasedDiscriminator : IDiscriminator
    {
        public PathBasedDiscriminator(string path)
        {
            Path = path ?? throw new System.ArgumentNullException(nameof(path));
        }

        public readonly string Path;

        public bool Matches(ITypedElement candidate)
        {
            ITypedElement[] values = candidate.Select(Path).ToArray();

            // Don't know how to handle a discriminating element that repeats - all? any?
            if (values.Length > 1)
                throw Error.NotImplemented($"The instance has multiple elements at '{candidate.Location}' " +
                    $"for the discriminator path '{Path}'. Don't know how to handle that.");
            else if (values.Length == 0)
                return false;
            else
                return MatchInternal(values.Single());
        }

        abstract protected bool MatchInternal(ITypedElement candidate);
    }
}
