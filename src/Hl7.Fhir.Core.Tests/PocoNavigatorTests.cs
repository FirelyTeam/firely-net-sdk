using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir
{
    [TestClass]
    public class PocoNavigatorTests
    {
#pragma warning disable 612,618
        [TestMethod]
        public void TestPocoPath()
        {
            // Ensure the FHIR extensions are registered
            ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();
            FhirPathCompiler.DefaultSymbolTable.Add("shortpathname",
            (object f) =>
            {
                if (f is IEnumerable<ITypedElement>)
                {
                    object[] bits = (f as IEnumerable<ITypedElement>).Select(i =>
                    {
                        return i is PocoElementNode ? (i as PocoElementNode).ShortPath : "?";
                    }).ToArray();
                    return ElementNode.CreateList(bits);
                }
                return ElementNode.CreateList("?");
            });

            Patient p = new Patient
            {
                Active = true
            };
            p.ActiveElement.ElementId = "314";
            p.ActiveElement.AddExtension("http://something.org", new FhirBoolean(false));
            p.ActiveElement.AddExtension("http://something.org", new Integer(314));
            p.Telecom = new List<ContactPoint>
            {
                new ContactPoint(ContactPoint.ContactPointSystem.Phone, null, "555-phone")
            };
            p.Telecom[0].Rank = 1;

            Assert.IsTrue(new FhirString("Patient.active").IsExactly(p.Select("descendants().shortpathname()").FirstOrDefault()));

            var patient = new PocoNavigator(p);

            Assert.AreEqual("Patient", patient.Location);

            patient.MoveToFirstChild();
            Assert.AreEqual("Patient.active[0]", patient.Location);
            Assert.AreEqual("Patient.active", patient.ShortPath);

            patient.MoveToFirstChild();
            Assert.AreEqual("Patient.active[0].id[0]", patient.Location);
            Assert.AreEqual("Patient.active.id", patient.ShortPath);

            Assert.IsTrue(patient.MoveToNext());
            Assert.AreEqual("Patient.active[0].extension[0]", patient.Location);
            Assert.AreEqual("Patient.active.extension[0]", patient.ShortPath);

            PocoNavigator v1 = patient.Clone() as PocoNavigator;
            v1.MoveToFirstChild();
            v1.MoveToNext();
            Assert.AreEqual("Patient.active[0].extension[0].value[0]", v1.Location);
            Assert.AreEqual("Patient.active.extension[0].value", v1.ShortPath);
            Assert.IsFalse(v1.MoveToNext());

            // Ensure that the original navigator hasn't changed
            Assert.AreEqual("Patient.active[0].extension[0]", patient.Location);
            Assert.AreEqual("Patient.active.extension[0]", patient.ShortPath);

            PocoNavigator v2 = patient.Clone() as PocoNavigator;
            v2.MoveToNext();
            v2.MoveToFirstChild();
            v2.MoveToNext();
            Assert.AreEqual("Patient.active[0].extension[1].value[0]", v2.Location);
            Assert.AreEqual("Patient.active.extension[1].value", v2.ShortPath);
            //Assert.AreEqual("Patient.active.extension('http://something.org').value", v2.CommonPath);

            PocoNavigator v3 = new PocoNavigator(p);
            v3.MoveToFirstChild(); System.Diagnostics.Trace.WriteLine($"{v3.ShortPath} = {v3.FhirValue.ToString()}");
            v3.MoveToNext(); System.Diagnostics.Trace.WriteLine($"{v3.ShortPath} = {v3.FhirValue.ToString()}");
            // v3.MoveToNext(); System.Diagnostics.Trace.WriteLine($"{v3.ShortPath} = {v3.FhirValue.ToString()}");
            // v3.MoveToNext(); System.Diagnostics.Trace.WriteLine($"{v3.ShortPath} = {v3.FhirValue.ToString()}");
            v3.MoveToFirstChild("system"); System.Diagnostics.Trace.WriteLine($"{v3.ShortPath} = {v3.FhirValue.ToString()}");
            Assert.AreEqual("Patient.telecom[0].system[0]", v3.Location);
            Assert.AreEqual("Patient.telecom[0].system", v3.ShortPath);
            //Assert.AreEqual("Patient.telecom.where(system='phone').system", v3.CommonPath);

            // Now check navigation bits
            Assert.AreEqual("Patient.telecom[0].system",
                (p.ToTypedElement().Select("Patient.telecom.where(system='phone').system").First() as PocoElementNode).ShortPath);
            var v4 = new PocoNavigator(p);
            Assert.AreEqual("Patient.telecom[0].system[0]",
                (v4.Select("Patient.telecom.where(system='phone').system").First()).Location);
        }
#pragma warning restore 612,618

        [TestMethod]
        public void PocoExtensionTest()
        {
            Patient p = new Patient
            {
                Active = true
            };
            p.ActiveElement.ElementId = "314";
            p.ActiveElement.AddExtension("http://something.org", new FhirBoolean(false));
            p.ActiveElement.AddExtension("http://something.org", new Integer(314));

            Assert.AreEqual(true, p.Scalar("Patient.active.first()"));
            Assert.AreEqual(true, p.Scalar("Patient.active[0]"));
            Assert.AreEqual("314", p.Scalar("Patient.active[0].id[0]"));

            var extensions = p.Select("Patient.active[0].extension");
            Assert.AreEqual(2, extensions.Count());
        }

        [TestMethod]
        public void PocoHasValueTest()
        {
            // Ensure the FHIR extensions are registered
            FhirPath.ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();

            Patient p = new Patient();

            Assert.AreEqual(false, p.Predicate("Patient.active.hasValue()"));
            Assert.AreEqual(false, p.Predicate("Patient.active.exists()"));

            p.Active = true;
            Assert.AreEqual(true, p.Predicate("Patient.active.hasValue()"));
            Assert.AreEqual(true, p.Predicate("Patient.active.exists()"));

            p.ActiveElement.AddExtension("http://something.org", new FhirBoolean(false));
            Assert.AreEqual(true, p.Predicate("Patient.active.hasValue()"));
            Assert.AreEqual(true, p.Predicate("Patient.active.exists()"));

            p.ActiveElement = new FhirBoolean();
            p.ActiveElement.AddExtension("http://something.org", new FhirBoolean(false));
            Assert.AreEqual(false, p.Predicate("Patient.active.hasValue()"));
            Assert.AreEqual(true, p.Predicate("Patient.active.exists()"));
        }

        [TestMethod]
        public void CompareToOtherElementNavigator()
        {
            var json = TestDataHelper.ReadTestData("TestPatient.json");
            var xml = TestDataHelper.ReadTestData("TestPatient.xml");

            var pocoP = (new FhirJsonParser()).Parse<Patient>(json).ToTypedElement();
            var jsonP = FhirJsonNode.Parse(json, settings: new FhirJsonParsingSettings { AllowJsonComments = true })
                .ToTypedElement(new PocoStructureDefinitionSummaryProvider());
            var xmlP = FhirXmlNode.Parse(xml).ToTypedElement(new PocoStructureDefinitionSummaryProvider());

            doCompare(pocoP, jsonP, "poco<->json");
            doCompare(pocoP, xmlP, "poco<->xml");

            void doCompare(ITypedElement one, ITypedElement two, string what)
            {
                var compare = one.IsEqualTo(two);

                if (compare.Success == false)
                {
                    Debug.WriteLine($"{what}: Difference in {compare.Details} at {compare.FailureLocation}");
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void IncorrectPathInTwoSuccessiveRepeatingMembers()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "issue-444-testdata.xml"));
            var cs = (new FhirXmlParser()).Parse<Conformance>(xml);
            var nav = cs.ToTypedElement();

            var rest = nav.Children().Where(c => c.Name == "rest").FirstOrDefault();

            Assert.IsNotNull(rest);

            Assert.IsTrue(rest.Location.Contains("Conformance.rest[0]"));
        }


        [TestMethod]
        public void PocoTypedElementPerformance()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var cs = (new FhirXmlParser()).Parse<Patient>(xml);
            var nav = cs.ToTypedElement();

            TypedElementPerformance(nav);
        }

        private static void TypedElementPerformance(ITypedElement nav)
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

        [TestMethod]
        public void PocoNavPerformance()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var cs = (new FhirXmlParser()).Parse<Patient>(xml);
#pragma warning disable CS0618 // Type or member is obsolete
            var nav = cs.ToElementNavigator();
#pragma warning restore CS0618 // Type or member is obsolete

            ElementNavPerformance(nav);
        }

#pragma warning disable CS0618 // Type or member is obsolete
        private static void ElementNavPerformance(IElementNavigator nav)
#pragma warning restore CS0618 // Type or member is obsolete
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


    }

}
