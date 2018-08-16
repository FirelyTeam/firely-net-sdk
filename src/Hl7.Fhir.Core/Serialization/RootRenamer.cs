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

        private class RootRenamer : ITypedElement, IAnnotated, IExceptionSource
        {
            private readonly string _rootName;
            private readonly ITypedElement _wrapped;

            public RootRenamer(ITypedElement wrapped, string name)
            {
                _wrapped = wrapped;
                _rootName = name;

                if (wrapped is IExceptionSource ies && ies.ExceptionHandler == null)
                    ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
            }

            public ExceptionNotificationHandler ExceptionHandler { get; set; }

            public string Name => _rootName;

            public string InstanceType => _wrapped.InstanceType;

            public object Value => _wrapped.Value;

            public string Location => _wrapped.Location;

            public IElementDefinitionSummary Definition => _wrapped.Definition;

            public IEnumerable<object> Annotations(Type type) => _wrapped.Annotations(type);

            public IEnumerable<ITypedElement> Children(string name = null) => _wrapped.Children(name);
        }
    }
}
