/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System.Collections.Generic;

namespace Hl7.Fhir.Model
{
    public interface IElementConstraintContainer
    {
        List<ElementDefinition.ConstraintComponent> InvariantConstraints { get; set; }
    }
}

