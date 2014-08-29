/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using Hl7.Fhir.Introspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Fhir.Profiling
{
    public static class Validation
    {
        public static Report Validate(this Specification specification)
        {
            SpecificationValidator pv = new SpecificationValidator(specification);
            return pv.Validate();
        }

        public static Report Validate(this Specification specification, XPathNavigator root)
        {
            ResourceValidator validator = new ResourceValidator(specification);
            Report report = validator.Validate(root);
            return report; 
        }

        public static SpecificationProvider GetSpecificationResolver()
        {
            SpecificationProvider provider = SpecificationProvider.CreateOffline();
            return provider;
        }

        public static Specification GetSpecification(Uri uri, bool expand)
        {
            SpecificationProvider provider = GetSpecificationResolver();
            SpecificationBuilder builder = new SpecificationBuilder(provider);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.Add(uri);
            if (expand) builder.Expand();

            return builder.ToSpecification();
        }

        public static Specification GetSpecification(XPathNavigator root, bool expand = true)
        {
            Uri uri = new Uri("http://hl7.org/fhir/profile/" + root.Name.ToLower());
            
            return GetSpecification(uri, expand);
        }

        public static Report Validate(XPathNavigator root)
        {
            Specification spec = GetSpecification(root);
            return spec.Validate(root);
        }

    }
}
