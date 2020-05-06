using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System;
using Xunit;

namespace Hl7.Fhir.Validation
{
    [Trait("Category", "Validation")]
    public class MinMaxValidationTests
    {
        [Fact]
        public void TestGetComparable()
        {
            var nodeQ = (new Fhir.Model.FhirDateTime(1972, 11, 30)).ToTypedElement();
            Assert.Equal(0, nodeQ.GetComparableValue().CompareTo(Fhir.Model.Primitives.PartialDateTime.Parse("1972-11-30")));

            nodeQ = (new Fhir.Model.Quantity(3.14m, "kg")).ToTypedElement();
            Assert.Equal(-1, nodeQ.GetComparableValue().CompareTo(new Fhir.Model.Primitives.Quantity(5.0m, "kg")));

            nodeQ = (new Fhir.Model.HumanName()).ToTypedElement();
            Assert.Null(nodeQ.GetComparableValue());

            var nodeQ2 = (new Fhir.Model.Quantity(3.14m, "kg")
            { Comparator = Fhir.Model.Quantity.QuantityComparator.GreaterOrEqual }).ToTypedElement();
            Assert.Throws<NotSupportedException>(() => nodeQ2.GetComparableValue());

            var nodeQ3 = (new Fhir.Model.Quantity()).ToTypedElement();
            Assert.Throws<NotSupportedException>(() => nodeQ3.GetComparableValue());
        }

        [Fact]
        public void TestCompare()
        {
            Assert.Equal(0, MinMaxValidationExtensions.Compare(Fhir.Model.Primitives.PartialDateTime.Parse("1972-11-30"), new Fhir.Model.FhirDateTime(1972, 11, 30)));
            Assert.Equal(1, MinMaxValidationExtensions.Compare(Fhir.Model.Primitives.PartialDateTime.Parse("1972-12-01"), new Fhir.Model.Date(1972, 11, 30)));
            Assert.Equal(-1,
                MinMaxValidationExtensions.Compare(Fhir.Model.Primitives.PartialDateTime.Parse("1972-12-01T13:00:00Z"),
                    new Fhir.Model.Instant(new DateTimeOffset(1972, 12, 01, 14, 00, 00, TimeSpan.Zero))));
            Assert.Equal(0, MinMaxValidationExtensions.Compare(Fhir.Model.Primitives.PartialTime.Parse("12:00:00Z"), new Fhir.Model.Time("12:00:00Z")));
            Assert.Equal(1, MinMaxValidationExtensions.Compare(Fhir.Model.Primitives.PartialDate.Parse("2016-02-01"), new Fhir.Model.Date("2016-01-01")));
            Assert.Equal(1, MinMaxValidationExtensions.Compare(3.14m, new Fhir.Model.FhirDecimal(2.14m)));
            Assert.Equal(-1, MinMaxValidationExtensions.Compare(-3L, new Fhir.Model.Integer(3)));
            Assert.Equal(-1, MinMaxValidationExtensions.Compare("aaa", new Fhir.Model.FhirString("bbb")));
            Assert.Equal(1, MinMaxValidationExtensions.Compare(new Fhir.Model.Primitives.Quantity(5.0m, "kg"), new Fhir.Model.Quantity(4.0m, "kg")));

            Assert.Throws<NotSupportedException>(() => MinMaxValidationExtensions.Compare(Fhir.Model.Primitives.PartialDateTime.Parse("1972-11-30"), new Fhir.Model.Quantity(4.0m, "kg")));
        }

        [Fact]
        public void TestMinMaxValue()
        {
            var validator = new Validator();

            var ed = new ElementDefinition
            {
                MinValue = new Fhir.Model.Integer(4),
                MaxValue = new Fhir.Model.Integer(6)
            };

            var node = (new Fhir.Model.Integer(5)).ToTypedElement();
            var outcome = validator.ValidateMinMaxValue(ed, node);
            Assert.True(outcome.Success);
            Assert.Equal(0, outcome.Warnings);

            node = (new Fhir.Model.Integer(4)).ToTypedElement();
            outcome = validator.ValidateMinMaxValue(ed, node);
            Assert.True(outcome.Success);
            Assert.Equal(0, outcome.Warnings);

            node = (new Fhir.Model.Integer(6)).ToTypedElement();
            outcome = validator.ValidateMinMaxValue(ed, node);
            Assert.True(outcome.Success);
            Assert.Equal(0, outcome.Warnings);

            node = (new Fhir.Model.Integer(1)).ToTypedElement();
            outcome = validator.ValidateMinMaxValue(ed, node);
            Assert.False(outcome.Success);
            Assert.Equal(0, outcome.Warnings);

            node = (new Fhir.Model.FhirString("hi")).ToTypedElement();
            outcome = validator.ValidateMinMaxValue(ed, node);
            Assert.True(outcome.Success);
            Assert.Equal(2, outcome.Warnings);

            ed.MinValue = new Fhir.Model.HumanName();
            ed.MaxValue = new Fhir.Model.FhirString("i comes after hi");
            outcome = validator.ValidateMinMaxValue(ed, node);
            Assert.True(outcome.Success);
            Assert.Equal(1, outcome.Warnings);
        }
    }
}
