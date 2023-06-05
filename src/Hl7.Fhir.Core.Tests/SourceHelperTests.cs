using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hl7.Fhir.Core.Tests
{
    [TestClass]
    public class SourceHelperTests
    {
        private const string _validDate = "2012-01-01";
        private const string _invalidDate = "2012/01/01";

        private const string _validInstant = "2012-01-02T03:04:05Z";
        private const string _invalidInstant = "2012-01-02T03.04.05Z";

        private const string _validTime = "01:02:03.400";
        private const string _invalidTime = "01.02.03.400";

        [TestMethodWithCulture("it-IT")]
        public void IsValidDate_ItalianCulture_UsesFhirSpec()
        {
            Assert.IsTrue(SourceHelpers.IsValidDate(_validDate));
            Assert.IsFalse(SourceHelpers.IsValidDate(_invalidDate));
        }

        [TestMethodWithCulture("en-US")]
        public void IsValidDate_USCulture_UsesFhirSpec()
        {
            Assert.IsTrue(SourceHelpers.IsValidDate(_validDate));
            Assert.IsFalse(SourceHelpers.IsValidDate(_invalidDate));
        }

        [TestMethodWithCulture("ar-DZ")]
        public void IsValidDate_ArabicAlgerianCulture_UsesFhirSpec()
        {
            Assert.IsTrue(SourceHelpers.IsValidDate(_validDate));
            Assert.IsFalse(SourceHelpers.IsValidDate(_invalidDate));
        }

        [TestMethodWithCulture("tr-TR")]
        public void IsValidDate_TurkishCulture_UsesFhirSpec()
        {
            Assert.IsTrue(SourceHelpers.IsValidDate(_validDate));
            Assert.IsFalse(SourceHelpers.IsValidDate(_invalidDate));
        }

        [TestMethodWithCulture("it-IT")]
        public void IsValidTime_ItalianCulture_UsesFhirSpec()
        {
            Assert.IsTrue(SourceHelpers.IsValidTime(_validTime));
            Assert.IsFalse(SourceHelpers.IsValidTime(_invalidTime));
        }

        [TestMethodWithCulture("en-US")]
        public void IsValidTime_USCulture_UsesFhirSpec()
        {
            Assert.IsTrue(SourceHelpers.IsValidTime(_validTime));
            Assert.IsFalse(SourceHelpers.IsValidTime(_invalidTime));
        }

        [TestMethodWithCulture("ar-DZ")]
        public void IsValidTime_ArabicAlgerianCulture_UsesFhirSpec()
        {
            Assert.IsTrue(SourceHelpers.IsValidTime(_validTime));
            Assert.IsFalse(SourceHelpers.IsValidTime(_invalidTime));
        }

        [TestMethodWithCulture("tr-TR")]
        public void IsValidTime_TurkishCulture_UsesFhirSpec()
        {
            Assert.IsTrue(SourceHelpers.IsValidTime(_validTime));
            Assert.IsFalse(SourceHelpers.IsValidTime(_invalidTime));
        }

        [TestMethodWithCulture("it-IT")]
        public void TryParseFhirInstant_ItalianCulture_UsesFhirSpec()
        {
            Assert.IsTrue(SourceHelpers.TryParseFhirInstant(_validInstant, out var instant));
            Assert.AreEqual(new DateTimeOffset(2012, 1, 2, 3, 4, 5, TimeSpan.Zero), instant);

            Assert.IsFalse(SourceHelpers.TryParseFhirInstant(_invalidInstant, out instant));
            Assert.AreEqual(default(DateTimeOffset), instant);
        }

        [TestMethodWithCulture("en-US")]
        public void TryParseFhirInstant_USCulture_UsesFhirSpec()
        {
            Assert.IsTrue(SourceHelpers.TryParseFhirInstant(_validInstant, out var instant));
            Assert.AreEqual(new DateTimeOffset(2012, 1, 2, 3, 4, 5, TimeSpan.Zero), instant);

            Assert.IsFalse(SourceHelpers.TryParseFhirInstant(_invalidInstant, out instant));
            Assert.AreEqual(default, instant);
        }

        [TestMethodWithCulture("ar-DZ")]
        public void TryParseFhirInstant_ArabicAlgerianCulture_UsesFhirSpec()
        {
            Assert.IsTrue(SourceHelpers.TryParseFhirInstant(_validInstant, out var instant));
            Assert.AreEqual(new DateTimeOffset(2012, 1, 2, 3, 4, 5, TimeSpan.Zero), instant);

            Assert.IsFalse(SourceHelpers.TryParseFhirInstant(_invalidInstant, out instant));
            Assert.AreEqual(default(DateTimeOffset), instant);
        }

        [TestMethodWithCulture("tr-TR")]
        public void TryParseFhirInstant_TurkishCulture_UsesFhirSpec()
        {
            Assert.IsTrue(SourceHelpers.TryParseFhirInstant(_validInstant, out var instant));
            Assert.AreEqual(new DateTimeOffset(2012, 1, 2, 3, 4, 5, TimeSpan.Zero), instant);

            Assert.IsFalse(SourceHelpers.TryParseFhirInstant(_invalidInstant, out instant));
            Assert.AreEqual(default, instant);
        }
    }
}
