using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hl7.FhirPath.Expressions;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;

namespace Hl7.Fhir
{
    [TestClass]
    public class PocoNavigatorTests
    {

        [TestMethod]
        public void TestPocoPath()
        {
            // Ensure the FHIR extensions are registered
            ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();
            FhirPathCompiler.DefaultSymbolTable.Add("shortpathname",
            (object f) =>
            {
                if (f is IEnumerable<IElementNavigator>)
                {
                    object[] bits = (f as IEnumerable<IElementNavigator>).Select(i =>
                    {
                        if (i is PocoNavigator)
                        {
                            return (i as PocoNavigator).ShortPath;
                        }
                        return "?";
                    }).ToArray();
                    return FhirValueList.Create(bits);
                }
                return FhirValueList.Create(new object[] { "?" });
            });

            Patient p = new Patient();
            p.Active = true;
            p.ActiveElement.ElementId = "314";
            p.ActiveElement.AddExtension("http://something.org", new FhirBoolean(false));
            p.ActiveElement.AddExtension("http://something.org", new Integer(314));
            p.Telecom = new List<ContactPoint>();
            p.Telecom.Add(new ContactPoint(ContactPoint.ContactPointSystem.Phone, null, "555-phone"));
            p.Telecom[0].Rank = 1;

            foreach (var item in p.Select("descendants().shortpathname()"))
            {
                System.Diagnostics.Trace.WriteLine(item.ToString());
            }
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
            Assert.AreEqual("Patient.active.extension('http://something.org').value", v2.CommonPath);

            PocoNavigator v3 = new PocoNavigator(p);
            v3.MoveToFirstChild(); System.Diagnostics.Trace.WriteLine($"{v3.ShortPath} = {v3.FhirValue.ToString()}");
            v3.MoveToNext(); System.Diagnostics.Trace.WriteLine($"{v3.ShortPath} = {v3.FhirValue.ToString()}");
            // v3.MoveToNext(); System.Diagnostics.Trace.WriteLine($"{v3.ShortPath} = {v3.FhirValue.ToString()}");
            // v3.MoveToNext(); System.Diagnostics.Trace.WriteLine($"{v3.ShortPath} = {v3.FhirValue.ToString()}");
            v3.MoveToFirstChild("system"); System.Diagnostics.Trace.WriteLine($"{v3.ShortPath} = {v3.FhirValue.ToString()}");
            Assert.AreEqual("Patient.telecom[0].system[0]", v3.Location);
            Assert.AreEqual("Patient.telecom[0].system", v3.ShortPath);
            Assert.AreEqual("Patient.telecom.where(system='phone').system", v3.CommonPath);

            // Now check navigation bits
            var v4 = new PocoNavigator(p);
            Assert.AreEqual("Patient.telecom.where(system='phone').system", 
                (v4.Select("Patient.telecom.where(system='phone').system").First() as PocoNavigator).CommonPath);
            v4 = new PocoNavigator(p);
            Assert.AreEqual("Patient.telecom[0].system",
                (v4.Select("Patient.telecom.where(system='phone').system").First() as PocoNavigator).ShortPath);
            v4 = new PocoNavigator(p);
            Assert.AreEqual("Patient.telecom[0].system[0]",
                (v4.Select("Patient.telecom.where(system='phone').system").First() as PocoNavigator).Location);
            v4 = new PocoNavigator(p);
            Assert.AreEqual("Patient.telecom.where(system='phone').system", 
                (v4.Select("Patient.telecom[0].system").First() as PocoNavigator).CommonPath);
            v4 = new PocoNavigator(p);
            Assert.AreEqual("Patient.telecom[0].system", 
                (v4.Select("Patient.telecom[0].system").First() as PocoNavigator).ShortPath);
        }

        [TestMethod]
        public void PocoExtensionTest()
        {
            Patient p = new Patient();

            p.Active = true;
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

            var pocoP = new PocoNavigator((new FhirJsonParser()).Parse<Patient>(json));
            var jsonP = JsonDomFhirNavigator.Create(json);

            var compare = pocoP.IsEqualTo(jsonP);

            if (compare.Success == false)
            {
                Debug.WriteLine($"Difference in {compare.Details} at {compare.FailureLocation}");
                Assert.Fail();
            }           
        }
    }

}
