using Hl7.Fhir.Model;
using Hl7.Fhir.Model.CdsHooks;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Hl7.Fhir.Support.Poco.Tests.CdsHooks;

[TestClass]
public class CdsHooksDeserializationTests
{
    [TestMethod]
    [Experimental("ExperimentalApi")]
    public void CdsHookList_Deserialize()
    {
        var json = """
                   {
                     "services": [
                       {
                         "hook": "patient-view",
                         "title": "Static CDS Service Example",
                         "description": "An example of a CDS Service that returns a static set of cards",
                         "id": "static-patient-greeter",
                         "prefetch": {
                           "patientToGreet": "Patient/{{context.patientId}}"
                         }
                       },
                       {
                         "hook": "order-select",
                         "title": "Order Echo CDS Service",
                         "description": "An example of a CDS Service that simply echoes the order(s) being placed",
                         "id": "order-echo",
                         "prefetch": {
                           "patient": "Patient/{{context.patientId}}",
                           "medications": "MedicationRequest?patient={{context.patientId}}"
                         }
                       },
                       {
                         "hook": "order-sign",
                         "title": "Pharmacogenomics CDS Service",
                         "description": "An example of a more advanced, precision medicine CDS Service",
                         "id": "pgx-on-order-sign",
                         "usageRequirements": "Note: functionality of this CDS Service is degraded without access to a FHIR Restful API as part of CDS recommendation generation."
                       }
                     ]
                   }
                   """;
        var options = new JsonSerializerOptions().ForCdsHooks();
        var result = JsonSerializer.Deserialize<DiscoveryResponse>(json, options);
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Services);
        Assert.HasCount(3, result.Services);
        Assert.AreEqual("patient-view", result.Services[0].Hook);
        Assert.AreEqual("Static CDS Service Example", result.Services[0].Title);
        Assert.AreEqual("An example of a CDS Service that returns a static set of cards", result.Services[0].Description);
        Assert.AreEqual("static-patient-greeter", result.Services[0].Id);
        Assert.IsNotNull(result.Services[0].Prefetch);
        Assert.HasCount(1, result.Services[0].Prefetch);
        Assert.AreEqual("Patient/{{context.patientId}}", result.Services[0].Prefetch["patientToGreet"]);
        Assert.AreEqual("order-select", result.Services[1].Hook);
        Assert.AreEqual("Order Echo CDS Service", result.Services[1].Title);
        Assert.AreEqual("An example of a CDS Service that simply echoes the order(s) being placed", result.Services[1].Description);
        Assert.AreEqual("order-echo", result.Services[1].Id);
        Assert.IsNotNull(result.Services[1].Prefetch);
        Assert.HasCount(2, result.Services[1].Prefetch);
        Assert.AreEqual("Patient/{{context.patientId}}", result.Services[1].Prefetch["patient"]);
        Assert.AreEqual("MedicationRequest?patient={{context.patientId}}", result.Services[1].Prefetch["medications"]);
        Assert.AreEqual("order-sign", result.Services[2].Hook);
        Assert.AreEqual("Pharmacogenomics CDS Service", result.Services[2].Title);
        Assert.AreEqual("An example of a more advanced, precision medicine CDS Service", result.Services[2].Description);
        Assert.AreEqual("pgx-on-order-sign", result.Services[2].Id);
        Assert.AreEqual("Note: functionality of this CDS Service is degraded without access to a FHIR Restful API as part of CDS recommendation generation.", result.Services[2].UsageRequirements);
    }

    [TestMethod]
    [Experimental("ExperimentalApi")]
    public void CdsHookWithResource_Deserialize()
    {
        var json = """
                   {
                     "hookInstance": "d1577c69-dfbe-44ad-ba6d-3e05e953b2ea",
                     "fhirServer": "http://hooks.smarthealthit.org:9080",
                     "hook": "patient-view",
                     "fhirAuthorization": {
                       "access_token": "some-opaque-fhir-access-token",
                       "token_type": "Bearer",
                       "expires_in": 300,
                       "scope": "user/Patient.read user/Observation.read",
                       "subject": "cds-service4"
                     },
                     "context": {
                       "userId": "Practitioner/example",
                       "patientId": "1288992",
                       "encounterId": "89284"
                     },
                     "prefetch": {
                       "patientToGreet": {
                         "resourceType": "Patient",
                         "gender": "male",
                         "birthDate": "1925-12-23",
                         "id": "1288992",
                         "active": true
                       }
                     }
                   }
                   """;
        var options = new JsonSerializerOptions().ForCdsHooks();
        var result = JsonSerializer.Deserialize<Request>(json, options);
        Assert.IsNotNull(result);
        Assert.AreEqual("d1577c69-dfbe-44ad-ba6d-3e05e953b2ea", result.HookInstance);
        Assert.AreEqual("http://hooks.smarthealthit.org:9080/", result.FhirServer?.ToString());
        Assert.AreEqual("patient-view", result.Hook);
        Assert.IsNotNull(result.FhirAuthorization);
        Assert.IsNotNull(result.Context);
        Assert.IsNotNull(result.Prefetch);
        Assert.HasCount(1, result.Prefetch);
        var patientJson = """
                          {
                            "resourceType": "Patient",
                            "gender": "male",
                            "birthDate": "1925-12-23",
                            "id": "1288992",
                            "active": true
                          }
                          """;
        Assert.IsTrue(result.Prefetch["patientToGreet"].IsExactly(getResourceFromJson(patientJson)));
    }

    [TestMethod]
    [Experimental("ExperimentalApi")]
    public void CdsHookWithUnderscoreProperties_Deserialize()
    {
        var json = """
                   {
                     "fhirAuthorization": {
                       "access_token": "some-opaque-fhir-access-token",
                       "token_type": "Bearer",
                       "expires_in": 300,
                       "scope": "user/Patient.read user/Observation.read",
                       "subject": "cds-service4"
                     }
                   }
                   """;
        var options = new JsonSerializerOptions().ForCdsHooks();
        var result = JsonSerializer.Deserialize<Request>(json, options);
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.FhirAuthorization);
        Assert.AreEqual("some-opaque-fhir-access-token", result.FhirAuthorization.AccessToken);
        Assert.AreEqual("Bearer", result.FhirAuthorization.TokenType);
        Assert.AreEqual(300, result.FhirAuthorization.ExpiresIn);
        Assert.AreEqual("user/Patient.read user/Observation.read", result.FhirAuthorization.Scope);
        Assert.AreEqual("cds-service4", result.FhirAuthorization.Subject);
    }

    [TestMethod]
    [Experimental("ExperimentalApi")]
    public void CdsHookCombined_Deserialize()
    {
        var json = """
                   {
                     "hookInstance": "d1577c69-dfbe-44ad-ba6d-3e05e953b2ea",
                     "fhirServer": "http://fhir.example.com",
                     "hook": "order-sign",
                     "context": {
                       "userId": "Practitioner/example",
                       "medications": [
                         {
                           "resourceType": "MedicationOrder",
                           "id": "medrx001",
                           "dateWritten": "2017-05-05",
                           "status": "draft",
                           "patient": {
                             "reference": "Patient/example"
                           },
                           "medicationCodeableConcept": {
                             "coding": [
                               {
                                 "system": "http://www.nlm.nih.gov/research/umls/rxnorm",
                                 "code": "857001",
                                 "display": "Acetaminophen 325 MG / Hydrocodone Bitartrate 10 MG Oral Tablet"
                               }
                             ]
                           },
                           "dosageInstruction": [
                             {
                               "text": "Take 1 tablet Oral every 4 hours as needed",
                               "timing": {
                                 "repeat": {
                                   "frequency": 6,
                                   "frequencyMax": 6,
                                   "period": 1,
                                   "unit": "d"
                                 }
                               },
                               "asNeededBoolean": true,
                               "doseQuantity": {
                                 "value": 10,
                                 "unit": "mg",
                                 "system": "http://unitsofmeasure.org",
                                 "code": "mg"
                               }
                             }
                           ]
                         }
                       ],
                       "patientId": "1288992"
                     },
                     "prefetch": {
                       "patient": {
                            "resourceType": "Patient",
                            "id": "1288992",
                            "active": true,
                            "name": [
                            {
                                "family": "Shaw",
                                "given": [
                                "Amy"
                                ]
                            }
                            ]
                        }
                     }
                   }
                   """;
        var options = new JsonSerializerOptions().ForCdsHooks();
        var result = JsonSerializer.Deserialize<Request>(json, options);

        // Validate hookInstance
        Assert.IsNotNull(result);
        Assert.AreEqual("d1577c69-dfbe-44ad-ba6d-3e05e953b2ea", result.HookInstance);

        // Validate fhirServer
        Assert.AreEqual("http://fhir.example.com/", result.FhirServer?.ToString());

        // Validate hook
        Assert.AreEqual("order-sign", result.Hook);

        // Validate context
        Assert.IsNotNull(result.Context);
        Assert.AreEqual("Practitioner/example", result.Context.UserId);
        Assert.AreEqual("1288992", result.Context.PatientId);
        Assert.IsNotNull(result.Context.Fields); // we cannot explore this (yet), since we do not know its type

        // Validate prefetch
        Assert.IsNotNull(result.Prefetch);
        Assert.IsNotNull(result.Prefetch["patient"]);
        var resourceJson = """
                           {
                               "resourceType": "Patient",
                               "id": "1288992",
                               "active": true,
                               "name": [
                               {
                                   "family": "Shaw",
                                   "given": [
                                   "Amy"
                                   ]
                               }
                               ]
                           }
                           """;
        Assert.IsTrue(result.Prefetch["patient"].IsExactly(getResourceFromJson(resourceJson)));
    }

    private Resource getResourceFromJson(string json)
    {
        var options = new JsonSerializerOptions().ForFhir();
        return JsonSerializer.Deserialize<Resource>(json, options);
    }
}