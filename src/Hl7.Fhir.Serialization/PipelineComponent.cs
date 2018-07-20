/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    public class PipelineComponent
    {
        public static PipelineComponent Create<T>() => new PipelineComponent { ComponentType = typeof(T) };

        public Type ComponentType;
    }


    public static class PipelineComponentExtensions
    {
        public static bool InPipeline(this IAnnotated ann, Type componentType) =>
            ann.Annotations<PipelineComponent>().Any(component => component.ComponentType == componentType);

        public static bool InPipeline(this ISourceNavigator navigator, Type componentType) =>
            navigator is IAnnotated ia ? ia.InPipeline(componentType) : false;

        public static bool InPipeline(this IElementNavigator navigator, Type componentType) =>
            navigator is IAnnotated ia ? ia.InPipeline(componentType) : false;

    }
}
