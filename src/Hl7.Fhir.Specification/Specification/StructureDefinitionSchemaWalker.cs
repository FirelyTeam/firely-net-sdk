/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification
{
    public class StructureDefinitionSchemaWalker : List<ElementDefinitionNavigator> //, ISchemaWalker
    {
        public readonly IEnumerable<ElementDefinitionNavigator> Set;
        public readonly IResourceResolver Resolver;

        public StructureDefinitionSchemaWalker(ElementDefinitionNavigator root, IResourceResolver resolver) : this(new[] { root }, resolver)
        {
            // calls other constructor
        }

        public StructureDefinitionSchemaWalker(IEnumerable<ElementDefinitionNavigator> collection, IResourceResolver resolver) : base(collection)
        {
            Set = collection.ToArray();
            Resolver = resolver;
        }

        public StructureDefinitionSchemaWalker(StructureDefinitionSchemaWalker other) : base(other)
        {
            Set = other.Set;
            Resolver = other.Resolver;
        }

        public StructureDefinitionSchemaWalker Child(string name)
        {
            var children = Set.SelectMany(def => firstChild(def, name));
            return new StructureDefinitionSchemaWalker(children, Resolver);

            IEnumerable<ElementDefinitionNavigator> firstChild(ElementDefinitionNavigator p, string n)
            {
                var scan = p.ShallowCopy();
                var appendedName = n + "[x]";

                if (scan.MoveToFirstChild())
                {
                    do
                    {
                        // just return first child found
                        if (scan.PathName == n || scan.PathName == appendedName)
                            return new[] { scan.ShallowCopy() };
                    }
                    while (scan.MoveToNext());
                }

                return Enumerable.Empty<ElementDefinitionNavigator>();
            }            
        }

        public StructureDefinitionSchemaWalker Resolve() { Console.WriteLine(".resolve()"); return this; }
        public StructureDefinitionSchemaWalker Extension(string url) { Console.WriteLine($"extension(\"{url}\")"); return this; }

        public StructureDefinitionSchemaWalker OfType(string type) { throw new NotImplementedException(); }

        public StructureDefinitionSchemaWalker Slice(string name) { throw new NotImplementedException();  }
    };

}
