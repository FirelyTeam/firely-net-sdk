using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization
{
    internal static class RootRenamerExtensions
    {
        public static ITypedElement Rename(this ITypedElement wrapped, string name) =>
            name != null ? new RootRenamer(wrapped, name) : wrapped;

        private class RootRenamer : BaseTypedElement
        {
            private readonly string _renamed;

            public RootRenamer(ITypedElement wrapped, string name) : base(wrapped)
            {
                _renamed = name ?? throw Error.ArgumentNull(name);
            }

            public override string Name => _renamed;
        }
    }
}
