/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Serialization
{
    public interface IFhirWriter : IDisposable
    {
        void WriteStartRootObject(string name, string id, bool contained);
        void WriteEndRootObject(bool contained);

        //void WriteStartProperty(string name);
        void WriteStartProperty(PropertyMapping propMap, string name);
        void WriteEndProperty();

        void WriteStartComplexContent();
        void WriteEndComplexContent();

        void WritePrimitiveContents(object value, XmlSerializationHint xmlFormatHint);

        void WriteStartArray();
        //void WriteStartArrayElement(string name);
        //void WriteEndArrayElement();
        void WriteEndArray();

        bool HasValueElementSupport { get; }
    }
}
