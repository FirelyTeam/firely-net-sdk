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
using Fhir.IO;
using System.Xml.XPath;
using System.Xml;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Introspection.Source;

namespace Fhir.Profiling.Tests
{
    public static class Factory
    {
        public static Specification GetPatientSpec(bool resolve)
        {
            SpecificationProvider resolver = SpecificationProvider.CreateDefault();
            SpecificationBuilder builder = new SpecificationBuilder(resolver);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.Add("http://hl7.org/fhir/profile/patient");
            if (resolve) builder.Expand();

            return builder.ToSpecification();
        }

        public static Specification GetProfileSpec(bool resolve)
        {
            SpecificationProvider resolver = SpecificationProvider.CreateDefault();
            SpecificationBuilder builder = new SpecificationBuilder(resolver);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.LoadXmlFile("TestData\\profiles.xml");
            if (resolve) builder.Expand();

            return builder.ToSpecification();
        }

        public static Specification GetLipidSpec(bool resolve)
        {
            SpecificationProvider resolver = SpecificationProvider.CreateDefault();
            SpecificationBuilder builder = new SpecificationBuilder(resolver);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.LoadXmlFile("TestData\\valueset.profile.xml");
            builder.LoadXmlFile("TestData\\lipid.profile.xml");
            if (resolve) builder.Expand();
            
            return builder.ToSpecification();

        }

        public static Specification GetExtendedPatientSpec(bool resolve)
        {
            SpecificationProvider resolver = SpecificationProvider.CreateDefault();
            SpecificationBuilder builder = new SpecificationBuilder(resolver);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.LoadXmlFile("TestData\\patient.extended.profile.xml");
            builder.LoadXmlFile("TestData\\type-Extension.profile.xml");
            if (resolve) builder.Expand();
            return builder.ToSpecification();

        }
    }

}
