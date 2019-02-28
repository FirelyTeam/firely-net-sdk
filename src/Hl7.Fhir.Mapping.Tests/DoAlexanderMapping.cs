/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Source;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Tests.Mapping
{
    [TestClass]
    public class MappingTest
    {
        private static readonly string DATA_DIRECTORY = Path.Combine("TestData", "data");
        private static readonly string DEFINITIONS_DIRECTORY = Path.Combine("TestData", "definitions");

        // TODO: We can feed a map from the "uses" statement, where SD.name from the referenced SD is used as the resource name.
        private static bool UKKoelnDeTypeMapper(string resourceName, out string canonical)
        {
            canonical = "http://uk-koeln.de/fhir/StructureDefinition/" + resourceName;
            return true;
        }

        [TestMethod]
        public void DoAlexanderMapping()
        {
            var resolver = new CachedResolver(
                                new MultiResolver(
                                    new DirectorySource(DEFINITIONS_DIRECTORY),
                                    ZipSource.CreateValidationSource()));

            var tp = File.ReadAllText(Path.Combine(DATA_DIRECTORY, "test1-source.json"));

            var sourceDefinitionProvider = new StructureDefinitionSummaryProvider(resolver, UKKoelnDeTypeMapper);
            var targetDefinitionProvider = new StructureDefinitionSummaryProvider(resolver);

            var sourceJson = FhirJsonNode.Parse(tp);

            // Set up the source and target - normally handled as a result of the "uses" statements
            var source = sourceJson.ToTypedElement(sourceDefinitionProvider);
            var target = ElementNode.Root(targetDefinitionProvider, "Patient");

            var mapper = new CtsTransportMapper(sourceDefinitionProvider, targetDefinitionProvider);
            mapper.Main(source, target);

        }


        public class CtsTransportMapper
        {
            public CtsTransportMapper(IStructureDefinitionSummaryProvider sourceProvider,
                        IStructureDefinitionSummaryProvider targetProvider)
            {
                SourceProvider = sourceProvider ?? throw new ArgumentNullException(nameof(sourceProvider));
                TargetProvider = targetProvider ?? throw new ArgumentNullException(nameof(targetProvider));
            }

            public IStructureDefinitionSummaryProvider SourceProvider { get; }
            public IStructureDefinitionSummaryProvider TargetProvider { get; }

            public void Main(ITypedElement src, ElementNode tgt)
            {
                Patient(src, tgt);
            }

            public void Patient(ITypedElement src, ElementNode tgt)
            {
                //src.patid as id -> tgt.id = id;  This is a "shorthand" copy, based on primitive values
                //TODO: we need a delayed & cachine "ToList()", that runs the enumerable the first time and then just returns
                //the cached copy
                var patid_ = src.Children("patid").ToList();
                foreach (var patid in patid_)
                    tgt.Add(TargetProvider, "id", patid.Value);

                var operations_ = src.Children("operations").ToList();
                foreach (var operations in operations_)
                {
                    var data_ = operations.Children("data").ToList();
                    foreach (var data in data_)
                    {
                        //NB: Notice how the FhirPath works on the "context" (=data), not data.values
                        if (data.IsBoolean("blockindex = 1 and groupindex = 0 and itemid = 'id_1131'", true))
                        {
                            var values_ = data.Children("values").ToList();
                            foreach(var values in values_)
                            {
                                var value_ = values.Children("value").ToList();
                                foreach(var value in value_)
                                    tgt.Add(TargetProvider, "gender", value.Value);
                            }
                        }

                        if (data.IsBoolean("blockindex = 1 and groupindex = 0 and itemid = 'id_1128'", true))
                        {
                            var values_ = data.Children("values").ToList();
                            foreach (var values in values_)
                            {
                                var value_ = values.Children("value").ToList();
                                foreach (var value in value_)
                                {
                                    var address = tgt.Add(TargetProvider, "address");
                                    address.Add(TargetProvider, "postalCode", value.Value);
                                }
                            }
                        }

                    }
                }
            }
        }
    }
}
