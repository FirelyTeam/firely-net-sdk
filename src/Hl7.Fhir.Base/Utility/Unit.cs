/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

namespace Hl7.Fhir.Utility
{
    /// <summary>
    /// Units is the formal type with one element and has no representation in .NET.
    /// It is used to provide a type where none is wanted or expected, e.g. to supply
    /// a mandatory generic type argument to a monad when no real value needs to be
    /// represented in the monad.
    /// </summary>
    /// <remarks>It's unfortunate that Microsoft does not allow us to use the already
    /// existing System.Void type for this - which is defined more or less equivalently.
    /// </remarks>
    public struct Unit
    {
        public override bool Equals(object obj) => obj is Unit;
        public override int GetHashCode() => 0;
        public override string ToString() => "unit value";
    }

}
