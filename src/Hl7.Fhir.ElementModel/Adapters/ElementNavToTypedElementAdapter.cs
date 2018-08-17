/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

 using System.Linq;
using System.Collections.Generic;
using System;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;

namespace Hl7.Fhir.ElementModel.Adapters
{
#pragma warning disable 612, 618
    internal class ElementNavToTypedElementAdapter : ITypedElement, IAnnotated, IExceptionSource
    {
        public readonly IElementNavigator Current;

        public ElementNavToTypedElementAdapter(IElementNavigator nav)
        {
            if (nav == null) throw Error.ArgumentNull(nameof(nav));

            Current = nav.Clone();

            if (nav is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private ElementNavToTypedElementAdapter(ElementNavToTypedElementAdapter parent, IElementNavigator child)
        {
            //Don't think this is necessary, since the only caller of this private constructor has
            //just called the .GetChildren() method, which returns clones.
            //Current = child.Clone();  
            Current = child;
            ExceptionHandler = parent.ExceptionHandler;
        }


        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name => Current.Name;

        public string InstanceType => Current.Type;

        public string Location => Current.Location;

        public object Value => Current.Value;

        public IElementDefinitionSummary Definition => Current.Annotation<ITypedElement>()?.Definition;

        public IEnumerable<ITypedElement> Children(string name=null) =>
            Current.Children(name).Select(c => new ElementNavToTypedElementAdapter(this, c));

        IEnumerable<object> IAnnotated.Annotations(Type type) => Current.Annotations(type);
    }
#pragma warning restore 612, 618
}