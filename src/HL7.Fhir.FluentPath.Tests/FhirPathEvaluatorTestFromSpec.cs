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
using Hl7.Fhir.FluentPath.InstanceTree;


using boolean = System.Boolean;
using DecimalType = dstu2::Hl7.Fhir.Model.FhirDecimal; // System.Decimal;
using UriType = dstu2::Hl7.Fhir.Model.FhirUri;
using dstu2::Hl7.Fhir.Serialization;
using System.IO;
using Hl7.Fhir.Tests.FhirPath;

static class ConverterExtensions
{
    public static ResourceReference getSubject(this Order me)
    {
        return me.Subject;
    }

    public static List<Parameters.ParameterComponent> getParameter(this Parameters me)
    {
        return me.Parameter;
    }

    public static ResourceReference getManagingOrganization(this Patient me)
    {
        return me.ManagingOrganization;
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

// Fix the static getters for the sample objects (removing clash of property and function(
//    insert an _ in front of the fieldname
// replace @SuppressWarnings("deprecation")
// remove throws FileNotFoundException, IOException, FHIRException
// remove throws FileNotFoundException, FHIRFormatError, IOException, FHIRException 
// remove throws FHIRException, IOException 
// rename the properties and the 
// replace .get(0) with [0]
// replace  @Test with  [TestMethod, TestCategory("FhirPath")]
// replace 
//        FhirParser.ParseResourceFromJson(File.ReadAllText 
//   with
//        FhirParser.ParseResourceFromJson(File.ReadAllText
// fix function signature 
//    private void test(Resource resource, String expression, int count, String types = null)
// fix the looping iterators

[TestClass]
public class FluentPathTests
{

    static private Patient _patient;
    static private Appointment _appointment;
    static private Observation _observation;
    static private ValueSet _valueset;
    static private Questionnaire _questionnaire;
    private Parameters _parameters;

    private Patient patient()
    {
        if (_patient == null)
            _patient = (Patient)FhirParser.ParseResourceFromXml(File.ReadAllText("TestData\\patient-example.xml"));
        return _patient;
    }

    private Appointment appointment()
    {
        if (_appointment == null)
            _appointment = (Appointment)FhirParser.ParseResourceFromXml(File.ReadAllText("TestData\\appointment-example-request.xml"));
        return _appointment;
    }

    private Questionnaire questionnaire()
    {
        if (_questionnaire == null)
            _questionnaire = (Questionnaire)FhirParser.ParseResourceFromXml(File.ReadAllText("TestData\\questionnaire-example.xml"));
        return _questionnaire;
    }

    private ValueSet valueset()
    {
        if (_valueset == null)
            _valueset = (ValueSet)FhirParser.ParseResourceFromXml(File.ReadAllText("TetData\\valueset-example-expansion.xml"));
        return _valueset;
    }

    private Observation observation()
    {
        if (_observation == null)
            _observation = (Observation)FhirParser.ParseResourceFromXml(File.ReadAllText("TestData\\observation-example.xml"));
        return _observation;
    }

    private Parameters parameters()
    {
        if (_parameters == null)
            _parameters = (Parameters)FhirParser.ParseResourceFromJson(File.ReadAllText("TestData\\example-gao-request-parameters-CT-head.json"));
        return _parameters;
    }

    private void test(Resource resource, String expression, long count)
    {
        test(resource, expression, count, new Type[] { });
    }

    private void test(Resource resource, String expression, long count, Type types)
    {
        test(resource, expression, count, new Type[] { types });
    }

    // @SuppressWarnings("deprecation")
    private void test(Resource resource, String expression, long count, Type[] types)
    {
        var tpXml = FhirSerializer.SerializeToXml(resource);
        var npoco = new ModelNavigator(resource);

        var outcome = PathExpression.Select(expression, FhirValueList.Create(npoco));
        Assert.AreEqual(count, outcome.Count());

        if (types != null && types.Count() > 0)
        {
            string msg = String.Join(",", types.Select(t => t.Name));
            foreach (IValueProvider b in outcome)
            {
                Assert.IsTrue(types.Contains(b.Value.GetType()), String.Format("Object type {0} not ok from {1}", b.Value.GetType().Name, msg));
            }
        }
    }

    // @SuppressWarnings("deprecation")
    private void testBoolean(Resource resource, String expression, boolean value)
    {
        Console.WriteLine("======================\r\nTesting for isTrue({0})\r\n----------------------", expression);
        // var tpXml = FhirSerializer.SerializeToXml(resource);
        // var tree = TreeConstructor.FromXml(tpXml);
        Assert.AreEqual(value, PathExpression.IsTrue(expression, FhirValueList.Create(new ModelNavigator(resource))));

        //        if (TestingUtilities.context == null)
        //    	TestingUtilities.context = SimpleWorkerContext.fromPack("C:\\work\\org.hl7.fhir\\build\\publish\\validation-min.xml.zip");
        //    FHIRPathEngine fp = new FHIRPathEngine(TestingUtilities.context);

        //ExpressionNode node = fp.parse(expression);
        //fp.check(null, null, resource.getResourceType().toString(), node);
        //    List<Base> outcome = fp.evaluate(null, null, resource, node);
        //    if (fp.hasLog())
        //      System.out.println(fp.takeLog());

        //    Assert.assertTrue("Wrong answer", fp.convertToBoolean(outcome) == value);
    }

    // @SuppressWarnings("deprecation")
    private void testBoolean(Resource resource, Base focus, String focusType, String expression, boolean value)
    {
        // var tpXml = FhirSerializer.SerializeToXml(resource);
        // var tree = TreeConstructor.FromXml(tpXml);
        Assert.AreEqual(value, PathExpression.IsTrue(expression, FhirValueList.Create(new ModelNavigator(resource))));
        //       Need the focus type to be handled here in the test
        //        if (TestingUtilities.context == null)
        //    	TestingUtilities.context = SimpleWorkerContext.fromPack("C:\\work\\org.hl7.fhir\\build\\publish\\validation-min.xml.zip");
        //    FHIRPathEngine fp = new FHIRPathEngine(TestingUtilities.context);

        //ExpressionNode node = fp.parse(expression);
        //fp.check(null, resource == null ? null : resource.getResourceType().toString(), focusType, node);
        //    List<Base> outcome = fp.evaluate(null, resource, focus, node);
        //    if (fp.hasLog())
        //      System.out.println(fp.takeLog());

        //    Assert.assertTrue("Wrong answer", fp.convertToBoolean(outcome) == value);
    }

    private void testWrong(Resource resource, String expression)
    {
        // var tpXml = FhirSerializer.SerializeToXml(resource);
        // var tree = TreeConstructor.FromXml(tpXml);
        Assert.IsFalse(PathExpression.IsTrue(expression, FhirValueList.Create(new ModelNavigator(resource))));

        //    if (TestingUtilities.context == null)
        //    	TestingUtilities.context = SimpleWorkerContext.fromPack("C:\\work\\org.hl7.fhir\\build\\publish\\validation-min.xml.zip");
        //    FHIRPathEngine fp = new FHIRPathEngine(TestingUtilities.context);

        //    try {
        //      ExpressionNode node = fp.parse(expression);
        //fp.check(null, null, resource.getResourceType().toString(), node);
        //      fp.evaluate(null, null, resource, node);
        //      if (fp.hasLog())
        //        System.out.println(fp.takeLog());
        //      Assert.assertTrue("Fail expected", false);
        //    } catch (PathEngineException e) {
        //      // ok  
        //    }
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testSimple()
    {
        test(patient(), "name.given", 3, typeof(string));
    }

    internal class TestingUtilities
    {
        internal static object context;
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testSimpleNone()
    {
        test(patient(), "name.period", 0);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testSimpleDoubleQuotes()
    {
        test(patient(), "name.\"given\"", 3, typeof(string));
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testSimpleFail()
    {
        testWrong(patient(), "name.given1");
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testSimpleWithContext()
    {
        test(patient(), "Patient.name.given", 3, typeof(string));
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testSimpleWithWrongContext()
    {
        testWrong(patient(), "Encounter.name.given");
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testPolymorphismA()
    {
        test(observation(), "Observation.value.unit", 1, typeof(string));
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testPolymorphismB()
    {
        testWrong(observation(), "Observation.valueQuantity.unit");
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testPolymorphismIsA()
    {
        testBoolean(observation(), "Observation.value.is(Quantity)", true);
        testBoolean(observation(), "Observation.value is Quantity", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testPolymorphismIsB()
    {
        testBoolean(observation(), "Observation.value.is(Period).not()", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testPolymorphismAsA()
    {
        testBoolean(observation(), "Observation.value.as(Quantity).unit", true);
        testBoolean(observation(), "(Observation.value as Quantity).unit", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testPolymorphismAsB()
    {
        testWrong(observation(), "(Observation.value as Period).unit");
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testPolymorphismAsC()
    {
        test(observation(), "Observation.value.as(Period).start", 0);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testDollarThis1()
    {
        test(patient(), "Patient.name.given.where(substring($this.length()-3) = 'out')", 0);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testDollarThis2()
    {
        test(patient(), "Patient.name.given.where(substring($this.length()-3) = 'ter')", 1, typeof(string));
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testDollarOrderAllowed()
    {
        test(patient(), "Patient.name.skip(1).given", 1, typeof(string));
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testDollarOrderAllowedA()
    {
        test(patient(), "Patient.name.skip(3).given", 0);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testDollarOrderNotAllowed()
    {
        testWrong(patient(), "Patient.children().skip(1)");
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testLiteralTrue()
    {
        testBoolean(patient(), "Patient.name.exists() = true", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testLiteralFalse()
    {
        testBoolean(patient(), "Patient.name.empty() = false", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testLiteralString()
    {
        testBoolean(patient(), "Patient.name.given.first() = 'Peter'", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testLiteralInteger()
    {
        testBoolean(patient(), "-3 != 3", true);
        testBoolean(patient(), "Patient.name.given.count() = 3", true);
        testBoolean(patient(), "Patient.name.given.count() > -3", true);
        testBoolean(patient(), "Patient.name.given.count() != 0", true);
        testBoolean(patient(), "1 < 2", true);
        testBoolean(patient(), "1 < -2", false);
        testBoolean(patient(), "+1 < +2", true);
        testBoolean(patient(), "-1 < 2", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testLiteralDecimal()
    {
        testBoolean(observation(), "Observation.value.value > 180.0", true);
        testBoolean(observation(), "Observation.value.value > 0.0", true);
        testBoolean(observation(), "Observation.value.value > 0", true);
        testBoolean(observation(), "Observation.value.value < 190", true);
        testBoolean(observation(), "Observation.value.value < 'test'", false);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testLiteralDate()
    {
        testBoolean(patient(), "Patient.birthDate = @1974-12-25", true);
        testBoolean(patient(), "Patient.birthDate != @1974-12-25T12:34:00", true);
        testBoolean(patient(), "Patient.birthDate != @1974-12-25T12:34:00-10:00", true);
        testBoolean(patient(), "Patient.birthDate != @1974-12-25T12:34:00+10:00", true);
        testBoolean(patient(), "Patient.birthDate != @1974-12-25T12:34:00Z", true);
        testBoolean(patient(), "Patient.birthDate != @T12:14:15", true);
        testBoolean(patient(), "Patient.birthDate != @T12:14", true);
    }


    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testLiteralUnicode()
    {
        testBoolean(patient(), "Patient.name.given.first() = 'P\\u0065ter'", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testLiteralEmptyCollection()
    {
        testBoolean(patient(), "Patient.name.given != {}", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testExpressions()
    {
        testBoolean(patient(), "Patient.name.select(given | family).distinct()", true);
        testBoolean(patient(), "Patient.name.given.count() = 1 + 2", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testEmpty()
    {
        testBoolean(patient(), "Patient.name.empty().not()", true);
        testBoolean(patient(), "Patient.link.empty()", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testNot()
    {
        testBoolean(patient(), "true.not() = false", true);
        testBoolean(patient(), "false.not() = true", true);
        testBoolean(patient(), "(0).not() = false", true);
        testBoolean(patient(), "(1).not() = false", true);
        testBoolean(patient(), "(1|2).not() = false", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testAll()
    {
        // all() has been changed to take a condition, as a shorthand of the select().all() 
        //testBoolean(patient(), "Patient.name.select(given.exists()).all()", true);
        //testBoolean(patient(), "Patient.name.select(family.exists()).all()", false);
        testBoolean(patient(), "Patient.name.all(given.exists())", true);
        testBoolean(patient(), "Patient.name.all(family.exists())", false);    
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testSubSetOf()
    {
        testBoolean(patient(), "Patient.name.first().subsetOf($this.name)", true);
        testBoolean(patient(), "Patient.name.subsetOf($this.name.first()).not()", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testSuperSetOf()
    {
        testBoolean(patient(), "Patient.name.first().supersetOf($this.name).not()", true);
        testBoolean(patient(), "Patient.name.supersetOf($this.name.first())", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testDistinct()
    {
        testBoolean(patient(), "(1 | 2 | 3).isDistinct()", true);
        testBoolean(questionnaire(), "Questionnaire.descendents().linkId.isDistinct()", true);
        testBoolean(questionnaire(), "Questionnaire.descendents().linkId.select(substring(0,1)).isDistinct().not()", true);
        test(patient(), "(1 | 2 | 3).distinct()", 3, typeof(long));
        test(questionnaire(), "Questionnaire.descendents().linkId.distinct()", 9, typeof(string));
        test(questionnaire(), "Questionnaire.descendents().linkId.select(substring(0,1)).distinct()", 2, typeof(string));
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testCount()
    {
        test(patient(), "Patient.name.count()", 1, typeof(long));
        testBoolean(patient(), "Patient.name.count() = 2", true);
        test(patient(), "Patient.name.first().count()", 1L, typeof(long));
        testBoolean(patient(), "Patient.name.first().count() = 1", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testWhere()
    {
        testBoolean(patient(), "Patient.name.count() = 2", true);
        testBoolean(patient(), "Patient.name.where(given = 'Jim').count() = 1", true);
        testBoolean(patient(), "Patient.name.where(given = 'X').count() = 0", true);
        testBoolean(patient(), "Patient.name.where($this.given = 'Jim').count() = 1", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testSelect()
    {
        testBoolean(patient(), "Patient.name.select(given) = 'Peter' | 'James' | 'Jim'", true);
        testBoolean(patient(), "Patient.name.select(given | family) = 'Peter' | 'James' | 'Chalmers' | 'Jim'", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testRepeat()
    {
        testBoolean(valueset(), "ValueSet.expansion.repeat(contains).count() = 10", true);
        testBoolean(questionnaire(), "Questionnaire.repeat(item).concept.count() = 10", true);
        testBoolean(questionnaire(), "Questionnaire.descendents().concept.count() = 10", true);
        testBoolean(questionnaire(), "Questionnaire.children().concept.count() = 2", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testIndexer()
    {
        testBoolean(patient(), "Patient.name[0].given = 'Peter' | 'James'", true);
        testBoolean(patient(), "Patient.name[1].given = 'Jim'", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testSingle()
    {
        testBoolean(patient(), "Patient.name.first().single().exists()", true);
        testWrong(patient(), "Patient.name.single().exists()");
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testFirstLast()
    {
        testBoolean(patient(), "Patient.name.first().given = 'Peter' | 'James'", true);
        testBoolean(patient(), "Patient.name.last().given = 'Jim'", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testTail()
    {
        testBoolean(patient(), "(0 | 1 | 2).tail() = 1 | 2", true);
        testBoolean(patient(), "Patient.name.tail().given = 'Jim'", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testSkip()
    {
        testBoolean(patient(), "(0 | 1 | 2).skip(1) = 1 | 2", true);
        testBoolean(patient(), "(0 | 1 | 2).skip(2) = 2", true);
        testBoolean(patient(), "Patient.name.skip(1).given = 'Jim'", true);
        testBoolean(patient(), "Patient.name.skip(2).given.exists() = false", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testTake()
    {
        testBoolean(patient(), "(0 | 1 | 2).take(1) = 0", true);
        testBoolean(patient(), "(0 | 1 | 2).take(2) = 0 | 1", true);
        testBoolean(patient(), "Patient.name.take(1).given = 'Peter' | 'James'", true);
        testBoolean(patient(), "Patient.name.take(2).given = 'Peter' | 'James' | 'Jim'", true);
        testBoolean(patient(), "Patient.name.take(3).given = 'Peter' | 'James' | 'Jim'", true);
        testBoolean(patient(), "Patient.name.take(0).given.exists() = false", true);
    }


    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testIif()
    {
        testBoolean(patient(), "iif(Patient.name.exists(), 'named', 'unnamed') = 'named'", true);
        testBoolean(patient(), "iif(Patient.name.empty(), 'unnamed', 'named') = 'named'", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testToInteger()
    {
        testBoolean(patient(), "'1'.toInteger() = 1", true);
        testBoolean(patient(), "'-1'.toInteger() = -1", true);
        testBoolean(patient(), "'0'.toInteger() = 0", true);
        testBoolean(patient(), "'0.0'.toInteger().empty()", true);
        testBoolean(patient(), "'st'.toInteger().empty()", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testToDecimal()
    {
        testBoolean(patient(), "'1'.toDecimal() = 1", true);
        testBoolean(patient(), "'-1'.toInteger() = -1", true);
        testBoolean(patient(), "'0'.toDecimal() = 0", true);
        testBoolean(patient(), "'0.0'.toDecimal() = 0.0", true);
        testBoolean(patient(), "'st'.toDecimal().empty()", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testToString()
    {
        testBoolean(patient(), "1.toString() = '1'", true);
        testBoolean(patient(), "'-1'.toInteger() = -1", true);
        testBoolean(patient(), "0.toString() = '0'", true);
        testBoolean(patient(), "0.0.toString() = '0.0'", true);
        testBoolean(patient(), "@2014-12-14.toString() = '2014-12-14'", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testSubstring()
    {
        testBoolean(patient(), "'12345'.substring(2) = '345'", true);
        testBoolean(patient(), "'12345'.substring(2,1) = '3'", true);
        testBoolean(patient(), "'12345'.substring(2,5) = '345'", true);
        testBoolean(patient(), "'12345'.substring(25).empty()", true);
        testBoolean(patient(), "'12345'.substring(-1).empty()", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testStartsWith()
    {
        testBoolean(patient(), "'12345'.startsWith('2') = false", true);
        testBoolean(patient(), "'12345'.startsWith('1') = true", true);
        testBoolean(patient(), "'12345'.startsWith('12') = true", true);
        testBoolean(patient(), "'12345'.startsWith('13') = false", true);
        testBoolean(patient(), "'12345'.startsWith('12345') = true", true);
        testBoolean(patient(), "'12345'.startsWith('123456') = false", true);
        testBoolean(patient(), "'12345'.startsWith('') = false", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testEndsWith()
    {
        testBoolean(patient(), "'12345'.endsWith('2') = false", true);
        testBoolean(patient(), "'12345'.endsWith('5') = true", true);
        testBoolean(patient(), "'12345'.endsWith('45') = true", true);
        testBoolean(patient(), "'12345'.endsWith('35') = false", true);
        testBoolean(patient(), "'12345'.endsWith('12345') = true", true);
        testBoolean(patient(), "'12345'.endsWith('012345') = false", true);
        testBoolean(patient(), "'12345'.endsWith('') = false", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testContainsString()
    {
        testBoolean(patient(), "'12345'.contains('6') = false", true);
        testBoolean(patient(), "'12345'.contains('5') = true", true);
        testBoolean(patient(), "'12345'.contains('45') = true", true);
        testBoolean(patient(), "'12345'.contains('35') = false", true);
        testBoolean(patient(), "'12345'.contains('12345') = true", true);
        testBoolean(patient(), "'12345'.contains('012345') = false", true);

        //Error in test: contains('') will always return true
        //testBoolean(patient(), "'12345'.contains('') = false", true);
        testBoolean(patient(), "'12345'.contains('')", true);
    }


    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testLength()
    {
        testBoolean(patient(), "'123456'.length() = 6", true);
        testBoolean(patient(), "'12345'.length() = 5", true);
        testBoolean(patient(), "'123'.length() = 3", true);
        testBoolean(patient(), "'1'.length() = 1", true);
        testBoolean(patient(), "''.length() = 0", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testTrace()
    {
        testBoolean(patient(), "name.given.trace('test').count() = 3", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testToday()
    {
        testBoolean(patient(), "Patient.birthDate < today()", true);
        testBoolean(patient(), "today().toString().length() = 10", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testNow()
    {
        testBoolean(patient(), "Patient.birthDate < now()", true);
        testBoolean(patient(), "now().toString().length() > 10", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testEquality()
    {
        testBoolean(patient(), "1 = 1", true);
        testBoolean(patient(), "{} = {}", true);
        testBoolean(patient(), "1 = 2", false);
        testBoolean(patient(), "'a' = 'a'", true);
        testBoolean(patient(), "'a' = 'A'", false);
        testBoolean(patient(), "'a' = 'b'", false);
        testBoolean(patient(), "1.1 = 1.1", true);
        testBoolean(patient(), "1.1 = 1.2", false);
        testBoolean(patient(), "1.10 = 1.1", false);
        testBoolean(patient(), "0 = 0", true);
        testBoolean(patient(), "0.0 = 0", false);
        testBoolean(patient(), "@2012-04-15 = @2012-04-15", true);
        testBoolean(patient(), "@2012-04-15 = @2012-04-16", false);
        testBoolean(patient(), "@2012-04-15 = @2012-04-15T10:00:00", false);
        testBoolean(patient(), "name = name", true);
        testBoolean(patient(), "name = name.first() | name.last()", true);
        testBoolean(patient(), "name = name.last() | name.first()", false);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testNEquality()
    {
        testBoolean(patient(), "1 != 1", false);
        testBoolean(patient(), "{} != {}", false);
        testBoolean(patient(), "1 != 2", true);
        testBoolean(patient(), "'a' != 'a'", false);
        testBoolean(patient(), "'a' != 'b'", true);
        testBoolean(patient(), "1.1 != 1.1", false);
        testBoolean(patient(), "1.1 != 1.2", true);
        testBoolean(patient(), "1.10 != 1.1", true);
        testBoolean(patient(), "0 != 0", false);
        testBoolean(patient(), "0.0 != 0", true);
        testBoolean(patient(), "@2012-04-15 != @2012-04-15", false);
        testBoolean(patient(), "@2012-04-15 != @2012-04-16", true);
        testBoolean(patient(), "@2012-04-15 != @2012-04-15T10:00:00", true);
        testBoolean(patient(), "name != name", false);
        testBoolean(patient(), "name != name.first() | name.last()", false);
        testBoolean(patient(), "name != name.last() | name.first()", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testEquivalent()
    {
        testBoolean(patient(), "1 ~ 1", true);
        testBoolean(patient(), "{} ~ {}", true);
        testBoolean(patient(), "1 ~ 2", false);
        testBoolean(patient(), "'a' ~ 'a'", true);
        testBoolean(patient(), "'a' ~ 'A'", true);
        testBoolean(patient(), "'a' ~ 'b'", false);
        testBoolean(patient(), "1.1 ~ 1.1", true);
        testBoolean(patient(), "1.1 ~ 1.2", false);
        testBoolean(patient(), "1.10 ~ 1.1", true);
        testBoolean(patient(), "0 ~ 0", true);
        testBoolean(patient(), "0.0 ~ 0", true);
        testBoolean(patient(), "@2012-04-15 ~ @2012-04-15", true);
        testBoolean(patient(), "@2012-04-15 ~ @2012-04-16", false);
        testBoolean(patient(), "@2012-04-15 ~ @2012-04-15T10:00:00", true);
        //    testBoolean(patient(), "name ~ name", true);
        testBoolean(patient(), "name.given ~ name.first().given | name.last().given", true);
        testBoolean(patient(), "name.given ~ name.last().given | name.first().given", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testNotEquivalent()
    {
        testBoolean(patient(), "1 !~ 1", false);
        testBoolean(patient(), "{} !~ {}", false);
        testBoolean(patient(), "1 !~ 2", true);
        testBoolean(patient(), "'a' !~ 'a'", false);
        testBoolean(patient(), "'a' !~ 'A'", false);
        testBoolean(patient(), "'a' !~ 'b'", true);
        testBoolean(patient(), "1.1 !~ 1.1", false);
        testBoolean(patient(), "1.1 !~ 1.2", true);
        testBoolean(patient(), "1.10 !~ 1.1", false);
        testBoolean(patient(), "0 !~ 0", false);
        testBoolean(patient(), "0.0 !~ 0", false);
        testBoolean(patient(), "@2012-04-15 !~ @2012-04-15", false);
        testBoolean(patient(), "@2012-04-15 !~ @2012-04-16", true);
        testBoolean(patient(), "@2012-04-15 !~ @2012-04-15T10:00:00", false);
        //    testBoolean(patient(), "name !~ name", true);
        testBoolean(patient(), "name.given !~ name.first().given | name.last().given", false);
        testBoolean(patient(), "name.given !~ name.last().given | name.first().given", false);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testLessThan()
    {
        testBoolean(patient(), "1 < 2", true);
        testBoolean(patient(), "1.0 < 1.2", true);
        testBoolean(patient(), "'a' < 'b'", true);
        testBoolean(patient(), "'A' < 'a'", true);
        testBoolean(patient(), "@2014-12-12 < @2014-12-13", true);
        testBoolean(patient(), "@2014-12-13T12:00:00 < @2014-12-13T12:00:01", true);
        testBoolean(patient(), "@T12:00:00 < @T14:00:00", true);

        testBoolean(patient(), "1 < 1", false);
        testBoolean(patient(), "1.0 < 1.0", false);
        testBoolean(patient(), "'a' < 'a'", false);
        testBoolean(patient(), "'A' < 'A'", false);
        testBoolean(patient(), "@2014-12-12 < @2014-12-12", false);
        testBoolean(patient(), "@2014-12-13T12:00:00 < @2014-12-13T12:00:00", false);
        testBoolean(patient(), "@T12:00:00 < @T12:00:00", false);

        testBoolean(patient(), "2 < 1", false);
        testBoolean(patient(), "1.1 < 1.0", false);
        testBoolean(patient(), "'b' < 'a'", false);
        testBoolean(patient(), "'B' < 'A'", false);
        testBoolean(patient(), "@2014-12-13 < @2014-12-12", false);
        testBoolean(patient(), "@2014-12-13T12:00:01 < @2014-12-13T12:00:00", false);
        testBoolean(patient(), "@T12:00:01 < @T12:00:00", false);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testLessOrEqual()
    {
        testBoolean(patient(), "1 <= 2", true);
        testBoolean(patient(), "1.0 <= 1.2", true);
        testBoolean(patient(), "'a' <= 'b'", true);
        testBoolean(patient(), "'A' <= 'a'", true);
        testBoolean(patient(), "@2014-12-12 <= @2014-12-13", true);
        testBoolean(patient(), "@2014-12-13T12:00:00 <= @2014-12-13T12:00:01", true);
        testBoolean(patient(), "@T12:00:00 <= @T14:00:00", true);

        testBoolean(patient(), "1 <= 1", true);
        testBoolean(patient(), "1.0 <= 1.0", true);
        testBoolean(patient(), "'a' <= 'a'", true);
        testBoolean(patient(), "'A' <= 'A'", true);
        testBoolean(patient(), "@2014-12-12 <= @2014-12-12", true);
        testBoolean(patient(), "@2014-12-13T12:00:00 <= @2014-12-13T12:00:00", true);
        testBoolean(patient(), "@T12:00:00 <= @T12:00:00", true);

        testBoolean(patient(), "2 <= 1", false);
        testBoolean(patient(), "1.1 <= 1.0", false);
        testBoolean(patient(), "'b' <= 'a'", false);
        testBoolean(patient(), "'B' <= 'A'", false);
        testBoolean(patient(), "@2014-12-13 <= @2014-12-12", false);
        testBoolean(patient(), "@2014-12-13T12:00:01 <= @2014-12-13T12:00:00", false);
        testBoolean(patient(), "@T12:00:01 <= @T12:00:00", false);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testGreatorOrEqual()
    {
        testBoolean(patient(), "1 >= 2", false);
        testBoolean(patient(), "1.0 >= 1.2", false);
        testBoolean(patient(), "'a' >= 'b'", false);
        testBoolean(patient(), "'A' >= 'a'", false);
        testBoolean(patient(), "@2014-12-12 >= @2014-12-13", false);
        testBoolean(patient(), "@2014-12-13T12:00:00 >= @2014-12-13T12:00:01", false);
        testBoolean(patient(), "@T12:00:00 >= @T14:00:00", false);

        testBoolean(patient(), "1 >= 1", true);
        testBoolean(patient(), "1.0 >= 1.0", true);
        testBoolean(patient(), "'a' >= 'a'", true);
        testBoolean(patient(), "'A' >= 'A'", true);
        testBoolean(patient(), "@2014-12-12 >= @2014-12-12", true);
        testBoolean(patient(), "@2014-12-13T12:00:00 >= @2014-12-13T12:00:00", true);
        testBoolean(patient(), "@T12:00:00 >= @T12:00:00", true);

        testBoolean(patient(), "2 >= 1", true);
        testBoolean(patient(), "1.1 >= 1.0", true);
        testBoolean(patient(), "'b' >= 'a'", true);
        testBoolean(patient(), "'B' >= 'A'", true);
        testBoolean(patient(), "@2014-12-13 >= @2014-12-12", true);
        testBoolean(patient(), "@2014-12-13T12:00:01 >= @2014-12-13T12:00:00", true);
        testBoolean(patient(), "@T12:00:01 >= @T12:00:00", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testGreatorThan()
    {
        testBoolean(patient(), "1 > 2", false);
        testBoolean(patient(), "1.0 > 1.2", false);
        testBoolean(patient(), "'a' > 'b'", false);
        testBoolean(patient(), "'A' > 'a'", false);
        testBoolean(patient(), "@2014-12-12 > @2014-12-13", false);
        testBoolean(patient(), "@2014-12-13T12:00:00 > @2014-12-13T12:00:01", false);
        testBoolean(patient(), "@T12:00:00 > @T14:00:00", false);

        testBoolean(patient(), "1 > 1", false);
        testBoolean(patient(), "1.0 > 1.0", false);
        testBoolean(patient(), "'a' > 'a'", false);
        testBoolean(patient(), "'A' > 'A'", false);
        testBoolean(patient(), "@2014-12-12 > @2014-12-12", false);
        testBoolean(patient(), "@2014-12-13T12:00:00 > @2014-12-13T12:00:00", false);
        testBoolean(patient(), "@T12:00:00 > @T12:00:00", false);

        testBoolean(patient(), "2 > 1", true);
        testBoolean(patient(), "1.1 > 1.0", true);
        testBoolean(patient(), "'b' > 'a'", true);
        testBoolean(patient(), "'B' > 'A'", true);
        testBoolean(patient(), "@2014-12-13 > @2014-12-12", true);
        testBoolean(patient(), "@2014-12-13T12:00:01 > @2014-12-13T12:00:00", true);
        testBoolean(patient(), "@T12:00:01 > @T12:00:00", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testUnion()
    {
        testBoolean(patient(), "(1 | 2 | 3).count() = 3", true);
        testBoolean(patient(), "(1 | 2 | 2).count() = 2", true); // merge duplicates
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testIn()
    {
        testBoolean(patient(), "1 in (1 | 2 | 3)", true);
        testBoolean(patient(), "1 in (2 | 3)", false);
        testBoolean(patient(), "'a' in ('a' | 'c' | 'd')", true);
        testBoolean(patient(), "'b' in ('a' | 'c' | 'd')", false);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testContainsCollection()
    {
        testBoolean(patient(), "(1 | 2 | 3) contains 1", true);
        testBoolean(patient(), "(2 | 3) contains 1 ", false);
        testBoolean(patient(), "('a' | 'c' | 'd') contains 'a'", true);
        testBoolean(patient(), "('a' | 'c' | 'd') contains 'b'", false);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testBooleanLogicAnd()
    {
        testBoolean(patient(), "(true and true) = true", true);
        testBoolean(patient(), "(true and false) = false", true);

        //test incorrect: = operator also propagates empty, this results in {}, not true
        //testBoolean(patient(), "(true and {}) = {}", true);
        testBoolean(patient(), "(true and {}).empty()", true);

        testBoolean(patient(), "(false and true) = false", true);
        testBoolean(patient(), "(false and false) = false", true);
        testBoolean(patient(), "(false and {}) = false", true);

        //test incorrect: = operator also propagates empty, this results in {}, not true
        //testBoolean(patient(), "({} and true) = {}", true);
        testBoolean(patient(), "({} and true).empty()", true);

        testBoolean(patient(), "({} and false) = false", true);

        //test incorrect: = operator also propagates empty, this results in {}, not true
        //testBoolean(patient(), "({} and {}) = {}", true);
        testBoolean(patient(), "({} and {}).empty()", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testBooleanLogicOr()
    {
        testBoolean(patient(), "(true or true) = true", true);
        testBoolean(patient(), "(true or false) = true", true);
        testBoolean(patient(), "(true or {}) = true", true);

        testBoolean(patient(), "(false or true) = true", true);
        testBoolean(patient(), "(false or false) = false", true);

        //test incorrect: = operator also propagates empty, this results in {}, not true
        //testBoolean(patient(), "(false or {}) = {}", true);
        testBoolean(patient(), "(false or {}).empty()", true);

        testBoolean(patient(), "({} or true) = true", true);

        //test incorrect: = operator also propagates empty, this results in {}, not true
        //testBoolean(patient(), "({} or false) = {}", true);
        testBoolean(patient(), "({} or false).empty()", true);

        //test incorrect: = operator also propagates empty, this results in {}, not true
        //testBoolean(patient(), "({} or {}) = {}", true);
        testBoolean(patient(), "({} or {}).empty()", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testBooleanLogicXOr()
    {
        testBoolean(patient(), "(true xor true) = false", true);
        testBoolean(patient(), "(true xor false) = true", true);

        //test incorrect: = operator also propagates empty, this results in {}, not true
        //testBoolean(patient(), "(true xor {}) = {}", true);
        testBoolean(patient(), "(true xor {}).empty()", true);

        testBoolean(patient(), "(false xor true) = true", true);
        testBoolean(patient(), "(false xor false) = false", true);

        //test incorrect: = operator also propagates empty, this results in {}, not true
        //testBoolean(patient(), "(false xor {}) = {}", true);
        testBoolean(patient(), "(false xor {}).empty()", true);

        //test incorrect: = operator also propagates empty, this results in {}, not true
        //testBoolean(patient(), "({} xor true) = {}", true);
        //testBoolean(patient(), "({} xor false) = {}", true);
        //testBoolean(patient(), "({} xor {}) = {}", true);
        testBoolean(patient(), "({} xor true).empty()", true);
        testBoolean(patient(), "({} xor false).empty()", true);
        testBoolean(patient(), "({} xor {}).empty()", true);

    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testBooleanImplies()
    {
        testBoolean(patient(), "(true implies true) = true", true);
        testBoolean(patient(), "(true implies false) = false", true);

        //testBoolean(patient(), "(true implies {}) = {}", true);
        //test incorrect: = operator also propagates empty, this results in {}, not true
        testBoolean(patient(), "(true implies {}).empty()", true);

        testBoolean(patient(), "(false implies true) = true", true);
        testBoolean(patient(), "(false implies false) = true", true);

        testBoolean(patient(), "(false implies {}) = true", true);        

        testBoolean(patient(), "({} implies true) = true", true);

        //testBoolean(patient(), "({} implies false) = true", true);
        //according to the spec, {} implies false = {}
        testBoolean(patient(), "({} implies false).empty()", true);

        //testBoolean(patient(), "({} implies {}) = true", true);
        //test incorrect: = operator also propagates empty, this results in {}, not true
        testBoolean(patient(), "({} implies {}).empty()", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testPlus()
    {
        testBoolean(patient(), "1 + 1 = 2", true);
        testBoolean(patient(), "1 + 0 = 1", true);
        testBoolean(patient(), "1.2 + 1.8 = 3.0", true);
        testBoolean(patient(), "'a'+'b' = 'ab'", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testConcatenate()
    {
        // Incorrect test, the & operator is only defined for strings?
        // Seems you want to it take the string representation of the
        // argument, in which {} = ""
        //testBoolean(patient(), "1 & 1 = '11'", true);
        //testBoolean(patient(), "1 & 'a' = '1a'", true);
        //testBoolean(patient(), "{} & 'b' = 'b'", true);
        //testBoolean(patient(), "(1 | 2 | 3) & 'b' = '1,2,3b'", true);
        //testBoolean(patient(), "'a'&'b' = 'ab'", true);
        testBoolean(patient(), "'1' & '1' = '11'", true);
        testBoolean(patient(), "'1' & 'a' = '1a'", true);
        testBoolean(patient(), "{} & 'b' = 'b'", true);

        // we have not defined how to "serialize" a collection into a string
        //testBoolean(patient(), "(1 | 2 | 3) & 'b' = '1,2,3b'", true);
        testBoolean(patient(), "'a'&'b' = 'ab'", true);
    }


    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testMinus()
    {
        testBoolean(patient(), "1 - 1 = 0", true);
        testBoolean(patient(), "1 - 0 = 1", true);
        testBoolean(patient(), "1.8 - 1.2 = 0.6", true);
        testWrong(patient(), "'a'-'b' = 'ab'");
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testMultiply()
    {
        testBoolean(patient(), "1 * 1 = 1", true);
        testBoolean(patient(), "1 * 0 = 0", true);
        testBoolean(patient(), "1.2 * 1.8 = 2.16", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testDivide()
    {
        testBoolean(patient(), "1 / 1 = 1", true);
        testBoolean(patient(), "4 / 2 = 2", true);
        testBoolean(patient(), "1 / 2 = 0.5", true);
        testBoolean(patient(), "1.2 / 1.8 = 0.67", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testDiv()
    {
        testBoolean(patient(), "1 div 1 = 1", true);
        testBoolean(patient(), "4 div 2 = 2", true);
        testBoolean(patient(), "5 div 2 = 2", true);
        testBoolean(patient(), "2.2 div 1.8 = 1", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testMod()
    {
        testBoolean(patient(), "1 mod 1 = 0", true);
        testBoolean(patient(), "4 mod 2 = 0", true);
        testBoolean(patient(), "5 mod 2 = 1", true);
        testBoolean(patient(), "2.2 mod 1.8 = 0.4", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testPrecedence()
    {
        testBoolean(patient(), "1+2*3+4 = 11", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testVariables()
    {
        testBoolean(patient(), "%sct = 'http://snomed.info/sct'", true);
        testBoolean(patient(), "%loinc = 'http://loinc.org'", true);
        testBoolean(patient(), "%ucum = 'http://unitsofmeasure.org'", true);
        testBoolean(patient(), "%\"vs-administrative-gender\" = 'http://hl7.org/fhir/ValueSet/administrative-gender'", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testExtension()
    {
        testBoolean(patient(), "Patient.birthDate.extension('http://hl7.org/fhir/StructureDefinition/patient-birthTime').exists()", true);
        testBoolean(patient(), "Patient.birthDate.extension(%\"ext-patient-birthTime\").exists()", true);
        testBoolean(patient(), "Patient.birthDate.extension('http://hl7.org/fhir/StructureDefinition/patient-birthTime1').empty()", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testDollarResource()
    {
        testBoolean(patient(), patient().getManagingOrganization(), "Reference", "reference.startsWith('#').not() or (reference.substring(1).trace('url') in %resource.contained.id.trace('ids'))", true);
        testBoolean(patient(), patient(), "Patient", "contained.select(('#'+id in %resource.descendents().reference).not()).empty()", true);
        testWrong(patient(), "contained.select(('#'+id in %resource.descendents().reference).not()).empty()");
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


    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testAppointment()
    {
        testBoolean(appointment(), "(start and end) or status = 'proposed' or status = 'cancelled'", true);
        testBoolean(appointment(), "start.empty() xor end.exists()", true);
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

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testDoubleEntryPoint()
    {
        testBoolean(patient(), "(Patient.name | Patient.address).count() = 3", true);
    }

    [TestMethod, TestCategory("FhirPathFromSpec")]
    public void testParameersConstraint()
    {
        Parameters p = parameters();
        Order o = (Order)parameters().getParameter()[0].Resource;

        testBoolean(o, o.getSubject(), "Reference", "reference.startsWith('#').not() or (reference.substring(1).trace('url') in %resource.contained.id.trace('ids'))", true);
    }

}
