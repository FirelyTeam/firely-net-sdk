﻿/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable 1591 // suppress XML summary warnings

namespace Hl7.Fhir.XPath
{
#if NET_XSD_SCHEMA
    public static class JsonReaderExtensions
    {
        public static JsonXPathNavigator GetNavigator(this JsonReader reader)
        {
            return new JsonXPathNavigator(reader);
        }            
    }
#endif
}
