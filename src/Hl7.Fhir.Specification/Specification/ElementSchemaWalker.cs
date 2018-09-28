/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hl7.Fhir.Specification
{
    public class ElementSchemaWalker : List<ITypeSerializationInfo>
    {
        public ElementSchemaWalker()
        {
            //
        }

        public ElementSchemaWalker(ITypeSerializationInfo root) : this(new[] { root })
        {
            //
        }

        public ElementSchemaWalker(IEnumerable<ITypeSerializationInfo> collection) : base(collection)
        {
            //
        }

        public ElementSchemaWalker(ElementSchemaWalker other) : base(other)
        {
            //
        }

        public string DebugResult = "";

        public ElementSchemaWalker Children(string name) { Console.WriteLine("." + name); return this; }
        public ElementSchemaWalker Resolve() { Console.WriteLine(".resolve()"); return this; }
        public ElementSchemaWalker Extension(string url) { Console.WriteLine($"extension(\"{url}\")"); return this; }
    };

}
