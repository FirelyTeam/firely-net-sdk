using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Hl7.Fhir.Serialization.Tests
{
#pragma warning disable SYSLIB0011 // Type or member is obsolete
    [TestClass]
    public class RoundtripBinarySerialization
    {
        [TestMethod]
        public async System.Threading.Tasks.Task RoundtripStructureDefinition()
        {
            var spec = ZipSource.CreateValidationSource();

            var sd = await spec.FindStructureDefinitionForCoreTypeAsync("Patient");

            BinaryFormatter formatter = new();

            var stream = new MemoryStream();

            formatter.Serialize(stream, sd);
            stream.Seek(0, SeekOrigin.Begin);

            var sd2 = formatter.Deserialize(stream) as Model.StructureDefinition;

            Assert.IsTrue(sd.IsExactly(sd2));
        }
    }
#pragma warning restore SYSLIB0011 // Type or member is obsolete
}
