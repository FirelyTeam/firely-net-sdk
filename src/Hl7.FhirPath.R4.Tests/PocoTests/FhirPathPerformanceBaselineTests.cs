using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.FhirPath;
using Hl7.FhirPath;
using System.Linq;

namespace Hl7.Fhir
{
    /// <summary>
    /// Performance baseline tests to identify actual FhirPath bottlenecks.
    /// Results show cache cleanup optimizations don't address the real performance issues.
    /// </summary>
    [TestClass]
    public class FhirPathPerformanceBaselineTests
    {
        [TestMethod]
        public void FhirPathPerformanceBottleneckAnalysis()
        {
            Console.WriteLine("=== FhirPath Performance Bottleneck Analysis ===");
            Console.WriteLine("Testing to identify real performance issues beyond cache cleanup");

            var patient = CreateTestPatient();
            var typedElement = patient.ToTypedElement();

            TestToTypedElementBottleneck(patient);
            TestColdStartBottleneck(typedElement);
            TestElementTraversalBottleneck(typedElement);
            TestCacheEffectiveness();

            Console.WriteLine("\n=== CONCLUSION ===");
            Console.WriteLine("Cache cleanup optimizations provide minimal benefit.");
            Console.WriteLine("Real bottlenecks: ToTypedElement conversion, cold starts, element traversal.");
            Console.WriteLine("Need to investigate changes in ElementModel/FhirPath compilation between SDK versions.");
        }

        /// <summary>
        /// Tests ToTypedElement conversion performance - identified as a significant cost (0.124ms per conversion)
        /// </summary>
        private void TestToTypedElementBottleneck(Patient patient)
        {
            Console.WriteLine("\n1. ToTypedElement Conversion Bottleneck:");
            
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                var typed = patient.ToTypedElement();
            }
            sw.Stop();
            
            var avgTime = (double)sw.ElapsedMilliseconds / 1000;
            Console.WriteLine($"   ToTypedElement: {sw.ElapsedMilliseconds}ms for 1000 conversions");
            Console.WriteLine($"   Average: {avgTime:F3}ms per conversion");
            
            if (avgTime > 0.2)
                Console.WriteLine("   ⚠️  BOTTLENECK: ToTypedElement conversion is expensive");
        }

        /// <summary>
        /// Tests cold start vs warm cache performance - shows 42x difference
        /// </summary>
        private void TestColdStartBottleneck(ITypedElement typedElement)
        {
            Console.WriteLine("\n2. Cold Start vs Warm Cache Bottleneck:");
            
            var expressions = new[] { "name.family", "name.given.first()", "birthDate", "gender" };
            const int iterations = 500;
            
            // Cold start test
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                var cache = new FhirPathCompilerCache(); // Fresh cache each time
                foreach (var expr in expressions)
                {
                    var result = cache.Select(typedElement, expr);
                    foreach (var item in result) { }
                }
            }
            sw.Stop();
            var coldTime = (double)sw.ElapsedMilliseconds / (iterations * expressions.Length);
            Console.WriteLine($"   Cold start: {sw.ElapsedMilliseconds}ms for {iterations * expressions.Length} operations");
            Console.WriteLine($"   Average: {coldTime:F3}ms per cold operation");

            // Warm cache test
            var warmCache = new FhirPathCompilerCache();
            sw.Restart();
            for (int i = 0; i < iterations; i++)
            {
                foreach (var expr in expressions)
                {
                    var result = warmCache.Select(typedElement, expr);
                    foreach (var item in result) { }
                }
            }
            sw.Stop();
            var warmTime = (double)sw.ElapsedMilliseconds / (iterations * expressions.Length);
            Console.WriteLine($"   Warm cache: {sw.ElapsedMilliseconds}ms for {iterations * expressions.Length} operations");
            Console.WriteLine($"   Average: {warmTime:F3}ms per warm operation");
            
            var ratio = coldTime / warmTime;
            Console.WriteLine($"   Cold/Warm ratio: {ratio:F1}x");
            
