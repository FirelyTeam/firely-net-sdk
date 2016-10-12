using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{

    internal class ProfileAssertion
    {
        private string _path;

        public ProfileAssertion(string instanceTypeProfile, string declaredTypeProfile, string path)
        {
            _path = path;
            if(instanceTypeProfile != null) SetInstanceType(instanceTypeProfile);
            if(declaredTypeProfile != null) SetDeclaredType(declaredTypeProfile);
        }

        private class ProfileEntry
        {
            public ProfileEntry(StructureDefinition def)
            {
                this.StructureDefinition = def;
            }

            public ProfileEntry(string url)
            {
                this.Reference = url;
            }

            public string Reference { get; private set; }

            private StructureDefinition _structureDefinition;

            public StructureDefinition StructureDefinition
            {
                get
                {
                    return _structureDefinition;
                }
                set
                {
                    _structureDefinition = value;
                    Reference = value.Url;
                }
            }
        }

        private ProfileEntry _instanceType;
        public StructureDefinition InstanceType
        {
            get
            {
                return _instanceType?.StructureDefinition;
            }
        }

        public void SetInstanceType(string canonical)
        {
            _instanceType = new ProfileEntry(canonical);
        }

        public void SetInstanceType(StructureDefinition instanceType)
        {
            _instanceType = new ProfileEntry(instanceType);
        }

        public void SetInstanceType(FHIRDefinedType instanceType)
        {
            SetInstanceType(ModelInfo.CanonicalUriForFhirCoreType(instanceType));
        }


        private ProfileEntry _declaredType;
        public StructureDefinition DeclaredType
        {
            get
            {
                return _declaredType?.StructureDefinition;
            }
        }

        public void SetDeclaredType(string canonical)
        {
            _declaredType = new ProfileEntry(canonical);
        }

        public void SetDeclaredType(StructureDefinition declaredType)
        {
            _declaredType = new ProfileEntry(declaredType);
        }

        public void SetDeclaredType(FHIRDefinedType declaredType)
        {
            SetDeclaredType(ModelInfo.CanonicalUriForFhirCoreType(declaredType));
        }



        private List<ProfileEntry> _statedProfiles = new List<ProfileEntry>();

        public IEnumerable<StructureDefinition> StatedProfiles
        {
            get
            {
                return _statedProfiles.Select(pe => pe.StructureDefinition);
            }
        }

        public void AddStatedProfile(StructureDefinition structureDefinition)
        {
            if (!_statedProfiles.Any(profile => profile.Reference == structureDefinition.Url))
                _statedProfiles.Add(new ProfileEntry(structureDefinition));
        }

        public void AddStatedProfile(string canonical)
        {
            if (!_statedProfiles.Any(profile => profile.Reference == canonical))
                _statedProfiles.Add(new ProfileEntry(canonical));
        }

        public void AddStatedProfile(IEnumerable<string> canonicals)
        {
            foreach (var canonical in canonicals) AddStatedProfile(canonical);
        }

        public void AddStatedProfile(IEnumerable<StructureDefinition> definitions)
        {
            foreach (var sd in definitions) AddStatedProfile(sd);
        }

        public IEnumerable<StructureDefinition> AllProfiles
        {
            get
            {
                return (new[] { InstanceType }).Concat(new[] { DeclaredType }).Concat(StatedProfiles);
            }
        }

        private IEnumerable<ProfileEntry> allEntries
        {
            get
            {
                return (new[] { _instanceType }).Concat(new[] { _declaredType }).Concat(_statedProfiles);
            }
        }

        /// <summary>
        /// Resolves the StructureDefinitions referred to by the given canonicals, and adds them
        /// to the list of StructureDefinitions available to the preprocessor
        /// </summary>
        /// <returns></returns>
        public OperationOutcome Resolve(Func<string, StructureDefinition> resolver)
        {
            var outcome = new OperationOutcome();

            // Go through all entries to find those that don't yet have a StructureDefinition
            foreach (var entry in allEntries.Where(e => e.StructureDefinition == null))
            {
                StructureDefinition structureDefinition = null;

                try
                {
                    // Once FindStructureDefinition() gets an overload to resolve multiple at the same time,
                    // use that one
                    structureDefinition = resolver(entry.Reference);

                    if (structureDefinition == null)
                        outcome.Info($"Unable to resolve reference to profile '{entry.Reference}'", Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, _path);
                    else
                    {
                        entry.StructureDefinition = structureDefinition;
                    }
                }
                catch (Exception e)
                {
                    outcome.Info($"Resolution of profile at '{entry.Reference}' failed: {e.Message}", Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, _path);
                    continue;
                }
            }

            return outcome;
        }

        /// <summary>
        /// Validates the instance, declared and stated profiles for consistenty.
        /// </summary>
        /// <returns></returns>
        public OperationOutcome Validate()
        {
            var outcome = new OperationOutcome();

            // If we have an instance type, it should be compatible with the declared type on the definition and the stated profiles
            if (InstanceType != null)
            {
                if (DeclaredType != null)
                {
                    if (!ModelInfo.IsInstanceTypeFor(DeclaredType.BaseType(), InstanceType.BaseType()))
                        outcome.Info($"The defined type of the element '{DeclaredType.Url}' is incompatible with that of the instance ('{InstanceType.BaseType()}')", 
                            Issue.CONTENT_ELEMENT_HAS_INCORRECT_TYPE, _path);
                }

                foreach (var type in StatedProfiles)
                {
                    if (!ModelInfo.IsInstanceTypeFor(type.BaseType(), InstanceType.BaseType()))
                        outcome.Info($"Instance of type '{InstanceType.BaseType()}' is incompatible with the constrained type '{type.BaseType()}' of stated profile '{type.Url}'", 
                            Issue.CONTENT_ELEMENT_HAS_INCORRECT_TYPE, _path);
                }
            }

            // All stated profiles should be profiling the same core type
            if (StatedProfiles.Any())
            {
                var baseTypes = StatedProfiles.Select(p => p.BaseType()).Distinct().ToList();

                if (baseTypes.Count > 1)
                {
                    var combinedNames = String.Join(" and ", baseTypes.Select(bt => bt.GetLiteral()));
                    outcome.Info($"The stated profiles are constraints on multiple different core types ({combinedNames}), which can never be satisfied.", 
                        Issue.CONTENT_MISMATCHING_PROFILES, _path);
                }
                else
                {
                    // The stated profiles should be compatible with the declared type of the element
                    if (DeclaredType != null)
                    {
                        if (!ModelInfo.IsInstanceTypeFor(DeclaredType.BaseType(), baseTypes.Single()))
                            outcome.Info($"The stated profiles are all constraints on '{baseTypes.Single()}', which is incompatible with type '{DeclaredType.BaseType()}' of the element",
                                Issue.CONTENT_MISMATCHING_PROFILES, _path);
                    }
                }
            }

            return outcome;
        }

        public IEnumerable<StructureDefinition> MinimalProfiles
        {
            get
            {
                // Provided validation was done, IF there are stated profiles, they are correct constraints on the instance, and compatible with the declared type
                // so we can just return that list (we might even remove the ones that are constraints on constraints)
                if (StatedProfiles.Any()) return StatedProfiles;

                // If there are no stated profiles, then:
                //  * If the declared type is a profile, it is more specific than the instance
                //  * If the declared type is a concrete core type, it is as specific as the instance
                // In both cases return the declared type.
                if (DeclaredType.ConstrainedType != null || !ModelInfo.IsCoreSuperType(DeclaredType.BaseType()))
                    return new[] { DeclaredType };

                // Else, all we have left is the instance type
                // If there is no known instance type, we have no profile to validate against
                if (InstanceType != null)
                    return new[] { InstanceType };
                else
                    return null;               
            }
        }
    }
}