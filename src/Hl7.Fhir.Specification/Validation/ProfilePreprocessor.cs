using Hl7.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    internal class ProfilePreprocessor
    {
        private List<string> _canonicals = new List<string>();
        private List<StructureDefinition> _sds = new List<StructureDefinition>();

        private Action<StructureDefinition> _onSnapshotNeeded;
        private Func<string, StructureDefinition> _onResolveProfiles;

        private string _path;

        public ProfilePreprocessor(Func<string, StructureDefinition> onResolveProfiles, Action<StructureDefinition> onSnapshotNeeded, string path)
        {
            _onResolveProfiles = onResolveProfiles;
            _onSnapshotNeeded = onSnapshotNeeded;
            _path = path;
        }

        public void AddProfile(string canonical)
        {
            if(!_canonicals.Contains(canonical))
                _canonicals.Add(canonical);
        }

        public void AddProfile(IEnumerable<string> canonicals)
        {
            foreach(var canonical in canonicals) AddProfile(canonical);
        }

        public void AddProfile(StructureDefinition definition)
        {
            if (!_sds.Any(sd => sd.Url == definition.Url))
                _sds.Add(definition);
        }

        public void AddProfile(IEnumerable<StructureDefinition> definitions)
        {
            foreach (var sd in definitions) AddProfile(sd);
        }


        /// <summary>
        /// Resolves the StructureDefinitions referred to by the given canonicals, and adds them
        /// to the list of StructureDefinitions available to the preprocessor
        /// </summary>
        /// <returns></returns>
        public OperationOutcome ResolveCanonicals()
        {
            var outcome = new OperationOutcome();

            foreach (var uri in _canonicals)
            {
                // Don't re-fetch a StructureDefinition that we already have in the preprocessor
                if (_sds.Any(sd => sd.Url == uri)) continue;

                StructureDefinition structureDefinition = null;

                try
                {
                    // Once FindStructureDefinition() gets an overload to resolve multiple at the same time,
                    // use that one
                    structureDefinition = _onResolveProfiles(uri);

                    if (structureDefinition == null)
                        outcome.Info($"Unable to resolve reference to profile '{uri}'", Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, _path);
                    else
                    {
                        _sds.Add(structureDefinition);
                    }

                }
                catch (Exception e)
                {
                    outcome.Info($"Resolution of profile at '{uri}' failed: {e.Message}", Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, _path);
                    continue;
                }
            }

            return outcome;
        }


        /// <summary>
        /// Cleans up the list of StructureDefinitions to remove duplicates and tautologies. Will also check consistency of the list
        /// of StructureDefinitions
        /// </summary>
        /// <returns></returns>
        public OperationOutcome NormalizeProfiles()
        {
            var outcome = new OperationOutcome();

            // Note: this assumes there are no duplicate profiles in the list of profiles
            // (which AddProfile and ResolveCanonicals ensure)

            // First, remove all SDs that are the base of a more specialized profile in our list
            var bases = _sds.Where(sd => sd.Base != null).Select(sd => sd.Base);
            _sds.RemoveAll(sd => bases.Contains(sd.Url));

            // Sanity check: all profiles on a core type (if any) must constrain the same core type
            StructureDefinition profileOne = null;

            foreach(var sd in _sds)
            {
                if(sd.ConstrainedType != null)
                {
                    if (profileOne != null)
                    {
                        if(sd.ConstrainedType != profileOne.ConstrainedType)
                        {
                            outcome.Info($"Profile '{profileOne.Url}' is a constraint on '{profileOne.ConstrainedType}', but '{sd.Url}' is a constraint on '{sd.ConstrainedType}'. This cannot both be true, so the instance is never valid",
                                            Issue.CONTENT_MISMATCHING_PROFILES, _path);
                            return outcome;
                        }
                    }
                    else
                        profileOne = sd;
                }
            }

            // Sanity check: all profiles must be either for a datatype or for a resource (a logical model can be combined with either)
            //profileOne = null;
            //foreach (var sd in _sds)
            //{
            //    if (sd.Kind == StructureDefinition.StructureDefinitionKind.Datatype || sd.Kind == StructureDefinition.StructureDefinitionKind.Resource)
            //    {
            //        if (profileOne != null)
            //        {
            //            if (sd.Kind != profileOne.Kind)
            //            {
            //                outcome.Info($"Profile '{profileOne.Url}' is a {profileOne.Kind}', but '{sd.Url}' is a '{sd.Kind}'. This cannot both be true, so the instance is never valid",
            //                                Issue.CONTENT_MISMATCHING_PROFILES, _path);
            //                return outcome;
            //            }
            //        }
            //        else
            //            profileOne = sd;
            //    }
            //}

            // Additional clean-up, depending on whether we are dealing with a datatype or resource, cleanup the abstract base classes
            if(profileOne?.Kind == StructureDefinition.StructureDefinitionKind.Resource)
            {
                _sds.RemoveAll(sd => sd.Url == ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Resource) ||
                                    sd.Url == ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.DomainResource));
            }
            else if (profileOne?.Kind == StructureDefinition.StructureDefinitionKind.Datatype)
            {
                _sds.RemoveAll(sd => sd.Url == ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Element));
            }


            // Now, we should only have at most ONE core type left, and profiles for that one core type
            var coreTypes = _sds.Where(sd => sd.ConstrainedType == null).ToList();

            if(coreTypes.Count > 1)
            {
                var combinedNames = coreTypes.Select(c => c.Id);
                outcome.Info($"Instance cannot be a { String.Join(" and a ", combinedNames) }", Issue.CONTENT_MISMATCHING_PROFILES, _path);
            }
            if(coreTypes.Count == 1)
            {
                FHIRDefinedType theType = EnumUtility.ParseLiteral<FHIRDefinedType>(coreTypes.Single().Id).Value;

                foreach(var profile in _sds.Where(sd => sd.ConstrainedType != null && sd.ConstrainedType != theType))
                {
                    outcome.Info($"Profile '{profile.Url}' must constrain the core type {theType}", Issue.CONTENT_MISMATCHING_PROFILES, _path);
                }
            }

            // This should still leave something to validate
            if (!_sds.Any())
            {
                outcome.Info("There is no (non-abstract) profile to validate against - nothing to validate", Issue.PROFILE_NO_PROFILE_TO_VALIDATE_AGAINST, _path);
            }

            return outcome;

            // If done correctly, the previous code will make sure this code is not needed anymore
            // First, a basic check: is the instance type equal to the defined type
            // Only do this when the underlying navigator has supplied a type (from the serialization)
            //if (instance.TypeName != null && elementConstraints.IsRootElement() && definition.StructureDefinition != null && definition.StructureDefinition.Id != null)
            //{
            //    var expected = definition.StructureDefinition.Id;

            //    if (!ModelInfo.IsCoreModelType(expected) || ModelInfo.IsProfiledQuantity(expected))
            //        expected = definition.StructureDefinition.ConstrainedType?.ToString();

            //    if (expected != null)
            //    {
            //        if (!outcome.Verify(() => instance.TypeName == expected, $"Type mismatch: instance value is of type '{instance.TypeName}', " +
            //                $"expected type '{expected}'", Issue.CONTENT_ELEMENT_HAS_INCORRECT_TYPE, instance))
            //            return outcome;     // Type mismatch, no use continuing validation
            //    }
            //}

            // For now, a quick algorithm
        }



        /// <summary>
        /// Generate snapshots for all StructureDefinitions available to the preprocessor
        /// </summary>
        /// <returns></returns>
        public OperationOutcome GenerateSnapshots()
        {
            var outcome = new OperationOutcome();

            foreach (var sd in _sds)
            {
                if (!sd.HasSnapshot)
                {
                    try
                    {
                        _onSnapshotNeeded(sd);
                    }
                    catch (Exception e)
                    {
                        outcome.Info($"Snapshot generation failed for '{sd.Url}'. Message: {e.Message}",
                               Issue.UNAVAILABLE_SNAPSHOT_GENERATION_FAILED, _path);
                    }
                }

                if (!sd.HasSnapshot)
                    outcome.Info($"Profile '{sd.Url}' does not include a snapshot.", Issue.UNAVAILABLE_NEED_SNAPSHOT, _path);
            }

            return outcome;
        }

        /// <summary>
        /// Generate navigators for all StructureDefinitions with snapshots available to the preprocessor
        /// </summary>
        /// <returns></returns>
        public List<ElementDefinitionNavigator> CreateNavigators()
        {
            return _sds.Where(sd => sd.HasSnapshot).Select(sd => new ElementDefinitionNavigator(sd)).ToList();
        }
    }
}
