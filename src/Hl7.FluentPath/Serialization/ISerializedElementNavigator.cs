/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.ElementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
    public interface ISerializedSourceNavigator : INavigator<ISerializedSourceNavigator>
    {
        string Path { get; }

        string Text { get; }

        string Type { get; }

        T GetSerializationDetails<T>();
    }


}
