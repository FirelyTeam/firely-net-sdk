/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System.Collections.Generic;

namespace Hl7.Fhir.Utility
{
    public static class DictionaryUtilities
    {
        public static U GetOrDefault<T, U>(this Dictionary<T, U> dict, T key) =>
            dict.TryGetValue(key, out U value) ? value : (default);
    }
}