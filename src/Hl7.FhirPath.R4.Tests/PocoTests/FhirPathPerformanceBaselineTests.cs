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
    [TestClass]
    public class FhirPathPerformanceBaselineTests
    {
        [TestMethod]
        public void FhirPathBaselinePerformanceTest()
        {
            Console.WriteLine("FhirPath Performance Baseline Analysis");
            Console.WriteLine("=====================================");

            // Create test data
            var patient = CreateTestPatient();
            var typedElement = patient.ToTypedElement();

            // Test different aspects of FhirPath performance
            TestCachePerformance();
            TestCompilationPerformance();
            TestEvaluationPerformance(typedElement);
            TestOverallPerformance(typedElement);
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

        private void TestCachePerformance()
        {
            Console.WriteLine("\n1. Cache Performance Test:");
            
            var cache = new FhirPathCompilerCache();
            var expressions = GenerateTestExpressions();
            
            // Test cache population (first-time compilation)
            var sw = Stopwatch.StartNew();
            foreach (var expr in expressions)
            {
                cache.GetCompiledExpression(expr);
            }
            sw.Stop();
            Console.WriteLine($"Cache population: {sw.ElapsedMilliseconds}ms for {expressions.Length} expressions");

            // Test cache retrieval (should be fast)
            sw.Restart();
            for (int i = 0; i < 1000; i++)
            {
                foreach (var expr in expressions)
                {
                    cache.GetCompiledExpression(expr);
                }
            }
            sw.Stop();
            Console.WriteLine($"Cache retrieval: {sw.ElapsedMilliseconds}ms for {1000 * expressions.Length} cached expressions");
            Console.WriteLine($"Average cache retrieval time: {(double)sw.ElapsedMilliseconds / (1000 * expressions.Length):F4}ms per expression");
        }

        private void TestCompilationPerformance()
        {
            Console.WriteLine("\n2. Compilation Performance Test:");
            
            var compiler = new FhirPathCompiler();
            var expressions = GenerateTestExpressions();
            
            var sw = Stopwatch.StartNew();
            foreach (var expr in expressions)
            {
                compiler.Compile(expr);
            }
            sw.Stop();
            Console.WriteLine($"Fresh compilation: {sw.ElapsedMilliseconds}ms for {expressions.Length} expressions");
            Console.WriteLine($"Average compilation time: {(double)sw.ElapsedMilliseconds / expressions.Length:F2}ms per expression");
        }

        private void TestEvaluationPerformance(ITypedElement typedElement)
        {
            Console.WriteLine("\n3. Evaluation Performance Test:");
            
            var cache = new FhirPathCompilerCache();
            var expressions = GenerateTestExpressions();
            
            // Pre-compile all expressions
            foreach (var expr in expressions)
            {
                cache.GetCompiledExpression(expr);
            }

            // Test evaluation performance
            var sw = Stopwatch.StartNew();
            const int evaluationIterations = 1000;
            
            for (int i = 0; i < evaluationIterations; i++)
            {
                foreach (var expr in expressions)
                {
                    var result = cache.Select(typedElement, expr);
                    // Consume result to ensure evaluation happens
                    foreach (var item in result) { }
                }
            }
            sw.Stop();
            Console.WriteLine($"Evaluation: {sw.ElapsedMilliseconds}ms for {evaluationIterations * expressions.Length} evaluations");
            Console.WriteLine($"Average evaluation time: {(double)sw.ElapsedMilliseconds / (evaluationIterations * expressions.Length):F4}ms per evaluation");
        }

        private void TestOverallPerformance(ITypedElement typedElement)
        {
            Console.WriteLine("\n4. Overall Performance Test (Cold Start):");
            
            var expressions = GenerateTestExpressions();
            const int iterations = 100;
            
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
            Console.WriteLine($"Cold start performance: {sw.ElapsedMilliseconds}ms for {iterations * expressions.Length} total operations");
            Console.WriteLine($"Average cold operation time: {(double)sw.ElapsedMilliseconds / (iterations * expressions.Length):F2}ms per operation");
        }

        private string[] GenerateTestExpressions()
        {
            return new[]
            {
                "name.family",
                "name.given.first()",
                "name.given.last()",
                "birthDate",
                "gender",
                "active",
                "id",
                "name.where(use = 'official').family",
                "name.where(use = 'official').given",
                "telecom.where(system = 'phone').value",
                "telecom.where(system = 'email').value",
                "address.line.first()",
                "address.city",
                "address.state",
                "address.postalCode",
                "name.count()",
                "telecom.count()",
                "name.where(family = 'Doe').given.first()",
                "address.where(country = 'USA').city",
                "name.given.where($this.startsWith('J'))"
            };
        }

        [TestMethod]
        public void FhirPathStressTest()
        {
            Console.WriteLine("\nFhirPath Stress Test - Simulating Heavy Usage");
            Console.WriteLine("=============================================");

            var patient = CreateTestPatient();
            var typedElement = patient.ToTypedElement();
            var cache = new FhirPathCompilerCache();
            
            // Simulate a heavy load scenario
            var expressions = GenerateTestExpressions();
            const int stressIterations = 5000;
            
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < stressIterations; i++)
            {
                var expr = expressions[i % expressions.Length];
                var result = cache.Select(typedElement, expr);
                foreach (var item in result) { }
            }
            sw.Stop();
            
            Console.WriteLine($"Stress test: {sw.ElapsedMilliseconds}ms for {stressIterations} mixed operations");
            Console.WriteLine($"Average time per operation: {(double)sw.ElapsedMilliseconds / stressIterations:F3}ms");
            
            // This should complete in reasonable time - if it takes too long, there's a performance issue
            Assert.IsTrue(sw.ElapsedMilliseconds < 10000, 
                $"Performance issue detected. Stress test took {sw.ElapsedMilliseconds}ms for {stressIterations} operations");
        }
    }
}