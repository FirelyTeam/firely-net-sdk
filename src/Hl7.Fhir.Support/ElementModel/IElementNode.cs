/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using System.Collections.Generic;

namespace Hl7.Fhir.ElementModel
{

    public interface IElementNode
    {
        IElementNode Parent { get; set; }

        IList<IElementNode> Children { get; set; }

        // TODO: Should reflect the properties present in IElementNavigator. Once that stabilizes, copy the code comments from
        // there over to here
        string Name { get; set; }
        string Type { get; set; }
        object Value { get; set; }
        string Location { get; }

        IElementNode Clone();
    }

}