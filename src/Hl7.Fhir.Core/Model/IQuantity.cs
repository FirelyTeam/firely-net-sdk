using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    public interface IQuantity : IElement
    {
        FhirDecimal ValueElement { get; set; }
        decimal? Value { get; set; }
        Code<QuantityComparator> ComparatorElement { get; set; }
        QuantityComparator? Comparator { get; set; }
        FhirString UnitElement { get; set; }
        string Unit { get; set; }
        FhirUri SystemElement { get; set; }
        string System { get; set; }
        Code CodeElement { get; set; }
        string Code { get; set; }
    }
}
