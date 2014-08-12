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

namespace Fhir.Profiling
{
    using Model = Hl7.Fhir.Model;

    public class SpecificationBuilder
    {
        private Specification specification = new Specification();
        public SpecificationResolver resolver = null;
        public SpecificationLoader loader = null;

        public SpecificationBuilder(SpecificationResolver resolver = null)
        {
            this.resolver = resolver;
            loader = new SpecificationLoader(this);
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
            if (resolver != null)
            {
                if (!knownUris.Contains(uri))
                {
                    IEnumerable<Structure> structures = ResolveProfile(uri);
                    this.Add(structures);
                    knownUris.Add(uri);
                    // todo: this not give an error if unresolved.
                }
            }
            else
            {
                throw new InvalidOperationException("This SpecificationBuilder has no SpecificationResolver");
            }
        }

        public IEnumerable<Structure> ResolveProfile(Uri uri)
        {
            Profile profile = resolver.Get<Profile>(uri);
            if (profile != null)
            {
                return loader.LoadStructures(profile);
            }
            else
            {
                return Enumerable.Empty<Structure>();
            }
        }

        public IEnumerable<Structure> ResolveProfile(string uri)
        {
            //uri = uri.ToLower();
            return ResolveProfile(new Uri(uri));
        }

        public IEnumerable<Structure> ResolveProfile(TypeRef typeref)
        {
            Uri uri;
            if (typeref.ProfileUri == null)
            {
                string name = typeref.Code.ToLower(); // todo: this is a temporary fix!!!
                uri = new Uri("http://hl7.org/fhir/profile/" + name);
            }
            else
            {
                uri = new Uri(typeref.ProfileUri);
            }
            return ResolveProfile(uri);
        }
      
        public ValueSet ResolveValueSet(string uri)
        {
            throw new NotImplementedException();
        }

        private bool resolveTypeRef(TypeRef typeref)
        {
            IEnumerable<Structure> structures = ResolveProfile(typeref);
            this.Add(structures);
            return structures.Count() > 0;
        }

        private bool resolveTypeRefs()
        {
            bool haschanges = false;
            List<TypeRef> typerefs = specification.TypeRefs.Where(t => t.Resolution == Resolution.New).ToList();

            foreach (TypeRef typeref in typerefs)
            {
                bool resolved = resolveTypeRef(typeref);
                typeref.Resolution = (resolved) ? Resolution.Resolved : Resolution.Unresolvable;
                haschanges |= resolved;
            }
            return haschanges;
        }

        private void resolveBindings()
        {
            IEnumerable<Uri> bindings = 
                specification.Elements
                    .Where(e => e.BindingUri != null)
                    .Select(e => new Uri(e.BindingUri))
                    .Distinct()
                    .Except(specification.ValueSetUris());

            foreach(Uri uri in bindings)
            {
                Model.ValueSet source = resolver.Get<Model.ValueSet>(uri);
                if (source != null)
                {
                    ValueSet target = loader.LoadValueSet(source);
                    specification.Add(target);
                }
            }
        }

        public void Resolve()
        {
            while (resolveTypeRefs());
            resolveBindings();
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
