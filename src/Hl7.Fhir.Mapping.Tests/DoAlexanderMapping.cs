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
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace Hl7.Fhir.Tests.Mapping
{

    public delegate Expression ManualBuilder(MappingSymbols sym);

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
            
            private static readonly MethodInfo ELEMENTNODE_ADD_MI = typeof(ElementNode).GetMethod(nameof(ElementNode.Add), new[] {
                typeof(IStructureDefinitionSummaryProvider), typeof(string), typeof(object), typeof(string) });
            private static readonly PropertyInfo TYPEDELEMENT_VALUE_PI = typeof(ITypedElement).GetProperty(nameof(ITypedElement.Value));
            private static readonly MethodInfo ENUMEXT_APPLY_MI = typeof(EnumerableExtensions).GetMethod(nameof(EnumerableExtensions.Apply));


            // -> target.element = source
            public MethodCallExpression makePrimitiveAdder(MappingSymbols sym, string source, Expression target, string element)
            {
                var sourceExpr = sym[source] ?? throw Error.Argument($"Cannot resolve symbol '{source}'");

                return Expression.Call(target, ELEMENTNODE_ADD_MI,
                        Expression.Constant(TargetProvider),
                        Expression.Constant(element),
                        Expression.Property(sourceExpr, TYPEDELEMENT_VALUE_PI),
                        Expression.Constant(null, typeof(string)));
            }

            // source as label -> body
            public Expression buildSourceStatement(MappingSymbols sym, Expression source, string label, ManualBuilder body)
            {
                var valParam = Expression.Parameter(typeof(ITypedElement), label);
                var bodySym = sym.Nest();
                bodySym[label] = valParam;

                var lambda = Expression.Lambda<Action<ITypedElement>>(body(bodySym),valParam);
                return Expression.Call(null, ENUMEXT_APPLY_MI, source, lambda);
            }

            public void Patient(ITypedElement src, ElementNode tgt)
            {
                src.Children("patid").Apply(patid => tgt.Add(TargetProvider, "id", patid.Value));

                src.Children("operations").Apply(operations =>
                {
                    operations.Children("data").Apply(data =>
                    {
                        if (data.IsBoolean("blockindex = 1 and groupindex = 0 and itemid = 'id_1131'", true))
                        {
                            data.Children("values").Apply(values =>
                            {
                                var value_ = values.Children("value");
               
                                var valueE = buildSourceStatement(new MappingSymbols(), Expression.Constant(value_), "value", 
                                    (sym) =>
                                        makePrimitiveAdder(sym,"value", Expression.Constant(tgt), "gender"));

                                var valuesMaker = Expression.Lambda<Action>(valueE).Compile();
                                valuesMaker();
                            });
                        }

                        if (data.IsBoolean("blockindex = 1 and groupindex = 0 and itemid = 'id_1128'", true))
                        {
                            data.Children("values").Apply(values =>
                            {
                                values.Children("value").Apply(value =>
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