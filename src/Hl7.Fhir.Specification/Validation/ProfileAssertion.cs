/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Validation
{

    internal class ProfileAssertion
    {
        private string _path;
        private Func<string, StructureDefinition> _resolver;

        private List<ProfileEntry> _allEntries = new List<ProfileEntry>();

        public ProfileAssertion(string path, Func<string, StructureDefinition> resolver)
        {
            _path = path;
            _resolver = resolver;
        }

        private ProfileEntry _instanceType;
        public StructureDefinition InstanceType
        {
            get
            {
                return _instanceType?.StructureDefinition;
            }
        }


        private ProfileEntry addEntry(string canonical)
        {
            var entry = _allEntries.SingleOrDefault(e => e.Reference == canonical);

            if (entry != null)
                return entry;
            else
            {
                var newEntry = new ProfileEntry(canonical);
                _allEntries.Add(newEntry);
                return newEntry;
            }
        }

        private ProfileEntry addEntry(StructureDefinition definition)
        {
            var entry = _allEntries.SingleOrDefault(e => e.Reference == definition.Url);

            if (entry != null)
            {
                if (entry.Unresolved)
                    entry.StructureDefinition = definition;

                return entry;
            }
            else
            {
                var newEntry = new ProfileEntry(definition);
                _allEntries.Add(newEntry);
                return newEntry;
            }
        }


        public void SetInstanceType(string canonical)
        {
            inputsChanged();
            _instanceType = addEntry(canonical);
        }

        public void SetInstanceType(StructureDefinition instanceType)
        {
            inputsChanged();
            _instanceType = addEntry(instanceType);
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
            inputsChanged();
            _declaredType = addEntry(canonical);
        }

        public void SetDeclaredType(StructureDefinition declaredType)
        {
            inputsChanged();
            _declaredType = addEntry(declaredType);
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
            var entry = addEntry(structureDefinition);

            if (!_statedProfiles.Contains(entry))
            {
                inputsChanged();
                _statedProfiles.Add(entry);
            }
        }

        public void AddStatedProfile(string canonical)
        {
            inputsChanged();
            var entry = addEntry(canonical);

            if (!_statedProfiles.Contains(entry))
            {
                inputsChanged();
                _statedProfiles.Add(entry);
            }
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
                return _allEntries.Select(e => e.StructureDefinition);
            }
        }


        OperationOutcome _lastResolutionOutcome = null;
        OperationOutcome _lastValidationOutcome = null;
        IEnumerable<StructureDefinition> _lastMinimalSet = null;

        private void inputsChanged()
        {
            _lastResolutionOutcome = null;
            _lastValidationOutcome = null;
            _lastMinimalSet = null;
        }

        /// <summary>
        /// Resolves the StructureDefinitions referred to by the given canonicals, and adds them
        /// to the list of StructureDefinitions available to the preprocessor
        /// </summary>
        /// <returns></returns>
        public OperationOutcome Resolve()
        {
            if (_lastResolutionOutcome != null) return _lastResolutionOutcome;

            var outcome = new OperationOutcome();

            // Go through all entries to find those that don't yet have a StructureDefinition
            foreach (var entry in _allEntries.Where(e => e.Unresolved))
            {
                StructureDefinition structureDefinition = null;

                try
                {
                    // Once FindStructureDefinition() gets an overload to resolve multiple at the same time,
                    // use that one
                    structureDefinition = _resolver(entry.Reference);

                    if (structureDefinition == null)
                        outcome.AddIssue($"Unable to resolve reference to profile '{entry.Reference}'", Issue.UNAVAILABLE_REFERENCED_PROFILE, _path);
                    else
                    {
                        entry.StructureDefinition = structureDefinition;
                    }
                }
                catch (Exception e)
                {
                    outcome.AddIssue($"Resolution of profile at '{entry.Reference}' failed: {e.Message}", Issue.UNAVAILABLE_REFERENCED_PROFILE, _path);
                    continue;
                }
            }

            _lastResolutionOutcome = outcome;
            return outcome;
        }

        /// <summary>
        /// Validates the instance type, declared and stated profiles for consistenty.
        /// </summary>
        /// <returns></returns>
        public OperationOutcome Validate()
        {
            if (_lastValidationOutcome != null) return _lastValidationOutcome;

            var outcome = new OperationOutcome();

            // Resolve input profiles first (note: this is cached)
            var resolutionOutcome = Resolve();
            if (!resolutionOutcome.Success)
                return resolutionOutcome;
            else
                outcome.Add(resolutionOutcome);
            
            // If we have an instance type, it should be compatible with the declared type on the definition and the stated profiles
            if (InstanceType != null)
            {
                if (DeclaredType != null)
                {
                    if (!ModelInfo.IsInstanceTypeFor(DeclaredType.BaseType(), InstanceType.BaseType()))
                        outcome.AddIssue($"The declared type of the element ({DeclaredType.ReadableName()}) is incompatible with that of the instance ('{InstanceType.ReadableName()}')", 
                            Issue.CONTENT_ELEMENT_HAS_INCORRECT_TYPE, _path);
                }

                foreach (var type in StatedProfiles)
                {
                    if (!ModelInfo.IsInstanceTypeFor(type.BaseType(), InstanceType.BaseType()))
                        outcome.AddIssue($"Instance of type '{InstanceType.ReadableName()}' is incompatible with the stated profile '{type.Url}' which is constraining constrained type '{type.ReadableName()}'", 
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
                    outcome.AddIssue($"The stated profiles are constraints on multiple different core types ({combinedNames}), which can never be satisfied.", 
                        Issue.CONTENT_MISMATCHING_PROFILES, _path);
                }
                else
                {
                    // The stated profiles should be compatible with the declared type of the element
                    if (DeclaredType != null)
                    {
                        if (!ModelInfo.IsInstanceTypeFor(DeclaredType.BaseType(), baseTypes.Single()))
                            outcome.AddIssue($"The stated profiles are all constraints on '{baseTypes.Single()}', which is incompatible with the declared type '{DeclaredType.ReadableName()}' of the element",
                                Issue.CONTENT_MISMATCHING_PROFILES, _path);
                    }
                }
            }

            _lastValidationOutcome = outcome;
            return outcome;
        }

        public IEnumerable<StructureDefinition> MinimalProfiles
        {
            get
            {
                if (_lastMinimalSet != null)
                    return _lastMinimalSet;
                        
                // Provided validation was done, IF there are stated profiles, they are correct constraints on the instance, and compatible with the declared type
                // so we can just return that list (we might even remove the ones that are constraints on constraints)
                if (StatedProfiles.Any())
                {
                    // Remove redundant bases, since the snapshots will contain their constraints anyway.
                    // Note: we're not doing a full closure by resolving all bases for performance sake 
                    var result = StatedProfiles.ToList();
                    var bases = StatedProfiles.Where(sp => sp.Base != null).Select(sp => sp.Base).Distinct().ToList();
                    bases.AddRange(StatedProfiles.Where(sp => sp.ConstrainedType != null)
                        .Select(sp => ModelInfo.CanonicalUriForFhirCoreType(sp.ConstrainedType.Value)).Distinct());

                    result.RemoveAll(r => bases.Contains(r.Url));
                    _lastMinimalSet = result;
                }

                // If there are no stated profiles, then:
                //  * If the declared type is a profile, it is more specific than the instance
                //  * If the declared type is a concrete core type, it is as specific as the instance
                // In both cases return the declared type.
                else if (DeclaredType != null && (DeclaredType.ConstrainedType != null || !ModelInfo.IsCoreSuperType(DeclaredType.BaseType())))
                    _lastMinimalSet = new[] { DeclaredType };

                // Else, all we have left is the instance type
                // If there is no known instance type, we have no profile to validate against
                else if (InstanceType != null)
                    _lastMinimalSet = new[] { InstanceType };
                else
                    _lastMinimalSet = Enumerable.Empty<StructureDefinition>();

                return _lastMinimalSet;
            }

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
                    if (_structureDefinition == null)
                        return new StructureDefinition { Url = Reference };
                    else
                        return _structureDefinition;
                }
                set
                {
                    _structureDefinition = value;
                    Reference = value.Url;
                }
            }

            public bool Unresolved
            {
                get
                {
                    return _structureDefinition == null;
                }
            }
        }

    }
}