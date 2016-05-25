/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Hl7.Fhir.Model;


namespace Hl7.Fhir.Serialization
{
    public class RepeatingElementReader
    {
        private IFhirReader _current;
        private ModelInspector _inspector;

        public ParserSettings Settings { get; private set; }

        public RepeatingElementReader(IFhirReader reader, ParserSettings settings)
        {
            _current = reader;
            _inspector = BaseFhirParser.Inspector;

            Settings = settings;
        }

        

        public IList Deserialize(PropertyMapping prop, string memberName, IList existing=null)
        {
            if (prop == null) throw Error.ArgumentNull("prop");

            IList result = existing;

            if (result == null) result = ReflectionHelper.CreateGenericList(prop.ElementType);

            var reader = new DispatchingReader(_current, Settings, arrayMode: true);                 
            result.Add(reader.Deserialize(prop, memberName));

            return result;
        }
    }
}
