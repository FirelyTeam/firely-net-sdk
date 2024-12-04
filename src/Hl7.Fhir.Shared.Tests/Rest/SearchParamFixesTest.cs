/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Hl7.Fhir.Test.Rest
{
    [TestClass]
    public class SearchParamFixesTest
    {
        [TestMethod]
        public void CheckAllSearchFhirPathExpressions()
        {
            int errorsFound = 0;
            foreach (var item in ModelInfo.SearchParameters)
            {
                string expression = item.Expression;
                if (string.IsNullOrEmpty(expression))
                {
                    System.Diagnostics.Trace.WriteLine(String.Format("Search parameter {0}.{1} ({2}) has no expression",
                        item.Resource, item.Name, item.Type.ToString()));
                    continue;
                }
                if (expression.Contains(" or "))
                {
                    System.Diagnostics.Trace.WriteLine(String.Format("Search parameter {0}.{1} ({2}) should not contain an 'or' statement",
                        item.Resource, item.Name, item.Type.ToString()));
                    System.Diagnostics.Trace.WriteLine("\t" + item.Expression);
                    errorsFound++;
                }
            }
            Assert.AreEqual(0, errorsFound, "Invalid FhirPath expression in search parameters");
        }


        [TestMethod]
        [DataRow("DiagnosticReport", "encounter", VersionIndependentResourceTypesAll.EpisodeOfCare)]
        [DataRow("RiskAssessment", "encounter", VersionIndependentResourceTypesAll.EpisodeOfCare)]
        [DataRow("List", "encounter", VersionIndependentResourceTypesAll.EpisodeOfCare)]
        [DataRow("VisionPrescription", "encounter", VersionIndependentResourceTypesAll.EpisodeOfCare)]
        [DataRow("ServiceRequest", "encounter", VersionIndependentResourceTypesAll.EpisodeOfCare)]
        [DataRow("Flag", "encounter", VersionIndependentResourceTypesAll.EpisodeOfCare)]
        [DataRow("Observation", "encounter", VersionIndependentResourceTypesAll.EpisodeOfCare)]
        [DataRow("NutritionOrder", "encounter", VersionIndependentResourceTypesAll.EpisodeOfCare)]
        [DataRow("Composition", "encounter", VersionIndependentResourceTypesAll.EpisodeOfCare)]
        [DataRow("DeviceRequest", "encounter", VersionIndependentResourceTypesAll.EpisodeOfCare)]
        [DataRow("Procedure", "encounter", VersionIndependentResourceTypesAll.EpisodeOfCare)]
        public void CheckFixesOfTemplateModelInfo(string resource, string spName, VersionIndependentResourceTypesAll targetResource)
        {
            var sp = ModelInfo.SearchParameters.FirstOrDefault(s => s.Resource == resource && s.Name == spName);
            if (sp is not null)
            {
                Assert.IsFalse(sp.Target?.Contains(targetResource),
                    $"Target {targetResource} should have been removed from searchparameter {resource}.{spName} by generator.");
            }
        }
    }
}