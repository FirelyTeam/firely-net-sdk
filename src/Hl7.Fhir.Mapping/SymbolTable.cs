/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System.Collections.Generic;
using System.Linq.Expressions;

namespace Hl7.Fhir.Mapping
{
    public class MappingSymbols
    {
        public MappingSymbols()
        {
        }

        private Dictionary<string, Expression> _symbols = new Dictionary<string, Expression>();

        public MappingSymbols Parent { get; private set; }

        public MappingSymbols Nest() => new MappingSymbols() { Parent = this };

        public Expression this[string label]
        {
            get => _symbols.TryGetValue(label, out var symbol) ? symbol : Parent?[label];
            set => _symbols[label] = value;
        }
    }
}
