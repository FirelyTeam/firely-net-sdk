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

namespace Hl7.Fhir.Specification.Model
{
    public class Constraint
    {
        public Constraint()
        {
            Compiled = false;
            IsValid = false;
        }
        public string Name;
        public string XPath;
        public string HumanReadable;
        public XPathExpression Expression {get ; set; }
        public bool IsValid;
        public bool Compiled { get; set; }
        public Exception CompilerError { get; set; }
        public override string ToString()
        {
            return string.Format("{0} [{1}]: {2}", Name, IsValid ? "Valid" : "Invalid", HumanReadable ?? XPath);
        }
    }
}
