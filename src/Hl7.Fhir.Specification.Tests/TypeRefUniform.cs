using System;
using Hl7.Fhir.Specification.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class TypeRefUniform
    {
        static Structure current = new Structure() { ProfileUri = new Uri("http://myprofiles/Netherlands") };

        static string TypeRefUri(string code, string profile)
        {
            TypeRef typeref = new TypeRef(code, profile);
            UriHelper.SetTypeRefIdentification(current, typeref);
            return typeref.Uri.ToString();
        }

        static string StructureUri(string profileuri, string type, string name)
        {
            Structure structure = new Structure();
            structure.Type = type;
            structure.Name = name;
            Uri uri = new Uri(profileuri);
            UriHelper.SetStructureIdentification(structure, uri);
            return structure.Uri.ToString();
        }

        [TestMethod]
        public void TypeRefs()
        {
            string s;

            s = TypeRefUri("string", null);
            Assert.AreEqual("http://hl7.org/fhir/Profile/string", s);

            s = TypeRefUri("Patient", null);
            Assert.AreEqual("http://hl7.org/fhir/Profile/Patient", s);

            s = TypeRefUri("ResourceReference", "http://hl7.org/fhir/Profile/Patient");
            Assert.AreEqual("http://hl7.org/fhir/Profile/ResourceReference", s);

            s = TypeRefUri("Resource", "http://myprofiles/Netherlands#patient");
            Assert.AreEqual("http://myprofiles/Netherlands#patient", s);


            s = TypeRefUri("Extension", "#calculated");
            Assert.AreEqual("http://myprofiles/Netherlands#calculated", s);

        }

        [TestMethod]
        public void Structures()
        {
            string s;

            s = StructureUri("http://myprofile", "Patient", "dutchpatient");
            Assert.AreEqual("http://myprofile/#dutchpatient", s);

            s = StructureUri("http://myprofile", "Patient", null);
            Assert.AreEqual("http://myprofile/#Patient", s);

            s = StructureUri("http://hl7.org/fhir/Profile/Patient", "Patient", null);
            Assert.AreEqual("http://hl7.org/fhir/Profile/Patient", s);
        }
    }
}
