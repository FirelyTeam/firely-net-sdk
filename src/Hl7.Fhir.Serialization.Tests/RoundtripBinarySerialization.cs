using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class RoundtripBinarySerialization
    {
        [TestMethod]
        public async System.Threading.Tasks.Task RoundtripStructureDefinition()
        {
            var spec = ZipSource.CreateValidationSource();

            var sd = await spec.FindStructureDefinitionForCoreTypeAsync("Patient");

            BinaryFormatter formatter = new BinaryFormatter();

            var stream = new MemoryStream();
            formatter.Serialize(stream, sd);
            stream.Seek(0, SeekOrigin.Begin);

            var sd2 = formatter.Deserialize(stream) as Model.StructureDefinition;

            Assert.IsTrue(sd.IsExactly(sd2));
        }
    }
}
