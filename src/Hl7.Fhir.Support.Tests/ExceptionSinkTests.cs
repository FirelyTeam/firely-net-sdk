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
    public class ExceptionSinkTests
    {
        class TestSink : IExceptionSink
        {
            public List<ExceptionRaisedEventArgs> Received = new List<ExceptionRaisedEventArgs>();

            public bool Raise(object sender, ExceptionRaisedEventArgs args)
            {
                Received.Add(args);
                return true;
            }
        }

        class TestSource : IExceptionSource
        {
            public IExceptionSink Sink { get; set; }

            public void Test(string message)
            {
                Sink.Raise(this, ExceptionRaisedEventArgs.Error(new FormatException(message)));
            }
        }

        [TestMethod]
        public void TestInterception()
        {
            var ts = new TestSink();
            var src = new TestSource
            {
                Sink = ts
            };

            src.Test("Unintercepted");
            Assert.AreEqual(1, ts.Received.Count());
            Assert.IsTrue(ts.Received.All(r => r.Message == "Unintercepted"));

            string intercepted = null;

            using (src.Intercept((_,args) => { intercepted = args.Message; return true; }))
            {
                src.Test("Intercepted-true");
            }

            Assert.AreEqual("Intercepted-true", intercepted);
            Assert.AreEqual(1, ts.Received.Count());   // since interceptor returned 'true'

            using (src.Intercept((_,args) => { intercepted = args.Message; return false; }))
            {
                src.Test("Intercepted-false");
            }

            Assert.AreEqual("Intercepted-false", intercepted);
            Assert.AreEqual(2, ts.Received.Count());   // since interceptor returned 'false'
            Assert.AreEqual("Intercepted-false", ts.Received.Last().Message);

            src.Test("Unintercepted2");

            Assert.AreEqual("Intercepted-false", intercepted);  // should not have intercepted anymore
            Assert.AreEqual(3, ts.Received.Count());   // original sink remains active
            Assert.AreEqual("Unintercepted2", ts.Received.Last().Message);
        }

        [TestMethod]
        public void TestInterceptionWhenNoSink()
        {
            var ts = new TestSink();
            var src = new TestSource
            {
                Sink = null   // that's the point
            };

            string intercepted = null;

            using (src.Intercept((_,args) => { intercepted = args.Message; return true; }))
            {
                src.Test("Intercepted-true");
            }

            Assert.AreEqual("Intercepted-true", intercepted);
        }
    }
}
