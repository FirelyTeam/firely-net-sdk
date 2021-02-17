/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class TerminologyTests
    {
        [TestMethod]
        public void TestExpansionCheckForCode()
        {
            var vs = new ValueSet();
            var sys1 = "http://example.org/system/system1";
            var sys2 = "http://example.org/system/system2";

            vs.Expansion = new ValueSet.ExpansionComponent();

            vs.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = sys1, Code = "code1" });
            var sys1code2 = new ValueSet.ContainsComponent { System = sys1, Code = "code2" };
            vs.Expansion.Contains.Add(sys1code2);
            vs.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = sys2, Code = "code1" });

            sys1code2.Contains.Add(new ValueSet.ContainsComponent { System = sys1, Code = "code1.1" });
            sys1code2.Contains.Add(new ValueSet.ContainsComponent { System = sys1, Code = "code1.2" });

            Assert.IsTrue(vs.CodeInExpansion("code1", sys1));
            Assert.IsTrue(vs.CodeInExpansion("code1", sys2));
            Assert.IsTrue(vs.CodeInExpansion("code1"));

            Assert.IsFalse(vs.CodeInExpansion("code2", sys2));
            Assert.IsTrue(vs.CodeInExpansion("code2"));

            Assert.IsTrue(vs.CodeInExpansion("code1.2"));
            Assert.IsTrue(vs.CodeInExpansion("code1.2", sys1));
            Assert.IsFalse(vs.CodeInExpansion("code1.2", sys2));
        }

        [TestMethod]
        public void TestImportExpansion()
        {
            var sys1 = "http://example.org/system/system1";
            var sys2 = "http://example.org/system/system2";

            var vs = new ValueSet();
            vs.Expansion = new ValueSet.ExpansionComponent();
            vs.Expansion.Total = 2;
            vs.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = sys1, Code = "code1" });
            vs.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = sys2, Code = "code1" });

            var vs2 = new ValueSet();
            vs2.Expansion = new ValueSet.ExpansionComponent();
            vs2.Expansion.Total = 2;
            vs2.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = sys1, Code = "code3" });
            vs2.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = sys2, Code = "code4" });

            vs.ImportExpansion(vs2);

            Assert.AreEqual(4, vs.Expansion.Total);
            Assert.AreEqual(4, vs.Expansion.Total);

            Assert.IsTrue(vs.CodeInExpansion("code1", sys2));
            Assert.IsTrue(vs.CodeInExpansion("code4", sys2));
        }

        [TestMethod]
        public void TestImportExpansionInEmptyVs()
        {
            var sys1 = "http://example.org/system/system1";
            var sys2 = "http://example.org/system/system2";

            var vs = new ValueSet();
            var vs2 = new ValueSet();
            vs2.Expansion = new ValueSet.ExpansionComponent();
            vs2.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = sys1, Code = "code3" });
            vs2.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = sys2, Code = "code4" });

            vs.ImportExpansion(vs2);

            Assert.AreEqual(2, vs.Expansion.Total);
            Assert.AreEqual(2, vs.Expansion.Total);

            Assert.IsTrue(vs.CodeInExpansion("code3", sys1));
            Assert.IsTrue(vs.CodeInExpansion("code4", sys2));
        }

        [TestMethod]
        public void CanConvertToSystemCoding()
        {
            var fhirCode = new Code("atem");
            var systemCode = fhirCode.ToSystemCode();
            Assert.AreEqual("atem", systemCode.Value);
            Assert.IsNull(systemCode.System);

            var fhirCodeOfT = new Code<AdministrativeGender>(AdministrativeGender.Male);
            systemCode = fhirCodeOfT.ToSystemCode();
            Assert.AreEqual("male", systemCode.Value);
            Assert.AreEqual("http://hl7.org/fhir/administrative-gender", systemCode.System);

            var fhirCoding = new Coding("http://nu.nl", "yes", "positive") { Version = "1.0.0" };
            systemCode = fhirCoding.ToSystemCode();
            verifyCoding(systemCode);

            var fhirCodeableConcept = new CodeableConcept("http://nu.nl", "yes", "positive", "Original text");
            fhirCodeableConcept.Coding[0].Version = "1.0.0";
            var systemConcept = fhirCodeableConcept.ToSystemConcept();
            Assert.AreEqual("Original text", systemConcept.Display);
            Assert.AreEqual(1, systemConcept.Codes.Count);
            verifyCoding(systemConcept.Codes.First());

            static void verifyCoding(ElementModel.Types.Code systemCode)
            {
                Assert.AreEqual("yes", systemCode.Value);
                Assert.AreEqual("http://nu.nl", systemCode.System);
                Assert.AreEqual("positive", systemCode.Display);
                Assert.AreEqual("1.0.0", systemCode.Version);
            }
        }
    }
}
