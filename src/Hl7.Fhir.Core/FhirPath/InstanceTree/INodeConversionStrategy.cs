using System.Collections.Generic;

namespace Hl7.Fhir.FhirPath.InstanceTree
{
    internal interface INodeConversionStrategy<TInput>
    {
        bool HandlesDocNode(TInput docNode);

        FhirInstanceTree ConstructTreeNode(TInput element, FhirInstanceTree parent);

        IEnumerable<TInput> SelectChildren(TInput element, FhirInstanceTree treeNode);

        void PostProcess(FhirInstanceTree convertedNode);
    }
}