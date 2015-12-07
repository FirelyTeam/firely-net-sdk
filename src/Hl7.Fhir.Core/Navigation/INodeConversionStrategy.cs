using System.Collections.Generic;

namespace Hl7.Fhir.Navigation
{
    internal interface INodeConversionStrategy<TInput>
    {
        bool HandlesDocNode(TInput docNode);

        FhirNavigationTree ConstructTreeNode(TInput element, FhirNavigationTree parent);

        IEnumerable<TInput> SelectChildren(TInput element, FhirNavigationTree treeNode);

        void PostProcess(FhirNavigationTree convertedNode);
    }
}