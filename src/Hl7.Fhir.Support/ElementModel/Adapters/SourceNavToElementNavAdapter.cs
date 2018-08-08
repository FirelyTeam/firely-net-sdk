using System;
using System.Collections.Generic;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel.Adapters
{
    internal class SourceNavToElementNavAdapter : IElementNavigator, IAnnotated, IExceptionSource
    {
        private ISourceNavigator _sourceNav;

        public SourceNavToElementNavAdapter(ISourceNavigator sourceNav)
        {
            this._sourceNav = sourceNav;

            if (sourceNav is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name => _sourceNav.Name;

        public string Type => _sourceNav.GetResourceType();

        public object Value => _sourceNav.Text;

        public string Location => _sourceNav.Location;

        public IElementNavigator Clone() =>
            new SourceNavToElementNavAdapter(_sourceNav.Clone())
            {
                ExceptionHandler = this.ExceptionHandler
            };

        public bool MoveToFirstChild(string nameFilter = null) => _sourceNav.MoveToFirstChild(nameFilter);

        public bool MoveToNext(string nameFilter = null) => _sourceNav.MoveToNext(nameFilter);

        IEnumerable<object> IAnnotated.Annotations(Type type)
        {
            if (type == typeof(SourceNavToElementNavAdapter))
                return new[] { this };
            else
                return _sourceNav.Annotations(type);
        }
    }
}