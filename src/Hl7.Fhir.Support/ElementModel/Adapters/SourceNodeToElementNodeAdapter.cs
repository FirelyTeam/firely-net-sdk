using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel.Adapters
{
    internal class SourceNodeToElementNodeAdapter : IElementNode, IAnnotated, IExceptionSource
    {
        public readonly ISourceNode Current;

        public SourceNodeToElementNodeAdapter(ISourceNode sourceNode)
        {
            this.Current = sourceNode;

            if (sourceNode is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private SourceNodeToElementNodeAdapter(SourceNodeToElementNodeAdapter parent, ISourceNode sourceNode)
        {
            this.Current = sourceNode;
            this.ExceptionHandler = parent.ExceptionHandler;
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name => Current.Name;

        public string InstanceType => Current.ResourceType;

        public object Value => Current.Text;

        public string Location => Current.Location;

        public IElementDefinitionSummary Definition => null;

        public IEnumerable<IElementNode> Children(string name) =>
            Current.Children(name).Select(c => new SourceNodeToElementNodeAdapter(this, c));

        IEnumerable<object> IAnnotated.Annotations(Type type)
        {
            if (type == typeof(SourceNodeToElementNodeAdapter))
                return new[] { this };
            else
                return Current.Annotations(type);
        }
    }
}