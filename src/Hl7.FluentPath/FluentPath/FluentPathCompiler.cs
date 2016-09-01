/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.FluentPath.Parser;
using Hl7.FluentPath;
using Hl7.FluentPath.Expressions;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.ElementModel;
using Hl7.FluentPath.Functions;

namespace Hl7.FluentPath
{
    public class FluentPathCompiler
    {
        private static Lazy<SymbolTable> _defaultSymbolTable = new Lazy<SymbolTable>(() => new SymbolTable().AddStandardFP(), isThreadSafe: false);

        public static SymbolTable DefaultSymbolTable
        { 
            get { return _defaultSymbolTable.Value; }
        }

        public SymbolTable Symbols { get; private set; }

        public FluentPathCompiler(SymbolTable symbols)
        {
            Symbols = symbols;
        }

        public FluentPathCompiler() : this(DefaultSymbolTable)
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

            return (IValueProvider focus, IValueProvider containerResource) =>
                {
                    var closure = Closure.Root(focus, containerResource);
                    return inv(closure, InvokeeFactory.EmptyArgs);
                };
        }

        public CompiledExpression Compile(string expression)
        {
            return Compile(Parse(expression));
        }
    }
}
