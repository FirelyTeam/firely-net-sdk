using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Serialization.Tests
{
    public class ParseDemoPatient
    {
        public static void ElementNavPerformance(ISourceNode nav)
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
                var usual = nav.Children("identifier").First().Children("use").First().Text;
                Assert.IsNotNull(usual);
                var phone = nav.Children("telecom").First().Children("system").First().Text;
                Assert.IsNotNull(usual);
                var prefs = nav.Children("communication").Where(c => c.Children("preferred").Any(pr => pr.Text == "true")).Count();
                Assert.AreNotEqual(0, prefs);
                var link = nav.Children("link").Children("other").Children("reference");
                Assert.IsNotNull(link);
            }
        }

        public static void ProducesCorrectUntypedLocations(ISourceNode patient)
        {
            Assert.AreEqual("Patient", patient.Location);

            Assert.AreEqual("Patient.id[0]", patient.Children().First().Location);
            var identifiers = patient.Children("identifier").ToList();
            Assert.AreEqual("Patient.identifier[0]", identifiers[0].Location);
            Assert.AreEqual("Patient.identifier[0].use[0]", identifiers[0].Children().First().Location);
            Assert.AreEqual("Patient.identifier[1]", identifiers[1].Location);
            Assert.AreEqual("Patient.identifier[1].use[0]", identifiers[1].Children().First().Location);
            Assert.AreEqual("Patient.deceasedBoolean[0]", patient.Children("deceasedBoolean").Single().Location);
            Assert.AreEqual("Patient.contained[0].name[1].use[0]",
                patient.Children("contained").First().Children("name").Skip(1).First().Children("use").Single().Location);
        }

        public static void ProducedCorrectTypedLocations(ITypedElement patient)
        {
            static string getPretty(ITypedElement n) => n.Annotation<IShortPathGenerator>().ShortPath;

            Assert.AreEqual("Patient", getPretty(patient));

            Assert.AreEqual("Patient.id", getPretty(patient.Children("id").First()));
            var ids = patient.Children("identifier").ToList();
            Assert.AreEqual("Patient.identifier[0]", getPretty(ids[0]));
            Assert.AreEqual("Patient.identifier[0].use", getPretty(ids[0].Children("use").Single()));
            Assert.AreEqual("Patient.identifier[1]", getPretty(ids[1]));
            Assert.AreEqual("Patient.identifier[1].use", getPretty(ids[1].Children("use").Single()));
            Assert.AreEqual("Patient.deceased", getPretty(patient.Children("deceased").Single()));
            Assert.AreEqual("Patient.contained[0].name[1].use",
                getPretty(patient.Children("contained").First().Children("name").Skip(1)
                .First().Children("use").Single()));
        }

        public static void HasLineNumbers<T>(ISourceNode nav) where T : class, IPositionInfo
        {
            nav = nav.Children().FirstOrDefault();
            Assert.IsNotNull(nav);

            var posInfo = (nav as IAnnotated)?.Annotation<T>();
            Assert.IsNotNull(posInfo);
            Assert.AreNotEqual(-1, posInfo.LineNumber);
            Assert.AreNotEqual(-1, posInfo.LinePosition);
            Assert.AreNotEqual(0, posInfo.LineNumber);
            Assert.AreNotEqual(0, posInfo.LinePosition);
        }

        public static void CheckBundleEntryNavigation(ITypedElement node)
        {
            var entryNav = node.Select("entry.resource").First();
            var id = entryNav.Scalar("id");
            Assert.IsNotNull(id);
        }

        public static async Task RoundtripXml(Func<string, object> navCreator)
        {
            var tp = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));

            // will allow whitespace and comments to come through      
            var nav = navCreator(tp);
            switch (nav)
            {
                case ISourceNode _:
                    break;
                case ITypedElement _:
                    break;
                default:
                    throw Error.InvalidOperation("Fix unit test");
            }

            string output;
            if (nav is ISourceNode isn2) output = await isn2.ToXmlAsync();
            else if (nav is ITypedElement ien2) output = await ien2.ToXmlAsync();
            else
                throw Error.InvalidOperation("Fix unit test");

            XmlAssert.AreSame("fp-test-patient.xml", tp, output);
        }

        public static async Task RoundtripJson(Func<string, Task<object>> navCreator)
        {
            var tp = await File.ReadAllTextAsync(Path.Combine("TestData", "json-edge-cases.json"));
            await compareJson(@"TestData\json-edge-cases.json", navCreator, tp);
        }

        private static async Task compareJson(string filename, Func<string, Task<object>> navCreator, string expected)
        {
            var nav = await navCreator(expected);
            switch (nav)
            {
                case ISourceNode _:
                    break;
                case ITypedElement _:
                    break;
                default:
                    throw Error.InvalidOperation("Fix unit test");
            }

            string output;
            if (nav is ISourceNode isn2) output = await isn2.ToJsonAsync();
            else if (nav is ITypedElement ien2) output = await ien2.ToJsonAsync();
            else
                throw Error.InvalidOperation("Fix unit test");

            List<string> errors = new List<string>();
            JsonAssert.AreSame(filename, expected, output, errors);
            Console.WriteLine(string.Join("\r\n", errors));
            Assert.AreEqual(0, errors.Count, "Errors were encountered comparing converted content");
        }

        public static void CanReadThroughTypedElement(ITypedElement n, bool typed)
        {
            Assert.AreEqual("Patient", n.Name);
            Assert.AreEqual("Patient", n.Annotation<IResourceTypeSupplier>()?.ResourceType);
            if (typed) Assert.AreEqual("Patient", n.InstanceType);

            var tes = n.Children().GetEnumerator();

            Assert.IsTrue(tes.MoveNext());
            Assert.AreEqual("id", tes.Current.Name);
            Assert.AreEqual("pat1", tes.Current.Value);
            if (typed) Assert.AreEqual("id", tes.Current.InstanceType);

            Assert.IsFalse(tes.Current.Children().Any());

            Assert.IsTrue(tes.MoveNext());
            Assert.AreEqual("text", tes.Current.Name);
            if (typed) Assert.AreEqual("Narrative", tes.Current.InstanceType);

            var text = n.Children("text").Children().GetEnumerator();

            Assert.IsTrue(text.MoveNext()); // status
            if (typed) Assert.AreEqual("code", text.Current.InstanceType);
            Assert.AreEqual("generated", text.Current.Value);

            Assert.IsTrue(text.MoveNext());
            Assert.AreEqual("div", text.Current.Name);
            Assert.IsTrue(((string)text.Current.Value).StartsWith("<div xmlns="));       // special handling of xhtml
            if (typed) Assert.AreEqual("xhtml", text.Current.InstanceType);

            Assert.IsFalse(text.Current.Children().Any()); // cannot move into xhtml
            Assert.AreEqual("div", text.Current.Name); // still on xhtml <div>
            var b = text.MoveNext();
            Assert.IsFalse(b);  // nothing more in <text>

            Assert.IsTrue(tes.MoveNext()); // contained
            Assert.AreEqual("contained", tes.Current.Name);
            Assert.AreEqual("Patient", tes.Current.Annotation<IResourceTypeSupplier>().ResourceType);
            if (typed) Assert.AreEqual("Patient", tes.Current.InstanceType);

            var contained = tes.Current.Children().GetEnumerator();
            Assert.IsTrue(contained.MoveNext()); // id
            if (typed) Assert.AreEqual("id", contained.Current.InstanceType);
            Assert.IsTrue(contained.MoveNext()); // identifier
            Assert.AreEqual("identifier", contained.Current.Name);
            if (typed) Assert.AreEqual("Identifier", contained.Current.InstanceType);

            var identifier = contained.Current.Children().GetEnumerator();

            Assert.IsTrue(identifier.MoveNext()); // system
            Assert.IsTrue(identifier.MoveNext()); // value
            var ic = identifier.Current;
            Assert.IsFalse(identifier.MoveNext()); // still value

            Assert.AreEqual("value", ic.Name);
            Assert.IsFalse(ic.Children().Any());
            Assert.AreEqual("444222222", ic.Value); // tests whether strings are trimmed

            var name = n.Children("contained").First().Children("name").First();
            Assert.AreEqual("name", name.Name);
            var inname = name.Children().ToList();
            Assert.IsTrue(inname.Any());
            Assert.AreEqual("id", inname[0].Name);
            Assert.AreEqual("firstname", inname[0].Value);
            Assert.AreEqual("use", inname[1].Name);

            var bd = n.Children("birthDate").Single();

            if (typed)
            {
                Assert.AreEqual("date", bd.InstanceType);
                Assert.AreEqual(P.Date.Parse("1974-12-25"), bd.Value);
            }
            else
                Assert.AreEqual("1974-12-25", bd.Value);

            

            if (typed)
            {
                var dec = n.Children("deceased").Single();
                Assert.AreEqual("boolean", dec.InstanceType);
                Assert.AreEqual(false, dec.Value);
            }
            else
            {
                var dec = n.Children("deceasedBoolean").Single();
                Assert.AreEqual("false", dec.Value);
            }
        }
    }
}
