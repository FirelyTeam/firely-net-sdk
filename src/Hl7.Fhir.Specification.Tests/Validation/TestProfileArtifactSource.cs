using Hl7.Fhir.Specification.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using System.IO;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Snapshot;

namespace Hl7.Fhir.Validation
{
    internal class TestProfileArtifactSource : IArtifactSource
    {
        List<StructureDefinition> TestProfiles = new List<StructureDefinition>
        {
            buildIdentifierWithBSN(),
            buildIdentifierWithDriversLicense(),
            buildDutchPatient()
        };

        public IEnumerable<string> ListArtifactNames()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConformanceInformation> ListConformanceResources()
        {
            throw new NotImplementedException();
        }

        public Stream LoadArtifactByName(string artifactName)
        {
            throw new NotImplementedException();
        }

        public Resource LoadConformanceResourceByUrl(string url)
        {
            return TestProfiles.SingleOrDefault(p => p.Url == url);
        }

        private static StructureDefinition buildDutchPatient()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/DutchPatient", "Dutch Patient",
                    "Test Patient which requires an Identifier with either BSN or drivers license", FHIRDefinedType.Patient);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Patient").OfType(FHIRDefinedType.Patient));
            cons.Add(new ElementDefinition("Patient.identifier").Required()
                        .OfType(FHIRDefinedType.Identifier, "http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN")
                        .OrType(FHIRDefinedType.Identifier, "http://validationtest.org/fhir/StructureDefinition/IdentifierWithDL"));

            return result;
        }

        private static StructureDefinition buildIdentifierWithBSN()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN", "BSN Identifier",
                    "Test Identifier which requires a BSN oid", FHIRDefinedType.Identifier);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Identifier").OfType(FHIRDefinedType.Identifier));
            cons.Add(new ElementDefinition("Identifier.system").Required().Value(fix: new FhirUri("urn:oid:2.16.840.1.113883.2.4.6.3")));
            cons.Add(new ElementDefinition("Identifier.period").Prohibited());
            cons.Add(new ElementDefinition("Identifier.assigner").Prohibited());

            return result;
        }

        private static StructureDefinition buildIdentifierWithDriversLicense()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/IdentifierWithDL", "Drivers license Identifier",
                    "Test Identifier which requires a drivers license oid", FHIRDefinedType.Identifier);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Identifier").OfType(FHIRDefinedType.Identifier));
            cons.Add(new ElementDefinition("Identifier.system").Required().Value(fix: new FhirUri("urn:oid:2.16.840.1.113883.2.4.6.12")));

            return result;
        }

        private static StructureDefinition createTestSD(string url, string name, string description, FHIRDefinedType constrainedType, string baseUri=null)
        {
            var result = new StructureDefinition();

            result.Url = url;
            result.Name = name;
            result.Status = ConformanceResourceStatus.Draft;
            result.Description = description;
            result.FhirVersion = ModelInfo.Version;

            if (ModelInfo.IsKnownResource(constrainedType))
                result.Kind = StructureDefinition.StructureDefinitionKind.Resource;
            else if (ModelInfo.IsDataType(constrainedType) || ModelInfo.IsPrimitive(constrainedType))
                result.Kind = StructureDefinition.StructureDefinitionKind.Datatype;
            else
                result.Kind = StructureDefinition.StructureDefinitionKind.Logical;

            result.ConstrainedType = constrainedType;
            result.Abstract = false;

            if(baseUri == null)
                baseUri = ResourceIdentity.Core(constrainedType).ToString();

            result.Base = baseUri;

            result.Differential = new StructureDefinition.DifferentialComponent();

            return result;
        }
    }
}
