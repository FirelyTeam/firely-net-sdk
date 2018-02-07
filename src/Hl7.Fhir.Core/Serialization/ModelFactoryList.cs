/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{
    public class ModelFactoryList : List<IModelClassFactory> {}

    public static class ModelFactoryListExtensions
    {
        public static IModelClassFactory FindFactory(this IEnumerable<IModelClassFactory> list, Type type)
        {
            if (type == null) throw Error.ArgumentNull(nameof(type));

            return list.First(fac => fac.CanCreate(type));
        }

        public static object InvokeFactory(this IEnumerable<IModelClassFactory> list, Type type)
        {
            var chosenFactory = list != null ? list.FindFactory(type) : null;
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (chosenFactory == null) throw Error.InvalidOperation($"No ModelClassFactory registered to handle type '{type.Name}'");

            object result = chosenFactory.Create(type);
            if (result == null) throw Error.InvalidOperation("Factory failed to create object");

            return result;
        }
    }


    public interface IModelClassFactory
    {
        bool CanCreate(Type type);
        object Create(Type type);
    }
}
