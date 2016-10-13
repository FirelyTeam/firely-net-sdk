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
        private Func<string, StructureDefinition> _profileResolver;
        private Action<StructureDefinition> _snapshotGenerator;
        private string _path;
        private ProfileAssertion _profiles;

        public ProfilePreprocessor(Func<string,StructureDefinition> profileResolver, Action<StructureDefinition> snapshotGenerator, IElementNavigator instance, string declaredTypeProfile, 
                IEnumerable<StructureDefinition> additionalProfiles, IEnumerable<string> additionalCanonicals)
        {
            _profileResolver = profileResolver;
            _snapshotGenerator = snapshotGenerator;

            _path = instance.Path;

            // This is only for resources, but I don't bother checking, since this will return empty anyway
            var statedProfiles = instance.GetChildrenByName("meta").ChildrenValues("profile").Cast<string>().ToList();

            var instanceTypeProfile = instance.TypeName != null ? ModelInfo.CanonicalUriForFhirCoreType(instance.TypeName) : null;

            _profiles = new ProfileAssertion(instanceTypeProfile, declaredTypeProfile, _path);

            if(additionalProfiles != null) _profiles.AddStatedProfile(additionalProfiles);
            if(additionalCanonicals != null) _profiles.AddStatedProfile(additionalCanonicals);
            if(statedProfiles != null) _profiles.AddStatedProfile(statedProfiles);
        }

        public IEnumerable<ElementDefinitionNavigator> Result { get; private set; }


        public OperationOutcome Process()
        {
            var outcome = new OperationOutcome();

            // Start preprocessing by resolving the references to the profiles (if any)
            var resolveOutcome = _profiles.Resolve(_profileResolver);
            outcome.Add(resolveOutcome);

            if (resolveOutcome.Success)
            {
                // Then, validate consistency of the profile assertions
                var validationOutcome = _profiles.Validate(_profileResolver);
                outcome.Add(validationOutcome);

                if (validationOutcome.Success)
                {
                    // Then, generate snapshots for all sds that we have found
                    var genSnapshotOutcome = GenerateSnapshots(_profiles.MinimalProfiles, _snapshotGenerator, _path);
                    outcome.Add(genSnapshotOutcome);

                    if (genSnapshotOutcome.Success)
                    {
                        // Finally, return navigators to the definitions
                        Result = CreateNavigators(_profiles.MinimalProfiles);
                    }
                }
            }

            return outcome;
        }


        /// <summary>
        /// Generate snapshots for all StructureDefinitions available to the preprocessor
        /// </summary>
        /// <returns></returns>
        public static OperationOutcome GenerateSnapshots(IEnumerable<StructureDefinition> sds, Action<StructureDefinition> snapshotGenerator, string path)
        {
            var outcome = new OperationOutcome();

            foreach (var sd in sds)
            {
                if (!sd.HasSnapshot)
                {
                    try
                    {
                        snapshotGenerator(sd);
                    }
                    catch (Exception e)
                    {
                        outcome.Info($"Snapshot generation failed for '{sd.Url}'. Message: {e.Message}",
                               Issue.UNAVAILABLE_SNAPSHOT_GENERATION_FAILED, path);
                    }
                }

                if (!sd.HasSnapshot)
                    outcome.Info($"Profile '{sd.Url}' does not include a snapshot.", Issue.UNAVAILABLE_NEED_SNAPSHOT, path);
            }

            return outcome;
        }



        /// <summary>
        /// Generate navigators for all StructureDefinitions with snapshots available to the preprocessor
        /// </summary>
        /// <returns></returns>
        public static List<ElementDefinitionNavigator> CreateNavigators(IEnumerable<StructureDefinition> sds)
        {
            return sds.Where(sd => sd.HasSnapshot).Select(sd => new ElementDefinitionNavigator(sd)).ToList();
        }
    }
}
