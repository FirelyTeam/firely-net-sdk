/*
* Copyright (c) 2014, Firely (info@fire.ly) and contributors
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

namespace Hl7.Fhir.XPath
{
    internal static class FhirNamespaceManager
    {
        public const string Fhir = "f";
        public const string Atom = "atom";
        public const string XHtml = "xhtml";

        public static XmlNamespaceManager CreateManager(XPathNavigator navigator)
        {
            XmlNamespaceManager nsm = new XPath2Context((NameTable)navigator.NameTable);
            addNamespaces(nsm);
            return nsm;

        }

        public static XmlNamespaceManager CreateManager()
        {
            XmlNamespaceManager nsm = new XPath2Context();
            addNamespaces(nsm);
            return nsm;
        }


        private static void addNamespaces(XmlNamespaceManager mgr)
        {
            mgr.AddNamespace(FhirNamespaceManager.Fhir, "http://hl7.org/fhir");
            mgr.AddNamespace(FhirNamespaceManager.Atom, "http://www.w3.org/2005/Atom");
            mgr.AddNamespace(FhirNamespaceManager.XHtml, "http://www.w3.org/1999/xhtml");
        }
    }
}