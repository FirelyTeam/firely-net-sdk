/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization
{
    internal class ResourceReader
    {
#pragma warning disable 612, 618
        private readonly ITypedElement _reader;
        private readonly ModelInspector _inspector;

        public ParserSettings Settings { get; private set; }

        internal ResourceReader(ITypedElement reader, ParserSettings settings)
        {
            _reader = reader;
            _inspector = BaseFhirParser.Inspector;
            Settings = settings;
        }
#pragma warning restore 612,618

        public Resource Deserialize(Resource existing=null)
        {
            if(_reader.InstanceType is null)
                ComplexTypeReader.RaiseFormatError(
                    "Underlying data source was not able to provide the actual instance type of the resource.", _reader.Location);

            var mapping = _inspector.FindClassMapping(_reader.InstanceType);

            if (mapping == null)
                ComplexTypeReader.RaiseFormatError($"Asked to deserialize unknown resource '{_reader.InstanceType}'", _reader.Location);
             
            // Delegate the actual work to the ComplexTypeReader, since
            // the serialization of Resources and ComplexTypes are virtually the same
            var cplxReader = new ComplexTypeReader(_reader, Settings);
            return (Resource)cplxReader.Deserialize(mapping, existing);
        }
    }
}
