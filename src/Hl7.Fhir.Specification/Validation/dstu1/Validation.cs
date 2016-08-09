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
using Hl7.Fhir.Specification.Model;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Specification.Source;

namespace Hl7.Fhir.Validation
{
    public class Validator
    {
        public Validator()
        {
            Source = ArtifactResolver.CreateCachedDefault();
        }


        public Validator(IArtifactSource source)
        {
            Source = source;
        }

        public IArtifactSource Source { get; private set; }

        public Report Validate(IElementNavigator root)
        {
            SpecificationWorkspace spec = getSpecification(root);
            ResourceValidator validator = new ResourceValidator(spec);
            //  Report report = validator.Validate(root);
            Report report = null;
            return report;
        }

        private SpecificationWorkspace getSpecification(Uri uri, bool expand)
        {
            SpecificationProvider provider = new SpecificationProvider(Source);
            SpecificationBuilder builder = new SpecificationBuilder(provider);
            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(uri);
            if (expand) builder.Expand();

            return builder.ToSpecification();
        }

        private SpecificationWorkspace getSpecification(IElementNavigator root, bool expand = true)
        {
            Uri uri = new Uri("http://hl7.org/fhir/StructureDefinition/" + root.Name.ToLower());

            return getSpecification(uri, expand);
        }

    }
}
