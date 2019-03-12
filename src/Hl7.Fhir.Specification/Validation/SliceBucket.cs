/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model.DSTU2;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification.Validation;

namespace Hl7.Fhir.Validation
{
    internal class SliceBucket : BaseBucket
    {
        public SliceBucket(ElementDefinitionNavigator root, Validator validator, string[] discriminator=null) : base(root.Current)
        {
            // TODO: Should check whether the discriminator is a valid child path of root. Wait until we have the
            // definition walker, which would walk across references if necessary.
            foreach (var d in discriminator)
            {
                if(d.EndsWith("@type"))
                    throw Error.NotImplemented($"Slicing with an '@type' discriminator is not yet supported by this validator.");
                else if (d.EndsWith("@profile"))
                    throw Error.NotImplemented($"Slicing with an '@profile' discriminator is not yet supported by this validator.");
            }

            Root = root.ShallowCopy();
            Validator = validator;
            Discriminator = discriminator;
        }

        public ElementDefinitionNavigator Root { get; private set; }

        public Validator Validator { get; private set; }

        public string[] Discriminator { get; private set; }

        private List<OperationOutcome> _successes = new List<OperationOutcome>();
        private List<OperationOutcome> _failures = new List<OperationOutcome>();

        public override bool Add(ITypedElement candidate)
        {
            var report = Validator.Validate(candidate, Root);

            // If the instance matches everything in the slice, it's definitely a member
            if (report.Success)
            {
                Members.Add(candidate);
                _successes.Add(report);
                return true;
            }

            // Now, it did not validate against the constraints...
            if(Discriminator?.Any() == true)
            {
                // Get the full path of the discriminator, which is rooted in the current instance path
                var baseInstancePath = candidate.Location;

                // remove all the [num] (from the instance path) and [x] (from the discriminator path) in one go,
                // so a path looking like this remains as a discriminator:  Patient.deceased
                // (note won't work if deceasedBoolean is allowed as a discriminator vlaue)
                var discriminatorPaths = Discriminator.Select(d => strip(baseInstancePath + "." + d)).ToArray();

                if(errorOnDiscriminator(discriminatorPaths, report))
                {
                    // Failed on a discriminator => this instance does not belong to this slice
                    return false;
                }
                else
                {
                    // Validated against the discriminating elements, instance belongs to this slice although there
                    // are validation errors on other elements
                    Members.Add(candidate);
                    _failures.Add(report);
                    return true;
                }
            }
            else
            {
                // No discriminator, and validation failed => not a member of this slice
                return false;
            }            
        }


        private static string strip(string instancePath)
        {
            return Regex.Replace(instancePath, @"\[(\d+|x)\]", "");
        }

        private static bool errorOnDiscriminator(string[] discriminators, OperationOutcome outcome)
        {
            foreach(var location in outcome.ListErrors().SelectMany(i => i.Location)
                .Union(outcome.Issue.Select(i => i.Annotation<SlicePathAnnotation>()?.Value)
                                    .Where(p => !string.IsNullOrEmpty(p))))
            {
                // Remove all the array indices from the instance path (e.g. you end up with Patient.telecom.system, not
                // Patient.telecom[2].system[0])
                var ni = strip(location);

                //TODO: Crude algorithm. Does not support navigating across references, nor type slices, nor discriminators on choices
                if (discriminators.Any(d => d == ni || ni.StartsWith(d + ".")))
                    return true;
            }

            return false;
        }

        public override OperationOutcome Validate(Validator validator, ITypedElement errorLocation)
        {
            OperationOutcome outcome = new OperationOutcome();

            foreach (var failure in _failures)
                outcome.Add(failure);

            outcome.Add(base.Validate(validator, errorLocation));

            return outcome;
        }
    }
}
