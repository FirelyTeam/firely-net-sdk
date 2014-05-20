/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Api.Properties;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public class ModelFactoryList : List<IModelClassFactory> {}

    public static class ModelFactoryListExtensions
    {
        public static IModelClassFactory FindFactory(this IEnumerable<IModelClassFactory> list, Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            return list.First(fac => fac.CanCreate(type));
        }

        public static object InvokeFactory(this IEnumerable<IModelClassFactory> list, Type type)
        {
            var chosenFactory = list != null ? list.FindFactory(type) : null;
            if (type == null) throw Error.ArgumentNull("type");
            if (chosenFactory == null) throw Error.InvalidOperation(Messages.NoSuchClassFactory, type.Name);

            object result = chosenFactory.Create(type);
            if (result == null) throw Error.InvalidOperation(Messages.FactoryCreationFailed);

            return result;
        }
    }


    public interface IModelClassFactory
    {
        bool CanCreate(Type type);
        object Create(Type type);
    }
}
