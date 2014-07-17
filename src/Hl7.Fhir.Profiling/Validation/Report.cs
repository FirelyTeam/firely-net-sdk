/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhir.Profiling
{

    public class Report : List<Outcome>
    {
        

        public IEnumerable<Outcome> Errors
        {
            get
            {
                return this.Where(outcome => outcome.Kind.Failed());
            }
        }

        public int ErrorCount
        {
            get 
            {
                return this.Count(o => o.Kind.Failed());
            }
        }

        public IEnumerable<Outcome> Where(Group group, Status kind)
        {
            return this.Where(outcome => outcome.Type == group && outcome.Kind == kind);
        }

        public bool Contains(Group group, Status kind)
        {
            return this.Where(group, kind).Count() > 0;
        }
        
        public bool IsValid
        {
            get
            {
                return this.Count(o => o.Kind.Failed()) == 0;
            }
        }

    }



}
