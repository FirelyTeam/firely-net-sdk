/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Navigation.FhirPath
{
    public static class DiscriminatorInterpeterExtensions
    {
        internal static IEnumerable<StructureDefinitionSchemaWalker> EvaluateDiscriminator(this StructureDefinitionSchemaWalker walker, Expression expr)
        {
            var interpeter = new DiscriminatorInterpreter(walker);
            interpeter.AssertSupportedRootExpression(expr);
            return expr.Accept(interpeter);
        }

        public static IEnumerable<StructureDefinitionSchemaWalker> Walk(this StructureDefinitionSchemaWalker me, string discriminatorExpression)
        {
            if (discriminatorExpression == null) throw Error.ArgumentNull(nameof(discriminatorExpression));

            var compiler = new FhirPathCompiler(new SymbolTable());
            var tree = compiler.Parse(discriminatorExpression);

            return me.EvaluateDiscriminator(tree);
        }

    }

}
