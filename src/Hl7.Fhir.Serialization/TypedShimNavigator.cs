using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{
    internal class TypedShimNavigator : IElementNavigator, IAnnotated, IExceptionSource, IExceptionSink
    {
        private ISourceNavigator _sourceNav;

        public TypedShimNavigator(ISourceNavigator sourceNav)
        {
            this._sourceNav = sourceNav;
        }

        public string Name => _sourceNav.Name;

        public string Type => throw Error.NotImplemented("You cannot invoke the Type getter on an (untyped) ISourceNavigator");

        public object Value => _sourceNav.Text;

        public string Location => _sourceNav.Path;

        public IElementNavigator Clone() => new TypedShimNavigator(_sourceNav.Clone());

        public bool MoveToFirstChild(string nameFilter = null) => _sourceNav.MoveToFirstChild(nameFilter);

        public bool MoveToNext(string nameFilter = null) => _sourceNav.MoveToNext(nameFilter);

        public IExceptionSink Sink { get; set; }

        public void Notify(object source, ExceptionNotification args) => Sink.NotifyOrThrow(source, args);

        IEnumerable<object> IAnnotated.Annotations(Type type)
        {
            if (_sourceNav is IAnnotated ia)
                return ia.Annotations(type);
            else
                return Enumerable.Empty<object>();
        }
    }
}