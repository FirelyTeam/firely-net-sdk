/*
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Tests.Serialization
{
    public partial class ResourceParsingTests
    {
        //test for https://github.com/FirelyTeam/firely-net-sdk/issues/1858
        [TestMethod]
        public async Tasks.Task CanReadSimilarValueXElements()
        {
            var json = TestDataHelper.ReadTestData("test-similar-valuex.json");
            var pser = new FhirJsonParser();

            // Assume that we can happily read the data and no errors occur
            var i = await pser.ParseAsync<Ingredient>(json);

            (i.Substance.Strength[0].Presentation as Quantity).Value.Should().Be(1);
            (i.Substance.Strength[0].Concentration as CodeableConcept).Text.Should().Be("text");
            i.Substance.Strength[0].ConcentrationText.Should().Be("Another text");
        }
    }
}
