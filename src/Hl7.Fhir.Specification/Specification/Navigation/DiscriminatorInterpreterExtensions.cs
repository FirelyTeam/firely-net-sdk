/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Navigation
{
    public static class DiscriminatorInterpeterExtensions
    {
        internal static IEnumerable<StructureDefinitionWalker> EvaluateDiscriminator(this StructureDefinitionWalker walker, Expression expr)
        {
            var interpeter = new DiscriminatorInterpreter(walker);
            //interpeter.AssertSupportedRootExpression(expr);
            return expr.Accept(interpeter, new SymbolTable());
        }

        /// <summary>
        /// Walk the definition given a discriminator path.
        /// </summary>
        /// <param name="me"></param>
        /// <param name="discriminatorExpression"></param>
        /// <returns></returns>
        /// <remarks>Discriminator paths are subsets of FhirPath as described here: http://www.hl7.org/implement/standards/fhir/profiling.html#discriminator</remarks>
        public static IList<StructureDefinitionWalker> Walk(this StructureDefinitionWalker me, string discriminatorExpression)
        {
            if (discriminatorExpression == null) throw Error.ArgumentNull(nameof(discriminatorExpression));

            var compiler = new FhirPathCompiler(new SymbolTable());  // Basic symbol table is enough to have ofType() and resolve()
            var tree = compiler.Parse(discriminatorExpression);

            return me.EvaluateDiscriminator(tree).ToList();
        }

    }

}
