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
    public class ResolvingElementNavigator : ElementNavigator
    {
        private Stack<ElementNavigator> _positions = new Stack<ElementNavigator>();

        public ResolvingElementNavigator(Profile.ProfileStructureComponent structure) : base(structure)
        {
        }

        public ResolvingElementNavigator(ElementNavigator other) : base(other)
        {
        }

        public override void Reset()
        {
            
        }

        public override bool MoveToParent()
        {
            return base.MoveToParent();
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
          
    }
}
