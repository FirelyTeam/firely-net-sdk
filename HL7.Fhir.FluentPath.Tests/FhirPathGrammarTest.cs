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
        public void FhirPath_Gramm_Literal()
        {
            var parser = Grammar.Literal.End();

            AssertParser.SucceedsMatch(parser, "'hi there'", new ConstantExpression("hi there", FluentPathType.String));
            AssertParser.SucceedsMatch(parser, "3", new ConstantExpression(3L, FluentPathType.Integer));
            AssertParser.SucceedsMatch(parser, "3.14", new ConstantExpression(3.14m, FluentPathType.Decimal));
            AssertParser.SucceedsMatch(parser, "@2013-12", new ConstantExpression(PartialDateTime.Parse("2013-12"), FluentPathType.DateTime));
            AssertParser.SucceedsMatch(parser, "@T12:23:34Z", new ConstantExpression(Time.Parse("T12:23:34Z"), FluentPathType.Time));
            AssertParser.SucceedsMatch(parser, "true", new ConstantExpression(true, FluentPathType.Bool));

            AssertParser.FailsMatch(parser, "%constant");
            AssertParser.FailsMatch(parser, "\"quotedstring\"");
            AssertParser.FailsMatch(parser, "A23identifier");
        }

        [TestMethod, TestCategory("FhirPath")]
        public void FhirPath_Gramm_Invocation()
        {
            var parser = Grammar.Invocation(AxisExpression.This).End();

            AssertParser.SucceedsMatch(parser, "childname", new ChildExpression(AxisExpression.This, "childname"));
            AssertParser.SucceedsMatch(parser, "$this", AxisExpression.This);
            AssertParser.SucceedsMatch(parser, "doSomething()", new FunctionCallExpression(AxisExpression.This, "doSomething", FluentPathType.Any));
            AssertParser.SucceedsMatch(parser, "doSomething('hi', 3.14, 3, $this, somethingElse(true))", new FunctionCallExpression(AxisExpression.This,"doSomething", FluentPathType.Any,
                        new ConstantExpression("hi", FluentPathType.String),
                        new ConstantExpression(3.14m, FluentPathType.Decimal),
                        new ConstantExpression(3L, FluentPathType.Integer),
                        AxisExpression.This,
                        new FunctionCallExpression(AxisExpression.This, "somethingElse", FluentPathType.Any, new ConstantExpression(true, FluentPathType.Bool))));

            AssertParser.FailsMatch(parser, "$that");
            AssertParser.FailsMatch(parser, "doSomething(");
        }

        [TestMethod, TestCategory("FhirPath")]
        public void FhirPath_Gramm_Term()
        {
            var parser = Grammar.Term.End();

            AssertParser.SucceedsMatch(parser, "childname", new ChildExpression(AxisExpression.This,"childname"));
            AssertParser.SucceedsMatch(parser, "$this", AxisExpression.This);
            AssertParser.SucceedsMatch(parser, "doSomething()", new FunctionCallExpression(AxisExpression.This, "doSomething", FluentPathType.Any));
            AssertParser.SucceedsMatch(parser, "doSomething('hi', 3.14)", new FunctionCallExpression(AxisExpression.This, "doSomething", FluentPathType.Any,
                        new ConstantExpression("hi", FluentPathType.String), new ConstantExpression(3.14m, FluentPathType.Decimal)));
            AssertParser.SucceedsMatch(parser, "%external", new ExternalConstantExpression("external"));
            AssertParser.SucceedsMatch(parser, "@2013-12", new ConstantExpression(PartialDateTime.Parse("2013-12"), FluentPathType.DateTime));
            AssertParser.SucceedsMatch(parser, "3", new ConstantExpression(3L, FluentPathType.Integer));
            AssertParser.SucceedsMatch(parser, "true", new ConstantExpression(true, FluentPathType.Bool));
            AssertParser.SucceedsMatch(parser, "(3)", new ConstantExpression(3L, FluentPathType.Integer));
            AssertParser.SucceedsMatch(parser, "{}", NewNodeListInitExpression.Empty);
        }


        [TestMethod, TestCategory("FhirPath")]
        public void FhirPath_Gramm_Expression_Invocation()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsMatch(parser, "Patient.name.doSomething(true)",
                    new FunctionCallExpression(
                            new ChildExpression(new ChildExpression(AxisExpression.This, "Patient"), "name"),
                            "doSomething", FluentPathType.Any, new ConstantExpression(true, FluentPathType.Bool)));
            //AssertParser.SucceedsMatch(parser, "Patient.name.", new ChildExpression(new ChildExpression(AxisExpression.This, "Patient"), "name"));
        }

        private void SucceedsConstantValueMatch(Parser<ConstantExpression> parser, string expr, object value, FluentPathType expected)
        {
            AssertParser.SucceedsWith(parser, expr,
                    v =>
                        {
                            Assert.AreEqual(v.Value, value);
                            Assert.AreEqual(v.ExpressionType, expected);
                        });
        }

    }
}
