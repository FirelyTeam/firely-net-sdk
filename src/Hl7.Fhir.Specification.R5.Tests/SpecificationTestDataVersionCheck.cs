namespace Hl7.Fhir.Specification.Tests
{
    public partial class SpecificationTestDataVersionCheck
    {
        private bool skipFiles(string item)
        {
            if (item.Contains("grahame-validation-examples"))
                return true;
            if (item.Contains("source-test"))
                return true;
            if (item.Contains("summary-test"))
                return true;
            // [WMR 20190822] Added
            if (item.Contains("Type Slicing"))
                return true;
            if (item.Contains("validation-test-suite"))
                return true;
            if (item.EndsWith(".dll"))
                return true;
            if (item.EndsWith(".exe"))
                return true;
            if (item.EndsWith(".pdb"))
                return true;
            if (item.EndsWith("manifest.json"))
                return true;
            if (item.EndsWith("profiles-resources.xml"))
                return true;
            return false;
        }
    }
}