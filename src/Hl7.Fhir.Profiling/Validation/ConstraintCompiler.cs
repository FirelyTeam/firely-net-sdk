/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using Fhir.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Fhir.IO;

namespace Fhir.Profiling
{
    public static class ConstraintCompiler
    {
        public static void Compile(Constraint constraint)
        {
            try
            {
                constraint.Compiled = true;
                var expr = XPathExpression.Compile(constraint.XPath);
                expr.SetContext(FhirNamespaceManager.CreateManager());
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
