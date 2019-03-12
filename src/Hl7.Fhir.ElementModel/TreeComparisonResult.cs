/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */


namespace Hl7.Fhir.ElementModel
{
    public struct TreeComparisonResult
    {
        public bool Success;
        public string FailureLocation;
        public string Details;

        public static TreeComparisonResult Fail(string location, string details = null) =>
            new TreeComparisonResult { Success = false, FailureLocation = location, Details = details };

        public static readonly TreeComparisonResult OK = new TreeComparisonResult() { Success = true };
    }
}
