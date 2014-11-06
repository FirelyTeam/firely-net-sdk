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
using System.Xml.XPath;

namespace Hl7.Fhir.Validation
{

    public class Report 
    {
        public List<Outcome> Outcomes = new List<Outcome>();

        public void Add(params Outcome[] outcomes)
        {
            Outcomes.AddRange(outcomes);
        }

        public void Clear()
        {
            Outcomes.Clear();
        }

        public IEnumerable<Outcome> Errors
        {
            get
            {
                return Outcomes.Where(outcome => outcome.Kind.Failed());
            }
        }

        

        public IEnumerable<Outcome> ErrorsAt(XPathNavigator node)
        {

            foreach (Outcome outcome in Errors)
            {
                XPathNavigator n = outcome.Vector.Node;

                if (outcome.Vector.Node.IsSamePosition(node))
                {
                    yield return outcome;
                }
            }
        }
        
        public bool HasErrorAt(XPathNavigator node)
        {
            return ErrorsAt(node).Count() > 0;
        }

        public int ErrorCount
        {
            get 
            {
                return Outcomes.Count(o => o.Kind.Failed());
            }
        }

        public IEnumerable<Outcome> Where(Group group, Status kind)
        {
            return Outcomes.Where(outcome => outcome.Type == group && outcome.Kind == kind);
        }

        public bool Contains(Group group, Status kind)
        {
            return this.Where(group, kind).Count() > 0;
        }
        
        public bool IsValid
        {
            get
            {
                return Outcomes.Count(o => o.Kind.Failed()) == 0;
            }
        }

    }



}
