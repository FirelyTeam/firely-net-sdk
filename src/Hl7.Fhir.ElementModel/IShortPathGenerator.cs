/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using System;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;
using System.Collections.Generic;

namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// This interface is implemented by implementers of <see cref="ITypedElement" /> to represent the short path
    /// to an element.
    /// </summary>
    /// <remarks>There is a difference between the <see cref="ITypedElement.Location"/> and the short path. The
    /// former always includes an index selector, even if the element does not repeat, whereas the short path
    /// only uses index selectors when necessary. As an example, a <c>Location</c> would be <c>Patient.active[0]</c>,
    /// whereas the short path for the same location would be <c>Patient.active</c>. For repeating nodes, the
    /// location and the short path are the same.</remarks>
    public interface IShortPathGenerator
    {
        /// <summary>
        /// Gets the short path of the node the <see cref="ITypedElement"/> represents.
        /// </summary>
        /// <value>Returns the short path, which is a dotted path notation to the node</value>
        string ShortPath { get; }
    }
}