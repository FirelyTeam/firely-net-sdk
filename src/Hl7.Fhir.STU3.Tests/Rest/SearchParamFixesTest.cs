/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Rest;

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
        public void CheckManualFixesOfTemplateModelInfo()
        {
            //Manualy removed target of EpisodeOfCare from searchparameter DiagnosticReport.encounter
            //Commit: 7a61694eb476619b65387341644c83200ef4d3dd
            var sp = ModelInfo.SearchParameters.Where(s => s.Resource == "DiagnosticReport" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp);
            Assert.IsTrue(sp.Path.Contains("DiagnosticReport.context"));           
            Assert.IsFalse(sp.Target.Contains(ResourceType.EpisodeOfCare));

            //Manualy removed this target from more occurances of the same searchparameter
            //Commit: 3b071d478ff3cb744cb6668ac8512dc7362e6737
            var sp2 = ModelInfo.SearchParameters.Where(s => s.Resource == "DocumentReference" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp2);
            Assert.IsFalse(sp2.Target.Contains(ResourceType.EpisodeOfCare));
           
            var sp3 = ModelInfo.SearchParameters.Where(s => s.Resource == "RiskAssessment" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp3);
            Assert.IsFalse(sp3.Target.Contains(ResourceType.EpisodeOfCare));
        
            var sp4 = ModelInfo.SearchParameters.Where(s => s.Resource == "List" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp4);
            Assert.IsFalse(sp4.Target.Contains(ResourceType.EpisodeOfCare));

            var sp5 = ModelInfo.SearchParameters.Where(s => s.Resource == "VisionPrescription" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp5);
            Assert.IsFalse(sp5.Target.Contains(ResourceType.EpisodeOfCare));

            var sp6 = ModelInfo.SearchParameters.Where(s => s.Resource == "ProcedureRequest" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp6);
            Assert.IsFalse(sp6.Target.Contains(ResourceType.EpisodeOfCare));

            var sp7 = ModelInfo.SearchParameters.Where(s => s.Resource == "Flag" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp7);
            Assert.IsFalse(sp7.Target.Contains(ResourceType.EpisodeOfCare));

            var sp8 = ModelInfo.SearchParameters.Where(s => s.Resource == "Observation" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp8);
            Assert.IsFalse(sp8.Target.Contains(ResourceType.EpisodeOfCare));

            var sp9 = ModelInfo.SearchParameters.Where(s => s.Resource == "NutritionOrder" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp9);
            Assert.IsFalse(sp9.Target.Contains(ResourceType.EpisodeOfCare));

            var sp10 = ModelInfo.SearchParameters.Where(s => s.Resource == "Composition" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp10);
            Assert.IsFalse(sp10.Target.Contains(ResourceType.EpisodeOfCare));

            //These occurances of the searchparameter do have the EpisodeOfCare as target
            var sp11 = ModelInfo.SearchParameters.Where(s => s.Resource == "DeviceRequest" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp11);
            Assert.IsTrue(sp11.Target.Contains(ResourceType.EpisodeOfCare));

            var sp12 = ModelInfo.SearchParameters.Where(s => s.Resource == "Procedure" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp12);
            Assert.IsTrue(sp12.Target.Contains(ResourceType.EpisodeOfCare));

        }
    }
}
