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
using System.Xml.XPath;
using Hl7.Fhir.Specification.Model;

namespace Hl7.Fhir.Validation
{
    public delegate void OutcomeLogger(Outcome outcome);
    public class ReportBuilder
    {
        private int nesting = 0;
        public Report Report = new Report();
        
        public void Clear()
        {
            Report.Clear();
        }

        public void Add(Outcome outcome)
        {

            Report.Add(outcome);
        }

        public Outcome Start(Group group, Vector vector = null)
        {
            Outcome outcome = new Outcome(group, Status.Start, vector, null, this.nesting);
            Add(outcome);
            nesting++;
            return outcome;
        }

        public Outcome End(Group group)
        {
            nesting--;
            Outcome outcome = new Outcome(group, Status.End, null, null, this.nesting);
            Add(outcome);
            
            return outcome;
        }

        public Outcome Log(Group group, Status status, Vector vector)
        {
            return new Outcome(group, status, vector, null, this.nesting);
        }

        public Outcome Log(Group group, Status status)
        {
            return new Outcome(group, status, null, null, this.nesting);
        }

        public Outcome Log(Group group, Status status, Vector vector, string message, params object[] args)
        {
            Outcome outcome = new Outcome(group, status, vector, string.Format(message, args), this.nesting);
            
            Add(outcome);
            return outcome;
        }

        public Outcome Log(Group group, Status status, string message, params object[] args)
        {
            Outcome outcome = new Outcome(group, status, null, string.Format(message, args), this.nesting);
            Add(outcome);
            return outcome;
        }

    }

}
