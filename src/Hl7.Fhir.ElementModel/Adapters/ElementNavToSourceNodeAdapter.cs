/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel.Adapters
{
#pragma warning disable 612, 618
    internal class ElementNavToSourceNodeAdapter : ISourceNode, IAnnotated, IExceptionSource
    {
        public readonly IElementNavigator Current;

        public ElementNavToSourceNodeAdapter(IElementNavigator nav)
        {
            if (nav == null) throw Error.ArgumentNull(nameof(nav));

            Current = nav.Clone();

            if (nav is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private ElementNavToSourceNodeAdapter(ElementNavToSourceNodeAdapter parent, IElementNavigator child)
        {
            //Don't think this is necessary, since the only caller of this private constructor has
            //just called the .GetChildren() method, which returns clones.
            //Current = child.Clone(); 
            Current = child;
            ExceptionHandler = parent.ExceptionHandler;
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Text => Current.Value == null ? null :
            PrimitiveTypeConverter.ConvertTo<string>(Current.Value);

        public string Location => Current.Location;

        public string ResourceType =>
            Current.Type ?? Current.Annotation<IResourceTypeSupplier>()?.ResourceType;

        public IEnumerable<ISourceNode> Children(string name = null) =>
            Current.Children()
                .Select(c => new ElementNavToSourceNodeAdapter(this, c))
                .Where(c => c.Name.MatchesPrefix(name));


        public string Name
        {
            get
            {
                var typeInfo = Current.Annotation<ITypedElement>()?.Definition;

                return typeInfo?.IsChoiceElement == true ?
                    Current.Name + Current.Type.Capitalize() : Current.Name;
            }
        }

        public object Value => Current.Value == null ? null :
            PrimitiveTypeConverter.ConvertTo<string>(Current.Value);

        IEnumerable<object> IAnnotated.Annotations(Type type) => Current.Annotations(type);
    }
#pragma warning restore 612, 618
}
