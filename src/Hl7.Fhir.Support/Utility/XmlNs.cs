/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Hl7.Fhir.Utility
{
    public static class XmlNs
    {
        public const string FHIR = "http://hl7.org/fhir";
        public const string XHTML = "http://www.w3.org/1999/xhtml";
        public const string NAMESPACE = "http://www.w3.org/XML/1998/namespace";
        public const string XMLNS = "http://www.w3.org/2000/xmlns/";

        public static readonly XNamespace XFHIR = FHIR;
        public static readonly XNamespace XHTMLNS = XHTML;

        public static readonly XName XHTMLDIV = XmlNs.XHTMLNS + "div";
        public static readonly XName XSCHEMALOCATION = XName.Get("{http://www.w3.org/2001/XMLSchema-instance}schemaLocation");
    }
}
