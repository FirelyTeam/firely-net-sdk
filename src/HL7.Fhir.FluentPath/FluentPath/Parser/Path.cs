/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using Sprache;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// The FhirPath parser is using Sprache, a monad-based approach to parsing
// Useful pointers:
// http://ericlippert.com/2013/02/21/monads-part-one/
// http://blogs.msdn.com/b/wesdyer/archive/2008/01/11/the-marvels-of-monads.aspx
// https://github.com/louthy/csharp-monad
// http://www.codeproject.com/Articles/649989/Monad-like-programming-with-Csharp


namespace Hl7.Fhir.FluentPath.Parser
{

    internal class Path
    {        

        // element recurse?
        public static readonly Parser<Evaluator> ElementPath =
            from element in Lexer.Identifier
            select Eval.Children(element);

        // item: element recurse? | function | axis_spec
        public static readonly Parser<Evaluator> Invoc =
            Functions.Function
            .Or(ElementPath)
            .Token();
    }
}
