using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class IssuesTests
    {
        /// <summary>
        /// See https://github.com/FirelyTeam/fhir-net-api/issues/474
        /// </summary>
        [TestMethod]
        public void Issue474StartdateIs0001_01_01()
        {
            var json = "{ \"resourceType\": \"Patient\", \"active\": true, \"contact\": [{\"organization\": {\"reference\": \"Organization/1\", \"display\": \"Walt Disney Corporation\" }, \"period\": { \"start\": \"0001-01-01\", \"end\": \"2018\" } } ],}";

            var ctx = new ValidationSettings()
            {
                ResourceResolver = ZipSource.CreateValidationSource(),
            };

            var validator = new Validator(ctx);

            var pat = new FhirJsonParser().Parse<Patient>(json);

            var report = validator.Validate(pat);
            Assert.IsTrue(report.Success);
        }

        [TestMethod]
        public void Issue1244AppointmentBundle()
        {           
            var appointment = new Appointment
            {
                IdElement = new Id("f1b652f7-61c5-4036-a897-9935b245b30c"),
                Status = Appointment.AppointmentStatus.Booked,
                Start = new System.DateTimeOffset(2019, 08, 03, 08, 00, 00, System.TimeSpan.FromHours(2.00)),
                End = new System.DateTimeOffset(2019, 08, 03, 08, 30, 00, System.TimeSpan.FromHours(2.00)),
                Participant = new List<Appointment.ParticipantComponent>
                {
                    new Appointment.ParticipantComponent
                    {
                        Actor = new ResourceReference("Practitioner/d33a6cd1-7127-4eaf-b6fc-45c28a5277b0"),
                        Status = ParticipationStatus.Accepted
                    }
                }
            };

            var bundle = new Bundle
            {
                IdElement = new Id("18c8c73a-9d96-498f-95c3-3e8c59636177"),
                Type = Bundle.BundleType.Collection,
                Entry = new List<Bundle.EntryComponent>
                {
                    new Bundle.EntryComponent
                    {
                        FullUrl = "Appointment/f1b652f7-61c5-4036-a897-9935b245b30c",
                        Resource = appointment
                    }
                }
            };

            var settings = new ValidationSettings()
            {
                ResourceResolver = ZipSource.CreateValidationSource(),
            };
            var validator = new Validator(settings);

            var report = validator.Validate(bundle);
            Assert.IsTrue(report.Success);
        }
    }
}
