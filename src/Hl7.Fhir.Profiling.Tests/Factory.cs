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
using Hl7.Fhir.Introspection.Source;
using Hl7.Fhir.Profiling;

namespace Fhir.Profiling.Tests
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

        public static Specification GetPatientSpec(bool expand, bool online)
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

        public static Specification GetProfileSpec(bool expand, bool online)
        {
            SpecificationProvider resolver = GetProvider(online);
            SpecificationBuilder builder = new SpecificationBuilder(resolver);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            if (expand) builder.Expand();

            return builder.ToSpecification();
        }

        public static Specification GetLipidSpec(bool expand, bool online)
        {
            SpecificationProvider resolver = GetProvider(online);
            SpecificationBuilder builder = new SpecificationBuilder(resolver);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.LoadXmlFile("TestData\\valueset.profile.xml");
            builder.LoadXmlFile("TestData\\lipid.profile.xml");
            if (expand) builder.Expand();
            
            return builder.ToSpecification();

        }

        public static Specification GetExtendedPatientSpec(bool expand, bool online)
        {
            SpecificationProvider resolver = GetProvider(online);
            SpecificationBuilder builder = new SpecificationBuilder(resolver);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.LoadXmlFile("TestData\\patient.extended.profile.xml");
            builder.LoadXmlFile("TestData\\type-Extension.profile.xml");
            if (expand) builder.Expand();
            return builder.ToSpecification();

        }
    }

}
