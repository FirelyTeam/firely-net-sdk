/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

namespace Hl7.Fhir.Utility
{
    public static class DebuggerDisplayExtensions
    {
        public const string DEBUGGER_DISPLAY_PROP_NAME = "DebuggerDisplay";
        public static string DebuggerDisplayString(this object target)
        {
            var debuggerDisplay = ReflectionHelper.FindProperty(target.GetType(), DEBUGGER_DISPLAY_PROP_NAME);
            if (debuggerDisplay is null) return null;

            return debuggerDisplay.GetValue(target, null) as string;
        }
    }
}
