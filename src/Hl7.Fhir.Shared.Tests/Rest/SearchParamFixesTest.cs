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
        [DataRow("DiagnosticReport", "encounter", ResourceType.EpisodeOfCare, "05bfc4f1d0a4568ca405e248c055a8a16d857ffb")]
        [DataRow("RiskAssessment", "encounter", ResourceType.EpisodeOfCare, "3b071d478ff3cb744cb6668ac8512dc7362e6737")]
        [DataRow("List", "encounter", ResourceType.EpisodeOfCare, "3b071d478ff3cb744cb6668ac8512dc7362e6737")]
        [DataRow("VisionPrescription", "encounter", ResourceType.EpisodeOfCare, "3b071d478ff3cb744cb6668ac8512dc7362e6737")]
        [DataRow("ServiceRequest", "encounter", ResourceType.EpisodeOfCare, "3b071d478ff3cb744cb6668ac8512dc7362e6737")]
        [DataRow("Flag", "encounter", ResourceType.EpisodeOfCare, "3b071d478ff3cb744cb6668ac8512dc7362e6737")]
        [DataRow("Observation", "encounter", ResourceType.EpisodeOfCare, "3b071d478ff3cb744cb6668ac8512dc7362e6737")]
        [DataRow("NutritionOrder", "encounter", ResourceType.EpisodeOfCare, "3b071d478ff3cb744cb6668ac8512dc7362e6737")]
        [DataRow("Composition", "encounter", ResourceType.EpisodeOfCare, "3b071d478ff3cb744cb6668ac8512dc7362e6737")]
        [DataRow("DeviceRequest", "encounter", ResourceType.EpisodeOfCare, "3b071d478ff3cb744cb6668ac8512dc7362e6737")]
        [DataRow("Procedure", "encounter", ResourceType.EpisodeOfCare, "3b071d478ff3cb744cb6668ac8512dc7362e6737")]
        public void CheckManualFixesOfTemplateModelInfo(string resource, string spName, ResourceType targetResource, string commit)
        {
            var sp = ModelInfo.SearchParameters.Where(s => s.Resource == resource && s.Name == spName).FirstOrDefault();
            if (sp is not null)
            {
                Assert.IsFalse(sp.Target.Contains(targetResource),
                    $"Manualy removed target {targetResource} from searchparameter {resource}.{spName}. Commit: {commit}");
            }
        }
    }
}