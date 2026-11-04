/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
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

        private static object annotationList(Base b) =>
            typeof(Base).GetField("_annotations", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(b);

        [TestMethod]
        public void ReadingAnnotationsDoesNotCreateAnAnnotationList()
        {
            var patient = new Patient { Id = "x" };
            Assert.IsNull(annotationList(patient));

            Assert.IsFalse(patient.HasAnnotation<AnnotationData>());
            Assert.IsFalse(patient.TryGetAnnotation<AnnotationData>(out _));
            Assert.IsNull(patient.Annotation<AnnotationData>());
            Assert.IsFalse(patient.Annotations(typeof(AnnotationData)).Any());
            Assert.IsFalse(patient.Annotations<AnnotationData>().Any());
            patient.RemoveAnnotations(typeof(AnnotationData));
            patient.RemoveAnnotations<AnnotationData>();

            Assert.IsNull(annotationList(patient), "Only reading or removing annotations should not allocate a list.");

            // The "self" annotation still works without a list.
            Assert.AreSame(patient, patient.Annotations(typeof(IFhirValueProvider)).Single());
            Assert.IsNull(annotationList(patient));

            // Writing creates it.
            patient.AddAnnotation(new AnnotationData { Data = "hi" });
            Assert.IsNotNull(annotationList(patient));
            Assert.AreEqual("hi", patient.Annotation<AnnotationData>().Data);
        }

        [TestMethod]
        public void DeepCopyOfPatientWithoutAnnotationsHasNoAnnotationList()
        {
            var patient = new Patient { Id = "x", Name = { new HumanName { Family = "Doe" } } };

            // Reading used to create an empty list on the source, which was then copied to the clone.
            _ = patient.HasAnnotation<AnnotationData>();
            _ = patient.Name[0].HasAnnotation<AnnotationData>();

            var copy = (Patient)patient.DeepCopy();

            Assert.IsNull(annotationList(patient));
            Assert.IsNull(annotationList(copy));
            Assert.IsNull(annotationList(copy.Name[0]));
        }

        [TestMethod]
        public void DeepCopyOfEmptiedAnnotationListHasNoAnnotationList()
        {
            var patient = new Patient();
            patient.AddAnnotation(new AnnotationData());
            patient.RemoveAnnotations<AnnotationData>();

            var copy = (Patient)patient.DeepCopy();

            Assert.IsNull(annotationList(copy));
        }

        [TestMethod]
        public void DeepCopyCopiesAnnotationsIndependently()
        {
            var patient = new Patient { Name = { new HumanName { Family = "Doe" } } };
            patient.AddAnnotation(new AnnotationData { Data = "patient" });
            patient.Name[0].AddAnnotation(new AnnotationData { Data = "name" });

            var copy = (Patient)patient.DeepCopy();

            Assert.AreEqual("patient", copy.Annotation<AnnotationData>().Data);
            Assert.AreEqual("name", copy.Name[0].Annotation<AnnotationData>().Data);

            // Changes to the copy do not affect the original, and vice versa.
            copy.AddAnnotation(new AnnotationData { Data = "copy only" });
            patient.AddAnnotation(new AnnotationData { Data = "original only" });
            copy.Name[0].RemoveAnnotations<AnnotationData>();

            Assert.AreEqual(2, copy.Annotations<AnnotationData>().Count());
            Assert.AreEqual("copy only", copy.Annotations<AnnotationData>().Last().Data);
            Assert.AreEqual(2, patient.Annotations<AnnotationData>().Count());
            Assert.AreEqual("original only", patient.Annotations<AnnotationData>().Last().Data);
            Assert.AreEqual("name", patient.Name[0].Annotation<AnnotationData>().Data);
            Assert.IsNull(copy.Name[0].Annotation<AnnotationData>());
        }

        [TestMethod]
        public void ConcurrentFirstAnnotationWritesOnAPocoAreAllKept()
        {
            const int threads = 16;

            for (var round = 0; round < 50; round++)
            {
                var patient = new Patient();
                var all = Enumerable.Range(0, threads).Select(i => new AnnotationData { Data = i.ToString() }).ToArray();

                Parallel.For(0, threads, new ParallelOptions { MaxDegreeOfParallelism = threads }, i => patient.AddAnnotation(all[i]));

                Assert.AreEqual(threads, patient.Annotations<AnnotationData>().Count());
            }
        }
    }
}