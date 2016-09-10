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
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Hl7.Fhir.XPath;
using Hl7.Fhir.Specification.Model;

namespace Hl7.Fhir.Validation
{
    public static class ConstraintCompiler
    {
        private volatile static Lazy<XmlNamespaceManager> nsm = new Lazy<XmlNamespaceManager>(delegate() { return FhirNamespaceManager.CreateManager(); });
        
        public static void Compile(this Constraint constraint)
        {
            try
            {
                constraint.Compiled = true;
                var expr = XPathExpression.Compile(constraint.XPath);
                expr.SetContext(nsm.Value);
                constraint.Expression = expr;
                constraint.IsValid = true;
            }
            catch (Exception e)
            {
                constraint.CompilerError = e;
                constraint.IsValid = false;
            }
        }
    }
}
