/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class AnnotationTests
    {
        internal class AnnotationData
        {
            public string Data;
        }


        [TestMethod]
        public void TestAddAnnotation()
        {
            FhirBoolean data = new FhirBoolean();

            Assert.IsNull(data.Annotation(typeof(AnnotationData)));
            data.AddAnnotation(new AnnotationData { Data = "hi!" });
            Assert.IsNotNull(data.Annotation(typeof(AnnotationData)));
            Assert.AreEqual("hi!", data.Annotation<AnnotationData>().Data);
            Assert.AreEqual(1, data.Annotations(typeof(AnnotationData)).Count());

            data.AddAnnotation(new AnnotationData { Data = "hi2!" });

            // Does not change original outcome (still the first)
            Assert.IsNotNull(data.Annotation(typeof(AnnotationData)));
            Assert.AreEqual("hi!", data.Annotation<AnnotationData>().Data);

            Assert.AreEqual(2, data.Annotations(typeof(AnnotationData)).Count());
            Assert.AreEqual("hi2!", data.Annotations<AnnotationData>().Skip(1).First().Data);

            data.AddAnnotation("Bare string");

            Assert.AreEqual(2, data.Annotations(typeof(AnnotationData)).Count());
            Assert.AreEqual("hi!", data.Annotation<AnnotationData>().Data);

            data.RemoveAnnotations<AnnotationData>();
            Assert.AreEqual(0, data.Annotations(typeof(AnnotationData)).Count());

            Assert.AreEqual("Bare string", data.Annotation<string>());
        }


        [TestMethod]
        public void TestAnnotationsAreCloneable()
        {
            FhirBoolean data = new FhirBoolean(true);

            data.AddAnnotation(new AnnotationData { Data = "Hi!" });

            var copied = (FhirBoolean)data.DeepCopy();

            Assert.AreEqual("Hi!", copied.Annotation<AnnotationData>().Data);
        }

        [TestMethod]
        public void TestAnnotationsEnumType()
        {
            FhirBoolean data = new FhirBoolean(true);

            data.SetAnnotation(SummaryType.True);

            var copied = (FhirBoolean)data.DeepCopy();

            Assert.AreEqual(SummaryType.True, copied.Annotation<SummaryType>());

            copied.SetAnnotation(SummaryType.Text);
            Assert.AreEqual(SummaryType.Text, copied.Annotation<SummaryType>());

            Assert.IsTrue(copied.HasAnnotation<SummaryType>());

            copied.RemoveAnnotations<SummaryType>();

            Assert.IsFalse(copied.HasAnnotation<SummaryType>());
        }


        [TestMethod]
        public void SetBaseUri()
        {
            // Indirect test of annotations
            Patient p = new Patient();

            p.ResourceBase = new Uri("http://nu.nl/");
            Assert.AreEqual("http://nu.nl/", p.ResourceBase.ToString());
        }

        /// <summary>
        /// Pre-generate testNumber of annotations.
        /// Set them on a single element in parallel.
        /// </summary>
        [TestMethod]
        public void SetAnnotationIsThreadSafe()
        {
            var testNumber = 10;
            var rnd = new Random();
            var annotations =
                    Enumerable.Range(0, testNumber)
                    .Select(i => new AnnotationData() { Data = rnd.Next(0, testNumber).ToString() })
                    .ToArray();
            var element = new FhirBoolean();
            try
            {
                Parallel.For(0, testNumber, new ParallelOptions() { MaxDegreeOfParallelism = testNumber }, (i) =>
                {
                    element.SetAnnotation(annotations[i]);
                }
                );

            }
            catch (Exception ex)
            {
                Assert.Fail($"AddAnnotation should not throw an exception, but it did: {ex.Message}");
            }

            Assert.AreEqual(1, element.Annotations(typeof(AnnotationData)).Count());

        }
    }
}