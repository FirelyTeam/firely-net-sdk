﻿/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;

namespace Hl7.Fhir.ElementModel
{
    public static class PocoBuilderExtensions
    {
        public static Base ToPoco(this ISourceNode source, Type pocoType = null, PocoBuilderSettings settings = null) =>
            source.ToPoco(ModelInfo.ModelInspector, pocoType, settings);

        public static T ToPoco<T>(this ISourceNode source, PocoBuilderSettings settings = null) where T : Base =>
               (T)source.ToPoco(ModelInfo.ModelInspector, typeof(T), settings);

        public static Base ToPoco(this ITypedElement element, PocoBuilderSettings settings = null) =>
            element.ToPoco(ModelInfo.ModelInspector, settings);

        public static T ToPoco<T>(this ITypedElement element, PocoBuilderSettings settings = null) where T : Base =>
               (T)element.ToPoco(ModelInfo.ModelInspector, settings);
    }
}
