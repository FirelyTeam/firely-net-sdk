using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.ElementModel
{
    public interface IScopedNode : ITypedElement, IAnnotated, IExceptionSource
    {
        ITypedElement Current { get; }
        IScopedNode ParentResource { get; }
        string LocalLocation { get; }
        bool AtResource { get; }
        IEnumerable<IBundledResource> BundledResources();
        string Id();
        IEnumerable<IScopedNode> ParentResources();
        string FullUrl();
        IEnumerable<IScopedNode> ContainedResources();
        ITypedElement ResourceContext { get; }
    }
}