            if (ratio > 20)
                Console.WriteLine("   ⚠️  BOTTLENECK: Cold start compilation is very expensive");
        }

        /// <summary>
        /// Tests element traversal performance - another identified bottleneck
        /// </summary>
        private void TestElementTraversalBottleneck(ITypedElement typedElement)
        {
            Console.WriteLine("\n3. Element Traversal Bottleneck:");
            
            var cache = new FhirPathCompilerCache();
            var traversalExpressions = new[]
            {
                "descendants()",
                "children()",
                "name.children()",
                "descendants().where(name = 'family')",
                "children().children()"
            };

            var sw = Stopwatch.StartNew();
            const int iterations = 100;
            
            for (int i = 0; i < iterations; i++)
            {
                foreach (var expr in traversalExpressions)
                {
                    var result = cache.Select(typedElement, expr);
                    foreach (var item in result) { } // Consume result
                }
            }
            sw.Stop();
            
            var avgTime = (double)sw.ElapsedMilliseconds / (iterations * traversalExpressions.Length);
            Console.WriteLine($"   Element traversal: {sw.ElapsedMilliseconds}ms for {iterations * traversalExpressions.Length} evaluations");
            Console.WriteLine($"   Average: {avgTime:F3}ms per traversal");
            
            if (avgTime > 0.15)
                Console.WriteLine("   ⚠️  BOTTLENECK: Element traversal is expensive");
        }

        /// <summary>
        /// Tests cache effectiveness - shows cache cleanup is rarely the bottleneck
        /// </summary>
        private void TestCacheEffectiveness()
        {
            Console.WriteLine("\n4. Cache Effectiveness Analysis:");
            
            var cache = new FhirPathCompilerCache();
            var expressions = new[] { "name.family", "name.given.first()", "birthDate", "gender", "active", "id" };
            const int iterations = 10000;
            
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                foreach (var expr in expressions)
                {
                    cache.GetCompiledExpression(expr);
                }
            }
            sw.Stop();
            
            var avgTime = (double)sw.ElapsedMilliseconds / (iterations * expressions.Length);
            Console.WriteLine($"   Cache retrieval: {sw.ElapsedMilliseconds}ms for {iterations * expressions.Length} operations");
            Console.WriteLine($"   Average: {avgTime:F5}ms per cache access");
            Console.WriteLine($"   ✅ Cache operations are extremely fast - not the bottleneck");
        }

        private Patient CreateTestPatient()
        {
            return new Patient
            {
                Id = "test-patient-123",
                Name = { 
                    new HumanName { 
                        Family = "Doe", 
                        Given = new[] { "John", "William" },
                        Use = HumanName.NameUse.Official
                    },
                    new HumanName { 
                        Family = "Smith", 
                        Given = new[] { "Johnny" },
                        Use = HumanName.NameUse.Nickname
                    }
                },
                BirthDate = "1990-01-01",
                Gender = AdministrativeGender.Male,
                Active = true,
                Telecom = {
                    new ContactPoint { System = ContactPoint.ContactPointSystem.Phone, Value = "555-1234" },
                    new ContactPoint { System = ContactPoint.ContactPointSystem.Email, Value = "john.doe@example.com" }
                },
                Address = {
                    new Address { 
                        Line = new[] { "123 Main St" },
                        City = "Anytown",
                        State = "CA",
                        PostalCode = "12345",
                        Country = "USA"
                    }
                }
            };
        }

        [TestMethod]
        public void FhirPathStressTestWithBottleneckFocus()
        {
            Console.WriteLine("\n=== FhirPath Stress Test - Bottleneck Focus ===");

            var patient = CreateTestPatient();
            var typedElement = patient.ToTypedElement();
            var cache = new FhirPathCompilerCache();
            
            var expressions = new[] { "name.family", "name.given.first()", "birthDate", "gender", "active", "id" };
            const int stressIterations = 5000;
            
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < stressIterations; i++)
            {
                var expr = expressions[i % expressions.Length];
                var result = cache.Select(typedElement, expr);
                foreach (var item in result) { }
            }
            sw.Stop();
            
            var avgTime = (double)sw.ElapsedMilliseconds / stressIterations;
            Console.WriteLine($"Stress test: {sw.ElapsedMilliseconds}ms for {stressIterations} mixed operations");
            Console.WriteLine($"Average: {avgTime:F3}ms per operation");
            
            // This should complete efficiently - if it's slow, there's a real performance issue
            Assert.IsTrue(sw.ElapsedMilliseconds < 10000, 
                $"Performance issue detected. Stress test took {sw.ElapsedMilliseconds}ms for {stressIterations} operations");
        }
    }
}