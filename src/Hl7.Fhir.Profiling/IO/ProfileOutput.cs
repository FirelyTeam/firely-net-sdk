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
    public static class ProfileOutput
    {
        public static void ToConsole(this IEnumerable<Outcome> outcomes)
        {
            foreach(Outcome outcome in outcomes)
            {
                Console.WriteLine(outcome);
            }
        }
    }
}
