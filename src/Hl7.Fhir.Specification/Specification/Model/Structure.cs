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

    public class Structure
    {
        public string Type { get; set; }
        public string Name { get; set; }
        
        public Uri ProfileUri { get; set; }
        public Uri Uri { get; set; }

        public List<Element> Elements = new List<Element>();
        
        public Element Root
        {
            get
            {
                return Elements.FirstOrDefault(e => e.Path.Count == 1);
            }
        }

        public override string ToString()
        {
            string name = (Name != null) ? string.Format("{0} ({1})", Name, Type) : Type;
            return string.Format("{0} ({1} elements)", name, Elements.Count);
        }

        public string NameSpacePrefix { get; set; }
    }
}
