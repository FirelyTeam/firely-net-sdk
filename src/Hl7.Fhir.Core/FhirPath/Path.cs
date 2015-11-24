using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FhirPath
{
    internal class Path
    {
        // item: element recurse? | function | axis_spec | '(' expr ')';
        public static readonly Parser<string> Item = Lexer.Element;

        // predicate: (root_spec | item) ('.' item)* ;
        public static readonly Parser<string> Predicate =
            from root in (Lexer.RootSpec.Or(Item))
            from after in (
                from dot in Parse.Char('.')
                from item in Item
                select item).Many()
            select root + "." + String.Join(".",after);
                                 
        // function: ID '(' param_list? ')';
        // param_list: expr(',' expr)*;

    }
}
