/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.FhirPath.Expressions;
using Hl7.FhirPath.Parser;
using Hl7.FhirPath.Sprache;
using System;

namespace Hl7.FhirPath
{
    public class FhirPathCompiler
    {
        private static Lazy<SymbolTable> _defaultSymbolTable = new(() => new SymbolTable().AddStandardFP());

        public static void SetDefaultSymbolTable(Lazy<SymbolTable> st)
        {
            _defaultSymbolTable = st;
        }

        public static SymbolTable DefaultSymbolTable
        {
            get { return _defaultSymbolTable.Value; }
        }

        public SymbolTable Symbols { get; private set; }

        public FhirPathCompiler(SymbolTable symbols)
        {
            Symbols = symbols;
        }

        public FhirPathCompiler() : this(DefaultSymbolTable)
        {
        }

#pragma warning disable CA1822 // Mark members as static
        public Expression Parse(string expression)
#pragma warning restore CA1822 // This might access instance data in the future.
        {
            var parse = Grammar.Expression.End().TryParse(expression);

            return parse.WasSuccessful ? parse.Value : throw new FormatException("Compilation failed: " + parse.ToString());
        }

        public CompiledExpression Compile(Expression expression)
        {
            Invokee inv = expression.ToEvaluator(Symbols);

            return (ITypedElement focus, EvaluationContext ctx) =>
                {
                    var closure = Closure.Root(focus, ctx);
                    return inv(closure, InvokeeFactory.EmptyArgs);
                };
        }

        public CompiledExpression Compile(string expression)
        {
            return Compile(Parse(expression));
        }
    }
}
