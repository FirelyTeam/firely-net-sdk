/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Model
{
    public class Cardinality
    {
        public string Min;
        public string Max;
        public bool InRange(int x)
        {
            int min = Convert.ToInt16(Min);
            if (x < min)
                return false;

            if (Max == "*")
                return true;

            int max = Convert.ToInt16(Max);
            if (x > max)
                return false;

            return true;
        }
        //public XPathExpression MinExpression;
        //public XPathExpression MaxExpression;
        public override string ToString()
        {
            return Min + ".." + Max;
        }
    }
}
