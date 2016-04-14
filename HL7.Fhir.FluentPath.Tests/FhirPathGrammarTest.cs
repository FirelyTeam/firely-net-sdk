/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.FluentPath;
using Sprache;
using System.Linq;
using HL7.Fhir.FluentPath.FluentPath.Expressions;
using Hl7.Fhir.FluentPath.Parser;

namespace Hl7.Fhir.Tests.FhirPath
{
    [TestClass]
    public class FhirPathExpressionTest
    {

        [TestMethod, TestCategory("FhirPath")]
        public void FhirPath_Lex_Literal()
        {
            var parser = Grammar.Literal.End();

            SucceedsConstantValueMatch(parser, "'hi there'", "hi there");
            SucceedsConstantValueMatch(parser, "3", 3L);
            SucceedsConstantValueMatch(parser, "3.14", 3.14m);
            SucceedsConstantValueMatch(parser, "@2013-12", PartialDateTime.Parse("2013-12"));
            SucceedsConstantValueMatch(parser, "@T12:23:34Z", Time.Parse("T12:23:34Z"));
            SucceedsConstantValueMatch(parser, "true", true);

            AssertParser.FailsMatch(parser, "%constant");
            AssertParser.FailsMatch(parser, "\"quotedstring\"");
            AssertParser.FailsMatch(parser, "A23identifier");
        }

        private void SucceedsConstantValueMatch(Parser<ConstantExpression> parser, string expr, object value)
        {
            AssertParser.SucceedsWith(parser, expr, v =>  Assert.AreEqual(v.Value, value));
        }

    }
}
