namespace Hl7.Fhir.Specification.Schema
{
    public interface IMergeable
    {
        IMergeable Merge(IMergeable other);
    }
}


