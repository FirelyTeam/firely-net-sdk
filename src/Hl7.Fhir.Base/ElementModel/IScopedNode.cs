/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScopedNode : IBaseElementNavigator<IScopedNode>
    {
        IScopedNode? Parent { get; }
    }
}

#nullable restore