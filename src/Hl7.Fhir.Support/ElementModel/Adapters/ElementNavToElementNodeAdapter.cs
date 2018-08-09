using System.Linq;
using System.Collections.Generic;
using System;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel
{
    internal class ElementNavToElementNodeAdapter : IElementNode, IAnnotated, IExceptionSource
    {
        public readonly IElementNavigator Current;

        public ElementNavToElementNodeAdapter(IElementNavigator nav)
        {
            Current = nav.Clone();

            if (nav is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private ElementNavToElementNodeAdapter(ElementNavToElementNodeAdapter parent, IElementNavigator child)
        {
            Current = child.Clone();
            ExceptionHandler = parent.ExceptionHandler;
        }


        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name => Current.Name;

        public string Type => Current.Type;

        public string Location => Current.Location;

        public object Value => Current.Value;

        public IEnumerable<IElementNode> Children(string name=null) =>
            Current.Children(name).Select(c => new ElementNavToElementNodeAdapter(this, c));

        IEnumerable<object> IAnnotated.Annotations(Type type)
        {
            if (type == typeof(ElementNavToElementNodeAdapter))
                return new[] { this };
            else
                return Current.Annotations(type);
        }
    }
}