/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;


namespace Hl7.Fhir.ElementModel.Adapters
{
    /// <summary>
    /// This class sole reason for existence is that the BaseFhirParser accepts untyped navigators (ISourceNavigator)
    /// only. This class below simulates a ISourceNavigator on top of an IElementNavigator, basically by throwing
    /// away the type information. The parser then will re-associate this information while it parses the source
    /// into the POCO.  Not the most efficient way of doing things, but until I simplify the poco parser to work
    /// on typed navigators, this stop-gap needs to stay in place.
    /// </summary>
    public class ElementNavToSourceNavAdapter : ISourceNavigator, IAnnotated, IExceptionSource
    {
        private IElementNavigator _sourceNav;

        public ElementNavToSourceNavAdapter(IElementNavigator sourceNav)
        {
            this._sourceNav = sourceNav;

            if (sourceNav is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private ElementNavToSourceNavAdapter()
        { }   // for clone

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name
        {
            get
            {
                var typeInfo = _sourceNav.GetElementDefinitionSummary();

                return typeInfo?.IsChoiceElement == true ?
                    _sourceNav.Name + _sourceNav.Type.Capitalize() : _sourceNav.Name;
            }
        }

        public string Text => _sourceNav.Value == null ? null :
            PrimitiveTypeConverter.ConvertTo<string>(_sourceNav.Value);

        public string Location => _sourceNav.Location;

        public ISourceNavigator Clone() =>
            new ElementNavToSourceNavAdapter()
            {
                _sourceNav = this._sourceNav.Clone(),
                ExceptionHandler = this.ExceptionHandler
            };

        public bool MoveToFirstChild(string nameFilter = null) =>
            nameFilter == null ? _sourceNav.MoveToFirstChild() :
            throw Error.NotImplemented($"This {nameof(ElementNavToSourceNavAdapter)} shim should not be called with a name filter.");

        public bool MoveToNext(string nameFilter = null) =>
            nameFilter == null ? _sourceNav.MoveToNext() :
            throw Error.NotImplemented($"This {nameof(ElementNavToSourceNavAdapter)} shim should not be called with a name filter.");


        IEnumerable<object> IAnnotated.Annotations(Type type)
        {
            if (type == typeof(ElementNavToSourceNavAdapter))
                return new[] { this };
            else
                return _sourceNav.Annotations(type);
        }
    }

}
