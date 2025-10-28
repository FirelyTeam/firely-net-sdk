using BenchmarkDotNet.Attributes;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using System;

namespace Firely.Sdk.Benchmarks
{
    [MemoryDiagnoser]
    public class ModelInspectorBenchmarks
    {
        [GlobalSetup]
        public void BenchmarkSetup()
        {
            //  PropertyInfoExtensions.NoCodeGenSupport = true;
        }


        internal Type[] PopularResources = new Type[]
        {
                typeof(Observation), typeof(Patient), typeof(Organization),
                typeof(Procedure), typeof(StructureDefinition), typeof(MedicationRequest),
                typeof(ValueSet), typeof(Questionnaire), typeof(Appointment),
                typeof(OperationOutcome)
        };

        [Benchmark]
        public void ScanAssemblies()
        {
            // Make sure we work uncached initially on each run
            //ModelInspector.Clear();
            //ClassMapping.Clear();

#pragma warning disable CS0618 // Type or member is obsolete
            _ = ModelInspector.ForAssembly(typeof(ModelInfo).Assembly);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        [Benchmark]
        public void GetPropertiesPopular()
        {
            // Make sure we work uncached initially on each run
            //ModelInspector.Clear();
            //ClassMapping.Clear();

#pragma warning disable CS0618 // Type or member is obsolete
            var inspector = ModelInspector.ForAssembly(typeof(ModelInfo).Assembly);
#pragma warning restore CS0618 // Type or member is obsolete
            foreach (var t in PopularResources)
            {
                var mapping = inspector.FindClassMapping(t);
                _ = mapping.PropertyMappings;
            }
        }

        //[Benchmark]
        //public void GetPropertiesAll()
        //{
        //    // Make sure we work uncached initially on each run
        //    ModelInspector.Clear();
        //    ClassMapping.Clear();

        //    var inspector = ModelInspector.ForAssembly(typeof(ModelInfo).Assembly);
        //    foreach (var m in inspector.ClassMappings)
        //    {
        //        _ = m.PropertyMappings;
        //    }
        //}

    }
}