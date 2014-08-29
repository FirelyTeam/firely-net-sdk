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
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Fhir.Profiling.IO;
using Hl7.Fhir.Introspection.Source;

namespace Fhir.Profiling
{
    using Model = Hl7.Fhir.Model;
    
    public enum Resolution { Unknown, Unresolvable, Resolved }

    public class Tracker
    {
        Dictionary<Uri, Resolution> list = new Dictionary<Uri, Resolution>();

        public void Log(Uri uri, Resolution resolution)
        {


        }

        public bool Knows(Uri uri)
        {
            Resolution resolution = Resolution.Unknown;
            list.TryGetValue(uri, out resolution);
            return resolution != Resolution.Unknown;
        }
    }

    public class SpecificationBuilder
    {
        private Specification specification = new Specification();
        private SpecificationProvider provider;
        private Tracker tracker = new Tracker();

        public SpecificationBuilder(SpecificationProvider resolver)
        {
            this.provider = resolver;
        }

        private List<string> knownUris = new List<string>();

        public TypeRef CreateTypeRef(string code, string profileUri)
        {
            // Create new or refer to existing

            TypeRef typeref = new TypeRef(code, profileUri);
            TypeRef existing = specification.FindTypeRef(typeref);
           
            if (existing != null)
            {
                return existing;
            }
            else
            {
                specification.Add(typeref);
                return typeref;
            }
        }

        public void Add(IEnumerable<ValueSet> valuesets)
        {
            specification.Add(valuesets);
        }

        public void Add(IEnumerable<Structure> structures)
        {
            specification.Add(structures);
        }

        public void Add(string uri)
        {
            if (provider != null)
            {
                if (!knownUris.Contains(uri))
                {
                    IEnumerable<Structure> structures = provider.GetStructures(uri);
                    this.Add(structures);
                    knownUris.Add(uri);
                    // todo: this should not give an error if unresolved.
                }
            }
            else
            {
                throw new InvalidOperationException("This SpecificationBuilder has no SpecificationResolver");
            }
        }

        private bool TryExpand(TypeRef typeref)
        {
            Uri uri = typeref.GetUri();
            if (tracker.Knows(uri)) return false;

            IEnumerable<Structure> structures = provider.GetStructures(typeref);
            this.Add(structures);
            return structures.Count() > 0;
        }

        private bool ExpandTypeRefs()
        {
            bool expanded = false;

            foreach (TypeRef typeref in specification.TypeRefs)
            {
                expanded |= TryExpand(typeref);
            }
            return expanded;
        }

        private void ExpandBindings()
        {
            IEnumerable<Uri> newbindings = specification.BindingUris.Except(specification.ValueSetUris);
            IEnumerable<ValueSet> valuesets = provider.GetValueSets(newbindings);
            this.Add(valuesets);
        }

        public void Expand()
        {
            while (ExpandTypeRefs());
            ExpandBindings();
        }

        public Specification ToSpecification()
        {
            if (!specification.Sealed)
            {
                SpecificationSealer.Seal(specification);
            }
            return specification;

        }
    }
}
