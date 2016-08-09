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
using Hl7.Fhir.Specification.Validation.Model;
using Hl7.Fhir.Specification.Model;
using Model = Hl7.Fhir.Model;


namespace Hl7.Fhir.Validation
{
    
    
    internal class SpecificationBuilder
    {
        public SpecificationBuilder(SpecificationProvider provider)
        {
            _provider = provider;
        }

        private SpecificationWorkspace specification = new SpecificationWorkspace();
        private SpecificationProvider _provider;
        private Tracker tracker = new Tracker();
        
        private void trackStructure(Structure structure)
        {
            Uri uri = structure.ProfileUri;
            tracker.MarkResolved(uri);
        }

        private void trackStructures(IEnumerable<Structure> structures)
        {
            var uris = structures.Select(s => s.Uri).Distinct();
            foreach (Uri u in uris)
            {
                tracker.MarkResolved(u);
            }
        }

        private bool tryAddStructures(Uri uri)
        {
            if (uri == null) return false;
            if (tracker.Knows(uri)) return false;
            
            Structure structure = _provider.GetStructure(uri);
            if (structure != null)
            {
                specification.Add(structure);
                tracker.Add(uri, Resolution.Resolved);
                trackStructure(structure);
                return true;
            }
            else
            {
                tracker.Add(uri, Resolution.Unresolvable);
                return true;
            }
        }

        private IEnumerable<Uri> unresolvedTypeRefKeys()
        {
            IEnumerable<Uri> uris =
                specification
                .UnresolvedTypeRefUris()
                .Where(uri => !tracker.Knows(uri));

            return uris;
        }

        private bool expandTypeRefs()
        {
            var uris = unresolvedTypeRefKeys().ToList();
            // ToList(), because expanding will modify this list.

            bool expanded = false;
            foreach (Uri uri in uris)
            {
                expanded |= tryAddStructures(uri);
            }
            return expanded;
        }

        private bool tryAddValueSet(Uri uri)
        {
            if (tracker.Knows(uri)) return false;

            ValueSet valueset = _provider.GetValueSet(uri);
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

        private void expandValueSets()
        {
            IEnumerable<Uri> newbindings = specification.BindingUris.ToList();
            
            foreach (Uri uri in newbindings)
            {
                tryAddValueSet(uri);
            }
        }

        public void Expand()
        {
            while (expandTypeRefs()) ;
            expandValueSets();
        }

        public void Add(Uri uri)
        {
            tryAddStructures(uri);
        }

        public void Add(string uri)
        {
            Add(new Uri(uri));
        }

        public void Add(IEnumerable<Structure> structures)
        {
            specification.Add(structures);
            trackStructures(structures);
        }

        public void Add(IEnumerable<ValueSet> valuesets)
        {
            specification.Add(valuesets);

        }

        public SpecificationWorkspace ToSpecification()
        {
            if (!specification.Sealed)
            {
                SpecificationBinder.Bind(specification);
            }
            return specification;

        }
    }
}
