using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.FhirPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hl7.Fhir.Validation
{
    [Trait("Category", "Validation")]
    public class MinMaxValidationTests
    {
        [Fact]
        public void TestGetComparable()
        {
            var nodeQ = (new Model.FhirDateTime(1972, 11, 30)).ToTypedElement();
            Assert.Equal(0,nodeQ.GetComparableValue(typeof(Model.FhirDateTime)).CompareTo(Model.Primitives.PartialDateTime.Parse("1972-11-30")));

            nodeQ = (new Model.Quantity(3.14m, "kg")).ToTypedElement();
            Assert.Equal(-1, nodeQ.GetComparableValue(typeof(Model.Quantity)).CompareTo(new Model.Primitives.Quantity(5.0m, "kg")));

            nodeQ = (new Model.HumanName()).ToTypedElement();
            Assert.Null(nodeQ.GetComparableValue(typeof(Model.HumanName)));

            var nodeQ2 = (new Model.Quantity(3.14m, "kg")
                { Comparator = Model.Quantity.QuantityComparator.GreaterOrEqual }).ToTypedElement();
            Assert.Throws<NotSupportedException>(() => nodeQ2.GetComparableValue(typeof(Model.Quantity)));

            var nodeQ3 = (new Model.Quantity()).ToTypedElement();
            Assert.Throws<NotSupportedException>(() => nodeQ3.GetComparableValue(typeof(Model.Quantity)));
        }

        [Fact]
        public void TestCompare()
        {
            Assert.Equal(0, MinMaxValidationExtensions.Compare(Model.Primitives.PartialDateTime.Parse("1972-11-30"), new Model.FhirDateTime(1972, 11, 30)));
            Assert.Equal(1, MinMaxValidationExtensions.Compare(Model.Primitives.PartialDateTime.Parse("1972-12-01"), new Model.Date(1972, 11, 30)));
            Assert.Equal(-1,
                MinMaxValidationExtensions.Compare(Model.Primitives.PartialDateTime.Parse("1972-12-01T13:00:00Z"),
                    new Model.Instant(new DateTimeOffset(1972, 12, 01, 14, 00, 00, TimeSpan.Zero))));
            Assert.Equal(0, MinMaxValidationExtensions.Compare(Model.Primitives.PartialTime.Parse("12:00:00Z"), new Model.Time("12:00:00Z")));
            Assert.Equal(1, MinMaxValidationExtensions.Compare(3.14m, new Model.FhirDecimal(2.14m)));
            Assert.Equal(-1, MinMaxValidationExtensions.Compare(-3L, new Model.Integer(3)));
            Assert.Equal(-1, MinMaxValidationExtensions.Compare("aaa", new Model.FhirString("bbb")));
            Assert.Equal(1, MinMaxValidationExtensions.Compare(new Model.Primitives.Quantity(5.0m, "kg"), new Model.Quantity(4.0m, "kg")));

            Assert.Throws<NotSupportedException>(() => MinMaxValidationExtensions.Compare(Model.Primitives.PartialDateTime.Parse("1972-11-30"), new Model.Quantity(4.0m, "kg")));
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
