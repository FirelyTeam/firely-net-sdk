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
    public struct Outcome
    {
        public string Type;
        public string Name;
        public bool Valid;
        public override string ToString()
        {
            return string.Format("[{0}] {1} = {2}\n", this.Type, this.Name, this.Valid ? "Valid" : "Failed");
        }
    }

    public class Report
    {
        List<Outcome> outcomes = new List<Outcome>();

        public void Add(string type, string name, bool valid)
        {
            Outcome outcome;
            outcome.Type = type;
            outcome.Name = name;
            outcome.Valid = valid;
            outcomes.Add(outcome);
        }

        public void Clear()
        {
            outcomes.Clear();
        }

        public void Print(string filename, bool? outcome = null)
        {
            foreach (Outcome o in outcomes)
            {
                if (outcome == o.Valid || outcome == null)
                {
                    string s = o.ToString();
                    File.AppendAllText(filename, s);
                }
            }
        }
    }

}
