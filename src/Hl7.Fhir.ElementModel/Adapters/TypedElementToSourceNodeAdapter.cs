/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel.Adapters
{
    internal class TypedElementToSourceNodeAdapter : ISourceNode, IAnnotated, IExceptionSource
    {
        public readonly ITypedElement Current;

        public TypedElementToSourceNodeAdapter(ITypedElement element)
        {
            this.Current = element ?? throw Error.ArgumentNull(nameof(element));

            if (element is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private TypedElementToSourceNodeAdapter(TypedElementToSourceNodeAdapter parent, ITypedElement child)
        {
            Current = child;
            ExceptionHandler = parent.ExceptionHandler;
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name
        {
            get
            {
                return Current.Definition?.IsChoiceElement == true ?
                    Current.Name + Current.InstanceType.Capitalize() : Current.Name;
            }
        }

        public string Text => Current.Value == null ? null :
            PrimitiveTypeConverter.ConvertTo<string>(Current.Value);

        public string Location => Current.Location;

        public string ResourceType
        {
            get
            {
                return Current.Definition?.IsResource == true ? Current.InstanceType : null;
            }
        }

        public IEnumerable<ISourceNode> Children(string name = null) =>
            Current.Children()
                .Select(c => new TypedElementToSourceNodeAdapter(this, c))
                .Where(c => c.Name.MatchesPrefix(name));

        IEnumerable<object> IAnnotated.Annotations(Type type) => Current.Annotations(type);
    }

}
