using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// A base class for creating components wrapping another <see cref="ITypedElement"/> component.
    /// </summary>
    /// <remarks>
    /// By default, all members of <see cref="ITypedElement"/> are forwarden to the instance passed in the
    /// constructor, but any of the members can be overridden in the subclass.
    /// </remarks>
    public class BaseTypedElement : ITypedElement, IAnnotated, IExceptionSource
    {
        public readonly ITypedElement Wrapped;

        public BaseTypedElement(ITypedElement wrapped)
        {
            Wrapped = wrapped;

            if (wrapped is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        public virtual ExceptionNotificationHandler ExceptionHandler { get; set; }

        public virtual string Name => Wrapped.Name;

        public virtual string InstanceType => Wrapped.InstanceType;

        public virtual object Value => Wrapped.Value;

        public virtual string Location => Wrapped.Location;

        public virtual IElementDefinitionSummary Definition => Wrapped.Definition;

        public virtual IEnumerable<object> Annotations(Type type) => Wrapped.Annotations(type);
        public virtual IEnumerable<ITypedElement> Children(string name = null) => Wrapped.Children(name);
    }
}
