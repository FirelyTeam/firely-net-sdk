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
    
    public class SpecificationBuilder
    {
        public SpecificationBuilder(SpecificationProvider provider)
        {
            this.provider = provider;
        }

        private Specification specification = new Specification();
        private SpecificationProvider provider;
        private Tracker tracker = new Tracker();

        private bool TryExpandStructures(Uri uri)
        {
            if (tracker.Knows(uri)) return false;
                
            IEnumerable<Structure> structures = provider.GetStructures(uri);
            if (structures.Count() > 0)
            {
                specification.Add(structures);
                tracker.Add(uri, Resolution.Resolved);
                return true;
            }
            else
            {
                tracker.Add(uri, Resolution.Unresolvable);
                return true;
            }
        }

        private List<Uri> UnresolvedTypeRefUris()
        {
            return 
                specification.TypeRefs
                .Where(t => t.Unresolved)
                .Select(t => t.ResolvingUri)
                .ToList();
        }

        private bool ExpandTypeRefs()
        {
            var uris = UnresolvedTypeRefUris();
            // ToList(), because expanding will modify this list.

            bool expanded = false;
            foreach (Uri uri in uris)
            {
                expanded |= TryExpandStructures(uri);
            }
            return expanded;
        }

        private bool TryExpandBinding(Uri uri)
        {
            if (tracker.Knows(uri)) return false;

            ValueSet valueset = provider.GetValueSet(uri);
            if (valueset != null)
            {
                tracker.Add(uri, Resolution.Resolved);
                specification.Add(valueset);
                return true;
            }
            else
            {
                tracker.Add(uri, Resolution.Unresolvable);
                return false;
            }
        }

        private void ExpandBindings()
        {
            IEnumerable<Uri> newbindings = specification.BindingUris;
            
            foreach (Uri uri in newbindings)
            {
                TryExpandBinding(uri);
            }
        }

        public void Expand()
        {
            while (ExpandTypeRefs()) ;
            ExpandBindings();
        }

        public void Add(Uri uri)
        {
            TryExpandStructures(uri);
        }
        public void Add(string uri)
        {
            Add(new Uri(uri));
        }

        public void Add(IEnumerable<Structure> structures)
        {
            specification.Add(structures);
        }

        public void Add(IEnumerable<ValueSet> valuesets)
        {
            specification.Add(valuesets);
        }

        public Specification ToSpecification()
        {
            if (!specification.Sealed)
            {
                SpecificationBinder.Bind(specification);
            }
            return specification;

        }
    }
}
