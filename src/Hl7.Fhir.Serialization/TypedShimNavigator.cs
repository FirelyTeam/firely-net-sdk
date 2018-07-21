using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{
    internal class TypedShimNavigator : IElementNavigator, IAnnotated, IExceptionSource
    {
        private ISourceNavigator _sourceNav;

        public TypedShimNavigator(ISourceNavigator sourceNav)
        {
            this._sourceNav = sourceNav;

            if (sourceNav is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name => _sourceNav.Name;

        public string Type => null;

        public object Value => _sourceNav.Text;

        public string Location => _sourceNav.Path;

        public IElementNavigator Clone() =>
            new TypedShimNavigator(_sourceNav.Clone())
            {
                ExceptionHandler = this.ExceptionHandler
            };

        public bool MoveToFirstChild(string nameFilter = null) => _sourceNav.MoveToFirstChild(nameFilter);

        public bool MoveToNext(string nameFilter = null) => _sourceNav.MoveToNext(nameFilter);

        private static readonly PipelineComponent _componentLabel = PipelineComponent.Create<TypedShimNavigator>();

        IEnumerable<object> IAnnotated.Annotations(Type type)
        {
            if (type == typeof(PipelineComponent))
                return (new[] { _componentLabel }).Union(_sourceNav.Annotations(typeof(PipelineComponent)));
            else
                return _sourceNav.Annotations(type);
        }
    }
}