using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.FhirPath;
using Hl7.FhirPath;

namespace Hl7.Fhir
{
    [TestClass]
    public class FhirPathCachePerformanceTests
    {
        [TestMethod]
        public void FhirPathCachePerformanceRegression()
        {
            // Create a test patient for FhirPath evaluation
            var patient = new Patient
            {
                Id = "test-patient",
                Name = { new HumanName { Family = "Doe", Given = new[] { "John" } } },
                BirthDate = "1990-01-01",
                Gender = AdministrativeGender.Male
            };

            // Test expressions that would be commonly cached
            var expressions = new[]
            {
                "name.family",
                "name.given.first()",
                "birthDate",
                "gender",
                "name.where(family = 'Doe').given",
                "extension.where(url = 'test').value",
                "id",
                "name.given.last()"
            };

            var cache = new FhirPathCompilerCache();
            var typedElement = patient.ToTypedElement();

            // Warmup - this will populate the cache with the expressions
            foreach (var expr in expressions)
            {
                cache.Select(typedElement, expr);
            }

            // Now measure performance of cached evaluations
            var sw = Stopwatch.StartNew();
            int iterations = 10000;
            
            for (int i = 0; i < iterations; i++)
            {
                foreach (var expr in expressions)
                {
                    var result = cache.Select(typedElement, expr);
                    // Consume the result to ensure evaluation happens
                    foreach (var item in result) { }
                }
            }
            
            sw.Stop();

            Console.WriteLine($"FhirPath cache performance: {sw.ElapsedMilliseconds}ms for {iterations * expressions.Length} cached evaluations");
            Console.WriteLine($"Average time per evaluation: {(double)sw.ElapsedMilliseconds / (iterations * expressions.Length):F3}ms");

            // The test should complete in reasonable time even with many iterations
            // This is not an exact performance test but ensures we don't have major regressions
            Assert.IsTrue(sw.ElapsedMilliseconds < 5000, 
                $"FhirPath cache performance regression detected. Took {sw.ElapsedMilliseconds}ms for {iterations * expressions.Length} evaluations");
        }

        [TestMethod]
        public void FhirPathCacheEvictionPerformance()
        {
            var cache = new FhirPathCompilerCache(cacheSize: 100); // Small cache to trigger eviction
            var patient = new Patient { Id = "test" };
            var typedElement = patient.ToTypedElement();

            var sw = Stopwatch.StartNew();
            
            // Create many unique expressions to trigger cache eviction
            for (int i = 0; i < 500; i++) // More than cache size to trigger cleanup
            {
                var expr = $"id.where(value = 'test{i}')";
                cache.Select(typedElement, expr);
            }
            
            sw.Stop();

            Console.WriteLine($"Cache eviction test: {sw.ElapsedMilliseconds}ms for 500 unique expressions");

            // Should handle cache eviction efficiently
            Assert.IsTrue(sw.ElapsedMilliseconds < 2000, 
                $"Cache eviction performance issue detected. Took {sw.ElapsedMilliseconds}ms");
        }
    }
}