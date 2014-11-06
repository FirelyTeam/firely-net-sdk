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
using System.Xml.XPath;
using System.Xml;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using Hl7.Fhir.Specification.Model;

namespace Hl7.Fhir.Specification.Tests
{
    public static class Factory
    {
        public static SpecificationProvider GetProvider(bool online)
        {
            if (online)
            {
                return SpecificationProvider.CreateDefault();
            }
            else
            {
                return SpecificationProvider.CreateOffline();
            }
        }

        public static SpecificationWorkspace GetPatientSpec(bool expand, bool online)
        {
            SpecificationProvider resolver = GetProvider(online);
            SpecificationBuilder builder = new SpecificationBuilder(resolver);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.MetaTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.Add("http://hl7.org/fhir/Profile/Patient");
            
            if (expand) builder.Expand();

            return builder.ToSpecification();
        }

        public static SpecificationWorkspace GetProfileSpec(bool expand, bool online)
        {
            SpecificationProvider resolver = GetProvider(online);
            
            SpecificationBuilder builder = new SpecificationBuilder(resolver);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            if (expand) builder.Expand();

            return builder.ToSpecification();
        }

        public static SpecificationWorkspace GetLipidSpec(bool expand, bool online)
        {
            SpecificationProvider resolver = GetProvider(online);
            SpecificationBuilder builder = new SpecificationBuilder(resolver);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.Add("http://here.there/TestData/valueset.profile.xml");
            builder.Add("http://here.there/TestData/lipid.profile.xml");
            if (expand) builder.Expand();
            
            return builder.ToSpecification();

        }

        public static SpecificationWorkspace GetExtendedPatientSpec(bool expand, bool online)
        {
            SpecificationProvider resolver = GetProvider(online);
            SpecificationBuilder builder = new SpecificationBuilder(resolver);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.Add("http://here.there/patient.extended.profile.xml");
            builder.Add("http://here.there/type-Extension.profile.xml");
            if (expand) builder.Expand();
            return builder.ToSpecification();

        }

        public static SpecificationWorkspace GetOtherSpec(bool expand, bool online, string uri)
        {
            SpecificationProvider resolver = GetProvider(online);
            SpecificationBuilder builder = new SpecificationBuilder(resolver);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.Add(uri);

            if (expand) builder.Expand();

            return builder.ToSpecification();
        }


        public static XPathNavigator LoadResource(string filename)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            var nav = document.CreateNavigator();
            nav.MoveToFirstChild();
            XmlNamespaceManager manager = new XmlNamespaceManager(nav.NameTable);
            manager.AddNamespace("f", "http://hl7.org/fhir");

            return nav;
        }
    }

}
