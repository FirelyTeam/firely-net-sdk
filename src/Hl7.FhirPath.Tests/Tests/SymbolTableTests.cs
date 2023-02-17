using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace HL7.FhirPath.Tests.Tests
{
    [TestClass]
    public class SymbolTableTests
    {
        [TestMethod]
        public void SymbolTableUsedByMultipleThreads()
        {
            // setup multiple tasks
            Task[] taskArray = new Task[10];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = new Task(compile);
            }

            // execute the tasks in parallel
            try
            {
                Parallel.ForEach<Task>(taskArray, (t) => { t.Start(); });
                Task.WaitAll(taskArray);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + " " + ex.StackTrace);
            }

            void compile()
            {
                var compiler = new FhirPathCompiler();
                compiler.Symbols.Add<ITypedElement, bool, string, EvaluationContext, ITypedElement>("noOp", NoOp);
                compiler.Compile("(CapabilityStatement.useContext.value as Quantity) | (CapabilityStatement.useContext.value as Range)");

                static ITypedElement NoOp(ITypedElement elem, bool condition, string message, EvaluationContext ctx) => elem;
            }
        }
    }
}
