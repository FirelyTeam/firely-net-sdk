/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Mapping;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Source;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace Hl7.Fhir.Tests.Mapping
{

    public delegate Expression ManualBuilder();

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
            var json = target.ToJson();
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
            
            private static readonly MethodInfo ELEMENTNODE_ADD_MI = typeof(ElementNode).GetMethod("Add", new[] {
                typeof(IStructureDefinitionSummaryProvider), typeof(string), typeof(object), typeof(string) });

            private static readonly PropertyInfo TYPEDELEMENT_VALUE_PI = typeof(ITypedElement).GetProperty("Value");

            public MethodCallExpression makePrimitiveAdder(Expression source, Expression target, string element)
            {
                return Expression.Call(target, ELEMENTNODE_ADD_MI,
                        Expression.Constant(TargetProvider),
                        Expression.Constant(element),
                        Expression.Property(source, TYPEDELEMENT_VALUE_PI),
                        Expression.Constant(null, typeof(string)));
            }

            public Expression buildSourceStatement(Expression source, ParameterExpression focus, ManualBuilder body)
            {
                var lambda = Expression.Lambda<Action<ITypedElement>>(body(), focus);
                var method = typeof(EnumerableExtensions).GetMethod("ForEach");
                return Expression.Call(null, method, source, lambda);
            }

            public void Patient(ITypedElement src, ElementNode tgt)
            {
                src.Children("patid").ForEach(patid => tgt.Add(TargetProvider, "id", patid.Value));

                src.Children("operations").ForEach(operations =>
                {
                    operations.Children("data").ForEach(data =>
                    {
                        if (data.IsBoolean("blockindex = 1 and groupindex = 0 and itemid = 'id_1131'", true))
                        {
                            data.Children("values").ForEach(values =>
                            {
                                var value_ = values.Children("value");

                                var valParam = Expression.Parameter(typeof(ITypedElement), "value");
                                var valueE = buildSourceStatement(Expression.Constant(value_), valParam, () =>
                                        makePrimitiveAdder(valParam, Expression.Constant(tgt), "gender"));
                                var valuesMaker = Expression.Lambda<Action>(valueE).Compile();
                                valuesMaker();
                            });
                        }

                        if (data.IsBoolean("blockindex = 1 and groupindex = 0 and itemid = 'id_1128'", true))
                        {
                            data.Children("values").ForEach(values =>
                            {
                                values.Children("value").ForEach(value =>
                                {
                                    var address = tgt.Add(TargetProvider, "address");
                                    address.Add(TargetProvider, "postalCode", value.Value);
                                });
                            });
                        }
                    });
                });
            }
        }
    }
}