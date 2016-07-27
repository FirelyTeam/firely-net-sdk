/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

extern alias dstu2;

using dstu2::Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.FluentPath;


using boolean = System.Boolean;
using DecimalType = dstu2::Hl7.Fhir.Model.FhirDecimal; // System.Decimal;
using UriType = dstu2::Hl7.Fhir.Model.FhirUri;
using dstu2::Hl7.Fhir.Serialization;
using System.IO;
using Hl7.Fhir.Tests.FhirPath;
using System.Xml;
using Hl7.Fhir.Support;
using Hl7.Fhir.FluentPath.Binding;

static class ConverterExtensions
{
    public static ResourceReference getSubject(this Order me)
    {
        return me.Subject;
    }

    public static void setValue(this Quantity me, double? value)
    {
        if (value.HasValue)
            me.Value = (decimal)value.Value;
        else
            me.Value = null;
    }
    public static void setUnit(this Quantity me, string value)
    {
        me.Unit = value;
    }
    public static void setCode(this Quantity me, string value)
    {
        me.Code = value;
    }
    public static void setSystem(this Quantity me, string value)
    {
        me.System = value;
    }
    public static void setValueSet(this ElementDefinition.BindingComponent me, Element value)
    {
        me.ValueSet = value;
    }
    public static Element getValueSet(this ElementDefinition.BindingComponent me)
    {
        return me.ValueSet;
    }

    public static Range setLow(this Range me, SimpleQuantity value)
    {
        me.Low = value;
        return me;
    }
    public static Range setHigh(this Range me, SimpleQuantity value)
    {
        me.High = value;
        return me;
    }

    public static RiskAssessment.PredictionComponent addPrediction(this RiskAssessment me)
    {
        var item = new RiskAssessment.PredictionComponent();
        me.Prediction.Add(item);
        return item;
    }
    public static List<RiskAssessment.PredictionComponent> getPrediction(this RiskAssessment me)
    {
        return me.Prediction;
    }

    public static Element getProbability(this RiskAssessment.PredictionComponent me)
    {
        return me.Probability;
    }
    public static RiskAssessment.PredictionComponent setProbability(this RiskAssessment.PredictionComponent me, Element value)
    {
        me.Probability = value;
        return me;
    }

}


[TestClass]
public class FluentPathTests
{
    private void test(Resource resource, String expression, int count)
    {
        test(resource, expression, count, new String[] { });
    }

    private void test(Resource resource, String expression, int count, String types)
    {
        test(resource, expression, count, new String[] { types });
    }
    
    private void test(Resource resource, String expression, int count, String[] types)
    {
        var tpXml = FhirSerializer.SerializeToXml(resource);
        var npoco = new ModelNavigator(resource);
 //       FhirPathEvaluatorTest.Render(npoco);

        var outcome = PathExpression.Compile(expression).Select(FhirValueList.Create(npoco));
        Assert.AreEqual(count, outcome.Count());

        if (types != null && types.Count() > 0)
        {
            string msg = String.Join(", ", types);
            foreach (IValueProvider b in outcome)
            {
                Assert.IsTrue(types.Contains(b.Value.GetType().Name.ToLower()), String.Format("Object type {0} not ok from {1}", b.Value.GetType().Name, msg));
            }
        }
    }

    // @SuppressWarnings("deprecation")
    private void testBoolean(Resource resource, String expression, boolean value)
    {
        var nav = new ModelNavigator(resource);
        Assert.IsTrue(PathExpression.IsBoolean(expression, value, FhirValueList.Create(nav)));
    }

    // @SuppressWarnings("deprecation")
    private void testBoolean(Resource resource, Base focus, String focusType, String expression, boolean value)
    {
        var context = BaseEvaluationContext.Root(resource==null ? ModelNavigator.CreateInput(focus) : ModelNavigator.CreateInput(resource));
        context.SetThis(ModelNavigator.CreateInput(focus));

        Assert.IsTrue(PathExpression.IsBoolean(expression, value, context));
    }


    enum ErrorType
    {
        Syntax,
        Semantics
    }

    private void testInvalid(Resource resource, ErrorType type, String expression)
    {
        try
        {
            PathExpression.Select(expression, FhirValueList.Create(new ModelNavigator(resource)));
            Assert.Fail();
        }
        catch(FormatException)
        {
            if (type != ErrorType.Syntax) Assert.Fail();
        }
        catch (InvalidCastException)
        {
            if (type != ErrorType.Semantics) Assert.Fail();
        }
        catch (InvalidOperationException)
        {
            if (type != ErrorType.Semantics) Assert.Fail();
        }
    }


    Dictionary<string, DomainResource> _cache = new Dictionary<string, DomainResource>();

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void TestPublishedTests()
    {
        var files = Directory.EnumerateFiles(@"C:\git\fluentpath\tests\dstu2", "*.xml", SearchOption.TopDirectoryOnly);

        foreach (var file in files)
        {
            Console.WriteLine("==== Running tests from file '{0}' ====".FormatWith(file));
            runTests(file);
            Console.WriteLine(Environment.NewLine);
        }
    }

