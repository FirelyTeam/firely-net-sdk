/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.FhirPath.Parser;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Hl7.FhirPath.Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath.Functions;

namespace Hl7.FhirPath
{
    public class FhirPathCompiler
    {
        private static Lazy<SymbolTable> _defaultSymbolTable = new Lazy<SymbolTable>(() => new SymbolTable().AddStandardFP());
        
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

        public Expression Parse(string expression)
        {         
            var parse = Grammar.Expression.End().TryParse(expression);

            if (parse.WasSuccessful)
            {
                return parse.Value;
            }
            else
            {
               throw new FormatException("Compilation failed: " + parse.ToString());
            }
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
