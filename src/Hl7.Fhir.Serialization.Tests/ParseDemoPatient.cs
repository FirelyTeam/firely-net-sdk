using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class ParseDemoPatient
    {
        public static void CloningWorks(IElementNavigator nav)
        {
            var copy = nav.Clone();
            Assert.IsTrue(nav.IsEqualTo(copy).Success);
        }

        public static void CloningWorks(ISourceNavigator nav)
        {
            var copy = nav.Clone();
            Assert.IsTrue(nav.IsEqualTo(copy).Success);
        }

        public static void ElementNavPerformance(IElementNavigator nav)
        {
            // run extraction once to allow for caching
            extract();

            //System.Threading.Thread.Sleep(20000);

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 5_000; i++)
            {
                extract();
            }
            sw.Stop();

            Debug.WriteLine($"Navigating took {sw.ElapsedMilliseconds / 5 } micros");

            void extract()
            {
                var usual = nav.Children("identifier").First().Children("use").First().Value;
                var phone = nav.Children("telecom").First().Children("system").First().Value;
                var prefs = nav.Children("communication").Where(c => c.Children("preferred").Any(pr => pr.Value is string s && s == "true")).Count();
                var link = nav.Children("link").Children("other").Children("reference");
            }
        }


        public static void ProducesCorrectUntypedLocations(ISourceNavigator patient)
        {
            Assert.AreEqual("Patient", patient.Location);

            patient.MoveToFirstChild();
            Assert.AreEqual("Patient.id[0]", patient.Location);

            patient.MoveToNext();   // text
            patient.MoveToNext("identifier");
            Assert.AreEqual("Patient.identifier[0]", patient.Location);
            var idNav = patient.Clone();

            Assert.IsTrue(patient.MoveToFirstChild());
            Assert.AreEqual("Patient.identifier[0].use[0]", patient.Location);

            idNav.MoveToNext(); // identifier
            Assert.AreEqual("Patient.identifier[1]", idNav.Location);

            Assert.IsTrue(idNav.MoveToFirstChild());
            Assert.AreEqual("Patient.identifier[1].use[0]", idNav.Location);
        }

        public static void ProducedCorrectTypedLocations(IElementNavigator patient)
        {
            var typedNav = (TypedNavigator)patient;

            Assert.AreEqual("Patient", patient.Location);

            patient.MoveToFirstChild();
            Assert.AreEqual("Patient.id", typedNav.PrettyPath);
            var patNav = patient.Clone();

            patient.MoveToNext();   // text
            patient.MoveToNext("identifier");
            Assert.AreEqual("Patient.identifier[0]", typedNav.PrettyPath);
            var idNav = patient.Clone();

            Assert.IsTrue(patient.MoveToFirstChild());
            Assert.AreEqual("Patient.identifier[0].use", typedNav.PrettyPath);

            Assert.IsTrue(patNav.MoveToNext("deceased"));
            Assert.AreEqual("Patient.deceased", patNav.Annotation<PrettyPath>().Path);

            idNav.MoveToNext(); // identifier
            Assert.AreEqual("Patient.identifier[1]", idNav.Annotation<PrettyPath>().Path);

            Assert.IsTrue(idNav.MoveToFirstChild());
            Assert.AreEqual("Patient.identifier[1].use", idNav.Annotation<PrettyPath>().Path);
        }


        public static void HasLineNumbers<T>(IElementNavigator nav) where T : class, IPositionInfo
        {
            Assert.IsTrue(nav.MoveToFirstChild());

            var posInfo = (nav as IAnnotated)?.Annotation<T>();
            Assert.IsNotNull(posInfo);
            Assert.AreNotEqual(-1, posInfo.LineNumber);
            Assert.AreNotEqual(-1, posInfo.LinePosition);
            Assert.AreNotEqual(0, posInfo.LineNumber);
            Assert.AreNotEqual(0, posInfo.LinePosition);
        }

        public static void RoundtripXml(Func<string, object> navCreator)
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.xml");

            // will allow whitespace and comments to come through      
            var nav = navCreator(tp);

            var outputBuilder = new StringBuilder();
            IElementDefinitionSummary serInfo = null;

            switch (nav)
            {
                case ISourceNavigator isn: serInfo = isn.GetElementDefinitionSummary(); return;
                case IElementNavigator ien: serInfo = ien.GetElementDefinitionSummary(); return;
            }

            bool hasTypeInfo = serInfo != null;

            var serializer = new FhirXmlWriter(new FhirXmlWriterSettings { AllowUntypedElements = !hasTypeInfo });
            using (var writer = XmlWriter.Create(outputBuilder))
            {
                if (nav is ISourceNavigator isn) serializer.Write(isn, writer);
                if (nav is IElementNavigator ien) serializer.Write(ien, writer);
            }

            var output = outputBuilder.ToString();
            XmlAssert.AreSame("fp-test-patient.xml", tp, output);
        }


        public static void RoundtripJson(Func<string, object> navCreator)
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            compareJson(navCreator, tp);

            tp = File.ReadAllText(@"TestData\json-edge-cases.json");
            compareJson(navCreator, tp);
        }

        private static void compareJson(Func<string, object> navCreator, string tp)
        {
            var nav = navCreator(tp);

            var outputBuilder = new StringBuilder();
            IElementDefinitionSummary serInfo = null;

            switch (nav)
            {
                case ISourceNavigator isn: serInfo = isn.GetElementDefinitionSummary(); return;
                case IElementNavigator ien: serInfo = ien.GetElementDefinitionSummary(); return;
            }

            bool hasTypeInfo = serInfo != null;


            var serializer = new FhirJsonWriter(new FhirJsonWriterSettings { AllowUntypedElements = !hasTypeInfo });
            using (var writer = new JsonTextWriter(new StringWriter(outputBuilder)))
            {
                if (nav is ISourceNavigator isn) serializer.Write(isn, writer);
                if (nav is IElementNavigator ien) serializer.Write(ien, writer);
            }

            var output = outputBuilder.ToString();
            JsonAssert.AreSame(tp, output);
        }

        public static void CanReadThroughNavigator(IElementNavigator nav, bool typed)
        {
            Assert.AreEqual("Patient", nav.Name);
            Assert.AreEqual("Patient", nav.GetResourceType());
            if (typed) Assert.AreEqual("Patient", nav.Type);

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("id", nav.Name);
            Assert.AreEqual("pat1", nav.Value);
            if (typed) Assert.AreEqual("id", nav.Type);

            var pat = nav.Clone();

            Assert.IsFalse(nav.MoveToFirstChild());

            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("text", nav.Name);
            if (typed) Assert.AreEqual("Narrative", nav.Type);
            var text = nav.Clone();

            Assert.IsTrue(text.MoveToFirstChild("status")); // status
            if (typed) Assert.AreEqual("code", text.Type);
            Assert.AreEqual("generated", text.Value);

            Assert.IsTrue(text.MoveToNext());
            Assert.AreEqual("div", text.Name);
            Assert.IsTrue(((string)text.Value).StartsWith("<div xmlns="));       // special handling of xhtml
            if (typed) Assert.AreEqual("xhtml", text.Type);

            Assert.IsFalse(text.MoveToFirstChild()); // cannot move into xhtml
            Assert.AreEqual("div", text.Name); // still on xhtml <div>
            Assert.IsFalse(text.MoveToNext());  // nothing more in <text>

            Assert.IsTrue(nav.MoveToNext()); // contained
            Assert.AreEqual("contained", nav.Name);
            Assert.AreEqual("Patient", nav.GetResourceType());
            if (typed) Assert.AreEqual("Patient", nav.Type);

            Assert.IsTrue(nav.MoveToFirstChild()); // id
            if (typed) Assert.AreEqual("id", nav.Type);
            Assert.IsTrue(nav.MoveToNext()); // identifier
            var identifier = nav.Clone();

            if (typed) Assert.AreEqual("Identifier", identifier.Type);
            Assert.IsTrue(identifier.MoveToFirstChild()); // system
            Assert.IsTrue(identifier.MoveToNext()); // value
            Assert.IsFalse(identifier.MoveToNext()); // still value

            Assert.AreEqual("value", identifier.Name);
            Assert.IsFalse(identifier.MoveToFirstChild());
            Assert.AreEqual("444222222", identifier.Value); // tests whether strings are trimmed

            Assert.IsTrue(nav.MoveToNext("name"));
            Assert.AreEqual("name", nav.Name);
            Assert.IsTrue(nav.MoveToFirstChild());  // id (attribute)
            Assert.AreEqual("id", nav.Name);
            Assert.AreEqual("firstname", nav.Value);
            Assert.IsTrue(nav.MoveToNext());  // use (element!)
            Assert.AreEqual("use", nav.Name);

            Assert.IsTrue(pat.MoveToNext("birthDate"));

            if (typed)
            {
                Assert.AreEqual("date", pat.Type);
                Assert.AreEqual(PartialDateTime.Parse("1974-12-25"), pat.Value);
            }
            else
                Assert.AreEqual("1974-12-25", pat.Value);

            if (typed)
            {
                Assert.IsTrue(pat.MoveToNext("deceased"));
                Assert.AreEqual("boolean", pat.Type);
                Assert.AreEqual(false, pat.Value);
            }
            else
            {
                Assert.IsTrue(pat.MoveToNext("deceasedBoolean"));
                Assert.AreEqual("false", pat.Value);
            }
        }
    }
}