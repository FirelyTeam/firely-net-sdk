/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Linq;
using System.Collections.Generic;
using Hl7.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Validation
{

    internal delegate bool Condition();

    internal static class OperationOutcomeExtensions
    {
        public static bool Verify(this OperationOutcome outcome, Condition condition, string message, Issue issue, INamedNode location)
        {
            if (!condition())
            {
                outcome.AddIssue(issue.ToIssueComponent(message, location));
                return false;
            }
            else
                return true;
        }

        public static void Info(this OperationOutcome outcome, string message, Issue infoIssue, INamedNode location)
        {
            outcome.AddIssue(infoIssue.ToIssueComponent(message, location));
        }


        public static OperationOutcome MakeInformational(this OperationOutcome outcome)
        {
            //var result = (OperationOutcome)outcome.DeepCopy();
            var result = outcome;

            foreach (var issue in result.Issue)
                issue.Severity = OperationOutcome.IssueSeverity.Information;

            return result;
        }



        public static void Combine(this OperationOutcome outcome, IEnumerable<OperationOutcome> inputs, INamedNode location, BatchValidationMode mode)
        {
            if (inputs.Count() == 1)
            {
                // To not pollute the output if there's just a single input, just add it to the output
                outcome.Add(inputs.Single());
                return;
            }

            outcome.Info("Combination of {0} child validation runs, {1} must succeed"
                 .FormatWith(inputs.Count(), mode == BatchValidationMode.All ? "ALL" : "ANY"), Issue.PROCESSING_PROGRESS, location);

            List<OperationOutcome> toInclude = new List<OperationOutcome>();

            var successes = inputs.Where(i => i.Success).Count();
            var failures = inputs.Count() - successes;
            var combinedSuccess = mode == BatchValidationMode.Any ? successes > 0 : failures == 0;

            int index = 1;

            foreach (var report in inputs)
            {
                var reportToAdd = report;

                outcome.Info("Report {0}: {1}".FormatWith(index, reportToAdd.Success ? "SUCCESS" : "FAILURE"), Issue.PROCESSING_PROGRESS, location);
                
                // We'd like to include all results of the combined reports, but if the total result is a success,
                // any errors in failing subreports should just be informational
                if (!report.Success && combinedSuccess)     
                    reportToAdd = report.MakeInformational();

                outcome.Include(reportToAdd);
                index += 1;
            }

            if (outcome.Success)
                outcome.Info("Combined validation succeeded", Issue.PROCESSING_PROGRESS, location);
            else
                outcome.Info("Combined validation failed, {0} child validation runs failed, {1} succeeded"
                    .FormatWith(failures, successes), Issue.PROCESSING_PROGRESS, location);
        }
    }

}