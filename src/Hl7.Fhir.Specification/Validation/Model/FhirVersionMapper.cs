using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class FhirVersionMapper : IAssignMapper<FHIRVersion?, string>
    {
        public static readonly FhirVersionMapper Current = new FhirVersionMapper();

        public string Map(MappingContext context, FHIRVersion? source)
        {
            if (source is null)
            {
                return null;
            }

            return source.GetLiteral();
        }

        public FHIRVersion? Map(MappingContext context, string source)
        {
            if (source is null)
            {
                return null;
            }

            if (!EnumUtility.TryParseLiteral<FHIRVersion>(source, out var result, true))
            {
                context.NotifyOrThrow(this, ExceptionNotification.Error(new NotImplementedException($"Can not parse FHIR version '{source}'")));
                return null;
            }

            return result;
        }
    }
}
