using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl7.Fhir.Specification.Schema
{
    /// <summary>
    /// Represents a constraint that validates the [x] portion of a choice type
    /// (which it will receive from the IElementNavigator, which will return the
    /// label when it encounters a choice of types)
    /// </summary>
    public class FhirTypeLabel : SimpleAssertion
    {
        public readonly FHIRDefinedType Label;

        public FhirTypeLabel(FHIRDefinedType label)
        {
            Label = label;
        }

        protected override string Key => "fhir-type-label";

        protected override object Value => Label.GetLiteral();

        public override Assertions Validate(IElementNavigator input, ValidationContext vc) =>
            throw new NotImplementedException();
    }
}