/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using Hl7.Fhir.Profiling;
using Hl7.Fhir.Specification.Model;

namespace Hl7.Fhir.Specification.IO
{
    internal static class FhirFile
    {

        public static Feed LoadXMLFeed(string filename)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            return new Feed(document);
        }

    }
}
