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
    class InvalidStructureException : Exception
    {
        public InvalidStructureException(string message, params object[] args) : base(string.Format(message, args))
        {
            
        }
    }
}
