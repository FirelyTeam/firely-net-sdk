/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;

namespace Hl7.Fhir.ElementModel
{
    public static class PocoBuilderExtensions
    {
        public static Base ToPoco(this ISourceNode source, ModelInspector inspector, Type pocoType = null, PocoBuilderSettings settings = null) =>
            new PocoBuilder(inspector, settings).BuildFrom(source, pocoType);

        public static T ToPoco<T>(this ISourceNode source, ModelInspector inspector, PocoBuilderSettings settings = null) where T : Base =>
               (T)source.ToPoco(inspector, typeof(T), settings);

        public static T ToPoco<T>(this ITypedElement element, ModelInspector inspector, PocoBuilderSettings settings = null) where T : Base =>
               (T)new PocoBuilder(inspector, settings).BuildFrom(element);

        public static Base ToPoco(this ITypedElement element, ModelInspector inspector, PocoBuilderSettings settings = null) =>
               (Base)new PocoBuilder(inspector, settings).BuildFrom(element);

        public static ISourceNode ToSourceNode(this Base @base, ModelInspector inspector, string rootName = null) =>
                @base.ToTypedElement(inspector, rootName).ToSourceNode();

    }
}
