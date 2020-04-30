using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Profile;

namespace Hl7.Fhir.Specification.Validation.Profile
{
    internal class CodingFacade : ICodingExt
    {
        private readonly Coding _coding;

        public CodingFacade(Coding coding)
        {
            _coding = coding;
        }

        public string System { get => _coding.System; set => _coding.System = value; }
        public string Version { get => _coding.Version; set => _coding.Version = value; }
        public string Code { get => _coding.Code; set => _coding.Code = value; }
        public string Display { get => _coding.Display; set => _coding.Display = value; }
        public bool? UserSelected { get => _coding.UserSelected; set => _coding.UserSelected = value; }
    }
}
