using FluentAssertions;
using Hl7.Fhir.Base.ElementModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P = Hl7.Fhir.ElementModel.Types;
namespace Hl7.Fhir.Support.Tests.ElementModel
{
    [TestClass]
    public class UcumTests
    {
        public enum MyEnum
        {
            smallerThan,
            Equals,
            greaterThan
        }

        [DataTestMethod]
        [DataRow(1.0f, "m", 1.0f, "m", MyEnum.Equals)]
        //[DataRow(1.0f, "m", 1.0f, "in", MyEnum.greaterThan)]
        //[DataRow(1.0f, "m", 1.0f, "in", MyEnum.greaterThan)]
        //[DataRow(1.0f, "m", 1.0f, "in", MyEnum.greaterThan)]
        public void MyTestMethod(decimal dLeft, string unitLeft, decimal dRight, string unitRight, MyEnum expectedResult)
        {

            var result = Ucum.Compare(new P.Quantity(dLeft, unitLeft), new P.Quantity(dRight, unitRight));

            result.Success.Should().BeTrue();
            switch (expectedResult)
            {
                case MyEnum.smallerThan:
                    result.ValueOrDefault().Should().BeNegative();
                    break;
                case MyEnum.Equals:
                    result.ValueOrDefault().Should().Be(0);
                    break;
                case MyEnum.greaterThan:
                    result.ValueOrDefault().Should().BePositive();
                    break;
            }

        }

    }
}
