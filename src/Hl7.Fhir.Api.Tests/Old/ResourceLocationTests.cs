using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Tests
{
    [TestClass]
    public class ResourceLocationTests
    {
        [TestMethod]
        public void ValidateUriAssertions()
        {
            Uri abs = new Uri("http://www.nu.nl", UriKind.RelativeOrAbsolute);
            Assert.IsTrue(abs.IsAbsoluteUri);

            abs = new Uri("http://www.nu.nl");
            Assert.IsTrue(abs.IsAbsoluteUri);
            
            var notabs = new Uri("server/index.html", UriKind.RelativeOrAbsolute);
            Assert.IsFalse(notabs.IsAbsoluteUri);
        }

        [TestMethod]
        public void DetermineCollectionName()
        {
            Patient p = new Patient();

            Assert.AreEqual("patient", ResourceLocation.GetCollectionNameForResource(p));
            Assert.AreEqual("patient", ResourceLocation.GetCollectionNameForResource(p.GetType()));
        }


        [TestMethod]
        public void TryParseLocationWithId()
        {
            ResourceLocation rl = new ResourceLocation("http://fhir.server.com/svc/patient/@1");
            Assert.AreEqual("http://fhir.server.com/svc/patient/@1", rl.ToString());
            Assert.AreEqual("http", rl.Scheme);
            Assert.AreEqual("fhir.server.com", rl.Host);
            Assert.AreEqual("/svc/patient/@1", rl.Path);
            Assert.AreEqual("svc", rl.Service);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("1", rl.Id);
            Assert.AreEqual("http://fhir.server.com/svc/", rl.ServiceUri.ToString());
            Assert.AreEqual("patient/@1", rl.OperationPath.ToString());

            rl = new ResourceLocation("http://fhir.server.com/patient/@1/history/@9/myoperation");
            Assert.AreEqual("http://fhir.server.com/patient/@1/history/@9/myoperation", rl.ToString());
            Assert.AreEqual("fhir.server.com", rl.Host);
            Assert.IsNull(rl.Service);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("1", rl.Id);
            Assert.AreEqual("9", rl.VersionId);
            Assert.AreEqual("myoperation", rl.Operation);
            Assert.AreEqual("patient/@1/history/@9/myoperation", rl.OperationPath.ToString());

            rl = new ResourceLocation("http://fhir.server.com/patient/@1/history/@9");
            Assert.AreEqual("http://fhir.server.com/patient/@1/history/@9", rl.ToString());
            Assert.IsNull(rl.Service);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("1", rl.Id);
            Assert.AreEqual("9", rl.VersionId);
            Assert.IsNull(rl.Operation);
            Assert.AreEqual("patient/@1/history/@9", rl.OperationPath.ToString());

            rl = new ResourceLocation("http://fhir.server.com/patient/@1/history");
            Assert.AreEqual("http://fhir.server.com/patient/@1/history", rl.ToString());
            Assert.IsNull(rl.Service);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("1", rl.Id);
            Assert.IsNull(rl.VersionId);
            Assert.AreEqual("history",rl.Operation);
            Assert.AreEqual("patient/@1/history", rl.OperationPath.ToString());

            rl = new ResourceLocation("http://fhir.server.com/patient/@1");
            Assert.AreEqual("http://fhir.server.com/patient/@1", rl.ToString());
            Assert.IsNull(rl.Service);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("1", rl.Id);
            Assert.IsNull(rl.VersionId);
            Assert.IsNull(rl.Operation);
            Assert.AreEqual("patient/@1", rl.OperationPath.ToString());

            rl = new ResourceLocation("http://www.hl7.org", "svc/patient/@1");
            Assert.AreEqual("http://www.hl7.org/svc/patient/@1", rl.ToString());
            Assert.AreEqual("svc", rl.Service);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("1", rl.Id);

            rl = new ResourceLocation("http://www.hl7.org/root", "svc/patient/@1");
            Assert.AreEqual("http://www.hl7.org/root/svc/patient/@1", rl.ToString());
            Assert.AreEqual("root/svc", rl.Service);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("1", rl.Id);

            rl = new ResourceLocation("http://www.hl7.org/svc/organization/", "../patient/@1/myoperation");
            Assert.AreEqual("www.hl7.org", rl.Host);
            Assert.AreEqual("svc", rl.Service);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("1", rl.Id);
            Assert.AreEqual("myoperation", rl.Operation);
        }

        [TestMethod]
        public void TryParseNonIdLocations()
        {
            var rl = new ResourceLocation("http://hl7.org/patient/validate/@1");
            Assert.IsNull(rl.Service);
            Assert.AreEqual("validate", rl.Operation);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("1", rl.Id);

            rl = new ResourceLocation("http://hl7.org/svc/patient/myoperation");
            Assert.AreEqual("svc", rl.Service);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("myoperation", rl.Operation);
            Assert.IsNull(rl.Id);
            Assert.IsNull(rl.VersionId);

            rl = new ResourceLocation("http://hl7.org/svc/history");
            Assert.AreEqual("http://hl7.org/svc/history", rl.ToString());
            Assert.AreEqual("svc", rl.Service);
            Assert.IsNull(rl.Collection);
            Assert.AreEqual("history", rl.Operation);
            Assert.AreEqual("history", rl.OperationPath.ToString());

            rl = new ResourceLocation("http://hl7.org/svc/history?_since=2010-10-30&_count=50");
            Assert.AreEqual("http://hl7.org/svc/history?_since=2010-10-30&_count=50", rl.ToString());
            Assert.AreEqual("svc", rl.Service);
            Assert.IsNull(rl.Collection);
            Assert.AreEqual("?_since=2010-10-30&_count=50", rl.Query);
            Assert.AreEqual("history", rl.Operation);
            Assert.AreEqual("history?_since=2010-10-30&_count=50", rl.OperationPath.ToString());

            rl = new ResourceLocation("http://hl7.org:1234/svc/patient");
            Assert.AreEqual("http://hl7.org:1234/svc/patient", rl.ToString());
            Assert.AreEqual("svc", rl.Service);
            Assert.AreEqual(1234, rl.Port);
            Assert.IsNull(rl.Operation);
            Assert.AreEqual("patient", rl.Collection);

            rl = new ResourceLocation("http://hl7.org/svc/patient");
            Assert.AreEqual("svc", rl.Service);
            Assert.IsNull(rl.Operation);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("patient", rl.OperationPath.ToString());

            rl = new ResourceLocation("http://hl7.org/patient");
            Assert.IsNull(rl.Service);
            Assert.IsNull(rl.Operation);
            Assert.AreEqual("patient", rl.Collection);

            rl = new ResourceLocation("http://hl7.org/svc");
            Assert.AreEqual("http://hl7.org/svc", rl.ToString());
            Assert.AreEqual("svc", rl.Service);
            Assert.IsNull(rl.Operation);
            Assert.IsNull(rl.Collection);

            rl = new ResourceLocation("http://hl7.org");
            Assert.AreEqual("http://hl7.org/", rl.ToString());
            Assert.IsNull(rl.Service);
            Assert.IsNull(rl.Operation);
            Assert.IsNull(rl.Collection);


            try
            {
                rl = new ResourceLocation("relative");
                Assert.Fail("Single-argument constructor should throw exception on relative path");
            }
            catch
            {
                // Ok!
            }


        }


        [TestMethod]
        public void TryNavigation()
        {
            var old = new ResourceLocation("http://www.hl7.org/svc/organization/");
            var rl = old.NavigateTo("../patient/@1/history");

            Assert.AreEqual("www.hl7.org", rl.Host);
            Assert.AreEqual("svc", rl.Service);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("1", rl.Id);
            Assert.AreEqual("history", rl.Operation);

            old = new ResourceLocation("http://hl7.org/fhir/patient/@1");
            rl = old.NavigateTo("@2");
            Assert.AreEqual("patient/@2", rl.OperationPath.ToString());

            rl = old.NavigateTo("../observation/@3");
            Assert.AreEqual("observation/@3", rl.OperationPath.ToString());

            old = new ResourceLocation("patient/@1");
            rl = old.NavigateTo("@2");
            Assert.AreEqual("patient/@2", rl.OperationPath.ToString());

            rl = old.NavigateTo("../observation/@3");
            Assert.AreEqual("observation/@3", rl.OperationPath.ToString());
        }


        [TestMethod]
        public void MakeResourceLocation()
        {
            var rl = ResourceLocation.Build(new Uri("http://hl7.org/svc"), "patient");
            Assert.AreEqual("hl7.org", rl.Host);
            Assert.AreEqual("svc", rl.Service);
            Assert.IsNull(rl.Operation);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("http://hl7.org/svc/patient/", rl.ToString());


            rl = ResourceLocation.Build(new Uri("http://hl7.org/svc"), "patient", "1");
            Assert.AreEqual("hl7.org", rl.Host);
            Assert.AreEqual("svc", rl.Service);
            Assert.IsNull(rl.Operation);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("1", rl.Id);
            Assert.AreEqual("http://hl7.org/svc/patient/@1", rl.ToString());


            rl = ResourceLocation.Build(new Uri("http://hl7.org/svc"), "patient", "1", "2");
            Assert.AreEqual("hl7.org", rl.Host);
            Assert.AreEqual("svc", rl.Service);
            Assert.AreEqual("patient", rl.Collection);
            Assert.AreEqual("1", rl.Id);
            Assert.IsNull(rl.Operation);
            Assert.AreEqual("2", rl.VersionId);
            Assert.AreEqual("http://hl7.org/svc/patient/@1/history/@2", rl.ToString());

            rl = ResourceLocation.Build("binary");
            Assert.AreEqual("binary", rl.Collection);

            rl = ResourceLocation.Build(new Uri("http://hl7.org/svc"), "patient", "1", "2");
            rl.Operation = ResourceLocation.RESTOPER_TAGS;
            Assert.AreEqual("http://hl7.org/svc/patient/@1/history/@2/tags", rl.ToString());

            rl = ResourceLocation.Build(new Uri("http://hl7.org/svc"), "patient");
            rl.Operation = ResourceLocation.RESTOPER_HISTORY;
            Assert.AreEqual("http://hl7.org/svc/patient/history", rl.ToString());

            rl = ResourceLocation.Build(new Uri("http://hl7.org/svc"), "patient", "1");
            rl.Operation = ResourceLocation.RESTOPER_VALIDATE;
            Assert.AreEqual("http://hl7.org/svc/patient/validate/@1", rl.ToString());
        }


        [TestMethod]
        public void LocationModification()
        {
            var rl = new ResourceLocation("http://hl7.org");
            Assert.AreEqual("http://hl7.org/", rl.ToString());

            rl.Service = "svc";
            Assert.AreEqual("http://hl7.org/svc/", rl.ToString());

            rl.Operation = "tags";
            Assert.AreEqual("http://hl7.org/svc/tags", rl.ToString());

            rl.Collection = "patient";
            Assert.AreEqual("http://hl7.org/svc/patient/tags", rl.ToString());

            rl.Id = "10";
            Assert.AreEqual("http://hl7.org/svc/patient/@10/tags", rl.ToString());

            rl.VersionId = "12";
            Assert.AreEqual("http://hl7.org/svc/patient/@10/history/@12/tags", rl.ToString());

            rl.Port = 2100;
            Assert.AreEqual("http://hl7.org:2100/svc/patient/@10/history/@12/tags", rl.ToString());

            rl.Query = "?_count=50";
            Assert.AreEqual("http://hl7.org:2100/svc/patient/@10/history/@12/tags?_count=50", rl.ToString());
        }


        [TestMethod]
        public void UseOfRelativePaths()
        {
            Assert.AreEqual("patient/@1", ResourceLocation.Build("patient", "1").OperationPath.ToString());
            Assert.AreEqual("patient/@1", new ResourceLocation("patient/@1").OperationPath.ToString());

            Assert.AreEqual("patient/@1/history/@4", ResourceLocation.Build("patient","1","4").OperationPath.ToString());
            Assert.AreEqual("patient/@1/history/@4", new ResourceLocation("patient/@1/history/@4").OperationPath.ToString());
            
            var rl = ResourceLocation.Build("patient", "1");
            rl.Operation = "x-history";
            Assert.AreEqual("patient/@1/x-history", rl.OperationPath.ToString());

            rl = new ResourceLocation("patient/@1/history/@4");

            Assert.AreEqual("1", rl.Id);
            Assert.AreEqual("4", rl.VersionId);
            Assert.AreEqual("patient", rl.Collection);
            Assert.IsNull(rl.Operation);
        }

        [TestMethod]
        public void ParamManipulation()
        {
            var rl = new ResourceLocation("patient/search?name=Kramer&name=Moreau&oauth=XXX");

            rl.SetParam("newParamA", "1");
            rl.SetParam("newParamB", "2");
            Assert.IsTrue(rl.ToString().EndsWith("oauth=XXX&newParamA=1&newParamB=2"));

            rl.SetParam("newParamA", "3");
            rl.ClearParam("newParamB");
            Assert.IsTrue(rl.ToString().EndsWith("oauth=XXX&newParamA=3"));

            rl.AddParam("newParamA", "4");
            Assert.IsTrue(rl.ToString().EndsWith("oauth=XXX&newParamA=3&newParamA=4"));

            rl.AddParam("newParamB", "5");
            Assert.IsTrue(rl.ToString().EndsWith("oauth=XXX&newParamA=3&newParamA=4&newParamB=5"));

            Assert.AreEqual("patient/search?name=Kramer&name=Moreau&oauth=XXX&newParamA=3&newParamA=4&newParamB=5",
                    rl.OperationPath.ToString());

            rl = new ResourceLocation("patient/search");
            rl.SetParam("firstParam", "1");
            rl.SetParam("sndParam", "2");
            rl.ClearParam("sndParam");
            Assert.AreEqual("patient/search?firstParam=1", rl.OperationPath.ToString());
            
            rl.ClearParam("firstParam");
            Assert.AreEqual("patient/search", rl.OperationPath.ToString());
            
            rl.SetParam("firstParam", "1");
            rl.SetParam("sndParam", "2");
            rl.ClearParams();
            Assert.AreEqual("patient/search", rl.OperationPath.ToString());
        }
    }
}
