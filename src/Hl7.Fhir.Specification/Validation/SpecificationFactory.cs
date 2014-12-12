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
using Hl7.Fhir.Profiling;
using Hl7.Fhir.Specification.Model;
using Hl7.Fhir.Specification.IO;

namespace Hl7.Fhir.Profiling
{
    public static class SpecificationFactory
    {
        private static SpecificationBuilder createSpecificationBuilder(SpecificationProvider provider, string[] uris)
        {
            var builder = new SpecificationBuilder(provider);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.MetaTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            foreach(string uri in uris)
            {
                builder.Add(uri);
            }
            builder.Expand();
            return builder;
        }

        public static SpecificationWorkspace Create(params string[] uris)
        {
            var resolver = SpecificationProvider.CreateOffline();
            var builder = createSpecificationBuilder(resolver, uris);
            return builder.ToSpecification();
        }

        public static SpecificationWorkspace CreateOnline(params string[] uris)
        {
            var resolver = SpecificationProvider.CreateDefault();
            var builder = createSpecificationBuilder(resolver, uris);
            return builder.ToSpecification();
        }
    }
}
