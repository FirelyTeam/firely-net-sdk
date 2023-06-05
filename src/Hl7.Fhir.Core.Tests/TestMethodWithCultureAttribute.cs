using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace Hl7.Fhir.Core.Tests
{
    public class TestMethodWithCultureAttribute : TestMethodAttribute
    {
        private readonly CultureInfo _culture;

        public TestMethodWithCultureAttribute(string culture)
        {
            _culture = new CultureInfo(culture, false);
        }

        public override TestResult[] Execute(ITestMethod testMethod)
        {
            var originalCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var originalUICulture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = _culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = _culture;

                return base.Execute(testMethod);
            }
            finally
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = originalCulture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = originalUICulture;
            }
        }
    }
}
