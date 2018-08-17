/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

 using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel.Adapters
{
    internal class SourceNodeToTypedElementAdapter : ITypedElement, IAnnotated, IExceptionSource
    {
        public readonly ISourceNode Current;

        public SourceNodeToTypedElementAdapter(ISourceNode node)
        {
            Current = node ?? throw Error.ArgumentNull(nameof(node));

            if (node is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private SourceNodeToTypedElementAdapter(SourceNodeToTypedElementAdapter parent, ISourceNode sourceNode)
        {
            this.Current = sourceNode;
            this.ExceptionHandler = parent.ExceptionHandler;
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name => Current.Name;

        public string InstanceType => Current.GetResourceTypeIndicator(); 

        public object Value => Current.Text;

        public string Location => Current.Location;

        public IElementDefinitionSummary Definition => null;

        public IEnumerable<ITypedElement> Children(string name) =>
            Current.Children(name).Select(c => new SourceNodeToTypedElementAdapter(this, c));

        IEnumerable<object> IAnnotated.Annotations(Type type) => Current.Annotations(type);
    }
}