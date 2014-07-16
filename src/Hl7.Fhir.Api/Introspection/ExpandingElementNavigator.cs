/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Introspection.Source;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Introspection
{
    public class ExpandingElementNavigator : ElementNavigator
    {
        public ExpandingElementNavigator(Profile.ProfileStructureComponent structure) : base(structure)
        {
        }

        public ExpandingElementNavigator(ElementNavigator other) : base(other)
        {
        }

        public override bool MoveToFirstChild()
        {
            throw new NotImplementedException();
            //var childPos = OrdinalPosition + 1;

            //if (childPos < Count && IsDirectChildPath(Path, Elements[childPos].Path))
            //{
            //    OrdinalPosition = childPos;
            //    return true;
            //}

            //return false;
        }
           
      //  public override bool Approach(string path)
      //  {
            //var bestMatch = -1;
            //var matchSize = -1;

            //for (var ix = 0; ix < Count; ix++)
            //{
            //    var scanPath = Elements[ix].Path; 

            //    // If we found a path that is exactly the same, we're ready, this is the best match
            //    if (scanPath == path)
            //    {
            //        bestMatch = ix;
            //        break;
            //    }

            //    // Else, if the path of this element is "on the way" to the path given
            //    else if (path.StartsWith(scanPath + "."))
            //    {
            //        // ...check whether it's a longer match than any previous found before
            //        if (scanPath.Length > matchSize)
            //        {
            //            matchSize = scanPath.Length;
            //            bestMatch = ix;
            //        }
            //    }
            //}

            //if (bestMatch == -1)
            //    return false;   // No match found
            //else
            //{
            //    // Move to the best match found
            //    OrdinalPosition = bestMatch;
            //    return true;
            //}
    //    }
    }
}
