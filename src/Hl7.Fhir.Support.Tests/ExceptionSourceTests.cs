using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Support.Tests
{
    [TestClass]
    public class ExceptionSourceTests
    {
        class TestSource : IExceptionSource
        {
            public ExceptionNotificationHandler ExceptionHandler { get; set; }


            public void Test(string message)
            {
                ExceptionHandler.NotifyOrThrow(this, ExceptionNotification.Error(new FormatException(message)));
            }
        }


        [TestMethod]
        public void TestRaiseOrThrow()
        {
            var src = new TestSource
            {
                ExceptionHandler = (o, a) => { }
            };

            src.Test("Bla");
            // should continue;

            src.ExceptionHandler = null;

            try
            {
                src.Test("Fail!!!!");
                Assert.Fail("Should have thrown");
            }
            catch(FormatException)
            {
                // ok!
            }
        }

        [TestMethod]
        public void TestInterception()
        {
            List<ExceptionNotification> Received = new List<ExceptionNotification>();
            var src = new TestSource
            {
                ExceptionHandler = (o, a) => Received.Add(a)
            };

            src.Test("Unintercepted");
            Assert.AreEqual(1, Received.Count());
            Assert.IsTrue(Received.All(r => r.Message == "Unintercepted"));

            string intercepted = null;

            using (src.Catch((_,args) => intercepted = args.Message))
            {
                src.Test("Intercepted-true");
            }

            Assert.AreEqual("Intercepted-true", intercepted);
            Assert.AreEqual(1, Received.Count());   // since we've intercepted
            Assert.IsFalse(Received.Any(r => r.Message == "Intercepted-true"));

            src.Test("Unintercepted2");

            Assert.AreEqual("Intercepted-true", intercepted);  // should not have intercepted anymore
            Assert.AreEqual(2, Received.Count());   // original sink remains active
            Assert.AreEqual("Unintercepted2", Received.Last().Message);
        }

        [TestMethod]
        public void TestInterceptionWhenNoSink()
        {
            var src = new TestSource
            {
                ExceptionHandler = null   // that's the point
            };

            string intercepted = null;

            using (src.Catch((_,args) => intercepted = args.Message))
            {
                src.Test("Intercepted-true");
            }

            Assert.AreEqual("Intercepted-true", intercepted);
        }
    }
}
