/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.FhirPath.Expressions
{
    public class NewNodeListInitExpression : Expression
    {
        public NewNodeListInitExpression(): this(Enumerable.Empty<Expression>())
        {
            //
        }

        public NewNodeListInitExpression(IEnumerable<Expression> contents) : base(TypeInfo.Any)
        {
            if (contents == null) throw Error.ArgumentNull("contents");

            Contents = contents;
        }

        public IEnumerable<Expression> Contents { get; private set;  }

        public override T Accept<T>(ExpressionVisitor<T> visitor, SymbolTable scope)
        {
            return visitor.VisitNewNodeListInit(this, scope);
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is NewNodeListInitExpression)
            {
                var f = (NewNodeListInitExpression)obj;

                return f.Contents.SequenceEqual(Contents);
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Contents.GetHashCode();
        }

        public static readonly NewNodeListInitExpression Empty = new NewNodeListInitExpression(Enumerable.Empty<Expression>());
    }
}
