using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace Hl7.Fhir.FhirPath
{
    [TestClass]
    public class AllResourceInvariantsTest
    {
        [TestMethod]
        public void TestAllResourceInvariants()
        {
            string failedExpressions = null;
            foreach (var item in ModelInfo.SupportedResources)
            {
                for (int n = 0; n < 10; n++)
                {
                    if (item == "Binary" || item == "Bundle" || item == "Parameters")
                        continue;
                    Type rt = ModelInfo.GetTypeForFhirType(item);
                    DomainResource dr = (DomainResource)Activator.CreateInstance(rt);
                    dr.AddDefaultConstraints();
                    if (dr.InvariantConstraints == null || dr.InvariantConstraints.Count == 0)
                        continue;
#if !NETCore
                    Trace.WriteLine(String.Format("Testing Validation: {0} ({1} rules)", item, (dr.InvariantConstraints != null ? dr.InvariantConstraints.Count : 0)));
#endif
                    var results = dr.Validate(new System.ComponentModel.DataAnnotations.ValidationContext(dr));
                    foreach (var result in results)
                    {
                        if (result.ErrorMessage.Contains("FATAL"))
                            failedExpressions += result.ErrorMessage + "\r\n";
#if !NETCore
                        Trace.WriteLine(result.ErrorMessage);
#endif
                    }
                    //if (results.Count() > 0)
#if !NETCore
                    Trace.WriteLine("");
#endif
                }
            }
            Assert.IsNull(failedExpressions);
        }
    }
}
