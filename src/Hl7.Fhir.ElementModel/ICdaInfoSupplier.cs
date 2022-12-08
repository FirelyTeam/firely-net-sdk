/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using System;

namespace Hl7.Fhir.ElementModel
{
    [Obsolete("The ICdaInfoSupplier interface is part of alpha-level support for parsing CDA and should not yet be used in production. This interface is subject to change.")]
    public interface ICdaInfoSupplier
    {
        /// <summary>
        /// Retrieves the xHtml text of a cda logic model
        /// </summary>
        string XHtmlText { get; }
    }
}