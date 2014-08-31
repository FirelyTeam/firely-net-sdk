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

namespace Fhir.Profiling
{
    public class ValueSet
    {
        public string System { get; set; }
        public List<string> codes = new List<string>();
        public bool Contains(string code)
        {
            return codes.Exists(c => c == code);
        }
        public override string ToString()
        {
            return System;
        }
    }
}