    private void runTests(string pathToTest)
    {
        // Read the test file, then execute each of them
        XmlDocument doc = new XmlDocument();

        doc.Load(pathToTest);

        int numFailed = 0;
        int totalTests = 0;

        foreach (XmlElement item in doc.SelectNodes("//test"))
        {
            string groupName = (item.ParentNode as XmlElement).GetAttribute("name");
            string name = item.GetAttribute("name");
            string inputfile = item.GetAttribute("inputfile");
            string mode = item.GetAttribute("mode");
            string expression = item.SelectSingleNode("expression").InnerText;

            if (mode == "strict") continue; // don't do 'strict' tests yet
       
            var output = item.SelectNodes("output");

            // Now perform this unit test
            DomainResource resource = null;
            if (!_cache.ContainsKey(inputfile))
            {
                string basepath = @"C:\git\fluentpath\tests\dstu2\input\";
                _cache.Add(inputfile, (DomainResource)(new FhirXmlParser().Parse<DomainResource>(File.ReadAllText(basepath + inputfile))));
            }
            resource = _cache[inputfile];

            try
            {
                totalTests += 1;
                runTestItem(item, expression, output, resource);
            }
            catch(AssertFailedException afe)
            {
                Console.WriteLine("FAIL: {0} - {1}: {2}", groupName, name, expression);
                Console.WriteLine(afe.Message);
                numFailed += 1;   
            }
            catch (InvalidOperationException ioe)
            {
                Console.WriteLine("FAIL: {0} - {1}: {2}", groupName, name, expression);
                Console.WriteLine(ioe.Message);
                numFailed += 1;
            }
            catch (FormatException fe)
            {
                Console.WriteLine("FAIL: {0} - {1}: {2}", groupName, name, expression);
                Console.WriteLine(fe.Message);
                numFailed += 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("FAIL: {0} - {1}: {2}", groupName, name, expression);
                throw e;
            }
        }

        Console.WriteLine("Ran {0} tests in total, {1} succeeded, {2} failed.".FormatWith(totalTests, totalTests - numFailed, numFailed));

        if(numFailed > 0)
        {
            Assert.Fail("There were {0} unsuccessful tests (out of a total of {1})".FormatWith(numFailed, totalTests));
        }
    }

    private void runTestItem(XmlElement item, string expression, XmlNodeList output, dstu2::Hl7.Fhir.Model.DomainResource resource)
    {
        if (output.Count == 1 && (output[0] as XmlElement).GetAttribute("type") == "boolean"
            && ((output[0] as XmlElement).InnerText == "true" || (output[0] as XmlElement).InnerText == "false"))
        {
            if ((output[0] as XmlElement).InnerText == "true")
                testBoolean(resource, expression, true);
            else
                testBoolean(resource, expression, false);
        }
        else if (!String.IsNullOrEmpty((item.SelectSingleNode("expression") as XmlElement).GetAttribute("invalid")))
        {
            var errorTypeS = (item.SelectSingleNode("expression") as XmlElement).GetAttribute("invalid");
            ErrorType errorType;

            if (errorTypeS == "syntax")
                errorType = ErrorType.Syntax;
            else if (errorTypeS == "semantic")
                errorType = ErrorType.Semantics;
            else
                throw new ArgumentException("unknown error type");

            testInvalid(resource, errorType, expression);
        }
        else
        {
            // Still need to check the types (and values)
            test(resource, expression, output.Count);
        }
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testTyping()
    {
        ElementDefinition ed = new ElementDefinition();
        ed.Binding = new ElementDefinition.BindingComponent();
        ed.Binding.setValueSet(new UriType("http://test.org"));
        testBoolean(null, ed.Binding.getValueSet(), "ElementDefinition.binding.valueSetUri", "startsWith('http:') or startsWith('https') or startsWith('urn:')", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testDecimalRA()
    {
        RiskAssessment r = new RiskAssessment();
        SimpleQuantity sq = new SimpleQuantity();
        sq.setValue(0.2);
        sq.setUnit("%");
        sq.setCode("%");
        sq.setSystem("http://unitsofmeasure.org");
        SimpleQuantity sq1 = new SimpleQuantity();
        sq1.setValue(0.4);
        sq1.setUnit("%");
        sq1.setCode("%");
        sq1.setSystem("http://unitsofmeasure.org");
        r.addPrediction().setProbability(new Range().setLow(sq).setHigh(sq1));
        testBoolean(r, r.getPrediction()[0].getProbability(), "RiskAssessment.prediction.probabilityRange",
            "(low.empty() or ((low.code = '%') and (low.system = %ucum))) and (high.empty() or ((high.code = '%') and (high.system = %ucum)))", true);
        testBoolean(r, r.getPrediction()[0], "RiskAssessment.prediction", "probability is decimal implies probability.as(decimal) <= 100", true);
        r.getPrediction()[0].setProbability(new DecimalType(80));
        testBoolean(r, r.getPrediction()[0], "RiskAssessment.prediction", "probability.as(decimal) <= 100", true);
    }

    /*  [TestMethod, TestCategory("FhirPathFromSpec")]
      public void testQuestionnaire()  {
        Questionnaire q = (Questionnaire)FhirParser.ParseResourceFromJson(File.ReadAllText("C:/work/org.hl7.fhir/build - DSTU2.0/publish/questionnaire-example-gcs.json"));
        for (QuestionnaireItemComponent qi : q.getItem()) {
          testQItem(qi);
        }
      }

      private void testQItem(QuestionnaireItemComponent qi)  {
        testBoolean(null, qi, "Questionnaire.item", "(type = 'choice' or type = 'open-choice') or (options.empty() and option.empty())", true);
      }
    */

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testExtensionDefinitions()
    {
        Bundle b = (Bundle)FhirParser.ParseResourceFromXml(File.ReadAllText("TestData\\extension-definitions.xml"));
        foreach (Bundle.EntryComponent be in b.Entry)
        {
            testStructureDefinition((StructureDefinition)be.Resource);
        }
    }

    private void testStructureDefinition(StructureDefinition sd)
    {
        testBoolean(sd, sd, "StructureDefinition", "snapshot.element.tail().all(path.startsWith(%resource.snapshot.element.first().path&'.')) and differential.element.tail().all(path.startsWith(%resource.differential.element.first().path&'.'))", true);
    }

}
