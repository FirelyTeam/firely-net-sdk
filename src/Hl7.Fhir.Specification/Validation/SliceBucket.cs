/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System;

namespace Hl7.Fhir.Validation
{
    internal class SliceBucket : BaseBucket
    {
        public SliceBucket(ElementDefinitionNavigator root, Validator validator, IElementNavigator errorLocation)
            : base(root.Current, validator, errorLocation)
        {
            Root = root.ShallowCopy();
        }

        public ElementDefinitionNavigator Root { get; private set; }

        private List<OperationOutcome> _successes = new List<OperationOutcome>();

        protected override OperationOutcome IsMember(IElementNavigator candidate)
        {
            var report = Validator.Validate(candidate, Root);

            if (report.Success)
                _successes.Add(report);

            return report;
        }

        public override OperationOutcome Validate()
        {
            // Since all members are already valid (otherwise they would not be members),
            // there's nothing to do beyond the checks in base (cardinality)
            return base.Validate();
        }
    }
}
