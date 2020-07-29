using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System;
using Xunit;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Validation
{
    [Trait("Category", "Validation")]
    public class MinMaxValidationTests
    {
        [Fact]
        public void TestGetComparable()
        {
            var nodeQ = new FhirDateTime(1972, 11, 30).ToTypedElement();
            Assert.Equal(0,nodeQ.GetComparableValue(typeof(FhirDateTime)).CompareTo(P.DateTime.Parse("1972-11-30")));

            nodeQ = new Quantity(3.14m, "kg").ToTypedElement();
            Assert.Equal(-1, nodeQ.GetComparableValue(typeof(Quantity)).CompareTo(new P.Quantity(5.0m, "kg")));

            nodeQ = new HumanName().ToTypedElement();
            Assert.Null(nodeQ.GetComparableValue(typeof(Model.HumanName)));

            var nodeQ2 = new Quantity(3.14m, "kg")
                { Comparator = Quantity.QuantityComparator.GreaterOrEqual }.ToTypedElement();
            Assert.Throws<NotSupportedException>(() => nodeQ2.GetComparableValue(typeof(Quantity)));

            var nodeQ3 = (new Quantity()).ToTypedElement();
            Assert.Throws<NotSupportedException>(() => nodeQ3.GetComparableValue(typeof(Quantity)));
        }

        [Fact]
        public void TestCompare()
        {
            Assert.Equal(0, MinMaxValidationExtensions.Compare(P.DateTime.Parse("1972-11-30"), new Model.FhirDateTime(1972, 11, 30)));
            Assert.Equal(1, MinMaxValidationExtensions.Compare(P.DateTime.Parse("1972-12-01"), new Model.Date(1972, 11, 30)));
            Assert.Equal(-1,
                MinMaxValidationExtensions.Compare(P.DateTime.Parse("1972-12-01T13:00:00Z"),
                    new Model.Instant(new DateTimeOffset(1972, 12, 01, 14, 00, 00, TimeSpan.Zero))));
            Assert.Equal(0, MinMaxValidationExtensions.Compare(P.Time.Parse("12:00:00Z"), new Model.Time("12:00:00Z")));
            Assert.Equal(1, MinMaxValidationExtensions.Compare(P.Date.Parse("2016-02-01"), new Model.Date("2016-01-01")));
            Assert.Equal(1, MinMaxValidationExtensions.Compare(3.14m, new Model.FhirDecimal(2.14m)));
            Assert.Equal(-1, MinMaxValidationExtensions.Compare(-3L, new Model.Integer(3)));
            Assert.Equal(-1, MinMaxValidationExtensions.Compare("aaa", new Model.FhirString("bbb")));
            Assert.Equal(1, MinMaxValidationExtensions.Compare(new P.Quantity(5.0m, "kg"), new Model.Quantity(4.0m, "kg")));

            Assert.Throws<NotSupportedException>(() => MinMaxValidationExtensions.Compare(P.DateTime.Parse("1972-11-30"), new Model.Quantity(4.0m, "kg")));
        }

        [Fact]
        public void TestMinMaxValue()
        {
            var validator = new Validator();

            var ed = new ElementDefinition
            {
                MinValue = new Model.Integer(4),
                MaxValue = new Model.Integer(6)
            };

            var node = (new Model.Integer(5)).ToTypedElement();
            var outcome = validator.ValidateMinMaxValue(ed, node);
            Assert.True(outcome.Success);
            Assert.Equal(0, outcome.Warnings);

            node = (new Model.Integer(4)).ToTypedElement();
            outcome = validator.ValidateMinMaxValue(ed, node);
            Assert.True(outcome.Success);
            Assert.Equal(0, outcome.Warnings);

            node = (new Model.Integer(6)).ToTypedElement();
            outcome = validator.ValidateMinMaxValue(ed, node);
            Assert.True(outcome.Success);
            Assert.Equal(0, outcome.Warnings);

            node = (new Model.Integer(1)).ToTypedElement();
            outcome = validator.ValidateMinMaxValue(ed, node);
            Assert.False(outcome.Success);
            Assert.Equal(0, outcome.Warnings);

            node = (new Model.FhirString("hi")).ToTypedElement();
            outcome = validator.ValidateMinMaxValue(ed, node);
            Assert.True(outcome.Success);
            Assert.Equal(2, outcome.Warnings);

            ed.MinValue = new Model.HumanName();
            ed.MaxValue = new Model.FhirString("i comes after hi");
            outcome = validator.ValidateMinMaxValue(ed, node);
            Assert.True(outcome.Success);
            Assert.Equal(1, outcome.Warnings);
        }
    }
}
