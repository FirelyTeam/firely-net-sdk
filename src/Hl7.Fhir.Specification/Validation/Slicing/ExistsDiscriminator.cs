/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal class ExistsDiscriminator : IDiscriminator
    {
        public bool ExistsMode { get; private set; }

        public ExistsDiscriminator(bool existsMode, string path)
        {
            Path = path;
            ExistsMode = existsMode;
        }

        public string Path { get; }

        public bool Matches(ITypedElement candidate)
        {
            ITypedElement[] values = candidate.Select(Path).ToArray();

            return ExistsMode == values.Any();
        }
    }
}
